﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace vsABF
{
    public partial class ABF
    {
        public Logger log;
        public ABFreader abfReader;

        // these things will be populated for every ABF (abf1 and abf2)
        public string abfFilePath;
        public string abfID;
        public int abfVersionMajor;
        public float[] holdingCommand;
        public string[] tagComments;
        public double[] tagTimesSec;
        private short nDataFormat;
        public int dataByteStart;
        public int dataPointCount;
        public int dataPointByteSize;
        public int channelCount;
        public int dataRate;
        public double dataSecPerPoint;
        public int dataPointsPerMs;
        public int sweepCount;
        public string[] adcUnits;
        public string[] adcNames;
        public string[] dacUnits;
        public string[] dacNames;
        public double[] dataGainByChannel;
        public double[] dataOffsetByChannel;

        public ABF(string abfFilePath)
        {
            // set up our logger, paths, and ensure file exists
            log = new Logger("ABF");
            abfFilePath = Path.GetFullPath(abfFilePath);
            this.abfFilePath = abfFilePath;
            this.abfID = Path.GetFileNameWithoutExtension(abfFilePath);

            if (!File.Exists(abfFilePath))
            {
                log.Critical($"File does not exist: {abfFilePath}");
                return;
            }

            log.Info($"Loading ABF file: {abfFilePath}");            
            ABFreader abfReader = new ABFreader(abfFilePath, log);
            if (abfReader.fileSignature=="ABF ")
            {
                // pull header information locally
                abfVersionMajor = 1;

                // ABF info
                holdingCommand = abfReader.headerV1.fEpochInitLevel;
                tagComments = new string[] { };
                tagTimesSec = new double[] { };

                // DATA info
                nDataFormat = abfReader.headerV1.nDataFormat;
                dataByteStart = abfReader.headerV1.lDataSectionPtr * ABFreader.BLOCKSIZE + abfReader.headerV1.nNumPointsIgnored;
                dataPointCount = abfReader.headerV1.lActualAcqLength;
                dataPointByteSize = 2; // ABF 1 files always have int16 points?
                channelCount = abfReader.headerV1.nADCNumChannels;
                dataRate = (int)((double)1e6 / (double)abfReader.headerV1.fADCSampleInterval);
                dataPointsPerMs = dataRate / 1000;
                dataSecPerPoint = 1 / (double)dataRate;
                sweepCount = abfReader.headerV1.lActualEpisodes;

                // channels and units
                adcUnits = abfReader.headerV1.sADCUnits;
                adcNames = abfReader.headerV1.sADCChannelName;
                dacUnits = new string[channelCount];
                dacNames = new string[channelCount];

                // determine gain and offset for each channel
                dataGainByChannel = new double[channelCount];
                dataOffsetByChannel = new double[channelCount];
                for (int i=0; i<channelCount; i++)
                {
                    dataGainByChannel[i] = 1;
                    dataGainByChannel[i] /= abfReader.headerV1.fInstrumentScaleFactor[i];
                    dataGainByChannel[i] /= abfReader.headerV1.fSignalGain[i];
                    dataGainByChannel[i] /= abfReader.headerV1.fADCProgrammableGain[i];
                    if (abfReader.headerV1.nTelegraphEnable[i] == 1)
                        dataGainByChannel[i] /= abfReader.headerV1.fTelegraphAdditGain[i];
                    dataGainByChannel[i] *= abfReader.headerV1.fADCRange;
                    dataGainByChannel[i] /= abfReader.headerV1.lADCResolution;
                    dataOffsetByChannel[i] = 0;
                    dataOffsetByChannel[i] += abfReader.headerV1.fInstrumentOffset[i];
                    dataOffsetByChannel[i] -= abfReader.headerV1.fSignalOffset[i];
                }

            }
            else if (abfReader.fileSignature == "ABF2")
            {
                // pull header information locally
                abfVersionMajor = 2;

                // ABF info
                holdingCommand = abfReader.dacSection.GetHoldByChannel();
                tagComments = abfReader.tagSection.GetTagComments();
                tagTimesSec = abfReader.tagSection.GetTagTimes(abfReader.protocolSection.fSynchTimeUnit/1e6);

                // DATA info
                nDataFormat = (short)abfReader.headerV2.nDataFormat;
                dataByteStart = abfReader.sectionMap.Data.byteStart;
                dataPointCount = abfReader.sectionMap.Data.itemCount;
                dataPointByteSize = abfReader.sectionMap.Data.itemSize;
                channelCount = abfReader.sectionMap.ADC.itemCount;
                dataRate = (int)(1e6/(double)abfReader.protocolSection.fADCSequenceInterval);
                dataPointsPerMs = dataRate / 1000;
                dataSecPerPoint = 1 / (double)dataRate;
                sweepCount = (int)abfReader.headerV2.lActualEpisodes;

                // channels and units (requires indexed strings)
                adcUnits = new string[channelCount];
                adcNames = new string[channelCount];
                dacUnits = new string[channelCount];
                dacNames = new string[channelCount];

                // determine gain and offset for each channel
                dataGainByChannel = new double[channelCount];
                dataOffsetByChannel = new double[channelCount];
                for (int i = 0; i < channelCount; i++)
                {
                    dataGainByChannel[i] = 1;
                    dataGainByChannel[i] /= abfReader.adcSection.ADCsections[i].fInstrumentScaleFactor;
                    dataGainByChannel[i] /= abfReader.adcSection.ADCsections[i].fSignalGain;
                    dataGainByChannel[i] /= abfReader.adcSection.ADCsections[i].fADCProgrammableGain;
                    if (abfReader.adcSection.ADCsections[i].nTelegraphEnable == 1)
                        dataGainByChannel[i] /= abfReader.adcSection.ADCsections[i].fTelegraphAdditGain;
                    dataGainByChannel[i] *= abfReader.protocolSection.fADCRange;
                    dataGainByChannel[i] /= abfReader.protocolSection.lADCResolution;
                    dataOffsetByChannel[i] = 0;
                    dataOffsetByChannel[i] += abfReader.adcSection.ADCsections[i].fInstrumentOffset;
                    dataOffsetByChannel[i] -= abfReader.adcSection.ADCsections[i].fSignalOffset;
                }

            } else
            {
                log.Critical("Unrecognized file format");
                return;
            }           

        }

        /// <summary>
        /// Return just the useful bits of header common to ABF1 and ABF2 files
        /// </summary>
        public string GetAbfInfo()
        {
            string info = "";
            info += $"abfID = {abfID}\n";
            info += $"abfVersionMajor = {abfVersionMajor}\n";
            info += $"abfFilePath = {abfFilePath}\n";
            info += $"holdingCommand = [{string.Join(", ", holdingCommand)}]\n";
            info += $"tagComments = [{string.Join(", ", tagComments)}]\n";
            info += $"tagTimesSec = [{string.Join(", ", tagTimesSec)}]\n";
            info += $"dataByteStart = {dataByteStart}\n";
            info += $"dataPointCount = {dataPointCount}\n";
            info += $"channelCount = {channelCount}\n";
            info += $"dataRate = {dataRate}\n";
            info += $"sweepCount = {sweepCount}\n";
            info += $"adcUnits = [{string.Join(", ", adcUnits)}]\n";
            info += $"adcNames = [{string.Join(", ", adcNames)}]\n";
            info += $"dacUnits = [{string.Join(", ", dacUnits)}]\n";
            info += $"dacNames = [{string.Join(", ", dacNames)}]\n";
            info += $"dataGainByChannel = [{string.Join(", ", dataGainByChannel)}]\n";
            info += $"dataOffsetByChannel = [{string.Join(", ", dataOffsetByChannel)}]\n";
            return info;
        }

        /// <summary>
        /// Return EVERYTHING we got from the header.
        /// </summary>
        public string GetHeaderInfo()
        {
            string info = "";
            if (abfVersionMajor == 1)
            {
                info += abfReader.headerV1.GetInfo();
            }
            else if (abfVersionMajor == 2)
            {
                info += abfReader.headerV2.GetInfo();
                info += abfReader.sectionMap.GetInfo();
                info += abfReader.protocolSection.GetInfo();
                info += abfReader.adcSection.GetInfo();
                info += abfReader.dacSection.GetInfo();
                info += abfReader.tagSection.GetInfo();
            }
            return info;
        }


    }
}
