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

                    if (val == null)
                    {
                        info += $"{x.Name} = null\n";
                    } else
                    {
                        Section sec = (Section)val;
                        info += $"{x.Name} = first byte {sec.byteStart}, item size {sec.itemSize} bytes, item count {sec.itemCount}\n";
                    }

                }
                return info;
            }
        }

        public class ProtocolSection : HeaderObject
        {
        }

        public class ADCSection : HeaderObject
        {
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
