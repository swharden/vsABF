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

        [DllImport("ABFFIO.dll", CharSet = CharSet.Ansi)]
        private static extern bool ABF_ReadRawChannel(Int32 nFile, ref AbfStructs.ABFFileHeader pFH, Int32 nChannel, Int32 dwEpisode, ref Int16 pfBuffer, ref UInt32 puNumSamples, ref Int32 pnError);

        [DllImport("ABFFIO.dll", CharSet = CharSet.Ansi)]
        private static extern bool ABF_MultiplexRead(Int32 nFile, ref AbfStructs.ABFFileHeader pFH, Int32 dwEpisode, ref Int16 pfBuffer, ref UInt32 puNumSamples, ref Int32 pnError);

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
            ABF_ReadOpen(abfFilePath, ref hFile, loadFlags, ref header, ref sweepPointCount, ref sweepCount, ref errorCode);
            if (errorCode != 0) {
                log($">>>ERROR: {errorCode}");
                errorCode = 0;
            }
            log($"points per sweep: {sweepPointCount}");
            log($"sweeps: {sweepCount}");

            /*
            // read the first sweep of the first channel
            log($"samples per episode: {header.lNumSamplesPerEpisode}");
            uint bufferSize = (uint)(header.lNumSamplesPerEpisode / header.nADCNumChannels);
            float[] sweepBuffer = new float[bufferSize];
            log($"buffer size: {bufferSize}");

            int channel = 1;
            int sweep = 3;
            ABF_ReadChannel(hFile, ref header, channel, sweep, ref sweepBuffer[0], ref bufferSize, ref errorCode);
            if (errorCode == 0)
            {
                string vals = $"{sweepBuffer[0]}, {sweepBuffer[1]}, {sweepBuffer[2]}, ... , {sweepBuffer[sweepBuffer.Length - 2]}, {sweepBuffer[sweepBuffer.Length - 1]}";
                log($"Ch {channel} Sweep {sweep}: {vals}");
            }
            else
            {
                log($"Ch {channel} Sweep {sweep}: ERROR {errorCode}");
                errorCode = 0;
            }
            */

            // perform a multiplexed read of a given sweep
            uint bufferSize = (uint)(header.lNumSamplesPerEpisode);
            Int16[] sweepBuffer = new Int16[bufferSize];
            ABF_MultiplexRead(hFile, ref header, 1, ref sweepBuffer[0], ref bufferSize, ref errorCode);

            if (errorCode == 0)
            {
                for (int channel=0; channel< header.nADCNumChannels; channel++)
                {
                    string vals = "";
                    for (int j=0; j<20; j++)
                    {
                        vals += $"{sweepBuffer[j * header.nADCNumChannels + channel]} ";
                    }
                    log($"Channel {channel}: {vals}");
                }
            }
            else
            {
                log($"ERROR {errorCode}");
                errorCode = 0;
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