using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace AxonDLL
{
    public class AbfReader
    {

        public string logString = "";
        private void log(string message)
        {
            Console.WriteLine($"[AbfReader] {message}");
            logString += message + "\n";
        }

        [DllImport("ABFFIO.dll", CharSet = CharSet.Ansi)]
        private static extern bool ABF_ReadOpen(String szFileName, ref Int32 phFile, UInt32 fFlags, ref AbfStructs.ABFFileHeader pFH, ref UInt32 puMaxSamples, ref UInt32 pdwMaxEpi, ref Int32 pnError);

        [DllImport("ABFFIO.dll", CharSet = CharSet.Ansi)]
        private static extern bool ABF_ReadChannel(Int32 nFile, ref AbfStructs.ABFFileHeader pFH, Int32 nChannel, Int32 dwEpisode, ref float pfBuffer, ref UInt32 puNumSamples, ref Int32 pnError);

        public unsafe AbfReader(string abfFilePath)
        {
            // start the timer
            log($"Loading {abfFilePath} ...");
            System.Diagnostics.Stopwatch stopwatch = System.Diagnostics.Stopwatch.StartNew();
            
            // prepare the arguments for ABF_ReadOpen() 
            AbfStructs.ABFFileHeader header = new AbfStructs.ABFFileHeader();
            Int32 hFile = 0;
            UInt32 sweepPointCount = 0;
            UInt32 sweepCount = 0;
            Int32 errorCode = 0;
            uint loadFlags = 0;
            errorCode = 0;
            ABF_ReadOpen(abfFilePath, ref hFile, loadFlags, ref header, ref sweepPointCount, ref sweepCount, ref errorCode);
            if (errorCode != 0) log($">>>ERROR: {errorCode}");
            
            // show every sweep of every channel
            float[] sweepBuffer = new float[sweepPointCount];
            int channelCount = header.nADCNumChannels;
            for (int channel = 0; channel < channelCount; channel++)
            {
                int physicalChannel = header.nADCSamplingSeq[channel];
                log($"\nChannel {channel} (phsical channel {physicalChannel}):");
                for (int sweep = 1; sweep <= sweepCount; sweep++)
                {
                    errorCode = 0;
                    ABF_ReadChannel(hFile, ref header, physicalChannel, sweep, ref sweepBuffer[0], ref sweepPointCount, ref errorCode);
                    if (errorCode != 0) log($"\n\n>>>ERROR: {errorCode}");
                    string vals = $"{sweepBuffer[0]}, {sweepBuffer[1]}, {sweepBuffer[2]}, ... , {sweepBuffer[sweepBuffer.Length - 2]}, {sweepBuffer[sweepBuffer.Length - 1]}";
                    log($"Ch {channel} Sweep {sweep}: {vals}");
                }
            }

            // finish stopwatch
            double timeMS = stopwatch.ElapsedTicks * 1000.0 / System.Diagnostics.Stopwatch.Frequency;
            log($"completed in {Math.Round(timeMS, 3)} ms");

            // display the full header
            log("\n\nFULL ABF HEADER:\n");
            log(AbfStructDisplay.headerToString(header));
        }
    }
}