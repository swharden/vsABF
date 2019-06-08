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
        public int tagCount;
        public readonly List<Tag> tags = new List<Tag>();

        // sweep properties
        public double[] sweepY;
        public int sweepNumber = 0;
        public int sweepChannel = 0;

        // developer flags
        public bool _invertSweepY = false;

        // internal objects
        AbffioInterface abffio;

        public ABF(string filePath)
        {
            abffio = new AbffioInterface(filePath);

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
            tagCount = abffio.header.lNumTagEntries;

            // smarter stuff
            ReadTags();

            // prepare the array to hold sweep data
            sweepY = new double[sweepPointCount];

            // load the first sweep of the first channel by default
            SetSweep(1, 0);
        }

        public void Close()
        {
            abffio.Close();
        }

        private void ReadTags()
        {
            tags.Clear();
            ABFFIOstructs.ABFTag[] abfTags = abffio.ReadTags();
            foreach (ABFFIOstructs.ABFTag abfTag in abfTags)
            {
                double timeSec = abfTag.lTagTime * abffio.header.fSynchTimeUnit / 1e6;
                string comment = new string(abfTag.sComment).Trim();
                int timeSweep = (int)(timeSec / sweepIntervalSec);
                Tag tag = new Tag(timeSec, timeSweep, comment, abfTag.nTagType);
                tags.Add(tag);
            }
        }

        /// <summary>
        /// Load data and units for the given sweep and channel. Sweeps start at 1, channels start at 0.
        /// </summary>
        public void SetSweep(int sweepNumber, int channelNumber = 0)
        {
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

        public override string ToString()
        {
            string description = string.Format("{0} with {2} channels, {3} sweeps, {4} comments, recorded at {1:0.0} kHz",
                abfFilename, sampleRate / 1000.0, channelCount, sweepCount, tagCount);
            if (tagCount == 0)
                description = description.Replace("0 comments, ", "");
            else if (tagCount == 1)
                description = description.Replace("comments", "comments");
            if (channelCount == 1)
                description = description.Replace("channels", "channel");
            if (sweepCount == 1)
                description = description.Replace("sweeps", "sweep");
            return description;
        }
    }
}
