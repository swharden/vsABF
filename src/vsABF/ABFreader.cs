using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace vsABF
{

    public partial class ABFreader
    {
        private BinaryReader br;
        public Logger log;
        public string abfFilePath;
        public string fileSignature;

        /// <summary>
        /// The ABFreader object reads an ABF file and populates each of the applicable header sections.
        /// It does not try to unify variables to a common naming system across ABF versions.
        /// </summary>
        /// <param name="abfFilePath"></param>
        /// <param name="log"></param>
        public ABFreader(string abfFilePath, Logger log)
        {
            this.abfFilePath = abfFilePath;
            this.log = log;

            FileOpen();
            fileSignature = FileReadString(0, 4);
            switch (fileSignature)
            {
                case "ABF ":
                    log.Debug("File is in ABF1 format");
                    ReadHeaderV1();
                    break;
                case "ABF2":
                    log.Debug("File is in ABF2 format");
                    ReadHeaderV2();
                    ReadSectionMap();
                    break;
                default:
                    log.Critical($"Unrecognized ABF format ({fileSignature})");
                    FileClose();
                    return;
            }
            FileClose();
        }

        public void FileOpen()
        {
            log.Debug($"opening {Path.GetFileName(abfFilePath)}");
            br = new BinaryReader(File.Open(abfFilePath, FileMode.Open));
        }

        public void FileClose()
        {
            br.Close();
            log.Debug($"closed {Path.GetFileName(abfFilePath)}");
        }

        public byte[] FileReadBytes(int bytePosition, int count)
        {
            if (bytePosition >= 0)
                br.BaseStream.Seek(bytePosition, SeekOrigin.Begin);
            return br.ReadBytes(count);
        }

        public string FileReadString(int bytePosition, int count)
        {
            if (bytePosition >= 0)
                br.BaseStream.Seek(bytePosition, SeekOrigin.Begin);
            return System.Text.Encoding.Default.GetString(br.ReadBytes(count));
        }

        public short FileReadShort(int bytePosition, int count)
        {
            if (bytePosition >= 0)
                br.BaseStream.Seek(bytePosition, SeekOrigin.Begin);
            return BitConverter.ToInt16(br.ReadBytes(2), 0);
        }

        public ushort FileReadUnsignedShort(int bytePosition, int count)
        {
            if (bytePosition >= 0)
                br.BaseStream.Seek(bytePosition, SeekOrigin.Begin);
            return BitConverter.ToUInt16(br.ReadBytes(2), 0);
        }

        public short[] FileReadShorts(int bytePosition, int count)
        {
            if (bytePosition >= 0)
                br.BaseStream.Seek(bytePosition, SeekOrigin.Begin);
            short[] vals = new short[count];
            for (int i = 0; i < count; i++)
                vals[i] = BitConverter.ToInt16(br.ReadBytes(2), 0);
            return vals;
        }

        public int FileReadInt(int bytePosition, int count)
        {
            if (bytePosition >= 0)
                br.BaseStream.Seek(bytePosition, SeekOrigin.Begin);
            return BitConverter.ToInt32(br.ReadBytes(4), 0);
        }

        public int[] FileReadInts(int bytePosition, int count)
        {
            if (bytePosition >= 0)
                br.BaseStream.Seek(bytePosition, SeekOrigin.Begin);
            int[] vals = new int[count];
            for (int i = 0; i < count; i++)
                vals[i] = BitConverter.ToInt32(br.ReadBytes(4), 0);
            return vals;
        }

        public uint FileReadUnsignedInt(int bytePosition, int count)
        {
            if (bytePosition >= 0)
                br.BaseStream.Seek(bytePosition, SeekOrigin.Begin);
            return BitConverter.ToUInt32(br.ReadBytes(4), 0);
        }

        public uint[] FileReadUnsignedInts(int bytePosition, int count)
        {
            if (bytePosition >= 0)
                br.BaseStream.Seek(bytePosition, SeekOrigin.Begin);
            uint[] vals = new uint[count];
            for (int i = 0; i < count; i++)
                vals[i] = BitConverter.ToUInt32(br.ReadBytes(4), 0);
            return vals;
        }

        public float FileReadFloat(int bytePosition, int count)
        {
            if (bytePosition >= 0)
                br.BaseStream.Seek(bytePosition, SeekOrigin.Begin);
            return BitConverter.ToSingle(br.ReadBytes(4), 0);
        }

        public float[] FileReadFloats(int bytePosition, int count)
        {
            if (bytePosition >= 0)
                br.BaseStream.Seek(bytePosition, SeekOrigin.Begin);
            float[] vals = new float[count];
            for (int i = 0; i < count; i++)
                vals[i] = BitConverter.ToSingle(br.ReadBytes(4), 0);
            return vals;
        }

        public HeaderV1 headerV1;
        public void ReadHeaderV1()
        {
            log.Debug("Reading ABF Header (v1)");
            headerV1 = new HeaderV1();
            headerV1.fFileSignature = FileReadString(0, 4); //4s;
            headerV1.fFileVersionNumber = FileReadFloat(4, 1); //f;
            headerV1.nOperationMode = FileReadShort(8, 1); //h;
            headerV1.lActualAcqLength = FileReadInt(10, 1); //i;
            headerV1.nNumPointsIgnored = FileReadShort(14, 1); //h;
            headerV1.lActualEpisodes = FileReadInt(16, 1); //i;
            headerV1.lFileStartTime = FileReadInt(24, 1); //i;
            headerV1.lDataSectionPtr = FileReadInt(40, 1); //i;
            headerV1.lTagSectionPtr = FileReadInt(44, 1); //i;
            headerV1.lNumTagEntries = FileReadInt(48, 1); //i;
            headerV1.lSynchArrayPtr = FileReadInt(92, 1); //i;
            headerV1.lSynchArraySize = FileReadInt(96, 1); //i;
            headerV1.nDataFormat = FileReadShort(100, 1); //h;
            headerV1.nADCNumChannels = FileReadShort(120, 1); //h;
            headerV1.fADCSampleInterval = FileReadFloat(122, 1); //f;
            headerV1.fSynchTimeUnit = FileReadFloat(130, 1); //f;
            headerV1.lNumSamplesPerEpisode = FileReadInt(138, 1); //i;
            headerV1.lPreTriggerSamples = FileReadInt(142, 1); //i;
            headerV1.lEpisodesPerRun = FileReadInt(146, 1); //i;
            headerV1.fADCRange = FileReadFloat(244, 1); //f;
            headerV1.lADCResolution = FileReadInt(252, 1); //i;
            headerV1.nFileStartMillisecs = FileReadShort(366, 1); //h;
            headerV1.nADCPtoLChannelMap = FileReadShorts(378, 16); //16h;
            headerV1.nADCSamplingSeq = FileReadShorts(410, 16); //16h;
            headerV1.sADCChannelName = FileReadString(442, 10); //10s;
            headerV1.sADCUnits = FileReadString(602, 8); //8s;
            headerV1.fADCProgrammableGain = FileReadFloats(730, 16); //16f;
            headerV1.fInstrumentScaleFactor = FileReadFloats(922, 16); //16f;
            headerV1.fInstrumentOffset = FileReadFloats(986, 16); //16f;
            headerV1.fSignalGain = FileReadFloats(1050, 16); //16f;
            headerV1.fSignalOffset = FileReadFloats(1114, 16); //16f;
            headerV1.nDigitalEnable = FileReadShort(1436, 1); //h;
            headerV1.nActiveDACChannel = FileReadShort(1440, 1); //h;
            headerV1.nDigitalHolding = FileReadShort(1584, 1); //h;
            headerV1.nDigitalInterEpisode = FileReadShort(1586, 1); //h;
            headerV1.nDigitalValue = FileReadShorts(2588, 10); //10h;
            headerV1.lDACFilePtr = FileReadInts(2048, 2); //2i;
            headerV1.lDACFileNumEpisodes = FileReadInts(2056, 2); //2i;
            headerV1.fDACCalibrationFactor = FileReadFloats(2074, 4); //4f;
            headerV1.fDACCalibrationOffset = FileReadFloats(2090, 4); //4f;
            headerV1.nWaveformEnable = FileReadShorts(2296, 2); //2h;
            headerV1.nWaveformSource = FileReadShorts(2300, 2); //2h;
            headerV1.nInterEpisodeLevel = FileReadShorts(2304, 2); //2h;
            headerV1.nEpochType = FileReadShorts(2308, 20); //20h;
            headerV1.fEpochInitLevel = FileReadFloats(2348, 20); //20f;
            headerV1.fEpochLevelInc = FileReadFloats(2428, 20); //20f;
            headerV1.lEpochInitDuration = FileReadInts(2508, 20); //20i;
            headerV1.lEpochDurationInc = FileReadInts(2588, 20); //20i;
            headerV1.nTelegraphEnable = FileReadShorts(4512, 16); //16h;
            headerV1.fTelegraphAdditGain = FileReadFloats(4576, 16); //16f;
            headerV1.sProtocolPath = FileReadString(4898, 384); //384s;
        }

        public HeaderV2 headerV2;
        public void ReadHeaderV2()
        {
            log.Debug("Reading ABF Header (v2)");
            br.BaseStream.Seek(0, SeekOrigin.Begin);
            headerV2 = new HeaderV2();
            headerV2.fFileSignature = FileReadString(-1, 4); //4s;
            headerV2.fFileVersionNumber = FileReadBytes(-1, 4); //4b;
            headerV2.uFileInfoSize = FileReadUnsignedInt(-1, 1); //I;
            headerV2.lActualEpisodes = FileReadUnsignedInt(-1, 1); //I;
            headerV2.uFileStartDate = FileReadUnsignedInt(-1, 1); //I;
            headerV2.uFileStartTimeMS = FileReadUnsignedInt(-1, 1); //I;
            headerV2.uStopwatchTime = FileReadUnsignedInt(-1, 1); //I;
            headerV2.nFileType = FileReadUnsignedShort(-1, 1); //H;
            headerV2.nDataFormat = FileReadUnsignedShort(-1, 1); //H;
            headerV2.nSimultaneousScan = FileReadUnsignedShort(-1, 1); //H;
            headerV2.nCRCEnable = FileReadUnsignedShort(-1, 1); //H;
            headerV2.uFileCRC = FileReadUnsignedInt(-1, 1); //I;
            headerV2.uFileGUID = FileReadBytes(-1, 16); //16B;
            headerV2.uCreatorVersion = FileReadBytes(-1, 4); //4B;
            headerV2.uCreatorNameIndex = FileReadUnsignedInt(-1, 1); //I;
            headerV2.uModifierVersion = FileReadUnsignedInt(-1, 1); //I;
            headerV2.uModifierNameIndex = FileReadUnsignedInt(-1, 1); //I;
            headerV2.uProtocolPathIndex = FileReadUnsignedInt(-1, 1); //I;
        }

        public SectionMap sectionMap;
        public void ReadSectionMap()
        {
            log.Debug("Reading ABF Section Map");
            sectionMap = new SectionMap();
            sectionMap.Protocol = new Section(FileReadUnsignedInts(76, 3));
            sectionMap.ADC = new Section(FileReadUnsignedInts(92, 3));
            sectionMap.DAC = new Section(FileReadUnsignedInts(108, 3));
            sectionMap.Epoch = new Section(FileReadUnsignedInts(124, 3));
            sectionMap.ADCPerDAC = new Section(FileReadUnsignedInts(140, 3));
            sectionMap.EpochPerDAC = new Section(FileReadUnsignedInts(156, 3));
            sectionMap.UserList = new Section(FileReadUnsignedInts(172, 3));
            sectionMap.StatsRegion = new Section(FileReadUnsignedInts(188, 3));
            sectionMap.Math = new Section(FileReadUnsignedInts(204, 3));
            sectionMap.Strings = new Section(FileReadUnsignedInts(220, 3));
            sectionMap.Data = new Section(FileReadUnsignedInts(236, 3));
            sectionMap.Tag = new Section(FileReadUnsignedInts(252, 3));
            sectionMap.Scope = new Section(FileReadUnsignedInts(268, 3));
            sectionMap.Delta = new Section(FileReadUnsignedInts(284, 3));
            sectionMap.VoiceTag = new Section(FileReadUnsignedInts(300, 3));
            sectionMap.SynchArray = new Section(FileReadUnsignedInts(316, 3));
            sectionMap.Annotation = new Section(FileReadUnsignedInts(332, 3));
            sectionMap.Stats = new Section(FileReadUnsignedInts(348, 3));


        }
    }
}
