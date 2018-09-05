using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;

namespace ABFLib
{

    public class ABFsection
    {
        private const int BLOCKSIZE = 512;
        public int block;
        public int size;
        public int count;
        public int firstByte { get { return block * BLOCKSIZE; } }
    }

    public class ABF
    {
        private const int BLOCKSIZE = 512;

        // ABF properties
        public string abfFilePath;
        public string abfID;
        public int abfVersionMajor;
        public int sweepCount;
        public int channelCount;
        public int sampleRate;

        public int dataLengthSec;
        public int dataPointCount;
        public int dataFirstByte;
        
        // ABF2 sections
        private ABFsection sectionProtocol;
        private ABFsection sectionADC;
        private ABFsection sectionDAC;
        private ABFsection sectionEpoch;
        private ABFsection sectionADCperDAC;
        private ABFsection sectionEpochperDAC;
        private ABFsection sectionStrings;
        private ABFsection sectionData;
        private ABFsection sectionTag;

        // internal objects
        private BinaryReader br;

        /// <summary>
        /// ABF1 and ABF2 file reader
        /// </summary>
        public ABF(string abfFilePath, bool preLoadData = true)
        {
            // format file paths
            abfFilePath = Path.GetFullPath(abfFilePath);
            this.abfFilePath = abfFilePath;
            abfID = Path.GetFileNameWithoutExtension(abfFilePath);
            
            // determine if ABF1 or ABF2 file
            FileOpen();
            ReadFileFormat();
            ReadSectionMap();
            ReadSweepCount();
            ReadChannelCount();
            ReadSampleRate();
            ReadDataInfo();

            // release the file
            FileClose();
        }


        /*
         *
         *
         * 
         * 
         * ABF CLASS SUPPORTIVE FUNCTIONS
         * 
         * 
         * 
         * 
        */

        /// <summary>
        /// Summarize this ABF's properties as a text block
        /// </summary>
        public string Info()
        {
            string info = $"### ABF Information for {abfID} ###\n";

            // populate a dictionary with all fields in this class
            Dictionary<string, string> dict = new Dictionary<string, string>();
            FieldInfo[] fields = this.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
            foreach (FieldInfo x in fields)
            {
                System.Object val = x.GetValue(this);
                string valStr = "null";

                // if it's null, say it's null
                if (val != null)
                {
                    valStr = val.ToString();
                }

                // format string based on object type
                if (val is ABFsection)
                {
                    ABFsection section = (ABFsection)val;
                    valStr = $"ABFsection (firstByte={section.firstByte}, size={section.size}, count={section.count})";
                }
                else if (val is BinaryReader)
                {
                    valStr = null;
                }

                // if our string contains text, show it
                if (valStr != null)
                    dict.Add(x.Name, valStr);
            }

            // print items in alphabetical order
            List<string> keyList = new List<string>(dict.Keys);
            keyList.Sort();
            foreach (string key in keyList)
            {
                info += $"{key} = {dict[key]}\n";
            }
            return info+"\n";
        }

        /*
         *
         *
         * 
         * FILE OPERATIONS
         * 
         * 
         * 
        */

        private void FileOpen()
        {
            if (File.Exists(abfFilePath))
            {
                br = new BinaryReader(File.Open(abfFilePath, FileMode.Open));       
            } else
            {
                throw new Exception($"File does not exist: {abfFilePath}");
            }
        }

        private void FileClose()
        {
            br.Close();
        }

        private double FileReadFloatSingle(int bytePosition = -1)
        {
            if (bytePosition >= 0)
                br.BaseStream.Seek(bytePosition, SeekOrigin.Begin);
            return (double)br.ReadSingle();
        }

        private double FileReadFloatDouble(int bytePosition = -1)
        {
            if (bytePosition >= 0)
                br.BaseStream.Seek(bytePosition, SeekOrigin.Begin);
            return (double)br.ReadDouble();
        }

        private int FileReadInt16(int bytePosition = -1)
        {
            if (bytePosition >= 0)
                br.BaseStream.Seek(bytePosition, SeekOrigin.Begin);
            return (int)br.ReadInt16();
        }

        private int FileReadInt32(int bytePosition = -1)
        {
            if (bytePosition >= 0)
                br.BaseStream.Seek(bytePosition, SeekOrigin.Begin);
            return (int)br.ReadInt32();
        }

        private int FileReadUInt16(int bytePosition = -1)
        {
            if (bytePosition >= 0)
                br.BaseStream.Seek(bytePosition, SeekOrigin.Begin);
            return (int)br.ReadUInt16();
        }

        private int FileReadUInt32(int bytePosition = -1)
        {
            if (bytePosition >= 0)
                br.BaseStream.Seek(bytePosition, SeekOrigin.Begin);
            return (int)br.ReadUInt32();
        }

        private string FileReadString(int charCount, int bytePosition = -1)
        {
            if (bytePosition >= 0)
                br.BaseStream.Seek(bytePosition, SeekOrigin.Begin);
            byte[] bytes = br.ReadBytes(charCount);
            string txt = System.Text.Encoding.Default.GetString(bytes);
            return txt;
        }


        /*
         * 
         * 
         * 
         * 
         * READING OF VALUES FROM THE BINARY CONTENT OF ABF FILES
         * 
         * 
         * 
         * 
         */

        private void ReadFileFormat()
        {
            string firstFour = FileReadString(4, 0);
            if (firstFour == "ABF ")
                abfVersionMajor = 1;
            else if (firstFour == "ABF2")
                abfVersionMajor = 2;
            else
                throw new Exception($"Invalid ABF file: {abfFilePath}");
        }

        private void ReadSectionMap()
        {
            if (abfVersionMajor == 1)
            {
                // ABF1 files don't have a section map
            }
            else if (abfVersionMajor == 2)
            {
                sectionProtocol = ReadSection(76);
                sectionADC = ReadSection(92);
                sectionDAC = ReadSection(108);
                sectionEpoch = ReadSection(124);
                sectionADCperDAC = ReadSection(140);
                sectionEpochperDAC = ReadSection(156);
                sectionStrings = ReadSection(220);
                sectionData = ReadSection(236);
                sectionTag = ReadSection(252);
            }
        }

        /// <summary>
        /// Return the ABFsection for the section defined at firstByte
        /// </summary>
        private ABFsection ReadSection(int firstByte)
        {
            ABFsection section = new ABFsection();
            section.block = FileReadUInt32(firstByte);
            section.size = FileReadUInt32();
            section.count = FileReadUInt32();
            return section;
        }

        private void ReadSweepCount()
        {
            if (abfVersionMajor == 1)
            {
                int lActualEpisodes = FileReadUInt32(16);
                sweepCount = lActualEpisodes;
            }
            else if (abfVersionMajor == 2)
            {
                int lActualEpisodes = FileReadUInt32(12);
                sweepCount = lActualEpisodes;
            }
        }

        private void ReadChannelCount()
        {
            if (abfVersionMajor == 1)
            {
                int nADCNumChannels = FileReadUInt16(120);
                channelCount = nADCNumChannels;
            }
            else if (abfVersionMajor == 2)
            {
                channelCount = sectionADC.count;
            }
        }

        private void ReadSampleRate()
        {
            if (abfVersionMajor == 1)
            {
                double fADCSampleInterval = FileReadFloatSingle(122);
                sampleRate = (int)(1e6 / fADCSampleInterval);
            }
            else if (abfVersionMajor == 2)
            {
                double fADCSequenceInterval = FileReadFloatSingle(sectionProtocol.firstByte + 2);
                sampleRate = (int)(1e6 / fADCSequenceInterval);
            }
        }

        private void ReadDataInfo()
        {
            if (abfVersionMajor == 1)
            {
                int lDataSectionPtr = FileReadInt32(40);
                dataFirstByte = lDataSectionPtr * BLOCKSIZE;

                int lActualAcqLength = FileReadInt32(10);
                dataPointCount = lActualAcqLength;
            }
            else if (abfVersionMajor == 2)
            {
                dataFirstByte = sectionData.firstByte;
                dataPointCount = sectionData.count;
            }

            dataLengthSec = dataPointCount / sampleRate / channelCount;
        }
    }
}
