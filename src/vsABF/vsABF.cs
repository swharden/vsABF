using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace vsABF
{
    public class ABF
    {

        // ABF properties
        public int sweepCount;
        public int channelCount;
        public int sweepPointCount;
        public int sampleRate;
        public double sweepIntervalSec;

        // sweep properties
        public double[] sweepY;
        public int sweepNumber = 0;
        public int sweepChannel = 0;

        // developer flags
        public bool _invertSweepY = false;

        // internal objects
        private Logging log = new Logging(LogLevel.INFO);
        ABFFIO abffio;

        public ABF(string abfFilePath)
        {
            log.Debug($"loading {abfFilePath}");
            abffio = new ABFFIO(abfFilePath);

            // set local variables based on the ABF
            sweepCount = abffio.header.lActualEpisodes;
            channelCount = abffio.header.nADCNumChannels;
            sweepPointCount = abffio.header.lNumSamplesPerEpisode / channelCount;
            sampleRate = (int)(1e6 / abffio.header.fADCSequenceInterval);
            sweepIntervalSec = abffio.header.fEpisodeStartToStart;
            if (sweepIntervalSec == 0)
                sweepIntervalSec = (double)sweepPointCount / sampleRate;
            Console.WriteLine("SWEEP INTERVAL: {sweepInterval}");

            // prepare the array to hold sweep data
            sweepY = new double[sweepPointCount];

            // load the first sweep of the first channel by default
            SetSweep(1, 0);

            log.Debug($"initialization complete");
        }

        public void Close()
        {
            abffio.Close();
        }

        /// <summary>
        /// Load data and units for the given sweep and channel. Sweeps start at 1, channels start at 0.
        /// </summary>
        public void SetSweep(int sweepNumber, int channelNumber = 0)
        {
            log.Debug($"setting channel {channelNumber} sweep {sweepNumber}");
            var dataFloat = abffio.ReadChannel(sweepNumber, channelNumber);
            this.sweepNumber = sweepNumber;
            this.sweepChannel = channelNumber;

            // todo: move this to inside abffio
            for (int i = 0; i < sweepPointCount; i++)
            {
                sweepY[i] = dataFloat[i];
                if (_invertSweepY)
                    sweepY[i] = -sweepY[i];
            }
        }
    }
}
