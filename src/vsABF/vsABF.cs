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
        public string abfFilePath;
        public string abfFilename;
        public string abfID;
        public string protocol;
        public string protocolPath;

        // sweep properties
        public double[] sweepY;
        public int sweepNumber = 0;
        public int sweepChannel = 0;

        // developer flags
        public bool _invertSweepY = false;

        // internal objects
        private Logging log = new Logging(LogLevel.INFO);
        ABFFIO abffio;

        public ABF(string filePath)
        {
            log.Debug($"loading {filePath}");
            abffio = new ABFFIO(filePath);
            if (!abffio.validAbfFile)
                return;

            // set class variables based on the ABF
            sweepCount = abffio.header.lActualEpisodes;
            channelCount = abffio.header.nADCNumChannels;
            sweepPointCount = abffio.header.lNumSamplesPerEpisode / channelCount;
            sampleRate = (int)(1e6 / abffio.header.fADCSequenceInterval);
            sweepIntervalSec = abffio.header.fEpisodeStartToStart;
            if (sweepIntervalSec == 0)
                sweepIntervalSec = (double)sweepPointCount / sampleRate;
            abfFilePath = filePath;
            abfFilename = Path.GetFileName(abfFilePath);
            abfID = Path.GetFileNameWithoutExtension(abfFilePath);
            protocolPath = abffio.header.sProtocolPath;
            protocol = System.IO.Path.GetFileNameWithoutExtension(protocolPath);

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
