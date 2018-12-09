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
            public fixed Int32 lDACFilePtr[8];
            public fixed Int32 lDACFileNumEpisodes[8];

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
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)] public string sProtocolPath;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)] public string sCreatorInfo;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)] public string sModifierInfo;
            public Int16 nCommentsEnable;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)] public string sFileComment;
            public fixed Int16 nTelegraphEnable[16];
            public fixed Int16 nTelegraphInstrument[16];
            public fixed Single fTelegraphAdditGain[16];
            public fixed Single fTelegraphFilter[16];
            public fixed Single fTelegraphMembraneCap[16];
            public fixed Single fTelegraphAccessResistance[16];
            public fixed Int16 nTelegraphMode[16];
            public fixed Int16 nTelegraphDACScaleFactorEnable[8];
            public Int16 nAutoAnalyseEnable;
            public Guid FileGUID;
            public fixed Single fInstrumentHoldingLevel[8];
            public UInt32 ulFileCRC;
            public Int16 nCRCEnable;

            // GROUP #7 - Multi-channel information
            public Int16 nSignalType;
            public fixed Int16 nADCPtoLChannelMap[16];
            public fixed Int16 nADCSamplingSeq[16];
            public fixed Single fADCProgrammableGain[16];
            public fixed Single fADCDisplayAmplification[16];
            public fixed Single fADCDisplayOffset[16];
            public fixed Single fInstrumentScaleFactor[16];
            public fixed Single fInstrumentOffset[16];
            public fixed Single fSignalGain[16];
            public fixed Single fSignalOffset[16];
            public fixed Single fSignalLowpassFilter[16];
            public fixed Single fSignalHighpassFilter[16];
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)] public string nLowpassFilterType;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)] public string nHighpassFilterType;
            public fixed Byte bHumFilterEnable[16];
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16*10)] public string sADCChannelName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16*8)] public string sADCUnits;
            public fixed Single fDACScaleFactor[8];
            public fixed Single fDACHoldingLevel[8];
            public fixed Single fDACCalibrationFactor[8];
            public fixed Single fDACCalibrationOffset[8];
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 8*10)] public string sDACChannelName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 8*8)] public string sDACChannelUnits;

            // GROUP #9 - Epoch Waveform and Pulses
            public Int16 nDigitalEnable;
            public Int16 nActiveDACChannel;
            public Int16 nDigitalDACChannel;
            public Int16 nDigitalHolding;
            public Int16 nDigitalInterEpisode;
            public Int16 nDigitalTrainActiveLogic;
            public fixed Int16 nDigitalValue[50];
            public fixed Int16 nDigitalTrainValue[50];
            public fixed Byte bEpochCompression[50];
            public fixed Int16 nWaveformEnable[8];
            public fixed Int16 nWaveformSource[8];
            public fixed Int16 nInterEpisodeLevel[8];
            public fixed Int16 nEpochType[8 * 50];
            public fixed Single fEpochInitLevel[8 * 50];
            public fixed Single fEpochFinalLevel[8 * 50];
            public fixed Single fEpochLevelInc[8 * 50];
            public fixed Int32 lEpochInitDuration[8 * 50];
            public fixed Int32 lEpochDurationInc[8 * 50];
            public fixed Int16 nEpochTableRepetitions[8];
            public fixed Single fEpochTableStartToStartInterval[8];

            // GROUP #10 - DAC Output File
            public fixed Single fDACFileScale[8];
            public fixed Single fDACFileOffset[8];
            public fixed Int32 lDACFileEpisodeNum[8];
            public fixed Int16 nDACFileADCNum[8];
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 8 * 256)] public string sDACFilePath;

            // GROUP #11a - Presweep (conditioning) pulse train
            public fixed Int16 nConditEnable[8];
            public fixed Int32 lConditNumPulses[8];
            public fixed Single fBaselineDuration[8];
            public fixed Single fBaselineLevel[8];
            public fixed Single fStepDuration[8];
            public fixed Single fStepLevel[8];
            public fixed Single fPostTrainPeriod[8];
            public fixed Single fPostTrainLevel[8];
            public fixed Single fCTStartLevel[8 * 50];
            public fixed Single fCTEndLevel[8 * 50];
            public fixed Single fCTIntervalDuration[8 * 50];
            public fixed Single fCTStartToStartInterval[8];

            // GROUP #11b - Membrane Test Between Sweeps
            public fixed Int16 nMembTestEnable[8];
            public fixed Single fMembTestPreSettlingTimeMS[8];
            public fixed Single fMembTestPostSettlingTimeMS[8];

            // GROUP #11c - PreSignal test pulse
            public fixed Int16 nPreSignalEnable[8];
            public fixed Single fPreSignalPreStepDuration[8];
            public fixed Single fPreSignalPreStepLevel[8];
            public fixed Single fPreSignalStepDuration[8];
            public fixed Single fPreSignalStepLevel[8];
            public fixed Single fPreSignalPostStepDuration[8];
            public fixed Single fPreSignalPostStepLevel[8];

            // GROUP #11d - Hum Silncer Adapt between sweeps
            public Int16 nAdaptEnable;
            public Single fInterSweepAdaptTimeS;

            // GROUP #12 - Variable parameter user list
            public fixed Int16 nULEnable[8];
            public fixed Int16 nULParamToVary[8];
            public fixed Int16 nULRepeat[8];
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 8 * 256)] public string sULParamValueList;

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
            public fixed Int32 lStatsMeasurements[24];
            public fixed Int32 lStatsStart[24];
            public fixed Int32 lStatsEnd[24];
            public fixed Int16 nRiseBottomPercentile[24];
            public fixed Int16 nRiseTopPercentile[24];
            public fixed Int16 nDecayBottomPercentile[24];
            public fixed Int16 nDecayTopPercentile[24];
            public fixed Int16 nStatsChannelPolarity[16];
            public fixed Int16 nStatsSearchMode[24];
            public fixed Int16 nStatsSearchDAC[24];

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
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 2)] public string sArithmeticOperator;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 8)] public string sArithmeticUnits;

            // GROUP #15 - Leak subtraction
            public Int16 nPNPosition;
            public Int16 nPNNumPulses;
            public Int16 nPNPolarity;
            public Single fPNSettlingTime;
            public Single fPNInterpulse;
            public fixed Int16 nLeakSubtractType[8];
            public fixed Single fPNHoldingLevel[8];
            public fixed Int16 nLeakSubtractADCIndex[8];

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
            public fixed Int32 lEpochPulsePeriod[8 * 50];
            public fixed Int32 lEpochPulseWidth[8 * 50];

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
            public fixed Int16 nLTPUsageOfDAC[8];
            public fixed Int16 nLTPPresynapticPulses[8];

            // GROUP #20 - Digidata 132x Trigger out flag
            public Int16 nScopeTriggerOut;

            // GROUP #21 - Epoch resistance
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 8 * 10)] public string sEpochResistanceSignalName;
            public fixed Int16 nEpochResistanceState[8];

            // GROUP #22 - Alternating episodic mode
            public Int16 nAlternateDACOutputState;
            public Int16 nAlternateDigitalOutputState;
            public fixed Int16 nAlternateDigitalValue[50];
            public fixed Int16 nAlternateDigitalTrainValue[50];

            // GROUP #23 - Post-processing actions
            public fixed Single fPostProcessLowpassFilter[16];
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)] public string nPostProcessLowpassFilterType;

            // GROUP #24 - Legacy gear shift info
            public Single fLegacyADCSequenceInterval;
            public Single fLegacyADCSecondSequenceInterval;
            public Int32 lLegacyClockChange;
            public Int32 lLegacyNumSamplesPerEpisode;

            // GROUP #25 - Gap-Free Config
            public fixed Int16 nGapFreeEpochType[8 * 50];
            public fixed Single fGapFreeEpochLevel[8 * 50];
            public fixed Int32 lGapFreeEpochDuration[8 * 50];
            public fixed Byte nGapFreeDigitalValue[8 * 50];
            public Int16 nGapFreeEpochStart;
        };
    }
}
