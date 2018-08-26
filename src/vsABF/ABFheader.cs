/* Code in this file deals with pulling data from certain positions in ABF files.
 * Code here may also lightly touch-up certain variables as needed.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace vsABF
{
    public partial class ABF
    {
        private string FileReadString(string name, int bytePosition, int count = 1, SeekOrigin origin = SeekOrigin.Begin)
        {
            br.BaseStream.Seek(bytePosition, origin);
            byte[] bytes = br.ReadBytes(count);
            string val = System.Text.Encoding.Default.GetString(bytes);
            log.Debug($"{name} = {val}");
            return val;
        }

        private float FileReadFloat(string name, int bytePosition, int count = 1, SeekOrigin origin = SeekOrigin.Begin)
        {
            br.BaseStream.Seek(bytePosition, origin);
            byte[] bytes = br.ReadBytes(4);
            float val = BitConverter.ToSingle(bytes, 0);
            log.Debug($"{name} = {val}");
            return val;
        }

        private float[] FileReadFloats(string name, int bytePosition, int count = 1, SeekOrigin origin = SeekOrigin.Begin)
        {
            br.BaseStream.Seek(bytePosition, origin);
            float[] vals = new float[count];
            for (int i = 0; i < count; i++)
            {
                byte[] bytes = br.ReadBytes(4);
                vals[i] = BitConverter.ToSingle(bytes, 0);
            }
            log.Debug($"{name} = [{string.Join(", ", vals)}]");
            return vals;
        }

        private short FileReadShort(string name, int bytePosition, int count = 1, SeekOrigin origin = SeekOrigin.Begin)
        {
            br.BaseStream.Seek(bytePosition, origin);
            byte[] bytes = br.ReadBytes(2);
            short val = BitConverter.ToInt16(bytes, 0);
            log.Debug($"{name} = {val}");
            return val;
        }

        private short[] FileReadShorts(string name, int bytePosition, int count = 1, SeekOrigin origin = SeekOrigin.Begin)
        {
            br.BaseStream.Seek(bytePosition, origin);
            short[] vals = new short[count];
            for (int i=0; i<count; i++)
            {
                byte[] bytes = br.ReadBytes(2);
                vals[i] = BitConverter.ToInt16(bytes, 0);
            }
            log.Debug($"{name} = [{string.Join(", ", vals)}]");
            return vals;
        }

        private int FileReadInt(string name, int bytePosition, int count = 1, SeekOrigin origin = SeekOrigin.Begin)
        {
            br.BaseStream.Seek(bytePosition, origin);
            byte[] bytes = br.ReadBytes(4);
            int val = BitConverter.ToInt32(bytes, 0);
            log.Debug($"{name} = {val}");
            return val;
        }

        private int[] FileReadInts(string name, int bytePosition, int count = 1, SeekOrigin origin = SeekOrigin.Begin)
        {
            br.BaseStream.Seek(bytePosition, origin);
            int[] vals = new int[count];
            for (int i = 0; i < count; i++)
            {
                byte[] bytes = br.ReadBytes(4);
                vals[i] = BitConverter.ToInt32(bytes, 0);
            }
            log.Debug($"{name} = [{string.Join(", ", vals)}]");
            return vals;
        }

        /*
            c	1 char 
            -s	x char[]
            b	1 signed char
            B	1 unsigned char
            -h	2 short
            H	2 unsigned short
            -i	4 int
            I	4 unsigned int
            l	4 long
            L	4 unsigned long
            -f	4 float
            d	8 double
        */

        private void ReadHeaderV1()
        {
            //HeaderV1
            string fFileSignature = FileReadString("fFileSignature", 0, 4); //4s;
            float fFileVersionNumber = FileReadFloat("fFileVersionNumber", 4, 1); //f;
            short nOperationMode = FileReadShort("nOperationMode", 8, 1); //h;
            int lActualAcqLength = FileReadInt("lActualAcqLength", 10, 1); //i;
            short nNumPointsIgnored = FileReadShort("nNumPointsIgnored", 14, 1); //h;
            int lActualEpisodes = FileReadInt("lActualEpisodes", 16, 1); //i;
            int lFileStartTime = FileReadInt("lFileStartTime", 24, 1); //i;
            int lDataSectionPtr = FileReadInt("lDataSectionPtr", 40, 1); //i;
            int lTagSectionPtr = FileReadInt("lTagSectionPtr", 44, 1); //i;
            int lNumTagEntries = FileReadInt("lNumTagEntries", 48, 1); //i;
            int lSynchArrayPtr = FileReadInt("lSynchArrayPtr", 92, 1); //i;
            int lSynchArraySize = FileReadInt("lSynchArraySize", 96, 1); //i;
            short nDataFormat = FileReadShort("nDataFormat", 100, 1); //h;
            short nADCNumChannels = FileReadShort("nADCNumChannels", 120, 1); //h;
            float fADCSampleInterval = FileReadFloat("fADCSampleInterval", 122, 1); //f;
            float fSynchTimeUnit = FileReadFloat("fSynchTimeUnit", 130, 1); //f;
            int lNumSamplesPerEpisode = FileReadInt("lNumSamplesPerEpisode", 138, 1); //i;
            int lPreTriggerSamples = FileReadInt("lPreTriggerSamples", 142, 1); //i;
            int lEpisodesPerRun = FileReadInt("lEpisodesPerRun", 146, 1); //i;
            float fADCRange = FileReadFloat("fADCRange", 244, 1); //f;
            int lADCResolution = FileReadInt("lADCResolution", 252, 1); //i;
            short nFileStartMillisecs = FileReadShort("nFileStartMillisecs", 366, 1); //h;
            short[] nADCPtoLChannelMap = FileReadShorts("nADCPtoLChannelMap", 378, 16); //16h;
            short[] nADCSamplingSeq = FileReadShorts("nADCSamplingSeq", 410, 16); //16h;
            string sADCChannelName = FileReadString("sADCChannelName", 442, 10); //10s;
            string sADCUnits = FileReadString("sADCUnits", 602, 8); //8s;
            float[] fADCProgrammableGain = FileReadFloats("fADCProgrammableGain", 730, 16); //16f;
            float[] fInstrumentScaleFactor = FileReadFloats("fInstrumentScaleFactor", 922, 16); //16f;
            float[] fInstrumentOffset = FileReadFloats("fInstrumentOffset", 986, 16); //16f;
            float[] fSignalGain = FileReadFloats("fSignalGain", 1050, 16); //16f;
            float[] fSignalOffset = FileReadFloats("fSignalOffset", 1114, 16); //16f;
            short nDigitalEnable = FileReadShort("nDigitalEnable", 1436, 1); //h;
            short nActiveDACChannel = FileReadShort("nActiveDACChannel", 1440, 1); //h;
            short nDigitalHolding = FileReadShort("nDigitalHolding", 1584, 1); //h;
            short nDigitalInterEpisode = FileReadShort("nDigitalInterEpisode", 1586, 1); //h;
            short[] nDigitalValue = FileReadShorts("nDigitalValue", 2588, 10); //10h;
            int[] lDACFilePtr = FileReadInts("lDACFilePtr", 2048, 2); //2i;
            int[] lDACFileNumEpisodes = FileReadInts("lDACFileNumEpisodes", 2056, 2); //2i;
            float[] fDACCalibrationFactor = FileReadFloats("fDACCalibrationFactor", 2074, 4); //4f;
            float[] fDACCalibrationOffset = FileReadFloats("fDACCalibrationOffset", 2090, 4); //4f;
            short[] nWaveformEnable = FileReadShorts("nWaveformEnable", 2296, 2); //2h;
            short[] nWaveformSource = FileReadShorts("nWaveformSource", 2300, 2); //2h;
            short[] nInterEpisodeLevel = FileReadShorts("nInterEpisodeLevel", 2304, 2); //2h;
            short[] nEpochType = FileReadShorts("nEpochType", 2308, 20); //20h;
            float[] fEpochInitLevel = FileReadFloats("fEpochInitLevel", 2348, 20); //20f;
            float[] fEpochLevelInc = FileReadFloats("fEpochLevelInc", 2428, 20); //20f;
            int[] lEpochInitDuration = FileReadInts("lEpochInitDuration", 2508, 20); //20i;
            int[] lEpochDurationInc = FileReadInts("lEpochDurationInc", 2588, 20); //20i;
            short[] nTelegraphEnable = FileReadShorts("nTelegraphEnable", 4512, 16); //16h;
            float[] fTelegraphAdditGain = FileReadFloats("fTelegraphAdditGain", 4576, 16); //16f;
            string sProtocolPath = FileReadString("sProtocolPath", 4898, 384); //384s;
        }
    }
}
