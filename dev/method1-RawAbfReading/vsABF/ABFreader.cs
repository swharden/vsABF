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

        public const int BLOCKSIZE = 512;

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
                    ReadProtocolSection();
                    ReadADCsection();
                    ReadDACsection();
                    ReadTagSection();
                    ReadStringsSection();
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

        public char FileReadChar(int bytePosition, int count)
        {
            if (bytePosition >= 0)
                br.BaseStream.Seek(bytePosition, SeekOrigin.Begin);
            return br.ReadChar();
        }

        public byte FileReadByte(int bytePosition, int count)
        {
            if (bytePosition >= 0)
                br.BaseStream.Seek(bytePosition, SeekOrigin.Begin);
            return br.ReadByte();
        }

        public string FileReadString(int bytePosition, int count)
        {
            if (bytePosition >= 0)
                br.BaseStream.Seek(bytePosition, SeekOrigin.Begin);
            return System.Text.Encoding.Default.GetString(br.ReadBytes(count));
        }

        public string[] FileReadStrings(int bytePosition, int charCount, int stringCount)
        {
            if (bytePosition >= 0)
                br.BaseStream.Seek(bytePosition, SeekOrigin.Begin);
            string[] s = new string[stringCount];
            for (int i=0; i<stringCount; i++)
            {
                s[i] = System.Text.Encoding.Default.GetString(br.ReadBytes(charCount));
            }
            return s;
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
            headerV1.sADCChannelName = FileReadStrings(442, 10, 16); //10s;
            headerV1.sADCUnits = FileReadStrings(602, 8, 16); //8s;
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

            // clean up adc and dac units
            for (int i=0; i<headerV1.sADCUnits.Length; i++)
            {
                headerV1.sADCUnits[i] = headerV1.sADCUnits[i].Trim();
            }
            for (int i = 0; i < headerV1.sADCChannelName.Length; i++)
            {
                headerV1.sADCChannelName[i] = headerV1.sADCChannelName[i].Trim();
            }
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
            log.Debug("Reading Section Map");
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

        public ProtocolSection protocolSection;
        public void ReadProtocolSection()
        {
            log.Debug("Reading Protocol Section");
            br.BaseStream.Seek(sectionMap.Protocol.byteStart, SeekOrigin.Begin);
            protocolSection = new ProtocolSection();
            protocolSection.nOperationMode = FileReadShort(-1, 1);
            protocolSection.fADCSequenceInterval = FileReadFloat(-1, 1);
            protocolSection.bEnableFileCompression = FileReadByte(-1, 1);
            protocolSection.sUnused = FileReadBytes(-1, 3);
            protocolSection.uFileCompressionRatio = FileReadUnsignedInt(-1, 1);
            protocolSection.fSynchTimeUnit = FileReadFloat(-1, 1);
            protocolSection.fSecondsPerRun = FileReadFloat(-1, 1);
            protocolSection.lNumSamplesPerEpisode = FileReadInt(-1, 1);
            protocolSection.lPreTriggerSamples = FileReadInt(-1, 1);
            protocolSection.lEpisodesPerRun = FileReadInt(-1, 1);
            protocolSection.lRunsPerTrial = FileReadInt(-1, 1);
            protocolSection.lNumberOfTrials = FileReadInt(-1, 1);
            protocolSection.nAveragingMode = FileReadShort(-1, 1);
            protocolSection.nUndoRunCount = FileReadShort(-1, 1);
            protocolSection.nFirstEpisodeInRun = FileReadShort(-1, 1);
            protocolSection.fTriggerThreshold = FileReadFloat(-1, 1);
            protocolSection.nTriggerSource = FileReadShort(-1, 1);
            protocolSection.nTriggerAction = FileReadShort(-1, 1);
            protocolSection.nTriggerPolarity = FileReadShort(-1, 1);
            protocolSection.fScopeOutputInterval = FileReadFloat(-1, 1);
            protocolSection.fEpisodeStartToStart = FileReadFloat(-1, 1);
            protocolSection.fRunStartToStart = FileReadFloat(-1, 1);
            protocolSection.lAverageCount = FileReadInt(-1, 1);
            protocolSection.fTrialStartToStart = FileReadFloat(-1, 1);
            protocolSection.nAutoTriggerStrategy = FileReadShort(-1, 1);
            protocolSection.fFirstRunDelayS = FileReadFloat(-1, 1);
            protocolSection.nChannelStatsStrategy = FileReadShort(-1, 1);
            protocolSection.lSamplesPerTrace = FileReadInt(-1, 1);
            protocolSection.lStartDisplayNum = FileReadInt(-1, 1);
            protocolSection.lFinishDisplayNum = FileReadInt(-1, 1);
            protocolSection.nShowPNRawData = FileReadShort(-1, 1);
            protocolSection.fStatisticsPeriod = FileReadFloat(-1, 1);
            protocolSection.lStatisticsMeasurements = FileReadInt(-1, 1);
            protocolSection.nStatisticsSaveStrategy = FileReadShort(-1, 1);
            protocolSection.fADCRange = FileReadFloat(-1, 1);
            protocolSection.fDACRange = FileReadFloat(-1, 1);
            protocolSection.lADCResolution = FileReadInt(-1, 1);
            protocolSection.lDACResolution = FileReadInt(-1, 1);
            protocolSection.nExperimentType = FileReadShort(-1, 1);
            protocolSection.nManualInfoStrategy = FileReadShort(-1, 1);
            protocolSection.nCommentsEnable = FileReadShort(-1, 1);
            protocolSection.lFileCommentIndex = FileReadInt(-1, 1);
            protocolSection.nAutoAnalyseEnable = FileReadShort(-1, 1);
            protocolSection.nSignalType = FileReadShort(-1, 1);
            protocolSection.nDigitalEnable = FileReadShort(-1, 1);
            protocolSection.nActiveDACChannel = FileReadShort(-1, 1);
            protocolSection.nDigitalHolding = FileReadShort(-1, 1);
            protocolSection.nDigitalInterEpisode = FileReadShort(-1, 1);
            protocolSection.nDigitalDACChannel = FileReadShort(-1, 1);
            protocolSection.nDigitalTrainActiveLogic = FileReadShort(-1, 1);
            protocolSection.nStatsEnable = FileReadShort(-1, 1);
            protocolSection.nStatisticsClearStrategy = FileReadShort(-1, 1);
            protocolSection.nLevelHysteresis = FileReadShort(-1, 1);
            protocolSection.lTimeHysteresis = FileReadInt(-1, 1);
            protocolSection.nAllowExternalTags = FileReadShort(-1, 1);
            protocolSection.nAverageAlgorithm = FileReadShort(-1, 1);
            protocolSection.fAverageWeighting = FileReadFloat(-1, 1);
            protocolSection.nUndoPromptStrategy = FileReadShort(-1, 1);
            protocolSection.nTrialTriggerSource = FileReadShort(-1, 1);
            protocolSection.nStatisticsDisplayStrategy = FileReadShort(-1, 1);
            protocolSection.nExternalTagType = FileReadShort(-1, 1);
            protocolSection.nScopeTriggerOut = FileReadShort(-1, 1);
            protocolSection.nLTPType = FileReadShort(-1, 1);
            protocolSection.nAlternateDACOutputState = FileReadShort(-1, 1);
            protocolSection.nAlternateDigitalOutputState = FileReadShort(-1, 1);
            protocolSection.fCellID = FileReadFloats(-1, 3);
            protocolSection.nDigitizerADCs = FileReadShort(-1, 1);
            protocolSection.nDigitizerDACs = FileReadShort(-1, 1);
            protocolSection.nDigitizerTotalDigitalOuts = FileReadShort(-1, 1);
            protocolSection.nDigitizerSynchDigitalOuts = FileReadShort(-1, 1);
            protocolSection.nDigitizerType = FileReadShort(-1, 1);
        }

        public ADCSection adcSection;
        public void ReadADCsection()
        {
            log.Debug("Reading ADC Section");

            adcSection = new ADCSection();
            adcSection.ADCsections = new ADCSectionByADC[sectionMap.ADC.itemCount];

            for (int adcNumber= 0; adcNumber<sectionMap.ADC.itemCount; adcNumber++)
            {
                log.Debug($"Reading ADC Section for ADC {adcNumber}");
                ADCSectionByADC thisAdc = new ADCSectionByADC();

                int firstByte = sectionMap.ADC.byteStart;
                firstByte += sectionMap.ADC.itemSize*adcNumber;
                br.BaseStream.Seek(firstByte, SeekOrigin.Begin);

                thisAdc.nADCNum = FileReadShort(-1, 1);
                thisAdc.nTelegraphEnable = FileReadShort(-1, 1);
                thisAdc.nTelegraphInstrument = FileReadShort(-1, 1);
                thisAdc.fTelegraphAdditGain = FileReadFloat(-1, 1);
                thisAdc.fTelegraphFilter = FileReadFloat(-1, 1);
                thisAdc.fTelegraphMembraneCap = FileReadFloat(-1, 1);
                thisAdc.nTelegraphMode = FileReadShort(-1, 1);
                thisAdc.fTelegraphAccessResistance = FileReadFloat(-1, 1);
                thisAdc.nADCPtoLChannelMap = FileReadShort(-1, 1);
                thisAdc.nADCSamplingSeq = FileReadShort(-1, 1);
                thisAdc.fADCProgrammableGain = FileReadFloat(-1, 1);
                thisAdc.fADCDisplayAmplification = FileReadFloat(-1, 1);
                thisAdc.fADCDisplayOffset = FileReadFloat(-1, 1);
                thisAdc.fInstrumentScaleFactor = FileReadFloat(-1, 1);
                thisAdc.fInstrumentOffset = FileReadFloat(-1, 1);
                thisAdc.fSignalGain = FileReadFloat(-1, 1);
                thisAdc.fSignalOffset = FileReadFloat(-1, 1);
                thisAdc.fSignalLowpassFilter = FileReadFloat(-1, 1);
                thisAdc.fSignalHighpassFilter = FileReadFloat(-1, 1);
                thisAdc.nLowpassFilterType = FileReadByte(-1, 1);
                thisAdc.nHighpassFilterType = FileReadByte(-1, 1);
                thisAdc.fPostProcessLowpassFilter = FileReadFloat(-1, 1);
                thisAdc.nPostProcessLowpassFilterType = FileReadByte(-1, 1);
                thisAdc.bEnabledDuringPN = FileReadByte(-1, 1);
                thisAdc.nStatsChannelPolarity = FileReadShort(-1, 1);
                thisAdc.lADCChannelNameIndex = FileReadInt(-1, 1);
                thisAdc.lADCUnitsIndex = FileReadInt(-1, 1);

                adcSection.ADCsections[adcNumber] = thisAdc;
            }
        }
        
        public DACSection dacSection;
        public void ReadDACsection()
        {
            log.Debug("Reading DAC Section");

            dacSection = new DACSection();
            dacSection.DACsections = new DACSectionPerDAC[sectionMap.DAC.itemCount];

            for (int dacNumber = 0; dacNumber < sectionMap.DAC.itemCount; dacNumber++)
            {
                log.Debug($"Reading DAC Section for DAC {dacNumber}");
                DACSectionPerDAC thisDac = new DACSectionPerDAC();

                int firstByte = sectionMap.DAC.byteStart;
                firstByte += sectionMap.DAC.itemSize * dacNumber;
                br.BaseStream.Seek(firstByte, SeekOrigin.Begin);

                thisDac.nDACNum = FileReadShort(-1, 1);
                thisDac.nTelegraphDACScaleFactorEnable = FileReadShort(-1, 1);
                thisDac.fInstrumentHoldingLevel = FileReadFloat(-1, 1);
                thisDac.fDACScaleFactor = FileReadFloat(-1, 1);
                thisDac.fDACHoldingLevel = FileReadFloat(-1, 1);
                thisDac.fDACCalibrationFactor = FileReadFloat(-1, 1);
                thisDac.fDACCalibrationOffset = FileReadFloat(-1, 1);
                thisDac.lDACChannelNameIndex = FileReadInt(-1, 1);
                thisDac.lDACChannelUnitsIndex = FileReadInt(-1, 1);
                thisDac.lDACFilePtr = FileReadInt(-1, 1);
                thisDac.lDACFileNumEpisodes = FileReadInt(-1, 1);
                thisDac.nWaveformEnable = FileReadShort(-1, 1);
                thisDac.nWaveformSource = FileReadShort(-1, 1);
                thisDac.nInterEpisodeLevel = FileReadShort(-1, 1);
                thisDac.fDACFileScale = FileReadFloat(-1, 1);
                thisDac.fDACFileOffset = FileReadFloat(-1, 1);
                thisDac.lDACFileEpisodeNum = FileReadInt(-1, 1);
                thisDac.nDACFileADCNum = FileReadShort(-1, 1);
                thisDac.nConditEnable = FileReadShort(-1, 1);
                thisDac.lConditNumPulses = FileReadInt(-1, 1);
                thisDac.fBaselineDuration = FileReadFloat(-1, 1);
                thisDac.fBaselineLevel = FileReadFloat(-1, 1);
                thisDac.fStepDuration = FileReadFloat(-1, 1);
                thisDac.fStepLevel = FileReadFloat(-1, 1);
                thisDac.fPostTrainPeriod = FileReadFloat(-1, 1);
                thisDac.fPostTrainLevel = FileReadFloat(-1, 1);
                thisDac.nMembTestEnable = FileReadShort(-1, 1);
                thisDac.nLeakSubtractType = FileReadShort(-1, 1);
                thisDac.nPNPolarity = FileReadShort(-1, 1);
                thisDac.fPNHoldingLevel = FileReadFloat(-1, 1);
                thisDac.nPNNumADCChannels = FileReadShort(-1, 1);
                thisDac.nPNPosition = FileReadShort(-1, 1);
                thisDac.nPNNumPulses = FileReadShort(-1, 1);
                thisDac.fPNSettlingTime = FileReadFloat(-1, 1);
                thisDac.fPNInterpulse = FileReadFloat(-1, 1);
                thisDac.nLTPUsageOfDAC = FileReadShort(-1, 1);
                thisDac.nLTPPresynapticPulses = FileReadShort(-1, 1);
                thisDac.lDACFilePathIndex = FileReadInt(-1, 1);
                thisDac.fMembTestPreSettlingTimeMS = FileReadFloat(-1, 1);
                thisDac.fMembTestPostSettlingTimeMS = FileReadFloat(-1, 1);
                thisDac.nLeakSubtractADCIndex = FileReadShort(-1, 1);
                
                dacSection.DACsections[dacNumber] = thisDac;
            }
        }

        public TagSection tagSection;
        public void ReadTagSection()
        {
            log.Debug("Reading Tag Section");
            tagSection = new TagSection();
            tagSection.tags = new TagSectionByTag[sectionMap.Tag.itemCount];

            for (int tagNumber = 0; tagNumber < sectionMap.Tag.itemCount; tagNumber++)
            {
                log.Debug($"Reading tag Section for tag {tagNumber}");

                TagSectionByTag thisTag = new TagSectionByTag();

                int firstByte = sectionMap.Tag.byteStart;
                firstByte += sectionMap.Tag.itemSize * tagNumber;
                br.BaseStream.Seek(firstByte, SeekOrigin.Begin);

                thisTag.lTagTime = FileReadInt(-1, 1);
                thisTag.sComment = FileReadString(-1, 56).Trim();
                thisTag.nTagType = FileReadShort(-1, 1);
                thisTag.nVoiceTagIndex = FileReadShort(-1, 1);

                tagSection.tags[tagNumber] = thisTag;
            }
        }


        public StringsIndexed stringsIndexed;
        public void ReadStringsSection()
        {
            log.Debug("Reading Strings Section");
            stringsIndexed = new StringsIndexed();

            // just the first string is useful
            string s = FileReadString(sectionMap.Strings.byteStart, sectionMap.Strings.itemSize);
            s = s.Substring(s.LastIndexOf("\x00\x00"));
            s = s.Replace("\xb5", "\x75"); // make mu u
            string[] strings = s.Split('\x00');
            strings = strings.Skip(1).ToArray();
            stringsIndexed.strings = strings;

            stringsIndexed.uCreatorName = strings[headerV2.uCreatorNameIndex];
            stringsIndexed.uModifierName = strings[headerV2.uModifierNameIndex];
            stringsIndexed.uProtocolPath = strings[headerV2.uProtocolPathIndex];
            stringsIndexed.lFileComment = strings[protocolSection.lFileCommentIndex];

            stringsIndexed.lADCChannelName = new string[adcSection.ADCsections.Length];
            stringsIndexed.lADCUnits = new string[adcSection.ADCsections.Length];
            for (int i=0;i<adcSection.ADCsections.Length; i++)
            {
                stringsIndexed.lADCChannelName[i] = strings[adcSection.ADCsections[i].lADCChannelNameIndex];
                stringsIndexed.lADCUnits[i] = strings[adcSection.ADCsections[i].lADCUnitsIndex];
            }

            stringsIndexed.lDACChannelName = new string[dacSection.DACsections.Length];
            stringsIndexed.lDACChannelUnits = new string[dacSection.DACsections.Length];
            stringsIndexed.lDACFilePath = new string[dacSection.DACsections.Length];
            stringsIndexed.nLeakSubtractADC = new string[dacSection.DACsections.Length];
            for (int i = 0; i < dacSection.DACsections.Length; i++)
            {
                stringsIndexed.lDACChannelName[i] = strings[dacSection.DACsections[i].lDACChannelNameIndex];
                stringsIndexed.lDACChannelUnits[i] = strings[dacSection.DACsections[i].lDACChannelUnitsIndex];
                stringsIndexed.lDACFilePath[i] = strings[dacSection.DACsections[i].lDACFilePathIndex];
                stringsIndexed.nLeakSubtractADC[i] = strings[dacSection.DACsections[i].nLeakSubtractADCIndex];
            }

        }

    }
}
