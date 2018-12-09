using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AxonDLL
{
    public class AbfStructs
    {
        public const int ABF_ADCCOUNT = 16; // number of ADC channels supported.
        public const int ABF_DACCOUNT = 8; // number of DAC channels supported.
        public const int ABF_EPOCHCOUNT = 50; // number of waveform epochs supported.
        public const int ABF_ADCUNITLEN = 8; // length of ADC units strings
        public const int ABF_ADCNAMELEN = 10; // length of actual ADC channel name strings
        public const int ABF_DACUNITLEN = 8; // length of DAC units strings
        public const int ABF_DACNAMELEN = 10; // length of DAC channel name strings
        public const int ABF_USERLISTLEN = 256; // length of the user list (V1.6)
        public const int ABF_USERLISTCOUNT = ABF_DACCOUNT; // number of independent user lists (V1.6)       
        public const int ABF_FILECOMMENTLEN = 128; // length of file comment string (V1.6)
        public const int ABF_PATHLEN = 256; // length of full path, used for DACFile and Protocol name.
        public const int ABF_CREATORINFOLEN = 16; // length of file creator info string
        public const int ABF_ARITHMETICOPLEN = 2; // length of the Arithmetic operator field
        public const int ABF_ARITHMETICUNITSLEN = 8; // length of arithmetic units string
        public const int ABF_STATS_REGIONS = 24; // The number of independent statistics regions. // ST-91
        
        //public const int ABF_ADCNAMELEN_USER = 8; // length of user-entered ADC channel name strings
        //public const int ABF_OLDFILECOMMENTLEN = 56; // length of file comment string (pre V1.6)
        //public const int ABF_TAGCOMMENTLEN = 56; // length of tag comment string
        //public const int ABF_BLOCKSIZE = 512; // Size of block alignment in ABF files.
        //public const int PCLAMP6_MAXSWEEPLENGTH = 16384; // Maximum multiplexed sweep length supported by pCLAMP6 apps.
        //public const int PCLAMP7_MAXSWEEPLEN_PERCHAN = 1032258; // Maximum per channel sweep length supported by pCLAMP7 apps.
        //public const int PCLAMP11_MAXSWEEPLEN_PERCHAN = 5161290; // Maximum per channel sweep length supported by pCLAMP11 apps. //ST-1
        //public const int ABF_MAX_SWEEPS_PER_AVERAGE = 65500; // The maximum number of sweeps that can be combined into a cumulative average 
        //public const int ABF_MAX_TRIAL_SAMPLES = 0x7FFFFFFF; // Maximum length of acquisition supported (samples)

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
        public unsafe struct ABFFileHeader
        {

            // GROUP #1 - File ID and size information
            public Single fFileVersionNumber;
            public Int16 nOperationMode;
            public Int32 lActualAcqLength;
            public Int16 nNumPointsIgnored;
            public Int32 lActualEpisodes;
            public UInt32 uFileStartDate;
            public UInt32 uFileStartTimeMS;
            public Int32 lStopwatchTime;
            public Single fHeaderVersionNumber;
            public Int16 nFileType;

            // GROUP #2 - File Structure
            public Int32 lDataSectionPtr;
            public Int32 lTagSectionPtr;
            public Int32 lNumTagEntries;
            public Int32 lScopeConfigPtr;
            public Int32 lNumScopes;
            public Int32 lDeltaArrayPtr;
            public Int32 lNumDeltas;
            public Int32 lVoiceTagPtr;
            public Int32 lVoiceTagEntries;
            public Int32 lSynchArrayPtr;
            public Int32 lSynchArraySize;
            public Int16 nDataFormat;
            public Int16 nSimultaneousScan;
            public Int32 lStatisticsConfigPtr;
            public Int32 lAnnotationSectionPtr;
            public Int32 lNumAnnotations;
            public fixed Int32 lDACFilePtr[ABF_DACCOUNT];
            public fixed Int32 lDACFileNumEpisodes[ABF_DACCOUNT];

            // GROUP #3 - Trial hierarchy information
            public Int16 nADCNumChannels;
            public Single fADCSequenceInterval;
            public UInt32 uFileCompressionRatio;
            public Byte bEnableFileCompression;
            public Single fSynchTimeUnit;
            public Single fSecondsPerRun;
            public Int32 lNumSamplesPerEpisode;
            public Int32 lPreTriggerSamples;
            public Int32 lEpisodesPerRun;
            public Int32 lRunsPerTrial;
            public Int32 lNumberOfTrials;
            public Int16 nAveragingMode;
            public Int16 nUndoRunCount;
            public Int16 nFirstEpisodeInRun;
            public Single fTriggerThreshold;
            public Int16 nTriggerSource;
            public Int16 nTriggerAction;
            public Int16 nTriggerPolarity;
            public Single fScopeOutputInterval;
            public Single fEpisodeStartToStart;
            public Single fRunStartToStart;
            public Single fTrialStartToStart;
            public Int32 lAverageCount;
            public Int16 nAutoTriggerStrategy;
            public Single fFirstRunDelayS;
            public UInt32 nTriggerTimeout;

            // GROUP #4 - Display Parameters
            public Int16 nDataDisplayMode;
            public Int16 nChannelStatsStrategy;
            public Int32 lSamplesPerTrace;
            public Int32 lStartDisplayNum;
            public Int32 lFinishDisplayNum;
            public Int16 nShowPNRawData;
            public Single fStatisticsPeriod;
            public Int32 lStatisticsMeasurements;
            public Int16 nStatisticsSaveStrategy;

            // GROUP #5 - Hardware information
            public Single fADCRange;
            public Single fDACRange;
            public Int32 lADCResolution;
            public Int32 lDACResolution;
            public Int16 nDigitizerADCs;
            public Int16 nDigitizerDACs;
            public Int16 nDigitizerTotalDigitalOuts;
            public Int16 nDigitizerSynchDigitalOuts;
            public Int16 nDigitizerType;

            // GROUP #6 Environmental Information
            public Int16 nExperimentType;
            public Int16 nManualInfoStrategy;
            public Single fCellID1;
            public Single fCellID2;
            public Single fCellID3;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = ABF_PATHLEN)] public string sProtocolPath;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = ABF_CREATORINFOLEN)] public string sCreatorInfo;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = ABF_CREATORINFOLEN)] public string sModifierInfo;
            public Int16 nCommentsEnable;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = ABF_FILECOMMENTLEN)] public string sFileComment;
            public fixed Int16 nTelegraphEnable[ABF_ADCCOUNT];
            public fixed Int16 nTelegraphInstrument[ABF_ADCCOUNT];
            public fixed Single fTelegraphAdditGain[ABF_ADCCOUNT];
            public fixed Single fTelegraphFilter[ABF_ADCCOUNT];
            public fixed Single fTelegraphMembraneCap[ABF_ADCCOUNT];
            public fixed Single fTelegraphAccessResistance[ABF_ADCCOUNT];
            public fixed Int16 nTelegraphMode[ABF_ADCCOUNT];
            public fixed Int16 nTelegraphDACScaleFactorEnable[ABF_DACCOUNT];
            public Int16 nAutoAnalyseEnable;
            public Guid FileGUID;
            public fixed Single fInstrumentHoldingLevel[ABF_DACCOUNT];
            public UInt32 ulFileCRC;
            public Int16 nCRCEnable;

            // GROUP #7 - Multi-channel information
            public Int16 nSignalType;
            public fixed Int16 nADCPtoLChannelMap[ABF_ADCCOUNT];
            public fixed Int16 nADCSamplingSeq[ABF_ADCCOUNT];
            public fixed Single fADCProgrammableGain[ABF_ADCCOUNT];
            public fixed Single fADCDisplayAmplification[ABF_ADCCOUNT];
            public fixed Single fADCDisplayOffset[ABF_ADCCOUNT];
            public fixed Single fInstrumentScaleFactor[ABF_ADCCOUNT];
            public fixed Single fInstrumentOffset[ABF_ADCCOUNT];
            public fixed Single fSignalGain[ABF_ADCCOUNT];
            public fixed Single fSignalOffset[ABF_ADCCOUNT];
            public fixed Single fSignalLowpassFilter[ABF_ADCCOUNT];
            public fixed Single fSignalHighpassFilter[ABF_ADCCOUNT];
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = ABF_ADCCOUNT)] public string nLowpassFilterType;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = ABF_ADCCOUNT)] public string nHighpassFilterType;
            public fixed Byte bHumFilterEnable[ABF_ADCCOUNT];
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = ABF_ADCCOUNT * ABF_ADCNAMELEN)] public string sADCChannelName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = ABF_ADCCOUNT * ABF_ADCUNITLEN)] public string sADCUnits;
            public fixed Single fDACScaleFactor[ABF_DACCOUNT];
            public fixed Single fDACHoldingLevel[ABF_DACCOUNT];
            public fixed Single fDACCalibrationFactor[ABF_DACCOUNT];
            public fixed Single fDACCalibrationOffset[ABF_DACCOUNT];
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = ABF_DACCOUNT * ABF_DACNAMELEN)] public string sDACChannelName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = ABF_DACCOUNT * ABF_DACUNITLEN)] public string sDACChannelUnits;

            // GROUP #9 - Epoch Waveform and Pulses
            public Int16 nDigitalEnable;
            public Int16 nActiveDACChannel;
            public Int16 nDigitalDACChannel;
            public Int16 nDigitalHolding;
            public Int16 nDigitalInterEpisode;
            public Int16 nDigitalTrainActiveLogic;
            public fixed Int16 nDigitalValue[ABF_EPOCHCOUNT];
            public fixed Int16 nDigitalTrainValue[ABF_EPOCHCOUNT];
            public fixed Byte bEpochCompression[ABF_EPOCHCOUNT];
            public fixed Int16 nWaveformEnable[ABF_DACCOUNT];
            public fixed Int16 nWaveformSource[ABF_DACCOUNT];
            public fixed Int16 nInterEpisodeLevel[ABF_DACCOUNT];
            public fixed Int16 nEpochType[ABF_DACCOUNT * ABF_EPOCHCOUNT];
            public fixed Single fEpochInitLevel[ABF_DACCOUNT * ABF_EPOCHCOUNT];
            public fixed Single fEpochFinalLevel[ABF_DACCOUNT * ABF_EPOCHCOUNT];
            public fixed Single fEpochLevelInc[ABF_DACCOUNT * ABF_EPOCHCOUNT];
            public fixed Int32 lEpochInitDuration[ABF_DACCOUNT * ABF_EPOCHCOUNT];
            public fixed Int32 lEpochDurationInc[ABF_DACCOUNT * ABF_EPOCHCOUNT];
            public fixed Int16 nEpochTableRepetitions[ABF_DACCOUNT];
            public fixed Single fEpochTableStartToStartInterval[ABF_DACCOUNT];

            // GROUP #10 - DAC Output File
            public fixed Single fDACFileScale[ABF_DACCOUNT];
            public fixed Single fDACFileOffset[ABF_DACCOUNT];
            public fixed Int32 lDACFileEpisodeNum[ABF_DACCOUNT];
            public fixed Int16 nDACFileADCNum[ABF_DACCOUNT];
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = ABF_DACCOUNT * ABF_PATHLEN)] public string sDACFilePath;

            // GROUP #11a - Presweep (conditioning) pulse train
            public fixed Int16 nConditEnable[ABF_DACCOUNT];
            public fixed Int32 lConditNumPulses[ABF_DACCOUNT];
            public fixed Single fBaselineDuration[ABF_DACCOUNT];
            public fixed Single fBaselineLevel[ABF_DACCOUNT];
            public fixed Single fStepDuration[ABF_DACCOUNT];
            public fixed Single fStepLevel[ABF_DACCOUNT];
            public fixed Single fPostTrainPeriod[ABF_DACCOUNT];
            public fixed Single fPostTrainLevel[ABF_DACCOUNT];
            public fixed Single fCTStartLevel[ABF_DACCOUNT * ABF_EPOCHCOUNT];
            public fixed Single fCTEndLevel[ABF_DACCOUNT * ABF_EPOCHCOUNT];
            public fixed Single fCTIntervalDuration[ABF_DACCOUNT * ABF_EPOCHCOUNT];
            public fixed Single fCTStartToStartInterval[ABF_DACCOUNT];

            // GROUP #11b - Membrane Test Between Sweeps
            public fixed Int16 nMembTestEnable[ABF_DACCOUNT];
            public fixed Single fMembTestPreSettlingTimeMS[ABF_DACCOUNT];
            public fixed Single fMembTestPostSettlingTimeMS[ABF_DACCOUNT];

            // GROUP #11c - PreSignal test pulse
            public fixed Int16 nPreSignalEnable[ABF_DACCOUNT];
            public fixed Single fPreSignalPreStepDuration[ABF_DACCOUNT];
            public fixed Single fPreSignalPreStepLevel[ABF_DACCOUNT];
            public fixed Single fPreSignalStepDuration[ABF_DACCOUNT];
            public fixed Single fPreSignalStepLevel[ABF_DACCOUNT];
            public fixed Single fPreSignalPostStepDuration[ABF_DACCOUNT];
            public fixed Single fPreSignalPostStepLevel[ABF_DACCOUNT];

            // GROUP #11d - Hum Silncer Adapt between sweeps
            public Int16 nAdaptEnable;
            public Single fInterSweepAdaptTimeS;

            // GROUP #12 - Variable parameter user list
            public fixed Int16 nULEnable[ABF_USERLISTCOUNT];
            public fixed Int16 nULParamToVary[ABF_USERLISTCOUNT];
            public fixed Int16 nULRepeat[ABF_USERLISTCOUNT];
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = ABF_USERLISTCOUNT * ABF_USERLISTLEN)] public string sULParamValueList;

            // GROUP #13 - Statistics measurements
            public Int16 nStatsEnable;
            public UInt16 nStatsActiveChannels;
            public UInt16 nStatsSearchRegionFlags;
            public Int16 nStatsSmoothing;
            public Int16 nStatsSmoothingEnable;
            public Int16 nStatsBaseline;
            public Int16 nStatsBaselineDAC;
            public Int32 lStatsBaselineStart;
            public Int32 lStatsBaselineEnd;
            public fixed Int32 lStatsMeasurements[ABF_STATS_REGIONS];
            public fixed Int32 lStatsStart[ABF_STATS_REGIONS];
            public fixed Int32 lStatsEnd[ABF_STATS_REGIONS];
            public fixed Int16 nRiseBottomPercentile[ABF_STATS_REGIONS];
            public fixed Int16 nRiseTopPercentile[ABF_STATS_REGIONS];
            public fixed Int16 nDecayBottomPercentile[ABF_STATS_REGIONS];
            public fixed Int16 nDecayTopPercentile[ABF_STATS_REGIONS];
            public fixed Int16 nStatsChannelPolarity[ABF_ADCCOUNT];
            public fixed Int16 nStatsSearchMode[ABF_STATS_REGIONS];
            public fixed Int16 nStatsSearchDAC[ABF_STATS_REGIONS];

            // GROUP #14 - Channel Arithmetic
            public Int16 nArithmeticEnable;
            public Int16 nArithmeticExpression;
            public Single fArithmeticUpperLimit;
            public Single fArithmeticLowerLimit;
            public Int16 nArithmeticADCNumA;
            public Int16 nArithmeticADCNumB;
            public Single fArithmeticK1;
            public Single fArithmeticK2;
            public Single fArithmeticK3;
            public Single fArithmeticK4;
            public Single fArithmeticK5;
            public Single fArithmeticK6;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = ABF_ARITHMETICOPLEN)] public string sArithmeticOperator;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = ABF_ARITHMETICUNITSLEN)] public string sArithmeticUnits;

            // GROUP #15 - Leak subtraction
            public Int16 nPNPosition;
            public Int16 nPNNumPulses;
            public Int16 nPNPolarity;
            public Single fPNSettlingTime;
            public Single fPNInterpulse;
            public fixed Int16 nLeakSubtractType[ABF_DACCOUNT];
            public fixed Single fPNHoldingLevel[ABF_DACCOUNT];
            public fixed Int16 nLeakSubtractADCIndex[ABF_DACCOUNT];

            // GROUP #16 - Miscellaneous variables
            public Int16 nLevelHysteresis;
            public Int32 lTimeHysteresis;
            public Int16 nAllowExternalTags;
            public Int16 nAverageAlgorithm;
            public Single fAverageWeighting;
            public Int16 nUndoPromptStrategy;
            public Int16 nTrialTriggerSource;
            public Int16 nStatisticsDisplayStrategy;
            public Int16 nExternalTagType;
            public Int32 lHeaderSize;
            public Int16 nStatisticsClearStrategy;
            public Int16 nEnableFirstLastHolding;

            // GROUP #17 - Trains parameters
            public fixed Int32 lEpochPulsePeriod[ABF_DACCOUNT * ABF_EPOCHCOUNT];
            public fixed Int32 lEpochPulseWidth[ABF_DACCOUNT * ABF_EPOCHCOUNT];

            // GROUP #18 - Application version data
            public Int16 nCreatorMajorVersion;
            public Int16 nCreatorMinorVersion;
            public Int16 nCreatorBugfixVersion;
            public Int16 nCreatorBuildVersion;
            public Int16 nModifierMajorVersion;
            public Int16 nModifierMinorVersion;
            public Int16 nModifierBugfixVersion;
            public Int16 nModifierBuildVersion;

            // GROUP #19 - LTP protocol
            public Int16 nLTPType;
            public fixed Int16 nLTPUsageOfDAC[ABF_DACCOUNT];
            public fixed Int16 nLTPPresynapticPulses[ABF_DACCOUNT];

            // GROUP #20 - Digidata 132x Trigger out flag
            public Int16 nScopeTriggerOut;

            // GROUP #21 - Epoch resistance
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = ABF_DACCOUNT * ABF_ADCNAMELEN)] public string sEpochResistanceSignalName;
            public fixed Int16 nEpochResistanceState[ABF_DACCOUNT];

            // GROUP #22 - Alternating episodic mode
            public Int16 nAlternateDACOutputState;
            public Int16 nAlternateDigitalOutputState;
            public fixed Int16 nAlternateDigitalValue[ABF_EPOCHCOUNT];
            public fixed Int16 nAlternateDigitalTrainValue[ABF_EPOCHCOUNT];

            // GROUP #23 - Post-processing actions
            public fixed Single fPostProcessLowpassFilter[ABF_ADCCOUNT];
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = ABF_ADCCOUNT)] public string nPostProcessLowpassFilterType;

            // GROUP #24 - Legacy gear shift info
            public Single fLegacyADCSequenceInterval;
            public Single fLegacyADCSecondSequenceInterval;
            public Int32 lLegacyClockChange;
            public Int32 lLegacyNumSamplesPerEpisode;

            // GROUP #25 - Gap-Free Config
            public fixed Int16 nGapFreeEpochType[ABF_DACCOUNT * ABF_EPOCHCOUNT];
            public fixed Single fGapFreeEpochLevel[ABF_DACCOUNT * ABF_EPOCHCOUNT];
            public fixed Int32 lGapFreeEpochDuration[ABF_DACCOUNT * ABF_EPOCHCOUNT];
            public fixed Byte nGapFreeDigitalValue[ABF_DACCOUNT * ABF_EPOCHCOUNT];
            public Int16 nGapFreeEpochStart;
        };
    }
}
