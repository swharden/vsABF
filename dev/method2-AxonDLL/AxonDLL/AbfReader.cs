using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace AxonDLL
{
    public class AbfReader
    {
        [DllImport("ABFFIO.dll", CharSet = CharSet.Ansi)]
        private static extern bool ABF_ReadOpen(
            String szFileName, 
            ref int phFile, 
            uint fFlags, 
            ref AbfStructs.ABFFileHeader pFH,
            ref uint puMaxSamples, 
            ref uint pdwMaxEpi, 
            ref int pnError);

        public string logString = "";
        private void log(string message)
        {
            Console.WriteLine($"[AbfReader] {message}");
            logString += message + "\n";
        }

        private void logHeader(string variable, string value)
        {
            if (variable == "")
            {
                log($"\n### {value} ###");
            }
            else
            {
                string line = $"{variable} = {value}";
                log(line);
            }
        }

        public unsafe void logHeaderArrayString(string variable, string value, int stringLength)
        {
            int stringCount = value.Length / stringLength;
            string s = "";
            for (int i=0; i<stringCount; i++)
            {
                string sub = value.Substring(i * stringLength, stringLength).Trim();
                if (sub == "") sub = "null";
                s += sub + ", ";
            }
            s = s.Trim().Trim(',');
            logHeader(variable, "[" + s.Trim() + "]");
        }

        public unsafe void logHeaderArrayByte(string variable, Byte* address, int count)
        {
            string s = "";
            for (int i = 0; i < count; i++)
            {
                Byte value = *(address + i);
                s += value.ToString() + ", ";
            }
            s = s.Trim().Trim(',');
            logHeader(variable, "[" + s.Trim() + "]");
        }

        public unsafe void logHeaderArraySingle(string variable, Single* address, int count)
        {
            string s = "";
            for (int i = 0; i < count; i++)
            {
                Single value = *(address + i);
                s += value.ToString() + ", ";
            }
            s = s.Trim().Trim(',');
            logHeader(variable, "[" + s.Trim() + "]");
        }

        public unsafe void logHeaderArrayInt16(string variable, Int16* address, int count)
        {
            string s = "";
            for (int i = 0; i < count; i++)
            {
                int value = *(address + i);
                s += value.ToString() + ", ";
            }
            s = s.Trim().Trim(',');
            logHeader(variable, "[" + s.Trim() + "]");
        }

        public unsafe void logHeaderArrayInt32(string variable, Int32* address, int count)
        {
            string s = "";
            for (int i = 0; i < count; i++)
            {
                int value = *(address + i);
                s += value.ToString() + ", ";
            }
            s = s.Trim().Trim(',');
            logHeader(variable, "[" + s.Trim() + "]");
        }

        public AbfReader(string abfFilePath)
        {
            log($"Loading {abfFilePath} ...");

            AbfStructs.ABFFileHeader header = new AbfStructs.ABFFileHeader();
            Int32 _file_handle = 0;
            UInt32 _max_samples = 16 * 1024;
            UInt32 _max_episodes = 0;
            Int32 _error_message = 0;
            UInt32 loadFlags = 0;
            bool result = new bool();

            System.Diagnostics.Stopwatch stopwatch = System.Diagnostics.Stopwatch.StartNew();

            result = ABF_ReadOpen(abfFilePath, ref _file_handle, loadFlags, ref header,
                ref _max_samples, ref _max_episodes, ref _error_message);

            double timeMS = stopwatch.ElapsedTicks * 1000.0 / System.Diagnostics.Stopwatch.Frequency;

            // display header just like ABFINFO program does
            unsafe
            {

                logHeader("", "GROUP #1 - File ID and size information");
                logHeader("fFileVersionNumber ", header.fFileVersionNumber.ToString());
                logHeader("nOperationMode", header.nOperationMode.ToString());
                logHeader("lActualAcqLength ", header.lActualAcqLength.ToString());
                logHeader("nNumPointsIgnored", header.nNumPointsIgnored.ToString());
                logHeader("lActualEpisodes", header.lActualEpisodes.ToString());
                logHeader("uFileStartDate", header.uFileStartDate.ToString());
                logHeader("uFileStartTimeMS", header.uFileStartTimeMS.ToString());
                logHeader("lStopwatchTime", header.lStopwatchTime.ToString());
                logHeader("fHeaderVersionNumber", header.fHeaderVersionNumber.ToString());
                logHeader("nFileType", header.nFileType.ToString());

                logHeader("", "GROUP #2 - File Structure");
                logHeader("lDataSectionPtr", header.lDataSectionPtr.ToString());
                logHeader("lTagSectionPtr", header.lTagSectionPtr.ToString());
                logHeader("lNumTagEntries", header.lNumTagEntries.ToString());
                logHeader("lScopeConfigPtr", header.lScopeConfigPtr.ToString());
                logHeader("lNumScopes", header.lNumScopes.ToString());
                logHeader("lDeltaArrayPtr", header.lDeltaArrayPtr.ToString());
                logHeader("lNumDeltas", header.lNumDeltas.ToString());
                logHeader("lVoiceTagPtr", header.lVoiceTagPtr.ToString());
                logHeader("lVoiceTagEntries", header.lVoiceTagEntries.ToString());
                logHeader("lSynchArrayPtr", header.lSynchArrayPtr.ToString());
                logHeader("lSynchArraySize", header.lSynchArraySize.ToString());
                logHeader("nDataFormat", header.nDataFormat.ToString());
                logHeader("nSimultaneousScan", header.nSimultaneousScan.ToString());
                logHeader("lStatisticsConfigPtr", header.lStatisticsConfigPtr.ToString());
                logHeader("lAnnotationSectionPtr", header.lAnnotationSectionPtr.ToString());
                logHeader("lNumAnnotations", header.lNumAnnotations.ToString());
                logHeaderArrayInt32("lDACFilePtr", header.lDACFilePtr, 8);
                logHeaderArrayInt32("lDACFileNumEpisodes", header.lDACFileNumEpisodes, 8);

                logHeader("", "GROUP #3 - Trial hierarchy information");
                logHeader("nADCNumChannels", header.nADCNumChannels.ToString());
                logHeader("fADCSequenceInterval", header.fADCSequenceInterval.ToString());
                logHeader("uFileCompressionRatio", header.uFileCompressionRatio.ToString());
                logHeader("bEnableFileCompression", header.bEnableFileCompression.ToString());
                logHeader("fSynchTimeUnit", header.fSynchTimeUnit.ToString());
                logHeader("fSecondsPerRun", header.fSecondsPerRun.ToString());
                logHeader("lNumSamplesPerEpisode", header.lNumSamplesPerEpisode.ToString());
                logHeader("lPreTriggerSamples", header.lPreTriggerSamples.ToString());
                logHeader("lEpisodesPerRun", header.lEpisodesPerRun.ToString());
                logHeader("lRunsPerTrial", header.lRunsPerTrial.ToString());
                logHeader("lNumberOfTrials", header.lNumberOfTrials.ToString());
                logHeader("nAveragingMode", header.nAveragingMode.ToString());
                logHeader("nUndoRunCount", header.nUndoRunCount.ToString());
                logHeader("nFirstEpisodeInRun", header.nFirstEpisodeInRun.ToString());
                logHeader("fTriggerThreshold", header.fTriggerThreshold.ToString());
                logHeader("nTriggerSource", header.nTriggerSource.ToString());
                logHeader("nTriggerAction", header.nTriggerAction.ToString());
                logHeader("nTriggerPolarity", header.nTriggerPolarity.ToString());
                logHeader("fScopeOutputInterval", header.fScopeOutputInterval.ToString());
                logHeader("fEpisodeStartToStart", header.fEpisodeStartToStart.ToString());
                logHeader("fRunStartToStart", header.fRunStartToStart.ToString());
                logHeader("fTrialStartToStart", header.fTrialStartToStart.ToString());
                logHeader("lAverageCount", header.lAverageCount.ToString());
                logHeader("nAutoTriggerStrategy", header.nAutoTriggerStrategy.ToString());
                logHeader("fFirstRunDelayS", header.fFirstRunDelayS.ToString());
                logHeader("nTriggerTimeout", header.nTriggerTimeout.ToString());
                
                logHeader("", "GROUP #4 - Display Parameters");
                logHeader("nDataDisplayMode", header.nDataDisplayMode.ToString());
                logHeader("nChannelStatsStrategy", header.nChannelStatsStrategy.ToString());
                logHeader("lSamplesPerTrace", header.lSamplesPerTrace.ToString());
                logHeader("lStartDisplayNum", header.lStartDisplayNum.ToString());
                logHeader("lFinishDisplayNum", header.lFinishDisplayNum.ToString());
                logHeader("nShowPNRawData", header.nShowPNRawData.ToString());
                logHeader("fStatisticsPeriod", header.fStatisticsPeriod.ToString());
                logHeader("lStatisticsMeasurements", header.lStatisticsMeasurements.ToString());
                logHeader("nStatisticsSaveStrategy", header.nStatisticsSaveStrategy.ToString());

                logHeader("", "GROUP #5 - Hardware information");
                logHeader("fADCRange", header.fADCRange.ToString());
                logHeader("fDACRange", header.fDACRange.ToString());
                logHeader("lADCResolution", header.lADCResolution.ToString());
                logHeader("lDACResolution", header.lDACResolution.ToString());
                logHeader("nDigitizerADCs", header.nDigitizerADCs.ToString());
                logHeader("nDigitizerDACs", header.nDigitizerDACs.ToString());
                logHeader("nDigitizerTotalDigitalOuts", header.nDigitizerTotalDigitalOuts.ToString());
                logHeader("nDigitizerSynchDigitalOuts", header.nDigitizerSynchDigitalOuts.ToString());
                logHeader("nDigitizerType", header.nDigitizerType.ToString());

                logHeader("", "GROUP #6 Environmental Information");
                logHeader("nExperimentType", header.nExperimentType.ToString());
                logHeader("nManualInfoStrategy", header.nManualInfoStrategy.ToString());
                logHeader("fCellID1", header.fCellID1.ToString());
                logHeader("fCellID2", header.fCellID2.ToString());
                logHeader("fCellID3", header.fCellID3.ToString());
                logHeader("sProtocolPath", header.sProtocolPath.ToString());
                logHeader("sCreatorInfo", header.sCreatorInfo.ToString());
                logHeader("sModifierInfo", header.sModifierInfo.ToString());
                logHeader("nCommentsEnable", header.nCommentsEnable.ToString());
                logHeader("sFileComment", header.sFileComment.ToString());
                logHeaderArrayInt16("nTelegraphEnable", header.nTelegraphEnable, 16);
                logHeaderArrayInt16("nTelegraphInstrument", header.nTelegraphInstrument, 16);
                logHeaderArraySingle("fTelegraphAdditGain", header.fTelegraphAdditGain, 16);
                logHeaderArraySingle("fTelegraphFilter", header.fTelegraphFilter, 16);
                logHeaderArraySingle("fTelegraphMembraneCap", header.fTelegraphMembraneCap, 16);
                logHeaderArraySingle("fTelegraphAccessResistance", header.fTelegraphAccessResistance, 16);
                logHeaderArrayInt16("nTelegraphMode", header.nTelegraphMode, 16);
                logHeaderArrayInt16("nTelegraphDACScaleFactorEnable", header.nTelegraphDACScaleFactorEnable, 8);
                logHeader("nAutoAnalyseEnable", header.nAutoAnalyseEnable.ToString());
                logHeader("FileGUID", header.FileGUID.ToString());
                logHeaderArraySingle("fInstrumentHoldingLevel", header.fInstrumentHoldingLevel, 8);
                logHeader("ulFileCRC", header.ulFileCRC.ToString());
                logHeader("nCRCEnable", header.nCRCEnable.ToString());
                
                logHeader("", "GROUP #7 Multi-channel information");
                logHeader("nSignalType", header.nSignalType.ToString());
                logHeaderArrayInt16("nADCPtoLChannelMap", header.nADCPtoLChannelMap, 16);
                logHeaderArrayInt16("nADCSamplingSeq", header.nADCSamplingSeq, 16);
                logHeaderArraySingle("fADCProgrammableGain", header.fADCProgrammableGain, 16);
                logHeaderArraySingle("fADCDisplayAmplification", header.fADCDisplayAmplification, 16);
                logHeaderArraySingle("fADCDisplayOffset", header.fADCDisplayOffset, 16);
                logHeaderArraySingle("fInstrumentScaleFactor", header.fInstrumentScaleFactor, 16);
                logHeaderArraySingle("fInstrumentOffset", header.fInstrumentOffset, 16);
                logHeaderArraySingle("fSignalGain", header.fSignalGain, 16);
                logHeaderArraySingle("fSignalOffset", header.fSignalOffset, 16);
                logHeaderArraySingle("fSignalLowpassFilter", header.fSignalLowpassFilter, 16);
                logHeaderArraySingle("fSignalHighpassFilter", header.fSignalHighpassFilter, 16);
                logHeader("nLowpassFilterType", header.nLowpassFilterType.ToString());
                logHeader("nHighpassFilterType", header.nHighpassFilterType.ToString());
                logHeaderArrayByte("bHumFilterEnable", header.bHumFilterEnable, 16);
                logHeaderArrayString("sADCChannelName", header.sADCChannelName, 10);
                logHeaderArrayString("sADCUnits", header.sADCUnits, 8);
                logHeaderArraySingle("fDACScaleFactor", header.fDACScaleFactor, 8);
                logHeaderArraySingle("fDACHoldingLevel", header.fDACHoldingLevel, 8);
                logHeaderArraySingle("fDACCalibrationFactor", header.fDACCalibrationFactor, 8);
                logHeaderArraySingle("fDACCalibrationOffset", header.fDACCalibrationOffset, 8);
                logHeaderArrayString("sDACChannelName", header.sDACChannelName, 10);
                logHeaderArrayString("sDACChannelUnits", header.sDACChannelUnits, 8);

                logHeader("", "GROUP #9 - Epoch Waveform and Pulses");
                logHeader("nDigitalEnable", header.nDigitalEnable.ToString());
                logHeader("nActiveDACChannel", header.nActiveDACChannel.ToString());
                logHeader("nDigitalDACChannel", header.nDigitalDACChannel.ToString());
                logHeader("nDigitalHolding", header.nDigitalHolding.ToString());
                logHeader("nDigitalInterEpisode", header.nDigitalInterEpisode.ToString());
                logHeader("nDigitalTrainActiveLogic", header.nDigitalTrainActiveLogic.ToString());
                logHeaderArrayInt16("nDigitalValue", header.nDigitalValue, 50);
                logHeaderArrayInt16("nDigitalTrainValue", header.nDigitalTrainValue, 50);
                logHeaderArrayByte("bEpochCompression", header.bEpochCompression, 50);
                logHeaderArrayInt16("nWaveformEnable", header.nWaveformEnable, 8);
                logHeaderArrayInt16("nWaveformSource", header.nWaveformSource, 8);
                logHeaderArrayInt16("nInterEpisodeLevel", header.nInterEpisodeLevel, 8*50);
                logHeaderArrayInt16("nEpochType", header.nEpochType, 8*50);
                logHeaderArraySingle("fEpochInitLevel", header.fEpochInitLevel, 8*50);
                logHeaderArraySingle("fEpochFinalLevel", header.fEpochFinalLevel, 8*50);
                logHeaderArraySingle("fEpochLevelInc", header.fEpochLevelInc, 8*50);
                logHeaderArrayInt32("lEpochInitDuration", header.lEpochInitDuration, 8*50);
                logHeaderArrayInt32("lEpochDurationInc", header.lEpochDurationInc, 8*50);
                logHeaderArrayInt16("nEpochTableRepetitions", header.nEpochTableRepetitions, 8);
                logHeaderArraySingle("fEpochTableStartToStartInterval", header.fEpochTableStartToStartInterval, 8);

                logHeader("", "GROUP #10 - DAC Output File");
                logHeaderArraySingle("fDACFileScale", header.fDACFileScale, 8);
                logHeaderArraySingle("fDACFileOffset", header.fDACFileOffset, 8);
                logHeaderArrayInt32("lDACFileEpisodeNum", header.lDACFileEpisodeNum, 8);
                logHeaderArrayInt16("nDACFileADCNum", header.nDACFileADCNum, 8);
                logHeaderArrayString("sDACFilePath", header.sDACFilePath, 256);

                logHeader("", "GROUP #11a - Presweep (conditioning) pulse train");
                logHeaderArrayInt16("nConditEnable", header.nConditEnable, 8);
                logHeaderArrayInt32("lConditNumPulses", header.lConditNumPulses, 8);
                logHeaderArraySingle("fBaselineDuration", header.fBaselineDuration, 8);
                logHeaderArraySingle("fBaselineLevel", header.fBaselineLevel, 8);
                logHeaderArraySingle("fStepDuration", header.fStepDuration, 8);
                logHeaderArraySingle("fStepLevel", header.fStepLevel, 8);
                logHeaderArraySingle("fPostTrainPeriod", header.fPostTrainPeriod, 8);
                logHeaderArraySingle("fPostTrainLevel", header.fPostTrainLevel, 8);
                logHeaderArraySingle("fCTStartLevel", header.fCTStartLevel, 8*50);
                logHeaderArraySingle("fCTEndLevel", header.fCTEndLevel, 8*50);
                logHeaderArraySingle("fCTIntervalDuration", header.fCTIntervalDuration, 8*50);
                logHeaderArraySingle("fCTStartToStartInterval", header.fCTStartToStartInterval, 8);

                logHeader("", "GROUP #11b - Membrane Test Between Sweeps");
                logHeaderArrayInt16("nMembTestEnable", header.nMembTestEnable, 8);
                logHeaderArraySingle("fMembTestPreSettlingTimeMS", header.fMembTestPreSettlingTimeMS, 8);
                logHeaderArraySingle("fMembTestPostSettlingTimeMS", header.fMembTestPostSettlingTimeMS, 8);

                logHeader("", "GROUP #11c - PreSignal test pulse");
                logHeaderArrayInt16("nPreSignalEnable", header.nPreSignalEnable, 8);
                logHeaderArraySingle("fPreSignalPreStepDuration", header.fPreSignalPreStepDuration, 8);
                logHeaderArraySingle("fPreSignalPreStepLevel", header.fPreSignalPreStepLevel, 8);
                logHeaderArraySingle("fPreSignalStepDuration", header.fPreSignalStepDuration, 8);
                logHeaderArraySingle("fPreSignalStepLevel", header.fPreSignalStepLevel, 8);
                logHeaderArraySingle("fPreSignalPostStepDuration", header.fPreSignalPostStepDuration, 8);
                logHeaderArraySingle("fPreSignalPostStepLevel", header.fPreSignalPostStepLevel, 8);

                logHeader("", "GROUP #11d - Hum Silncer Adapt between sweeps");
                logHeader("nAdaptEnable", header.nAdaptEnable.ToString());
                logHeader("fInterSweepAdaptTimeS", header.fInterSweepAdaptTimeS.ToString());

                logHeader("", "GROUP #12 - Variable parameter user list");
                logHeaderArrayInt16("nULEnable", header.nULEnable, 4);
                logHeaderArrayInt16("nULParamToVary", header.nULParamToVary, 4);
                logHeaderArrayInt16("nULRepeat", header.nULRepeat, 4);
                logHeaderArrayString("sULParamValueList", header.sULParamValueList, 256);

                logHeader("", "GROUP #13 - Statistics measurements");
                logHeader("nStatsEnable", header.nStatsEnable.ToString());
                logHeader("nStatsActiveChannels", header.nStatsActiveChannels.ToString());
                logHeader("nStatsSearchRegionFlags", header.nStatsSearchRegionFlags.ToString());
                logHeader("nStatsSmoothing", header.nStatsSmoothing.ToString());
                logHeader("nStatsSmoothingEnable", header.nStatsSmoothingEnable.ToString());
                logHeader("nStatsBaseline", header.nStatsBaseline.ToString());
                logHeader("nStatsBaselineDAC", header.nStatsBaselineDAC.ToString());
                logHeader("lStatsBaselineStart", header.lStatsBaselineStart.ToString());
                logHeader("lStatsBaselineEnd", header.lStatsBaselineEnd.ToString());
                logHeaderArrayInt32("lStatsMeasurements", header.lStatsMeasurements, 8);
                logHeaderArrayInt32("lStatsStart", header.lStatsStart, 8);
                logHeaderArrayInt32("lStatsEnd", header.lStatsEnd, 8);
                logHeaderArrayInt16("nRiseBottomPercentile", header.nRiseBottomPercentile, 8);
                logHeaderArrayInt16("nRiseTopPercentile", header.nRiseTopPercentile, 8);
                logHeaderArrayInt16("nDecayBottomPercentile", header.nDecayBottomPercentile, 8);
                logHeaderArrayInt16("nDecayTopPercentile", header.nDecayTopPercentile, 8);
                logHeaderArrayInt16("nStatsChannelPolarity", header.nStatsChannelPolarity, 16);
                logHeaderArrayInt16("nStatsSearchMode", header.nStatsSearchMode, 8);
                logHeaderArrayInt16("nStatsSearchDAC", header.nStatsSearchDAC, 8);

                logHeader("", "GROUP #14 - Channel Arithmetic");
                logHeader("nArithmeticEnable", header.nArithmeticEnable.ToString());
                logHeader("nArithmeticExpression", header.nArithmeticExpression.ToString());
                logHeader("fArithmeticUpperLimit", header.fArithmeticUpperLimit.ToString());
                logHeader("fArithmeticLowerLimit", header.fArithmeticLowerLimit.ToString());
                logHeader("nArithmeticADCNumA", header.nArithmeticADCNumA.ToString());
                logHeader("nArithmeticADCNumB", header.nArithmeticADCNumB.ToString());
                logHeader("fArithmeticK1", header.fArithmeticK1.ToString());
                logHeader("fArithmeticK2", header.fArithmeticK2.ToString());
                logHeader("fArithmeticK3", header.fArithmeticK3.ToString());
                logHeader("fArithmeticK4", header.fArithmeticK4.ToString());
                logHeader("fArithmeticK5", header.fArithmeticK5.ToString());
                logHeader("fArithmeticK6", header.fArithmeticK6.ToString());
                logHeader("sArithmeticOperator", header.sArithmeticOperator.ToString());
                logHeader("sArithmeticUnits", header.sArithmeticUnits.ToString());

                logHeader("", "GROUP #15 - Leak subtraction");
                logHeader("nPNPosition", header.nPNPosition.ToString());
                logHeader("nPNNumPulses", header.nPNNumPulses.ToString());
                logHeader("nPNPolarity", header.nPNPolarity.ToString());
                logHeader("fPNSettlingTime", header.fPNSettlingTime.ToString());
                logHeader("fPNInterpulse", header.fPNInterpulse.ToString());
                logHeaderArrayInt16("nLeakSubtractType", header.nLeakSubtractType, 8);
                logHeaderArraySingle("fPNHoldingLevel", header.fPNHoldingLevel, 8);
                logHeaderArrayInt16("nLeakSubtractADCIndex", header.nLeakSubtractADCIndex, 8);

                logHeader("", "GROUP #16 - Miscellaneous variables");
                logHeader("nLevelHysteresis", header.nLevelHysteresis.ToString());
                logHeader("lTimeHysteresis", header.lTimeHysteresis.ToString());
                logHeader("nAllowExternalTags", header.nAllowExternalTags.ToString());
                logHeader("nAverageAlgorithm", header.nAverageAlgorithm.ToString());
                logHeader("fAverageWeighting", header.fAverageWeighting.ToString());
                logHeader("nUndoPromptStrategy", header.nUndoPromptStrategy.ToString());
                logHeader("nTrialTriggerSource", header.nTrialTriggerSource.ToString());
                logHeader("nStatisticsDisplayStrategy", header.nStatisticsDisplayStrategy.ToString());
                logHeader("nExternalTagType", header.nExternalTagType.ToString());
                logHeader("lHeaderSize", header.lHeaderSize.ToString());
                logHeader("nStatisticsClearStrategy", header.nStatisticsClearStrategy.ToString());
                logHeader("nEnableFirstLastHolding", header.nEnableFirstLastHolding.ToString());

                logHeader("", "GROUP #17 - Trains parameters");
                logHeaderArrayInt32("lEpochPulsePeriod", header.lEpochPulsePeriod, 50);
                logHeaderArrayInt32("lEpochPulseWidth", header.lEpochPulseWidth, 50);

                logHeader("", "GROUP #18 - Application version data");
                logHeader("nCreatorMajorVersion", header.nCreatorMajorVersion.ToString());
                logHeader("nCreatorMinorVersion", header.nCreatorMinorVersion.ToString());
                logHeader("nCreatorBugfixVersion", header.nCreatorBugfixVersion.ToString());
                logHeader("nCreatorBuildVersion", header.nCreatorBuildVersion.ToString());
                logHeader("nModifierMajorVersion", header.nModifierMajorVersion.ToString());
                logHeader("nModifierMinorVersion", header.nModifierMinorVersion.ToString());
                logHeader("nModifierBugfixVersion", header.nModifierBugfixVersion.ToString());
                logHeader("nModifierBuildVersion", header.nModifierBuildVersion.ToString());

                logHeader("", "GROUP #19 - LTP protocol");
                logHeader("nLTPType", header.nLTPType.ToString());
                logHeaderArrayInt16("nLTPUsageOfDAC", header.nLTPUsageOfDAC, 8);
                logHeaderArrayInt16("nLTPPresynapticPulses", header.nLTPPresynapticPulses, 8);

                logHeader("", "GROUP #20 - Digidata 132x Trigger out flag");
                logHeader("nScopeTriggerOut", header.nScopeTriggerOut.ToString());

                logHeader("", "GROUP #21 - Epoch resistance");
                logHeaderArrayString("sEpochResistanceSignalName", header.sEpochResistanceSignalName, 10);
                logHeaderArrayInt16("nEpochResistanceState", header.nEpochResistanceState, 8);

                logHeader("", "GROUP #22 - Alternating episodic mode");
                logHeader("nAlternateDACOutputState", header.nAlternateDACOutputState.ToString());
                logHeader("nAlternateDigitalOutputState", header.nAlternateDigitalOutputState.ToString());
                logHeaderArrayInt16("nAlternateDigitalValue", header.nAlternateDigitalValue, 50);
                logHeaderArrayInt16("nAlternateDigitalTrainValue", header.nAlternateDigitalTrainValue, 50);

                logHeader("", "GROUP #23 - Post-processing actions");
                logHeaderArraySingle("fPostProcessLowpassFilter", header.fPostProcessLowpassFilter, 16);
                logHeader("nPostProcessLowpassFilterType", header.nPostProcessLowpassFilterType.ToString());

                logHeader("", "GROUP #24 - Legacy gear shift info");
                logHeader("fLegacyADCSequenceInterval", header.fLegacyADCSequenceInterval.ToString());
                logHeader("fLegacyADCSecondSequenceInterval", header.fLegacyADCSecondSequenceInterval.ToString());
                logHeader("lLegacyClockChange", header.lLegacyClockChange.ToString());
                logHeader("lLegacyNumSamplesPerEpisode", header.lLegacyNumSamplesPerEpisode.ToString());

                logHeader("", "GROUP #25 - Gap-Free Config");
                logHeaderArrayInt16("nGapFreeEpochType", header.nGapFreeEpochType, 8 * 50);
                logHeaderArraySingle("fGapFreeEpochLevel", header.fGapFreeEpochLevel, 8 * 50);
                logHeaderArrayInt32("lGapFreeEpochDuration", header.lGapFreeEpochDuration, 8 * 50);
                logHeaderArrayByte("nGapFreeDigitalValue", header.nGapFreeDigitalValue, 8 * 50);
                logHeader("nGapFreeEpochStart", header.nGapFreeEpochStart.ToString());
            }
        }
    }
}