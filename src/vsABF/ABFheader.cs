using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

namespace vsABF
{
    public class AbfHeaderReader
    {
        public BinaryReader br;
        public Logger log;

        private const int BLOCKSIZE = 512;

        public AbfHeaderReader(BinaryReader br, Logger log)
        {
            this.br = br;
            this.log = log;
        }

        public string FileReadString(string name, int bytePosition, int count = 1)
        {
            if (bytePosition>-1)
                br.BaseStream.Seek(bytePosition, SeekOrigin.Begin);
            byte[] bytes = br.ReadBytes(count);
            string val = System.Text.Encoding.Default.GetString(bytes);
            log.Debug($"{name} = {val}");
            return val;
        }

        public byte[] FileReadBytes(string name, int bytePosition, int count = 1, SeekOrigin origin = SeekOrigin.Begin)
        {
            if (bytePosition > -1)
                br.BaseStream.Seek(bytePosition, SeekOrigin.Begin);
            byte[] vals = new byte[count];
            for (int i = 0; i < count; i++)
            {
                byte[] bytes = br.ReadBytes(1);
                vals[i] = bytes[0];
            }
            log.Debug($"{name} = [{string.Join(", ", vals)}]");
            return vals;
        }

        public float FileReadFloat(string name, int bytePosition, int count = 1, SeekOrigin origin = SeekOrigin.Begin)
        {
            if (bytePosition > -1)
                br.BaseStream.Seek(bytePosition, SeekOrigin.Begin);
            byte[] bytes = br.ReadBytes(4);
            float val = BitConverter.ToSingle(bytes, 0);
            log.Debug($"{name} = {val}");
            return val;
        }

        public float[] FileReadFloats(string name, int bytePosition, int count = 1, SeekOrigin origin = SeekOrigin.Begin)
        {
            if (bytePosition > -1)
                br.BaseStream.Seek(bytePosition, SeekOrigin.Begin);
            float[] vals = new float[count];
            for (int i = 0; i < count; i++)
            {
                byte[] bytes = br.ReadBytes(4);
                vals[i] = BitConverter.ToSingle(bytes, 0);
            }
            log.Debug($"{name} = [{string.Join(", ", vals)}]");
            return vals;
        }

        public short FileReadShort(string name, int bytePosition, int count = 1, SeekOrigin origin = SeekOrigin.Begin)
        {
            if (bytePosition > -1)
                br.BaseStream.Seek(bytePosition, SeekOrigin.Begin);
            byte[] bytes = br.ReadBytes(2);
            short val = BitConverter.ToInt16(bytes, 0);
            log.Debug($"{name} = {val}");
            return val;
        }

        public ushort FileReadUnsignedShort(string name, int bytePosition, int count = 1, SeekOrigin origin = SeekOrigin.Begin)
        {
            if (bytePosition > -1)
                br.BaseStream.Seek(bytePosition, SeekOrigin.Begin);
            byte[] bytes = br.ReadBytes(2);
            ushort val = BitConverter.ToUInt16(bytes, 0);
            log.Debug($"{name} = {val}");
            return val;
        }

        public short[] FileReadShorts(string name, int bytePosition, int count = 1, SeekOrigin origin = SeekOrigin.Begin)
        {
            if (bytePosition > -1)
                br.BaseStream.Seek(bytePosition, SeekOrigin.Begin);
            short[] vals = new short[count];
            for (int i = 0; i < count; i++)
            {
                byte[] bytes = br.ReadBytes(2);
                vals[i] = BitConverter.ToInt16(bytes, 0);
            }
            log.Debug($"{name} = [{string.Join(", ", vals)}]");
            return vals;
        }

        public int FileReadInt(string name, int bytePosition, int count = 1, SeekOrigin origin = SeekOrigin.Begin)
        {
            if (bytePosition > -1)
                br.BaseStream.Seek(bytePosition, SeekOrigin.Begin);
            byte[] bytes = br.ReadBytes(4);
            int val = BitConverter.ToInt32(bytes, 0);
            log.Debug($"{name} = {val}");
            return val;
        }

        public uint FileReadUnsignedInt(string name, int bytePosition, int count = 1, SeekOrigin origin = SeekOrigin.Begin)
        {
            if (bytePosition > -1)
                br.BaseStream.Seek(bytePosition, SeekOrigin.Begin);
            byte[] bytes = br.ReadBytes(4);
            uint val = BitConverter.ToUInt32(bytes, 0);
            log.Debug($"{name} = {val}");
            return val;
        }

        public int[] FileReadInts(string name, int bytePosition, int count = 1, SeekOrigin origin = SeekOrigin.Begin)
        {
            if (bytePosition > -1)
                br.BaseStream.Seek(bytePosition, SeekOrigin.Begin);
            int[] vals = new int[count];
            for (int i = 0; i < count; i++)
            {
                byte[] bytes = br.ReadBytes(4);
                vals[i] = BitConverter.ToInt32(bytes, 0);
            }
            log.Debug($"{name} = [{string.Join(", ", vals)}]");
            return vals;
        }

        public class Section
        {
            public uint blockNumber;
            public uint bytesPerItem;
            public uint itemCount;
            public uint firstByte;
            public Section(string sectionName, BinaryReader br, Logger log, int sectionByteLocation)
            {
                br.BaseStream.Seek(sectionByteLocation, SeekOrigin.Begin);
                blockNumber = BitConverter.ToUInt32(br.ReadBytes(4), 0);
                bytesPerItem = BitConverter.ToUInt32(br.ReadBytes(4), 0);
                itemCount = BitConverter.ToUInt32(br.ReadBytes(4), 0);
                firstByte = BLOCKSIZE * blockNumber;
                log.Debug($"{sectionName}.blockNumber = {blockNumber}");
                log.Debug($"{sectionName}.bytesPerItem = {bytesPerItem}");
                log.Debug($"{sectionName}.itemCount = {itemCount}");
                log.Debug($"{sectionName}.firstByte = {firstByte}");
            }
        }
    }

    public class AbfHeaderV1 : AbfHeaderReader
    {

        public AbfHeaderV1(BinaryReader br, Logger log) : base(br, log)
        {
            this.log = log;
            ReadHeaderV1();
        }

        public string fFileSignature;
        public float fFileVersionNumber;
        public short nOperationMode;
        public int lActualAcqLength;
        public short nNumPointsIgnored;
        public int lActualEpisodes;
        public int lFileStartTime;
        public int lDataSectionPtr;
        public int lTagSectionPtr;
        public int lNumTagEntries;
        public int lSynchArrayPtr;
        public int lSynchArraySize;
        public short nDataFormat;
        public short nADCNumChannels;
        public float fADCSampleInterval;
        public float fSynchTimeUnit;
        public int lNumSamplesPerEpisode;
        public int lPreTriggerSamples;
        public int lEpisodesPerRun;
        public float fADCRange;
        public int lADCResolution;
        public short nFileStartMillisecs;
        public short[] nADCPtoLChannelMap;
        public short[] nADCSamplingSeq;
        public string sADCChannelName;
        public string sADCUnits;
        public float[] fADCProgrammableGain;
        public float[] fInstrumentScaleFactor;
        public float[] fInstrumentOffset;
        public float[] fSignalGain;
        public float[] fSignalOffset;
        public short nDigitalEnable;
        public short nActiveDACChannel;
        public short nDigitalHolding;
        public short nDigitalInterEpisode;
        public short[] nDigitalValue;
        public int[] lDACFilePtr;
        public int[] lDACFileNumEpisodes;
        public float[] fDACCalibrationFactor;
        public float[] fDACCalibrationOffset;
        public short[] nWaveformEnable;
        public short[] nWaveformSource;
        public short[] nInterEpisodeLevel;
        public short[] nEpochType;
        public float[] fEpochInitLevel;
        public float[] fEpochLevelInc;
        public int[] lEpochInitDuration;
        public int[] lEpochDurationInc;
        public short[] nTelegraphEnable;
        public float[] fTelegraphAdditGain;
        public string sProtocolPath;

        private void ReadHeaderV1()
        {
            log.Debug("### ABF v1 header ###");
            fFileSignature = FileReadString("fFileSignature", 0, 4); //4s;
            fFileVersionNumber = FileReadFloat("fFileVersionNumber", 4, 1); //f;
            nOperationMode = FileReadShort("nOperationMode", 8, 1); //h;
            lActualAcqLength = FileReadInt("lActualAcqLength", 10, 1); //i;
            nNumPointsIgnored = FileReadShort("nNumPointsIgnored", 14, 1); //h;
            lActualEpisodes = FileReadInt("lActualEpisodes", 16, 1); //i;
            lFileStartTime = FileReadInt("lFileStartTime", 24, 1); //i;
            lDataSectionPtr = FileReadInt("lDataSectionPtr", 40, 1); //i;
            lTagSectionPtr = FileReadInt("lTagSectionPtr", 44, 1); //i;
            lNumTagEntries = FileReadInt("lNumTagEntries", 48, 1); //i;
            lSynchArrayPtr = FileReadInt("lSynchArrayPtr", 92, 1); //i;
            lSynchArraySize = FileReadInt("lSynchArraySize", 96, 1); //i;
            nDataFormat = FileReadShort("nDataFormat", 100, 1); //h;
            nADCNumChannels = FileReadShort("nADCNumChannels", 120, 1); //h;
            fADCSampleInterval = FileReadFloat("fADCSampleInterval", 122, 1); //f;
            fSynchTimeUnit = FileReadFloat("fSynchTimeUnit", 130, 1); //f;
            lNumSamplesPerEpisode = FileReadInt("lNumSamplesPerEpisode", 138, 1); //i;
            lPreTriggerSamples = FileReadInt("lPreTriggerSamples", 142, 1); //i;
            lEpisodesPerRun = FileReadInt("lEpisodesPerRun", 146, 1); //i;
            fADCRange = FileReadFloat("fADCRange", 244, 1); //f;
            lADCResolution = FileReadInt("lADCResolution", 252, 1); //i;
            nFileStartMillisecs = FileReadShort("nFileStartMillisecs", 366, 1); //h;
            nADCPtoLChannelMap = FileReadShorts("nADCPtoLChannelMap", 378, 16); //16h;
            nADCSamplingSeq = FileReadShorts("nADCSamplingSeq", 410, 16); //16h;
            sADCChannelName = FileReadString("sADCChannelName", 442, 10); //10s;
            sADCUnits = FileReadString("sADCUnits", 602, 8); //8s;
            fADCProgrammableGain = FileReadFloats("fADCProgrammableGain", 730, 16); //16f;
            fInstrumentScaleFactor = FileReadFloats("fInstrumentScaleFactor", 922, 16); //16f;
            fInstrumentOffset = FileReadFloats("fInstrumentOffset", 986, 16); //16f;
            fSignalGain = FileReadFloats("fSignalGain", 1050, 16); //16f;
            fSignalOffset = FileReadFloats("fSignalOffset", 1114, 16); //16f;
            nDigitalEnable = FileReadShort("nDigitalEnable", 1436, 1); //h;
            nActiveDACChannel = FileReadShort("nActiveDACChannel", 1440, 1); //h;
            nDigitalHolding = FileReadShort("nDigitalHolding", 1584, 1); //h;
            nDigitalInterEpisode = FileReadShort("nDigitalInterEpisode", 1586, 1); //h;
            nDigitalValue = FileReadShorts("nDigitalValue", 2588, 10); //10h;
            lDACFilePtr = FileReadInts("lDACFilePtr", 2048, 2); //2i;
            lDACFileNumEpisodes = FileReadInts("lDACFileNumEpisodes", 2056, 2); //2i;
            fDACCalibrationFactor = FileReadFloats("fDACCalibrationFactor", 2074, 4); //4f;
            fDACCalibrationOffset = FileReadFloats("fDACCalibrationOffset", 2090, 4); //4f;
            nWaveformEnable = FileReadShorts("nWaveformEnable", 2296, 2); //2h;
            nWaveformSource = FileReadShorts("nWaveformSource", 2300, 2); //2h;
            nInterEpisodeLevel = FileReadShorts("nInterEpisodeLevel", 2304, 2); //2h;
            nEpochType = FileReadShorts("nEpochType", 2308, 20); //20h;
            fEpochInitLevel = FileReadFloats("fEpochInitLevel", 2348, 20); //20f;
            fEpochLevelInc = FileReadFloats("fEpochLevelInc", 2428, 20); //20f;
            lEpochInitDuration = FileReadInts("lEpochInitDuration", 2508, 20); //20i;
            lEpochDurationInc = FileReadInts("lEpochDurationInc", 2588, 20); //20i;
            nTelegraphEnable = FileReadShorts("nTelegraphEnable", 4512, 16); //16h;
            fTelegraphAdditGain = FileReadFloats("fTelegraphAdditGain", 4576, 16); //16f;
            sProtocolPath = FileReadString("sProtocolPath", 4898, 384); //384s;
        }
    }


    public class AbfHeaderV2 : AbfHeaderReader
    {

        public AbfHeaderV2(BinaryReader br, Logger log) : base(br, log)
        {
            this.log = log;
            ReadHeaderV2();
            ReadSectionMap();
        }


        public string fFileSignature;
        public byte[] fFileVersionNumber;
        public uint uFileInfoSize;
        public uint lActualEpisodes;
        public uint uFileStartDate;
        public uint uFileStartTimeMS;
        public uint uStopwatchTime;
        public ushort nFileType;
        public ushort nDataFormat;
        public ushort nSimultaneousScan;
        public ushort nCRCEnable;
        public uint uFileCRC;
        public byte[] uFileGUID;
        public byte[] uCreatorVersion;
        public uint uCreatorNameIndex;
        public uint uModifierVersion;
        public uint uModifierNameIndex;
        public uint uProtocolPathIndex;

        private void ReadHeaderV2()
        {
            log.Debug("### ABF v2 header ###");
            fFileSignature = FileReadString("fFileSignature", 0, 4); //4s;
            fFileVersionNumber = FileReadBytes("fFileVersionNumber", -1, 4); //4b;
            uFileInfoSize = FileReadUnsignedInt("uFileInfoSize", -1, 1); //I;
            lActualEpisodes = FileReadUnsignedInt("lActualEpisodes", -1, 1); //I;
            uFileStartDate = FileReadUnsignedInt("uFileStartDate", -1, 1); //I;
            uFileStartTimeMS = FileReadUnsignedInt("uFileStartTimeMS", -1, 1); //I;
            uStopwatchTime = FileReadUnsignedInt("uStopwatchTime", -1, 1); //I;
            nFileType = FileReadUnsignedShort("nFileType", -1, 1); //H;
            nDataFormat = FileReadUnsignedShort("nDataFormat", -1, 1); //H;
            nSimultaneousScan = FileReadUnsignedShort("nSimultaneousScan", -1, 1); //H;
            nCRCEnable = FileReadUnsignedShort("nCRCEnable", -1, 1); //H;
            uFileCRC = FileReadUnsignedInt("uFileCRC", -1, 1); //I;
            uFileGUID = FileReadBytes("uFileGUID", -1, 16); //16B;
            uCreatorVersion = FileReadBytes("uCreatorVersion", -1, 4); //4B;
            uCreatorNameIndex = FileReadUnsignedInt("uCreatorNameIndex", -1, 1); //I;
            uModifierVersion = FileReadUnsignedInt("uModifierVersion", -1, 1); //I;
            uModifierNameIndex = FileReadUnsignedInt("uModifierNameIndex", -1, 1); //I;
            uProtocolPathIndex = FileReadUnsignedInt("uProtocolPathIndex", -1, 1); //I;
        }


        private void ReadSectionMap()
        {
            log.Debug("### Section Map ###");
            Section ProtocolSection = new Section("ProtocolSection", br, log, 76);
            Section UserListSection = new Section("UserListSection", br, log, 172);
            Section StatsRegionSection = new Section("StatsRegionSection", br, log, 188);
            Section MathSection = new Section("MathSection", br, log, 204);
            Section ScopeSection = new Section("ScopeSection", br, log, 268);
            Section DeltaSection = new Section("DeltaSection", br, log, 284);
            Section VoiceTagSection = new Section("VoiceTagSection", br, log, 300);
            Section SynchArraySection = new Section("SynchArraySection", br, log, 316);
            Section AnnotationSection = new Section("AnnotationSection", br, log, 332);
            Section StatsSection = new Section("StatsSection", br, log, 348);
        }

    }



}
