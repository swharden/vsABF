using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AxonDLL
{
    public class AbfStructDisplay
    {

        private static string txtValue(string variable, string value)
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

        public static unsafe string txtArrayString(string variable, string value, int stringLength)
        {
            int stringCount = value.Length / stringLength;
            string s = "";
            for (int i = 0; i < stringCount; i++)
            {
                string sub = value.Substring(i * stringLength, stringLength).Trim();
                if (sub == "") sub = "null";
                s += sub + ", ";
            }
            s = s.Trim().Trim(',');
            return txtValue(variable, "[" + s.Trim() + "]");
        }

        public static unsafe string txtArrayByte(string variable, Byte* address, int count)
        {
            string s = "";
            for (int i = 0; i < count; i++)
            {
                Byte value = *(address + i);
                s += value.ToString() + ", ";
            }
            s = s.Trim().Trim(',');
            return txtValue(variable, "[" + s.Trim() + "]");
        }

        public static unsafe string txtArraySingle(string variable, Single* address, int count)
        {
            string s = "";
            for (int i = 0; i < count; i++)
            {
                Single value = *(address + i);
                s += value.ToString() + ", ";
            }
            s = s.Trim().Trim(',');
            return txtValue(variable, "[" + s.Trim() + "]");
        }

        public static unsafe string txtArrayInt16(string variable, Int16* address, int count)
        {
            string s = "";
            for (int i = 0; i < count; i++)
            {
                int value = *(address + i);
                s += value.ToString() + ", ";
            }
            s = s.Trim().Trim(',');
            return txtValue(variable, "[" + s.Trim() + "]");
        }

        public static unsafe string txtArrayInt32(string variable, Int32* address, int count)
        {
            string s = "";
            for (int i = 0; i < count; i++)
            {
                int value = *(address + i);
                s += value.ToString() + ", ";
            }
            s = s.Trim().Trim(',');
            return txtValue(variable, "[" + s.Trim() + "]");
        }

        // display header just like ABFINFO program does
        public static unsafe string headerToString(AbfStructs.ABFFileHeader header)
        {
            string txt = "";

            txt += txtValue("", "GROUP #1 - File ID and size information");
            txt += txtValue("fFileVersionNumber ", header.fFileVersionNumber.ToString());
            txt += txtValue("nOperationMode", header.nOperationMode.ToString());
            txt += txtValue("lActualAcqLength ", header.lActualAcqLength.ToString());
            txt += txtValue("nNumPointsIgnored", header.nNumPointsIgnored.ToString());
            txt += txtValue("lActualEpisodes", header.lActualEpisodes.ToString());
            txt += txtValue("uFileStartDate", header.uFileStartDate.ToString());
            txt += txtValue("uFileStartTimeMS", header.uFileStartTimeMS.ToString());
            txt += txtValue("lStopwatchTime", header.lStopwatchTime.ToString());
            txt += txtValue("fHeaderVersionNumber", header.fHeaderVersionNumber.ToString());
            txt += txtValue("nFileType", header.nFileType.ToString());

            txt += txtValue("", "GROUP #2 - File Structure");
            txt += txtValue("lDataSectionPtr", header.lDataSectionPtr.ToString());
            txt += txtValue("lTagSectionPtr", header.lTagSectionPtr.ToString());
            txt += txtValue("lNumTagEntries", header.lNumTagEntries.ToString());
            txt += txtValue("lScopeConfigPtr", header.lScopeConfigPtr.ToString());
            txt += txtValue("lNumScopes", header.lNumScopes.ToString());
            txt += txtValue("lDeltaArrayPtr", header.lDeltaArrayPtr.ToString());
            txt += txtValue("lNumDeltas", header.lNumDeltas.ToString());
            txt += txtValue("lVoiceTagPtr", header.lVoiceTagPtr.ToString());
            txt += txtValue("lVoiceTagEntries", header.lVoiceTagEntries.ToString());
            txt += txtValue("lSynchArrayPtr", header.lSynchArrayPtr.ToString());
            txt += txtValue("lSynchArraySize", header.lSynchArraySize.ToString());
            txt += txtValue("nDataFormat", header.nDataFormat.ToString());
            txt += txtValue("nSimultaneousScan", header.nSimultaneousScan.ToString());
            txt += txtValue("lStatisticsConfigPtr", header.lStatisticsConfigPtr.ToString());
            txt += txtValue("lAnnotationSectionPtr", header.lAnnotationSectionPtr.ToString());
            txt += txtValue("lNumAnnotations", header.lNumAnnotations.ToString());
            txt += txtArrayInt32("lDACFilePtr", header.lDACFilePtr, 8);
            txt += txtArrayInt32("lDACFileNumEpisodes", header.lDACFileNumEpisodes, 8);

            txt += txtValue("", "GROUP #3 - Trial hierarchy information");
            txt += txtValue("nADCNumChannels", header.nADCNumChannels.ToString());
            txt += txtValue("fADCSequenceInterval", header.fADCSequenceInterval.ToString());
            txt += txtValue("uFileCompressionRatio", header.uFileCompressionRatio.ToString());
            txt += txtValue("bEnableFileCompression", header.bEnableFileCompression.ToString());
            txt += txtValue("fSynchTimeUnit", header.fSynchTimeUnit.ToString());
            txt += txtValue("fSecondsPerRun", header.fSecondsPerRun.ToString());
            txt += txtValue("lNumSamplesPerEpisode", header.lNumSamplesPerEpisode.ToString());
            txt += txtValue("lPreTriggerSamples", header.lPreTriggerSamples.ToString());
            txt += txtValue("lEpisodesPerRun", header.lEpisodesPerRun.ToString());
            txt += txtValue("lRunsPerTrial", header.lRunsPerTrial.ToString());
            txt += txtValue("lNumberOfTrials", header.lNumberOfTrials.ToString());
            txt += txtValue("nAveragingMode", header.nAveragingMode.ToString());
            txt += txtValue("nUndoRunCount", header.nUndoRunCount.ToString());
            txt += txtValue("nFirstEpisodeInRun", header.nFirstEpisodeInRun.ToString());
            txt += txtValue("fTriggerThreshold", header.fTriggerThreshold.ToString());
            txt += txtValue("nTriggerSource", header.nTriggerSource.ToString());
            txt += txtValue("nTriggerAction", header.nTriggerAction.ToString());
            txt += txtValue("nTriggerPolarity", header.nTriggerPolarity.ToString());
            txt += txtValue("fScopeOutputInterval", header.fScopeOutputInterval.ToString());
            txt += txtValue("fEpisodeStartToStart", header.fEpisodeStartToStart.ToString());
            txt += txtValue("fRunStartToStart", header.fRunStartToStart.ToString());
            txt += txtValue("fTrialStartToStart", header.fTrialStartToStart.ToString());
            txt += txtValue("lAverageCount", header.lAverageCount.ToString());
            txt += txtValue("nAutoTriggerStrategy", header.nAutoTriggerStrategy.ToString());
            txt += txtValue("fFirstRunDelayS", header.fFirstRunDelayS.ToString());
            txt += txtValue("nTriggerTimeout", header.nTriggerTimeout.ToString());

            txt += txtValue("", "GROUP #4 - Display Parameters");
            txt += txtValue("nDataDisplayMode", header.nDataDisplayMode.ToString());
            txt += txtValue("nChannelStatsStrategy", header.nChannelStatsStrategy.ToString());
            txt += txtValue("lSamplesPerTrace", header.lSamplesPerTrace.ToString());
            txt += txtValue("lStartDisplayNum", header.lStartDisplayNum.ToString());
            txt += txtValue("lFinishDisplayNum", header.lFinishDisplayNum.ToString());
            txt += txtValue("nShowPNRawData", header.nShowPNRawData.ToString());
            txt += txtValue("fStatisticsPeriod", header.fStatisticsPeriod.ToString());
            txt += txtValue("lStatisticsMeasurements", header.lStatisticsMeasurements.ToString());
            txt += txtValue("nStatisticsSaveStrategy", header.nStatisticsSaveStrategy.ToString());

            txt += txtValue("", "GROUP #5 - Hardware information");
            txt += txtValue("fADCRange", header.fADCRange.ToString());
            txt += txtValue("fDACRange", header.fDACRange.ToString());
            txt += txtValue("lADCResolution", header.lADCResolution.ToString());
            txt += txtValue("lDACResolution", header.lDACResolution.ToString());
            txt += txtValue("nDigitizerADCs", header.nDigitizerADCs.ToString());
            txt += txtValue("nDigitizerDACs", header.nDigitizerDACs.ToString());
            txt += txtValue("nDigitizerTotalDigitalOuts", header.nDigitizerTotalDigitalOuts.ToString());
            txt += txtValue("nDigitizerSynchDigitalOuts", header.nDigitizerSynchDigitalOuts.ToString());
            txt += txtValue("nDigitizerType", header.nDigitizerType.ToString());

            txt += txtValue("", "GROUP #6 Environmental Information");
            txt += txtValue("nExperimentType", header.nExperimentType.ToString());
            txt += txtValue("nManualInfoStrategy", header.nManualInfoStrategy.ToString());
            txt += txtValue("fCellID1", header.fCellID1.ToString());
            txt += txtValue("fCellID2", header.fCellID2.ToString());
            txt += txtValue("fCellID3", header.fCellID3.ToString());
            txt += txtValue("sProtocolPath", header.sProtocolPath.ToString());
            txt += txtValue("sCreatorInfo", header.sCreatorInfo.ToString());
            txt += txtValue("sModifierInfo", header.sModifierInfo.ToString());
            txt += txtValue("nCommentsEnable", header.nCommentsEnable.ToString());
            txt += txtValue("sFileComment", header.sFileComment.ToString());
            txt += txtArrayInt16("nTelegraphEnable", header.nTelegraphEnable, 16);
            txt += txtArrayInt16("nTelegraphInstrument", header.nTelegraphInstrument, 16);
            txt += txtArraySingle("fTelegraphAdditGain", header.fTelegraphAdditGain, 16);
            txt += txtArraySingle("fTelegraphFilter", header.fTelegraphFilter, 16);
            txt += txtArraySingle("fTelegraphMembraneCap", header.fTelegraphMembraneCap, 16);
            txt += txtArraySingle("fTelegraphAccessResistance", header.fTelegraphAccessResistance, 16);
            txt += txtArrayInt16("nTelegraphMode", header.nTelegraphMode, 16);
            txt += txtArrayInt16("nTelegraphDACScaleFactorEnable", header.nTelegraphDACScaleFactorEnable, 8);
            txt += txtValue("nAutoAnalyseEnable", header.nAutoAnalyseEnable.ToString());
            txt += txtValue("FileGUID", header.FileGUID.ToString());
            txt += txtArraySingle("fInstrumentHoldingLevel", header.fInstrumentHoldingLevel, 8);
            txt += txtValue("ulFileCRC", header.ulFileCRC.ToString());
            txt += txtValue("nCRCEnable", header.nCRCEnable.ToString());

            txt += txtValue("", "GROUP #7 Multi-channel information");
            txt += txtValue("nSignalType", header.nSignalType.ToString());
            txt += txtArrayInt16("nADCPtoLChannelMap", header.nADCPtoLChannelMap, 16);
            txt += txtArrayInt16("nADCSamplingSeq", header.nADCSamplingSeq, 16);
            txt += txtArraySingle("fADCProgrammableGain", header.fADCProgrammableGain, 16);
            txt += txtArraySingle("fADCDisplayAmplification", header.fADCDisplayAmplification, 16);
            txt += txtArraySingle("fADCDisplayOffset", header.fADCDisplayOffset, 16);
            txt += txtArraySingle("fInstrumentScaleFactor", header.fInstrumentScaleFactor, 16);
            txt += txtArraySingle("fInstrumentOffset", header.fInstrumentOffset, 16);
            txt += txtArraySingle("fSignalGain", header.fSignalGain, 16);
            txt += txtArraySingle("fSignalOffset", header.fSignalOffset, 16);
            txt += txtArraySingle("fSignalLowpassFilter", header.fSignalLowpassFilter, 16);
            txt += txtArraySingle("fSignalHighpassFilter", header.fSignalHighpassFilter, 16);
            txt += txtValue("nLowpassFilterType", header.nLowpassFilterType.ToString());
            txt += txtValue("nHighpassFilterType", header.nHighpassFilterType.ToString());
            txt += txtArrayByte("bHumFilterEnable", header.bHumFilterEnable, 16);
            txt += txtArrayString("sADCChannelName", header.sADCChannelName, 10);
            txt += txtArrayString("sADCUnits", header.sADCUnits, 8);
            txt += txtArraySingle("fDACScaleFactor", header.fDACScaleFactor, 8);
            txt += txtArraySingle("fDACHoldingLevel", header.fDACHoldingLevel, 8);
            txt += txtArraySingle("fDACCalibrationFactor", header.fDACCalibrationFactor, 8);
            txt += txtArraySingle("fDACCalibrationOffset", header.fDACCalibrationOffset, 8);
            txt += txtArrayString("sDACChannelName", header.sDACChannelName, 10);
            txt += txtArrayString("sDACChannelUnits", header.sDACChannelUnits, 8);

            txt += txtValue("", "GROUP #9 - Epoch Waveform and Pulses");
            txt += txtValue("nDigitalEnable", header.nDigitalEnable.ToString());
            txt += txtValue("nActiveDACChannel", header.nActiveDACChannel.ToString());
            txt += txtValue("nDigitalDACChannel", header.nDigitalDACChannel.ToString());
            txt += txtValue("nDigitalHolding", header.nDigitalHolding.ToString());
            txt += txtValue("nDigitalInterEpisode", header.nDigitalInterEpisode.ToString());
            txt += txtValue("nDigitalTrainActiveLogic", header.nDigitalTrainActiveLogic.ToString());
            txt += txtArrayInt16("nDigitalValue", header.nDigitalValue, 50);
            txt += txtArrayInt16("nDigitalTrainValue", header.nDigitalTrainValue, 50);
            txt += txtArrayByte("bEpochCompression", header.bEpochCompression, 50);
            txt += txtArrayInt16("nWaveformEnable", header.nWaveformEnable, 8);
            txt += txtArrayInt16("nWaveformSource", header.nWaveformSource, 8);
            txt += txtArrayInt16("nInterEpisodeLevel", header.nInterEpisodeLevel, 8 * 50);
            txt += txtArrayInt16("nEpochType", header.nEpochType, 8 * 50);
            txt += txtArraySingle("fEpochInitLevel", header.fEpochInitLevel, 8 * 50);
            txt += txtArraySingle("fEpochFinalLevel", header.fEpochFinalLevel, 8 * 50);
            txt += txtArraySingle("fEpochLevelInc", header.fEpochLevelInc, 8 * 50);
            txt += txtArrayInt32("lEpochInitDuration", header.lEpochInitDuration, 8 * 50);
            txt += txtArrayInt32("lEpochDurationInc", header.lEpochDurationInc, 8 * 50);
            txt += txtArrayInt16("nEpochTableRepetitions", header.nEpochTableRepetitions, 8);
            txt += txtArraySingle("fEpochTableStartToStartInterval", header.fEpochTableStartToStartInterval, 8);

            txt += txtValue("", "GROUP #10 - DAC Output File");
            txt += txtArraySingle("fDACFileScale", header.fDACFileScale, 8);
            txt += txtArraySingle("fDACFileOffset", header.fDACFileOffset, 8);
            txt += txtArrayInt32("lDACFileEpisodeNum", header.lDACFileEpisodeNum, 8);
            txt += txtArrayInt16("nDACFileADCNum", header.nDACFileADCNum, 8);
            txt += txtArrayString("sDACFilePath", header.sDACFilePath, 256);

            txt += txtValue("", "GROUP #11a - Presweep (conditioning) pulse train");
            txt += txtArrayInt16("nConditEnable", header.nConditEnable, 8);
            txt += txtArrayInt32("lConditNumPulses", header.lConditNumPulses, 8);
            txt += txtArraySingle("fBaselineDuration", header.fBaselineDuration, 8);
            txt += txtArraySingle("fBaselineLevel", header.fBaselineLevel, 8);
            txt += txtArraySingle("fStepDuration", header.fStepDuration, 8);
            txt += txtArraySingle("fStepLevel", header.fStepLevel, 8);
            txt += txtArraySingle("fPostTrainPeriod", header.fPostTrainPeriod, 8);
            txt += txtArraySingle("fPostTrainLevel", header.fPostTrainLevel, 8);
            txt += txtArraySingle("fCTStartLevel", header.fCTStartLevel, 8 * 50);
            txt += txtArraySingle("fCTEndLevel", header.fCTEndLevel, 8 * 50);
            txt += txtArraySingle("fCTIntervalDuration", header.fCTIntervalDuration, 8 * 50);
            txt += txtArraySingle("fCTStartToStartInterval", header.fCTStartToStartInterval, 8);

            txt += txtValue("", "GROUP #11b - Membrane Test Between Sweeps");
            txt += txtArrayInt16("nMembTestEnable", header.nMembTestEnable, 8);
            txt += txtArraySingle("fMembTestPreSettlingTimeMS", header.fMembTestPreSettlingTimeMS, 8);
            txt += txtArraySingle("fMembTestPostSettlingTimeMS", header.fMembTestPostSettlingTimeMS, 8);

            txt += txtValue("", "GROUP #11c - PreSignal test pulse");
            txt += txtArrayInt16("nPreSignalEnable", header.nPreSignalEnable, 8);
            txt += txtArraySingle("fPreSignalPreStepDuration", header.fPreSignalPreStepDuration, 8);
            txt += txtArraySingle("fPreSignalPreStepLevel", header.fPreSignalPreStepLevel, 8);
            txt += txtArraySingle("fPreSignalStepDuration", header.fPreSignalStepDuration, 8);
            txt += txtArraySingle("fPreSignalStepLevel", header.fPreSignalStepLevel, 8);
            txt += txtArraySingle("fPreSignalPostStepDuration", header.fPreSignalPostStepDuration, 8);
            txt += txtArraySingle("fPreSignalPostStepLevel", header.fPreSignalPostStepLevel, 8);

            txt += txtValue("", "GROUP #11d - Hum Silncer Adapt between sweeps");
            txt += txtValue("nAdaptEnable", header.nAdaptEnable.ToString());
            txt += txtValue("fInterSweepAdaptTimeS", header.fInterSweepAdaptTimeS.ToString());

            txt += txtValue("", "GROUP #12 - Variable parameter user list");
            txt += txtArrayInt16("nULEnable", header.nULEnable, 4);
            txt += txtArrayInt16("nULParamToVary", header.nULParamToVary, 4);
            txt += txtArrayInt16("nULRepeat", header.nULRepeat, 4);
            txt += txtArrayString("sULParamValueList", header.sULParamValueList, 256);

            txt += txtValue("", "GROUP #13 - Statistics measurements");
            txt += txtValue("nStatsEnable", header.nStatsEnable.ToString());
            txt += txtValue("nStatsActiveChannels", header.nStatsActiveChannels.ToString());
            txt += txtValue("nStatsSearchRegionFlags", header.nStatsSearchRegionFlags.ToString());
            txt += txtValue("nStatsSmoothing", header.nStatsSmoothing.ToString());
            txt += txtValue("nStatsSmoothingEnable", header.nStatsSmoothingEnable.ToString());
            txt += txtValue("nStatsBaseline", header.nStatsBaseline.ToString());
            txt += txtValue("nStatsBaselineDAC", header.nStatsBaselineDAC.ToString());
            txt += txtValue("lStatsBaselineStart", header.lStatsBaselineStart.ToString());
            txt += txtValue("lStatsBaselineEnd", header.lStatsBaselineEnd.ToString());
            txt += txtArrayInt32("lStatsMeasurements", header.lStatsMeasurements, 8);
            txt += txtArrayInt32("lStatsStart", header.lStatsStart, 8);
            txt += txtArrayInt32("lStatsEnd", header.lStatsEnd, 8);
            txt += txtArrayInt16("nRiseBottomPercentile", header.nRiseBottomPercentile, 8);
            txt += txtArrayInt16("nRiseTopPercentile", header.nRiseTopPercentile, 8);
            txt += txtArrayInt16("nDecayBottomPercentile", header.nDecayBottomPercentile, 8);
            txt += txtArrayInt16("nDecayTopPercentile", header.nDecayTopPercentile, 8);
            txt += txtArrayInt16("nStatsChannelPolarity", header.nStatsChannelPolarity, 16);
            txt += txtArrayInt16("nStatsSearchMode", header.nStatsSearchMode, 8);
            txt += txtArrayInt16("nStatsSearchDAC", header.nStatsSearchDAC, 8);

            txt += txtValue("", "GROUP #14 - Channel Arithmetic");
            txt += txtValue("nArithmeticEnable", header.nArithmeticEnable.ToString());
            txt += txtValue("nArithmeticExpression", header.nArithmeticExpression.ToString());
            txt += txtValue("fArithmeticUpperLimit", header.fArithmeticUpperLimit.ToString());
            txt += txtValue("fArithmeticLowerLimit", header.fArithmeticLowerLimit.ToString());
            txt += txtValue("nArithmeticADCNumA", header.nArithmeticADCNumA.ToString());
            txt += txtValue("nArithmeticADCNumB", header.nArithmeticADCNumB.ToString());
            txt += txtValue("fArithmeticK1", header.fArithmeticK1.ToString());
            txt += txtValue("fArithmeticK2", header.fArithmeticK2.ToString());
            txt += txtValue("fArithmeticK3", header.fArithmeticK3.ToString());
            txt += txtValue("fArithmeticK4", header.fArithmeticK4.ToString());
            txt += txtValue("fArithmeticK5", header.fArithmeticK5.ToString());
            txt += txtValue("fArithmeticK6", header.fArithmeticK6.ToString());
            txt += txtValue("sArithmeticOperator", header.sArithmeticOperator.ToString());
            txt += txtValue("sArithmeticUnits", header.sArithmeticUnits.ToString());

            txt += txtValue("", "GROUP #15 - Leak subtraction");
            txt += txtValue("nPNPosition", header.nPNPosition.ToString());
            txt += txtValue("nPNNumPulses", header.nPNNumPulses.ToString());
            txt += txtValue("nPNPolarity", header.nPNPolarity.ToString());
            txt += txtValue("fPNSettlingTime", header.fPNSettlingTime.ToString());
            txt += txtValue("fPNInterpulse", header.fPNInterpulse.ToString());
            txt += txtArrayInt16("nLeakSubtractType", header.nLeakSubtractType, 8);
            txt += txtArraySingle("fPNHoldingLevel", header.fPNHoldingLevel, 8);
            txt += txtArrayInt16("nLeakSubtractADCIndex", header.nLeakSubtractADCIndex, 8);

            txt += txtValue("", "GROUP #16 - Miscellaneous variables");
            txt += txtValue("nLevelHysteresis", header.nLevelHysteresis.ToString());
            txt += txtValue("lTimeHysteresis", header.lTimeHysteresis.ToString());
            txt += txtValue("nAllowExternalTags", header.nAllowExternalTags.ToString());
            txt += txtValue("nAverageAlgorithm", header.nAverageAlgorithm.ToString());
            txt += txtValue("fAverageWeighting", header.fAverageWeighting.ToString());
            txt += txtValue("nUndoPromptStrategy", header.nUndoPromptStrategy.ToString());
            txt += txtValue("nTrialTriggerSource", header.nTrialTriggerSource.ToString());
            txt += txtValue("nStatisticsDisplayStrategy", header.nStatisticsDisplayStrategy.ToString());
            txt += txtValue("nExternalTagType", header.nExternalTagType.ToString());
            txt += txtValue("lHeaderSize", header.lHeaderSize.ToString());
            txt += txtValue("nStatisticsClearStrategy", header.nStatisticsClearStrategy.ToString());
            txt += txtValue("nEnableFirstLastHolding", header.nEnableFirstLastHolding.ToString());

            txt += txtValue("", "GROUP #17 - Trains parameters");
            txt += txtArrayInt32("lEpochPulsePeriod", header.lEpochPulsePeriod, 50);
            txt += txtArrayInt32("lEpochPulseWidth", header.lEpochPulseWidth, 50);

            txt += txtValue("", "GROUP #18 - Application version data");
            txt += txtValue("nCreatorMajorVersion", header.nCreatorMajorVersion.ToString());
            txt += txtValue("nCreatorMinorVersion", header.nCreatorMinorVersion.ToString());
            txt += txtValue("nCreatorBugfixVersion", header.nCreatorBugfixVersion.ToString());
            txt += txtValue("nCreatorBuildVersion", header.nCreatorBuildVersion.ToString());
            txt += txtValue("nModifierMajorVersion", header.nModifierMajorVersion.ToString());
            txt += txtValue("nModifierMinorVersion", header.nModifierMinorVersion.ToString());
            txt += txtValue("nModifierBugfixVersion", header.nModifierBugfixVersion.ToString());
            txt += txtValue("nModifierBuildVersion", header.nModifierBuildVersion.ToString());

            txt += txtValue("", "GROUP #19 - LTP protocol");
            txt += txtValue("nLTPType", header.nLTPType.ToString());
            txt += txtArrayInt16("nLTPUsageOfDAC", header.nLTPUsageOfDAC, 8);
            txt += txtArrayInt16("nLTPPresynapticPulses", header.nLTPPresynapticPulses, 8);

            txt += txtValue("", "GROUP #20 - Digidata 132x Trigger out flag");
            txt += txtValue("nScopeTriggerOut", header.nScopeTriggerOut.ToString());

            txt += txtValue("", "GROUP #21 - Epoch resistance");
            txt += txtArrayString("sEpochResistanceSignalName", header.sEpochResistanceSignalName, 10);
            txt += txtArrayInt16("nEpochResistanceState", header.nEpochResistanceState, 8);

            txt += txtValue("", "GROUP #22 - Alternating episodic mode");
            txt += txtValue("nAlternateDACOutputState", header.nAlternateDACOutputState.ToString());
            txt += txtValue("nAlternateDigitalOutputState", header.nAlternateDigitalOutputState.ToString());
            txt += txtArrayInt16("nAlternateDigitalValue", header.nAlternateDigitalValue, 50);
            txt += txtArrayInt16("nAlternateDigitalTrainValue", header.nAlternateDigitalTrainValue, 50);

            txt += txtValue("", "GROUP #23 - Post-processing actions");
            txt += txtArraySingle("fPostProcessLowpassFilter", header.fPostProcessLowpassFilter, 16);
            txt += txtValue("nPostProcessLowpassFilterType", header.nPostProcessLowpassFilterType.ToString());

            txt += txtValue("", "GROUP #24 - Legacy gear shift info");
            txt += txtValue("fLegacyADCSequenceInterval", header.fLegacyADCSequenceInterval.ToString());
            txt += txtValue("fLegacyADCSecondSequenceInterval", header.fLegacyADCSecondSequenceInterval.ToString());
            txt += txtValue("lLegacyClockChange", header.lLegacyClockChange.ToString());
            txt += txtValue("lLegacyNumSamplesPerEpisode", header.lLegacyNumSamplesPerEpisode.ToString());

            txt += txtValue("", "GROUP #25 - Gap-Free Config");
            txt += txtArrayInt16("nGapFreeEpochType", header.nGapFreeEpochType, 8 * 50);
            txt += txtArraySingle("fGapFreeEpochLevel", header.fGapFreeEpochLevel, 8 * 50);
            txt += txtArrayInt32("lGapFreeEpochDuration", header.lGapFreeEpochDuration, 8 * 50);
            txt += txtArrayByte("nGapFreeDigitalValue", header.nGapFreeDigitalValue, 8 * 50);
            txt += txtValue("nGapFreeEpochStart", header.nGapFreeEpochStart.ToString());

            return txt.Trim();
        }
    }
}
