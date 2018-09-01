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
        public class HeaderObject
        {
            public string GetInfo()
            {
                string info = $"\n### {this.GetType().Name} ###\n";
                foreach (System.Reflection.FieldInfo x in this.GetType().GetFields())
                {
                    System.Object val = x.GetValue(this);

                    if (val == null)
                    {
                        info += $"{x.Name} = null\n";
                    }
                    else
                    {
                        string valStr = "";
                        if (val.GetType().IsArray)
                        {
                            Array objects = (Array)val;
                            string[] strings = new string[objects.GetUpperBound(0)];
                            for (int i = 0; i < strings.Length; i++)
                            {
                                strings[i] += objects.GetValue(i).ToString();
                            }
                            valStr += "[" + string.Join(", ", strings.ToArray()) + "]";
                        }
                        else
                        {
                            valStr = val.ToString();
                        }
                        info += $"{x.Name} = {valStr}\n";
                    }
                }
                return info;
            }
        }

        public class HeaderV1 : HeaderObject
        {
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
            public string[] sADCChannelName;
            public string[] sADCUnits;
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
    }

        public class HeaderV2 : HeaderObject
        {
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
        }

        public class Section
        {
            private int BLOCKSIZE = 512;
            public int blockStart;
            public int byteStart { get { return BLOCKSIZE * blockStart; }  }
            public int itemSize;
            public int itemCount;
            public Section(uint[] ints)
            {
                blockStart = (int)ints[0];
                itemSize = (int)ints[1];
                itemCount = (int)ints[2];
            }
        }

        public class SectionMap
        {
            public Section Protocol;
            public Section ADC;
            public Section DAC;
            public Section Epoch;
            public Section ADCPerDAC;
            public Section EpochPerDAC;
            public Section UserList;
            public Section StatsRegion;
            public Section Math;
            public Section Strings;
            public Section Data;
            public Section Tag;
            public Section Scope;
            public Section Delta;
            public Section VoiceTag;
            public Section SynchArray;
            public Section Annotation;
            public Section Stats;

            public string GetInfo()
            {
                string info = $"\n### {this.GetType().Name} ###\n";
                foreach (System.Reflection.FieldInfo x in this.GetType().GetFields())
                {
                    System.Object val = x.GetValue(this);
                    Section sec = (Section)val;
                    info += $"{x.Name} = {sec.itemCount} items ({sec.itemSize} bytes each) at byte {sec.byteStart}\n";
                }
                return info;
            }
        }

        public class ProtocolSection : HeaderObject
        {
            //ProtocolSection
            public short nOperationMode;
            public float fADCSequenceInterval;
            public byte bEnableFileCompression;
            public byte[] sUnused;
            public uint uFileCompressionRatio;
            public float fSynchTimeUnit;
            public float fSecondsPerRun;
            public int lNumSamplesPerEpisode;
            public int lPreTriggerSamples;
            public int lEpisodesPerRun;
            public int lRunsPerTrial;
            public int lNumberOfTrials;
            public short nAveragingMode;
            public short nUndoRunCount;
            public short nFirstEpisodeInRun;
            public float fTriggerThreshold;
            public short nTriggerSource;
            public short nTriggerAction;
            public short nTriggerPolarity;
            public float fScopeOutputInterval;
            public float fEpisodeStartToStart;
            public float fRunStartToStart;
            public int lAverageCount;
            public float fTrialStartToStart;
            public short nAutoTriggerStrategy;
            public float fFirstRunDelayS;
            public short nChannelStatsStrategy;
            public int lSamplesPerTrace;
            public int lStartDisplayNum;
            public int lFinishDisplayNum;
            public short nShowPNRawData;
            public float fStatisticsPeriod;
            public int lStatisticsMeasurements;
            public short nStatisticsSaveStrategy;
            public float fADCRange;
            public float fDACRange;
            public int lADCResolution;
            public int lDACResolution;
            public short nExperimentType;
            public short nManualInfoStrategy;
            public short nCommentsEnable;
            public int lFileCommentIndex;
            public short nAutoAnalyseEnable;
            public short nSignalType;
            public short nDigitalEnable;
            public short nActiveDACChannel;
            public short nDigitalHolding;
            public short nDigitalInterEpisode;
            public short nDigitalDACChannel;
            public short nDigitalTrainActiveLogic;
            public short nStatsEnable;
            public short nStatisticsClearStrategy;
            public short nLevelHysteresis;
            public int lTimeHysteresis;
            public short nAllowExternalTags;
            public short nAverageAlgorithm;
            public float fAverageWeighting;
            public short nUndoPromptStrategy;
            public short nTrialTriggerSource;
            public short nStatisticsDisplayStrategy;
            public short nExternalTagType;
            public short nScopeTriggerOut;
            public short nLTPType;
            public short nAlternateDACOutputState;
            public short nAlternateDigitalOutputState;
            public float[] fCellID;
            public short nDigitizerADCs;
            public short nDigitizerDACs;
            public short nDigitizerTotalDigitalOuts;
            public short nDigitizerSynchDigitalOuts;
            public short nDigitizerType;
        }

        public class ADCSectionByADC
        {
            public short nADCNum;
            public short nTelegraphEnable;
            public short nTelegraphInstrument;
            public float fTelegraphAdditGain;
            public float fTelegraphFilter;
            public float fTelegraphMembraneCap;
            public short nTelegraphMode;
            public float fTelegraphAccessResistance;
            public short nADCPtoLChannelMap;
            public short nADCSamplingSeq;
            public float fADCProgrammableGain;
            public float fADCDisplayAmplification;
            public float fADCDisplayOffset;
            public float fInstrumentScaleFactor;
            public float fInstrumentOffset;
            public float fSignalGain;
            public float fSignalOffset;
            public float fSignalLowpassFilter;
            public float fSignalHighpassFilter;
            public byte nLowpassFilterType;
            public byte nHighpassFilterType;
            public float fPostProcessLowpassFilter;
            public byte nPostProcessLowpassFilterType;
            public byte bEnabledDuringPN;
            public short nStatsChannelPolarity;
            public int lADCChannelNameIndex;
            public int lADCUnitsIndex;
        }

        public class ADCSection
        {
            public ADCSectionByADC[] ADCsections;

            public string GetInfo()
            {
                string info = $"\n### ADCSection ###\n";
                for (int adcNumber=0; adcNumber < ADCsections.Length; adcNumber++)
                {
                    info += $"--- ADC {adcNumber} ---\n";
                    ADCSectionByADC thisAdc = ADCsections[adcNumber];
                    foreach (System.Reflection.FieldInfo x in thisAdc.GetType().GetFields())
                    {
                        info += $"{x.Name} = {x.GetValue(thisAdc).ToString()}\n";
                    }
                }
                return info;
            }

        }

        public class DACSectionPerDAC
        {
            public short nDACNum;
            public short nTelegraphDACScaleFactorEnable;
            public float fInstrumentHoldingLevel;
            public float fDACScaleFactor;
            public float fDACHoldingLevel;
            public float fDACCalibrationFactor;
            public float fDACCalibrationOffset;
            public int lDACChannelNameIndex;
            public int lDACChannelUnitsIndex;
            public int lDACFilePtr;
            public int lDACFileNumEpisodes;
            public short nWaveformEnable;
            public short nWaveformSource;
            public short nInterEpisodeLevel;
            public float fDACFileScale;
            public float fDACFileOffset;
            public int lDACFileEpisodeNum;
            public short nDACFileADCNum;
            public short nConditEnable;
            public int lConditNumPulses;
            public float fBaselineDuration;
            public float fBaselineLevel;
            public float fStepDuration;
            public float fStepLevel;
            public float fPostTrainPeriod;
            public float fPostTrainLevel;
            public short nMembTestEnable;
            public short nLeakSubtractType;
            public short nPNPolarity;
            public float fPNHoldingLevel;
            public short nPNNumADCChannels;
            public short nPNPosition;
            public short nPNNumPulses;
            public float fPNSettlingTime;
            public float fPNInterpulse;
            public short nLTPUsageOfDAC;
            public short nLTPPresynapticPulses;
            public int lDACFilePathIndex;
            public float fMembTestPreSettlingTimeMS;
            public float fMembTestPostSettlingTimeMS;
            public short nLeakSubtractADCIndex;
        }

        public class DACSection
        {
            public DACSectionPerDAC[] DACsections;

            public string GetInfo()
            {
                string info = $"\n### DACSection ###\n";
                for (int adcNumber = 0; adcNumber < DACsections.Length; adcNumber++)
                {
                    info += $"--- DAC {adcNumber} ---\n";
                    DACSectionPerDAC thisDac = DACsections[adcNumber];
                    foreach (System.Reflection.FieldInfo x in thisDac.GetType().GetFields())
                    {
                        info += $"{x.Name} = {x.GetValue(thisDac).ToString()}\n";
                    }
                }
                return info;
            }

            public float[] GetHoldByChannel()
            {
                float[] commands = new float[DACsections.Length];
                for (int i=0; i< DACsections.Length; i++)
                {
                    commands[i] = DACsections[i].fDACHoldingLevel;
                }
                return commands;
            }
        }

        public class EpochPerDACSection
        {
            //TODO: this
        }

        public class EpochSection
        {
            //TODO: this
        }

        public class TagSectionByTag
        {
            public int lTagTime;
            public string sComment;
            public short nTagType;
            public short nVoiceTagIndex;
        }

        public class TagSection
        {
            public TagSectionByTag[] tags;

            public string GetInfo()
            {
                string info = $"\n### Tags ###\n";
                for (int tagNumber=0; tagNumber<tags.Length; tagNumber++)
                {
                    TagSectionByTag tag = tags[tagNumber];
                    info += $"Tag at {tag.lTagTime} says [{tag.sComment}]\n";
                }
                return info;
            }

            public string[] GetTagComments()
            {
                string[] comments = new string[tags.Length];
                for (int i=0; i<tags.Length; i++)
                {
                    comments[i] = tags[i].sComment;
                }
                return comments;
            }

            public double[] GetTagTimes(double tagMult =1)
            {
                double[] times = new double[tags.Length];
                for (int i = 0; i < tags.Length; i++)
                {
                    times[i] = (double)(tags[i].lTagTime*tagMult);
                }
                return times;
            }
        }

        public class StringsSection
        {
            //TODO: this
        }

        public class StringsIndexed
        {
            //TODO: this
        }

    }

}
