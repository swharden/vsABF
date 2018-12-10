using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AxonDLL
{
    public class AbfStructDisplay
    {

        private static string txtFormat(string variable, string value)
        {
            if (variable == "")
            {
                return $"\n### {value} ###\n";
            }
            else
            {
                return $"{variable} = {value}\n";
            }
        }

        // display header just like ABFINFO program does
        public static unsafe string headerToString(AbfStructs.ABFFileHeader header)
        {
            string txt = "";

            txt += txtFormat("", "GROUP #1 - File ID and size information");
            txt += txtFormat("fFileVersionNumber ", header.fFileVersionNumber.ToString());
            txt += txtFormat("nOperationMode", header.nOperationMode.ToString());
            txt += txtFormat("lActualAcqLength ", header.lActualAcqLength.ToString());
            txt += txtFormat("nNumPointsIgnored", header.nNumPointsIgnored.ToString());
            txt += txtFormat("lActualEpisodes", header.lActualEpisodes.ToString());
            txt += txtFormat("uFileStartDate", header.uFileStartDate.ToString());
            txt += txtFormat("uFileStartTimeMS", header.uFileStartTimeMS.ToString());
            txt += txtFormat("lStopwatchTime", header.lStopwatchTime.ToString());
            txt += txtFormat("fHeaderVersionNumber", header.fHeaderVersionNumber.ToString());
            txt += txtFormat("nFileType", header.nFileType.ToString());

            txt += txtFormat("", "GROUP #2 - File Structure");
            txt += txtFormat("lDataSectionPtr", header.lDataSectionPtr.ToString());
            txt += txtFormat("lTagSectionPtr", header.lTagSectionPtr.ToString());
            txt += txtFormat("lNumTagEntries", header.lNumTagEntries.ToString());
            txt += txtFormat("lScopeConfigPtr", header.lScopeConfigPtr.ToString());
            txt += txtFormat("lNumScopes", header.lNumScopes.ToString());
            txt += txtFormat("lDeltaArrayPtr", header.lDeltaArrayPtr.ToString());
            txt += txtFormat("lNumDeltas", header.lNumDeltas.ToString());
            txt += txtFormat("lVoiceTagPtr", header.lVoiceTagPtr.ToString());
            txt += txtFormat("lVoiceTagEntries", header.lVoiceTagEntries.ToString());
            txt += txtFormat("lSynchArrayPtr", header.lSynchArrayPtr.ToString());
            txt += txtFormat("lSynchArraySize", header.lSynchArraySize.ToString());
            txt += txtFormat("nDataFormat", header.nDataFormat.ToString());
            txt += txtFormat("nSimultaneousScan", header.nSimultaneousScan.ToString());
            txt += txtFormat("lStatisticsConfigPtr", header.lStatisticsConfigPtr.ToString());
            txt += txtFormat("lAnnotationSectionPtr", header.lAnnotationSectionPtr.ToString());
            txt += txtFormat("lNumAnnotations", header.lNumAnnotations.ToString());
            txt += txtFormat("lDACFilePtr", String.Join(", ", header.lDACFilePtr));
            txt += txtFormat("lDACFileNumEpisodes", String.Join(", ", header.lDACFileNumEpisodes));

            txt += txtFormat("", "GROUP #3 - Trial hierarchy information");
            txt += txtFormat("nADCNumChannels", header.nADCNumChannels.ToString());
            txt += txtFormat("fADCSequenceInterval", header.fADCSequenceInterval.ToString());
            txt += txtFormat("uFileCompressionRatio", header.uFileCompressionRatio.ToString());
            txt += txtFormat("bEnableFileCompression", header.bEnableFileCompression.ToString());
            txt += txtFormat("fSynchTimeUnit", header.fSynchTimeUnit.ToString());
            txt += txtFormat("fSecondsPerRun", header.fSecondsPerRun.ToString());
            txt += txtFormat("lNumSamplesPerEpisode", header.lNumSamplesPerEpisode.ToString());
            txt += txtFormat("lPreTriggerSamples", header.lPreTriggerSamples.ToString());
            txt += txtFormat("lEpisodesPerRun", header.lEpisodesPerRun.ToString());
            txt += txtFormat("lRunsPerTrial", header.lRunsPerTrial.ToString());
            txt += txtFormat("lNumberOfTrials", header.lNumberOfTrials.ToString());
            txt += txtFormat("nAveragingMode", header.nAveragingMode.ToString());
            txt += txtFormat("nUndoRunCount", header.nUndoRunCount.ToString());
            txt += txtFormat("nFirstEpisodeInRun", header.nFirstEpisodeInRun.ToString());
            txt += txtFormat("fTriggerThreshold", header.fTriggerThreshold.ToString());
            txt += txtFormat("nTriggerSource", header.nTriggerSource.ToString());
            txt += txtFormat("nTriggerAction", header.nTriggerAction.ToString());
            txt += txtFormat("nTriggerPolarity", header.nTriggerPolarity.ToString());
            txt += txtFormat("fScopeOutputInterval", header.fScopeOutputInterval.ToString());
            txt += txtFormat("fEpisodeStartToStart", header.fEpisodeStartToStart.ToString());
            txt += txtFormat("fRunStartToStart", header.fRunStartToStart.ToString());
            txt += txtFormat("fTrialStartToStart", header.fTrialStartToStart.ToString());
            txt += txtFormat("lAverageCount", header.lAverageCount.ToString());
            txt += txtFormat("nAutoTriggerStrategy", header.nAutoTriggerStrategy.ToString());
            txt += txtFormat("fFirstRunDelayS", header.fFirstRunDelayS.ToString());
            txt += txtFormat("nTriggerTimeout", header.nTriggerTimeout.ToString());

            txt += txtFormat("", "GROUP #4 - Display Parameters");
            txt += txtFormat("nDataDisplayMode", header.nDataDisplayMode.ToString());
            txt += txtFormat("nChannelStatsStrategy", header.nChannelStatsStrategy.ToString());
            txt += txtFormat("lSamplesPerTrace", header.lSamplesPerTrace.ToString());
            txt += txtFormat("lStartDisplayNum", header.lStartDisplayNum.ToString());
            txt += txtFormat("lFinishDisplayNum", header.lFinishDisplayNum.ToString());
            txt += txtFormat("nShowPNRawData", header.nShowPNRawData.ToString());
            txt += txtFormat("fStatisticsPeriod", header.fStatisticsPeriod.ToString());
            txt += txtFormat("lStatisticsMeasurements", header.lStatisticsMeasurements.ToString());
            txt += txtFormat("nStatisticsSaveStrategy", header.nStatisticsSaveStrategy.ToString());

            txt += txtFormat("", "GROUP #5 - Hardware information");
            txt += txtFormat("fADCRange", header.fADCRange.ToString());
            txt += txtFormat("fDACRange", header.fDACRange.ToString());
            txt += txtFormat("lADCResolution", header.lADCResolution.ToString());
            txt += txtFormat("lDACResolution", header.lDACResolution.ToString());
            txt += txtFormat("nDigitizerADCs", header.nDigitizerADCs.ToString());
            txt += txtFormat("nDigitizerDACs", header.nDigitizerDACs.ToString());
            txt += txtFormat("nDigitizerTotalDigitalOuts", header.nDigitizerTotalDigitalOuts.ToString());
            txt += txtFormat("nDigitizerSynchDigitalOuts", header.nDigitizerSynchDigitalOuts.ToString());
            txt += txtFormat("nDigitizerType", header.nDigitizerType.ToString());

            txt += txtFormat("", "GROUP #6 Environmental Information");
            txt += txtFormat("nExperimentType", header.nExperimentType.ToString());
            txt += txtFormat("nManualInfoStrategy", header.nManualInfoStrategy.ToString());
            txt += txtFormat("fCellID1", header.fCellID1.ToString());
            txt += txtFormat("fCellID2", header.fCellID2.ToString());
            txt += txtFormat("fCellID3", header.fCellID3.ToString());
            txt += txtFormat("sProtocolPath", header.sProtocolPath.ToString());
            txt += txtFormat("sCreatorInfo", header.sCreatorInfo.ToString());
            txt += txtFormat("sModifierInfo", header.sModifierInfo.ToString());
            txt += txtFormat("nCommentsEnable", header.nCommentsEnable.ToString());
            txt += txtFormat("sFileComment", header.sFileComment.ToString());
            txt += txtFormat("nTelegraphEnable", String.Join(", ", header.nTelegraphEnable));
            txt += txtFormat("nTelegraphInstrument", String.Join(", ", header.nTelegraphInstrument));
            txt += txtFormat("fTelegraphAdditGain", String.Join(", ", header.fTelegraphAdditGain));
            txt += txtFormat("fTelegraphFilter", String.Join(", ", header.fTelegraphFilter));
            txt += txtFormat("fTelegraphMembraneCap", String.Join(", ", header.fTelegraphMembraneCap));
            txt += txtFormat("fTelegraphAccessResistance", String.Join(", ", header.fTelegraphAccessResistance));
            txt += txtFormat("nTelegraphMode", String.Join(", ", header.nTelegraphMode));
            txt += txtFormat("nTelegraphDACScaleFactorEnable", String.Join(", ", header.nTelegraphDACScaleFactorEnable));
            txt += txtFormat("nAutoAnalyseEnable", header.nAutoAnalyseEnable.ToString());
            txt += txtFormat("FileGUID", header.FileGUID.ToString());
            txt += txtFormat("fInstrumentHoldingLevel", String.Join(", ", header.fInstrumentHoldingLevel));
            txt += txtFormat("ulFileCRC", header.ulFileCRC.ToString());
            txt += txtFormat("nCRCEnable", header.nCRCEnable.ToString());

            txt += txtFormat("", "GROUP #7 Multi-channel information");
            txt += txtFormat("nSignalType", header.nSignalType.ToString());
            txt += txtFormat("nADCPtoLChannelMap", String.Join(", ", header.nADCPtoLChannelMap));
            txt += txtFormat("nADCSamplingSeq", String.Join(", ", header.nADCSamplingSeq));
            txt += txtFormat("fADCProgrammableGain", String.Join(", ", header.fADCProgrammableGain));
            txt += txtFormat("fADCDisplayAmplification", String.Join(", ", header.fADCDisplayAmplification));
            txt += txtFormat("fADCDisplayOffset", String.Join(", ", header.fADCDisplayOffset));
            txt += txtFormat("fInstrumentScaleFactor", String.Join(", ", header.fInstrumentScaleFactor));
            txt += txtFormat("fInstrumentOffset", String.Join(", ", header.fInstrumentOffset));
            txt += txtFormat("fSignalGain", String.Join(", ", header.fSignalGain));
            txt += txtFormat("fSignalOffset", String.Join(", ", header.fSignalOffset));
            txt += txtFormat("fSignalLowpassFilter", String.Join(", ", header.fSignalLowpassFilter));
            txt += txtFormat("fSignalHighpassFilter", String.Join(", ", header.fSignalHighpassFilter));
            txt += txtFormat("nLowpassFilterType", header.nLowpassFilterType.ToString());
            txt += txtFormat("nHighpassFilterType", header.nHighpassFilterType.ToString());
            txt += txtFormat("bHumFilterEnable", String.Join(", ", header.bHumFilterEnable));
            txt += txtFormat("sADCChannelName", String.Join(", ", header.sADCChannelName));
            txt += txtFormat("sADCUnits", String.Join(", ", header.sADCUnits));
            txt += txtFormat("fDACScaleFactor", String.Join(", ", header.fDACScaleFactor));
            txt += txtFormat("fDACHoldingLevel", String.Join(", ", header.fDACHoldingLevel));
            txt += txtFormat("fDACCalibrationFactor", String.Join(", ", header.fDACCalibrationFactor));
            txt += txtFormat("fDACCalibrationOffset", String.Join(", ", header.fDACCalibrationOffset));
            txt += txtFormat("sDACChannelName", String.Join(", ", header.sDACChannelName));
            txt += txtFormat("sDACChannelUnits", String.Join(", ", header.sDACChannelUnits));

            txt += txtFormat("", "GROUP #9 - Epoch Waveform and Pulses");
            txt += txtFormat("nDigitalEnable", header.nDigitalEnable.ToString()); txt += txtFormat("nActiveDACChannel", header.nActiveDACChannel.ToString());
            txt += txtFormat("nDigitalDACChannel", header.nDigitalDACChannel.ToString()); txt += txtFormat("nDigitalHolding", header.nDigitalHolding.ToString());
            txt += txtFormat("nDigitalInterEpisode", header.nDigitalInterEpisode.ToString()); txt += txtFormat("nDigitalTrainActiveLogic", header.nDigitalTrainActiveLogic.ToString());
            txt += txtFormat("nDigitalValue", String.Join(", ", header.nDigitalValue)); txt += txtFormat("nDigitalTrainValue", String.Join(", ", header.nDigitalTrainValue));
            txt += txtFormat("bEpochCompression", String.Join(", ", header.bEpochCompression)); txt += txtFormat("nWaveformEnable", String.Join(", ", header.nWaveformEnable));
            txt += txtFormat("nWaveformSource", String.Join(", ", header.nWaveformSource));
            txt += txtFormat("nInterEpisodeLevel", String.Join(", ", header.nInterEpisodeLevel));
            txt += txtFormat("nEpochType", String.Join(", ", header.nEpochType));
            txt += txtFormat("fEpochInitLevel", String.Join(", ", header.fEpochInitLevel));
            txt += txtFormat("fEpochFinalLevel", String.Join(", ", header.fEpochFinalLevel));
            txt += txtFormat("fEpochLevelInc", String.Join(", ", header.fEpochLevelInc));
            txt += txtFormat("lEpochInitDuration", String.Join(", ", header.lEpochInitDuration));
            txt += txtFormat("lEpochDurationInc", String.Join(", ", header.lEpochDurationInc));
            txt += txtFormat("nEpochTableRepetitions", String.Join(", ", header.nEpochTableRepetitions));
            txt += txtFormat("fEpochTableStartToStartInterval", String.Join(", ", header.fEpochTableStartToStartInterval));

            txt += txtFormat("", "GROUP #10 - DAC Output File");
            txt += txtFormat("fDACFileScale", String.Join(", ", header.fDACFileScale));
            txt += txtFormat("fDACFileOffset", String.Join(", ", header.fDACFileOffset));
            txt += txtFormat("lDACFileEpisodeNum", String.Join(", ", header.lDACFileEpisodeNum));
            txt += txtFormat("nDACFileADCNum", String.Join(", ", header.nDACFileADCNum));
            txt += txtFormat("sDACFilePath", String.Join(", ", header.sDACFilePath));

            txt += txtFormat("", "GROUP #11a - Presweep (conditioning) pulse train");
            txt += txtFormat("nConditEnable", String.Join(", ", header.nConditEnable));
            txt += txtFormat("lConditNumPulses", String.Join(", ", header.lConditNumPulses));
            txt += txtFormat("fBaselineDuration", String.Join(", ", header.fBaselineDuration));
            txt += txtFormat("fBaselineLevel", String.Join(", ", header.fBaselineLevel));
            txt += txtFormat("fStepDuration", String.Join(", ", header.fStepDuration));
            txt += txtFormat("fStepLevel", String.Join(", ", header.fStepLevel));
            txt += txtFormat("fPostTrainPeriod", String.Join(", ", header.fPostTrainPeriod));
            txt += txtFormat("fPostTrainLevel", String.Join(", ", header.fPostTrainLevel));
            txt += txtFormat("fCTStartLevel", String.Join(", ", header.fCTStartLevel));
            txt += txtFormat("fCTEndLevel", String.Join(", ", header.fCTEndLevel));
            txt += txtFormat("fCTIntervalDuration", String.Join(", ", header.fCTIntervalDuration));
            txt += txtFormat("fCTStartToStartInterval", String.Join(", ", header.fCTStartToStartInterval));

            txt += txtFormat("", "GROUP #11b - Membrane Test Between Sweeps");
            txt += txtFormat("nMembTestEnable", String.Join(", ", header.nMembTestEnable));
            txt += txtFormat("fMembTestPreSettlingTimeMS", String.Join(", ", header.fMembTestPreSettlingTimeMS));
            txt += txtFormat("fMembTestPostSettlingTimeMS", String.Join(", ", header.fMembTestPostSettlingTimeMS));

            txt += txtFormat("", "GROUP #11c - PreSignal test pulse");
            txt += txtFormat("nPreSignalEnable", String.Join(", ", header.nPreSignalEnable));
            txt += txtFormat("fPreSignalPreStepDuration", String.Join(", ", header.fPreSignalPreStepDuration));
            txt += txtFormat("fPreSignalPreStepLevel", String.Join(", ", header.fPreSignalPreStepLevel));
            txt += txtFormat("fPreSignalStepDuration", String.Join(", ", header.fPreSignalStepDuration));
            txt += txtFormat("fPreSignalStepLevel", String.Join(", ", header.fPreSignalStepLevel));
            txt += txtFormat("fPreSignalPostStepDuration", String.Join(", ", header.fPreSignalPostStepDuration));
            txt += txtFormat("fPreSignalPostStepLevel", String.Join(", ", header.fPreSignalPostStepLevel));

            txt += txtFormat("", "GROUP #11d - Hum Silncer Adapt between sweeps");
            txt += txtFormat("nAdaptEnable", header.nAdaptEnable.ToString());
            txt += txtFormat("fInterSweepAdaptTimeS", header.fInterSweepAdaptTimeS.ToString());

            txt += txtFormat("", "GROUP #12 - Variable parameter user list");
            txt += txtFormat("nULEnable", String.Join(", ", header.nULEnable));
            txt += txtFormat("nULParamToVary", String.Join(", ", header.nULParamToVary));
            txt += txtFormat("nULRepeat", String.Join(", ", header.nULRepeat));
            txt += txtFormat("sULParamValueList", String.Join(", ", header.sULParamValueList));

            txt += txtFormat("", "GROUP #13 - Statistics measurements");
            txt += txtFormat("nStatsEnable", header.nStatsEnable.ToString());
            txt += txtFormat("nStatsActiveChannels", header.nStatsActiveChannels.ToString());
            txt += txtFormat("nStatsSearchRegionFlags", header.nStatsSearchRegionFlags.ToString());
            txt += txtFormat("nStatsSmoothing", header.nStatsSmoothing.ToString());
            txt += txtFormat("nStatsSmoothingEnable", header.nStatsSmoothingEnable.ToString());
            txt += txtFormat("nStatsBaseline", header.nStatsBaseline.ToString());
            txt += txtFormat("nStatsBaselineDAC", header.nStatsBaselineDAC.ToString());
            txt += txtFormat("lStatsBaselineStart", header.lStatsBaselineStart.ToString());
            txt += txtFormat("lStatsBaselineEnd", header.lStatsBaselineEnd.ToString());
            txt += txtFormat("lStatsMeasurements", String.Join(", ", header.lStatsMeasurements));
            txt += txtFormat("lStatsStart", String.Join(", ", header.lStatsStart));
            txt += txtFormat("lStatsEnd", String.Join(", ", header.lStatsEnd));
            txt += txtFormat("nRiseBottomPercentile", String.Join(", ", header.nRiseBottomPercentile));
            txt += txtFormat("nRiseTopPercentile", String.Join(", ", header.nRiseTopPercentile));
            txt += txtFormat("nDecayBottomPercentile", String.Join(", ", header.nDecayBottomPercentile));
            txt += txtFormat("nDecayTopPercentile", String.Join(", ", header.nDecayTopPercentile));
            txt += txtFormat("nStatsChannelPolarity", String.Join(", ", header.nStatsChannelPolarity));
            txt += txtFormat("nStatsSearchMode", String.Join(", ", header.nStatsSearchMode));
            txt += txtFormat("nStatsSearchDAC", String.Join(", ", header.nStatsSearchDAC));

            txt += txtFormat("", "GROUP #14 - Channel Arithmetic");
            txt += txtFormat("nArithmeticEnable", header.nArithmeticEnable.ToString());
            txt += txtFormat("nArithmeticExpression", header.nArithmeticExpression.ToString());
            txt += txtFormat("fArithmeticUpperLimit", header.fArithmeticUpperLimit.ToString());
            txt += txtFormat("fArithmeticLowerLimit", header.fArithmeticLowerLimit.ToString());
            txt += txtFormat("nArithmeticADCNumA", header.nArithmeticADCNumA.ToString());
            txt += txtFormat("nArithmeticADCNumB", header.nArithmeticADCNumB.ToString());
            txt += txtFormat("fArithmeticK1", header.fArithmeticK1.ToString());
            txt += txtFormat("fArithmeticK2", header.fArithmeticK2.ToString());
            txt += txtFormat("fArithmeticK3", header.fArithmeticK3.ToString());
            txt += txtFormat("fArithmeticK4", header.fArithmeticK4.ToString());
            txt += txtFormat("fArithmeticK5", header.fArithmeticK5.ToString());
            txt += txtFormat("fArithmeticK6", header.fArithmeticK6.ToString());
            txt += txtFormat("sArithmeticOperator", header.sArithmeticOperator.ToString());
            txt += txtFormat("sArithmeticUnits", header.sArithmeticUnits.ToString());

            txt += txtFormat("", "GROUP #15 - Leak subtraction");
            txt += txtFormat("nPNPosition", header.nPNPosition.ToString());
            txt += txtFormat("nPNNumPulses", header.nPNNumPulses.ToString());
            txt += txtFormat("nPNPolarity", header.nPNPolarity.ToString());
            txt += txtFormat("fPNSettlingTime", header.fPNSettlingTime.ToString());
            txt += txtFormat("fPNInterpulse", header.fPNInterpulse.ToString());
            txt += txtFormat("nLeakSubtractType", String.Join(", ", header.nLeakSubtractType));
            txt += txtFormat("fPNHoldingLevel", String.Join(", ", header.fPNHoldingLevel));
            txt += txtFormat("nLeakSubtractADCIndex", String.Join(", ", header.nLeakSubtractADCIndex));

            txt += txtFormat("", "GROUP #16 - Miscellaneous variables");
            txt += txtFormat("nLevelHysteresis", header.nLevelHysteresis.ToString());
            txt += txtFormat("lTimeHysteresis", header.lTimeHysteresis.ToString());
            txt += txtFormat("nAllowExternalTags", header.nAllowExternalTags.ToString());
            txt += txtFormat("nAverageAlgorithm", header.nAverageAlgorithm.ToString());
            txt += txtFormat("fAverageWeighting", header.fAverageWeighting.ToString());
            txt += txtFormat("nUndoPromptStrategy", header.nUndoPromptStrategy.ToString());
            txt += txtFormat("nTrialTriggerSource", header.nTrialTriggerSource.ToString());
            txt += txtFormat("nStatisticsDisplayStrategy", header.nStatisticsDisplayStrategy.ToString());
            txt += txtFormat("nExternalTagType", header.nExternalTagType.ToString());
            txt += txtFormat("lHeaderSize", header.lHeaderSize.ToString());
            txt += txtFormat("nStatisticsClearStrategy", header.nStatisticsClearStrategy.ToString());
            txt += txtFormat("nEnableFirstLastHolding", header.nEnableFirstLastHolding.ToString());

            txt += txtFormat("", "GROUP #17 - Trains parameters");
            txt += txtFormat("lEpochPulsePeriod", String.Join(", ", header.lEpochPulsePeriod));
            txt += txtFormat("lEpochPulseWidth", String.Join(", ", header.lEpochPulseWidth));

            txt += txtFormat("", "GROUP #18 - Application version data");
            txt += txtFormat("nCreatorMajorVersion", header.nCreatorMajorVersion.ToString());
            txt += txtFormat("nCreatorMinorVersion", header.nCreatorMinorVersion.ToString());
            txt += txtFormat("nCreatorBugfixVersion", header.nCreatorBugfixVersion.ToString());
            txt += txtFormat("nCreatorBuildVersion", header.nCreatorBuildVersion.ToString());
            txt += txtFormat("nModifierMajorVersion", header.nModifierMajorVersion.ToString());
            txt += txtFormat("nModifierMinorVersion", header.nModifierMinorVersion.ToString());
            txt += txtFormat("nModifierBugfixVersion", header.nModifierBugfixVersion.ToString());
            txt += txtFormat("nModifierBuildVersion", header.nModifierBuildVersion.ToString());

            txt += txtFormat("", "GROUP #19 - LTP protocol");
            txt += txtFormat("nLTPType", header.nLTPType.ToString());
            txt += txtFormat("nLTPUsageOfDAC", String.Join(", ", header.nLTPUsageOfDAC));
            txt += txtFormat("nLTPPresynapticPulses", String.Join(", ", header.nLTPPresynapticPulses));

            txt += txtFormat("", "GROUP #20 - Digidata 132x Trigger out flag");
            txt += txtFormat("nScopeTriggerOut", header.nScopeTriggerOut.ToString());

            txt += txtFormat("", "GROUP #21 - Epoch resistance");
            txt += txtFormat("sEpochResistanceSignalName", String.Join(", ", header.sEpochResistanceSignalName));
            txt += txtFormat("nEpochResistanceState", String.Join(", ", header.nEpochResistanceState));

            txt += txtFormat("", "GROUP #22 - Alternating episodic mode");
            txt += txtFormat("nAlternateDACOutputState", header.nAlternateDACOutputState.ToString());
            txt += txtFormat("nAlternateDigitalOutputState", header.nAlternateDigitalOutputState.ToString());
            txt += txtFormat("nAlternateDigitalValue", String.Join(", ", header.nAlternateDigitalValue));
            txt += txtFormat("nAlternateDigitalTrainValue", String.Join(", ", header.nAlternateDigitalTrainValue));

            txt += txtFormat("", "GROUP #23 - Post-processing actions");
            txt += txtFormat("fPostProcessLowpassFilter", String.Join(", ", header.fPostProcessLowpassFilter));
            txt += txtFormat("nPostProcessLowpassFilterType", header.nPostProcessLowpassFilterType.ToString());

            txt += txtFormat("", "GROUP #24 - Legacy gear shift info");
            txt += txtFormat("fLegacyADCSequenceInterval", header.fLegacyADCSequenceInterval.ToString());
            txt += txtFormat("fLegacyADCSecondSequenceInterval", header.fLegacyADCSecondSequenceInterval.ToString());
            txt += txtFormat("lLegacyClockChange", header.lLegacyClockChange.ToString());
            txt += txtFormat("lLegacyNumSamplesPerEpisode", header.lLegacyNumSamplesPerEpisode.ToString());

            txt += txtFormat("", "GROUP #25 - Gap-Free Config");
            txt += txtFormat("nGapFreeEpochType", String.Join(", ", header.nGapFreeEpochType));
            txt += txtFormat("fGapFreeEpochLevel", String.Join(", ", header.fGapFreeEpochLevel));
            txt += txtFormat("lGapFreeEpochDuration", String.Join(", ", header.lGapFreeEpochDuration));
            txt += txtFormat("nGapFreeDigitalValue", String.Join(", ", header.nGapFreeDigitalValue));
            txt += txtFormat("nGapFreeEpochStart", header.nGapFreeEpochStart.ToString());

            return txt.Trim();
        }
    }
}
