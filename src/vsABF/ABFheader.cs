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
            /// <summary>
            /// Display text information about the current header object.
            /// </summary>
            /// <returns></returns>
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
            public short nADCNum; //h 0
            public short nTelegraphEnable; //h 2
            public short nTelegraphInstrument; //h 4
            public float fTelegraphAdditGain; //f 6
            public float fTelegraphFilter; //f 10
            public float fTelegraphMembraneCap; //f 14
            public short nTelegraphMode; //h 18
            public float fTelegraphAccessResistance; //f 20
            public short nADCPtoLChannelMap; //h 24
            public short nADCSamplingSeq; //h 26
            public float fADCProgrammableGain; //f 28
            public float fADCDisplayAmplification; //f 32
            public float fADCDisplayOffset; //f 36
            public float fInstrumentScaleFactor; //f 40
            public float fInstrumentOffset; //f 44
            public float fSignalGain; //f 48
            public float fSignalOffset; //f 52
            public float fSignalLowpassFilter; //f 56
            public float fSignalHighpassFilter; //f 60
            public byte nLowpassFilterType; //b 64
            public byte nHighpassFilterType; //b 65
            public float fPostProcessLowpassFilter; //f 66
            public byte nPostProcessLowpassFilterType; //c 70
            public byte bEnabledDuringPN; //b 71
            public short nStatsChannelPolarity; //h 72
            public int lADCChannelNameIndex; //i 74
            public int lADCUnitsIndex; //i 78
        }

        public class ADCSection
        {
            public ADCSectionByADC[] ADCsections;

            public string GetInfo()
            {
                string info = $"\n### {this.GetType().Name} ###\n";
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

        public class DACSection : HeaderObject
        {
        }

        public class EpochPerDACSection : HeaderObject
        {
        }

        public class EpochSection : HeaderObject
        {
        }

        public class TagSection : HeaderObject
        {
        }

        public class StringsSection : HeaderObject
        {
        }

        public class StringsIndexed : HeaderObject
        {
        }

    }

}
