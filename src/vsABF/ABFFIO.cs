/*
 * Code here interacts with ABFFIO.DLL so you don't have to!
 * This is a minimal wrapper with only the most core functions 
 * in ABFFIO.DLL exposed.
 * 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace vsABF
{
    public class ABFFIO
    {

        public Logging log = new Logging(LogLevel.INFO);
        public bool validAbfFile = false;

        private UInt32 sweepPointCount;
        private string abfFilePath;

        public ABFFIOstructs.ABFFileHeader header = new ABFFIOstructs.ABFFileHeader();

        public ABFFIO(string abfFilePath)
        {

            // clean-up the file path
            if (System.IO.File.Exists(abfFilePath))
            {
                this.abfFilePath = System.IO.Path.GetFullPath(abfFilePath);
            }
            else
            {
                log.Critical($"file does not exist: {abfFilePath}");
                return;
            }

            // prepare a stopwatch for benchmarking
            System.Diagnostics.Stopwatch stopwatch = System.Diagnostics.Stopwatch.StartNew();

            // automatically open the given ABF file
            ReadOpen(abfFilePath);

            // read the first sweep of the first channel
            ReadChannel(1, 0);

            // benchmark to this point
            double timeMS = stopwatch.ElapsedTicks * 1000.0 / System.Diagnostics.Stopwatch.Frequency;
            log.Debug(string.Format("ABFFIO initialization completed in {0:0.00} ms", timeMS));

            // public indication the ABF loaded properly
            validAbfFile = true;
        }

        [DllImport("ABFFIO.dll", CharSet = CharSet.Ansi)]
        private static extern bool ABF_IsABFFile(String szFileName, ref Int32 pnDataFormat, ref Int32 pnError);

        [DllImport("ABFFIO.dll", CharSet = CharSet.Ansi)]
        private static extern bool ABF_ReadOpen(String szFileName, ref Int32 phFile, UInt32 fFlags, ref ABFFIOstructs.ABFFileHeader pFH, ref UInt32 puMaxSamples, ref UInt32 pdwMaxEpi, ref Int32 pnError);

        [DllImport("ABFFIO.dll", CharSet = CharSet.Ansi)]
        private static extern bool ABF_ReadChannel(Int32 nFile, ref ABFFIOstructs.ABFFileHeader pFH, Int32 nChannel, Int32 dwEpisode, ref float pfBuffer, ref UInt32 puNumSamples, ref Int32 pnError);

        [DllImport("ABFFIO.dll", CharSet = CharSet.Ansi)]
        private static extern bool ABF_Close(Int32 nFile, ref Int32 pnError);

        [DllImport("ABFFIO.dll", CharSet = CharSet.Ansi)]
        private static extern bool ABF_ReadTags(Int32 nFile, ref ABFFIOstructs.ABFFileHeader pFH, UInt32 dwFirstTag, ref ABFFIOstructs.ABFTag pTagArray, UInt32 uNumTags, ref Int32 pnError);

        [DllImport("ABFFIO.dll", CharSet = CharSet.Ansi)]
        private static extern bool ABF_ReadTags(Int32 nFile, ref ABFFIOstructs.ABFFileHeader pFH, UInt32 dwFirstTag, ref ABFFIOstructs.ABFTag[] pTagArray, UInt32 uNumTags, ref Int32 pnError);

        public void Close()
        {
            Int32 fileHandle = 0;
            Int32 errorCode = 0;
            ABF_Close(fileHandle, ref errorCode);
            log.Debug($"ABF file closed.");
        }

        public bool IsABFFile()
        {
            Int32 dataFormat = 0;
            Int32 errorCode = 0;

            log.Debug($"Calling ABFFIO.DLL ABF_IsABFFile()");
            ABF_IsABFFile(abfFilePath, ref dataFormat, ref errorCode);
            ProcessErrorCode(errorCode);

            if (errorCode == 0)
                return true;
            else
                return false;
        }

        private void ReadOpen(string abfFilePath)
        {
            Int32 fileHandle = 0;
            UInt32 sweepPointCount = 0;
            UInt32 sweepCount = 0;
            Int32 errorCode = 0;
            uint loadFlags = 0;

            log.Debug($"Calling ABFFIO.DLL ABF_ReadOpen()");
            ABF_ReadOpen(abfFilePath, ref fileHandle, loadFlags, ref header, ref sweepPointCount, ref sweepCount, ref errorCode);
            ProcessErrorCode(errorCode);

            this.sweepPointCount = sweepPointCount;
        }

        public float[] ReadChannel(int sweepNumber, int channelNumber)
        {

            float[] sweepBuffer = new float[sweepPointCount];
            int physicalChannel = header.nADCSamplingSeq[channelNumber];

            Int32 errorCode = 0;
            Int32 fileHandle = 0;

            log.Debug($"Calling ABFFIO.DLL ABF_ReadOpen()");
            ABF_ReadChannel(fileHandle, ref header, physicalChannel, sweepNumber, ref sweepBuffer[0], ref sweepPointCount, ref errorCode);
            ProcessErrorCode(errorCode);

            string desc = $"Ch {channelNumber} ({physicalChannel}) Sweep {sweepNumber}";
            string vals = $"{sweepBuffer[0]}, {sweepBuffer[1]}, {sweepBuffer[2]}, ... , {sweepBuffer[sweepBuffer.Length - 2]}, {sweepBuffer[sweepBuffer.Length - 1]}";
            log.Debug($"{desc}: {vals}");

            return sweepBuffer;
        }

        public ABFFIOstructs.ABFTag[] ReadTags()
        {
            Int32 fileHandle = 0;
            Int32 errorCode = 0;
            ABFFIOstructs.ABFTag[] abfTags = new ABFFIOstructs.ABFTag[(UInt32)header.lNumTagEntries];
            for (uint i = 0; i < abfTags.Length; i++)
                ABF_ReadTags(fileHandle, ref header, i, ref abfTags[i], 1, ref errorCode);
            return abfTags;
        }

        private void ProcessErrorCode(Int32 errorCode)
        {
            string description;
            if (errorCode == 0) description = "ABF_SUCCESS";
            else if (errorCode == 1001) description = "ABF_EUNKNOWNFILETYPE";
            else if (errorCode == 1002) description = "ABF_EBADFILEINDEX";
            else if (errorCode == 1003) description = "ABF_TOOMANYFILESOPEN";
            else if (errorCode == 1004) description = "ABF_EOPENFILE - could not open file";
            else if (errorCode == 1005) description = "ABF_EBADPARAMETERS";
            else if (errorCode == 1006) description = "ABF_EREADDATA";
            else if (errorCode == 1008) description = "ABF_OUTOFMEMORY";
            else if (errorCode == 1009) description = "ABF_EREADSYNCH";
            else if (errorCode == 1010) description = "ABF_EBADSYNCH";
            else if (errorCode == 1011) description = "ABF_EEPISODERANGE - invalid sweep number";
            else if (errorCode == 1012) description = "ABF_EINVALIDCHANNEL";
            else if (errorCode == 1013) description = "ABF_EEPISODESIZE";
            else if (errorCode == 1014) description = "ABF_EREADONLYFILE";
            else if (errorCode == 1015) description = "ABF_EDISKFULL";
            else if (errorCode == 1016) description = "ABF_ENOTAGS";
            else if (errorCode == 1017) description = "ABF_EREADTAG";
            else if (errorCode == 1018) description = "ABF_ENOSYNCHPRESENT";
            else if (errorCode == 1019) description = "ABF_EREADDACEPISODE";
            else if (errorCode == 1020) description = "ABF_ENOWAVEFORM";
            else if (errorCode == 1021) description = "ABF_EBADWAVEFORM";
            else if (errorCode == 1022) description = "ABF_BADMATHCHANNEL";
            else if (errorCode == 1023) description = "ABF_BADTEMPFILE";
            else if (errorCode == 1025) description = "ABF_NODOSFILEHANDLES";
            else if (errorCode == 1026) description = "ABF_ENOSCOPESPRESENT";
            else if (errorCode == 1027) description = "ABF_EREADSCOPECONFIG";
            else if (errorCode == 1028) description = "ABF_EBADCRC";
            else if (errorCode == 1029) description = "ABF_ENOCOMPRESSION";
            else if (errorCode == 1030) description = "ABF_EREADDELTA";
            else if (errorCode == 1031) description = "ABF_ENODELTAS";
            else if (errorCode == 1032) description = "ABF_EBADDELTAID";
            else if (errorCode == 1033) description = "ABF_EWRITEONLYFILE";
            else if (errorCode == 1034) description = "ABF_ENOSTATISTICSCONFIG";
            else if (errorCode == 1035) description = "ABF_EREADSTATISTICSCONFIG";
            else if (errorCode == 1036) description = "ABF_EWRITERAWDATAFILE";
            else if (errorCode == 1037) description = "ABF_EWRITEMATHCHANNEL";
            else if (errorCode == 1038) description = "ABF_EWRITEANNOTATION";
            else if (errorCode == 1039) description = "ABF_EREADANNOTATION";
            else if (errorCode == 1040) description = "ABF_ENOANNOTATIONS";
            else if (errorCode == 1041) description = "ABF_ECRCVALIDATIONFAILED";
            else if (errorCode == 1042) description = "ABF_EWRITESTRING";
            else if (errorCode == 1043) description = "ABF_ENOSTRINGS";
            else if (errorCode == 1044) description = "ABF_EFILECORRUPT";
            else description = "UNKNOWN";

            string errorMessage = $"error code: {errorCode} ({description})";

            if (errorCode == 0)
                log.Debug(errorMessage);
            else
                log.Critical(errorMessage);
        }
    }
}
