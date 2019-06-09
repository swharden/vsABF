/* 
 * The ABF class provides a modern .NET interface to files in the Axon Binary Format (ABF).
 * 
 * ABF access is provided by ABFFIO.DLL, but interacting with it is cumbersome, slow, and
 * causes problems like file-locking. This class has been designed to maximize speed by 
 * minimizing the interaction with this DLL. Upon instantiation the ABF class uses the DLL
 * to read all header and sweep data into memory (and the DLL is not used again).
 * 
 * All sweep data is stored in memory as a 3D double array. This doesn't take-up that much
 * memory: 1 hour of 20kHz data is just 576MB. The result is highspeed data analysis which 
 * does not depend on file-locking DLL calls just to load data from different sweeps.
 * 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace vsABF
{

    public class Sweep
    {

        private readonly AbfInfo info;

        public int number { get; private set; }
        public int channel { get; private set; }

        public readonly int length;
        public readonly double lengthSec;
        public readonly double lengthMin;
        public readonly double intervalSec;
        public readonly double intervalMin;
        public readonly double[] values;

        public Sweep(AbfInfo info)
        {
            this.info = info;
            length = info.sweepLengthPoints;
            lengthSec = info.sweepLengthSec;
            lengthMin = info.sweepLengthMin;
            intervalSec = info.sweepIntervalSec;
            intervalMin = info.sweepIntervalMin;
            values = new double[info.sweepLengthPoints];
        }
    }

    public class ABF : IDisposable
    {

        private AbffioInterface abffio;
        public AbfInfo info;
        public Sweep sweep;
        public double[,,] data; // sweep, position, channel
        private bool[,] dataLoaded; // sweep, channel

        public ABF(string filePath, bool preLoadSweepData = true)
        {
            abffio = new AbffioInterface(filePath);
            info = new AbfInfo(filePath, abffio.header);
            sweep = new Sweep(info);
            ReadTags();

            if (preLoadSweepData)
            {
                LoadAllSweeps();
                SetSweep(0, 0);
            }

        }

        public void Dispose()
        {
            Close();
        }

        public override string ToString()
        {
            return $"ABF ({info.fileName}) set to sweep X of {info.sweepCount}";
        }

        public void Close()
        {
            abffio.Close();
        }

        private void ReadTags()
        {
            // populates info.tags from data retrieved from abffio module
            ABFFIOstructs.ABFTag[] abfTags = abffio.ReadTags();
            for (int i = 0; i < abfTags.Length; i++)
            {
                ABFFIOstructs.ABFTag abfTag = abfTags[i];
                double timeSec = abfTag.lTagTime * abffio.header.fSynchTimeUnit / 1e6;
                string comment = new string(abfTag.sComment).Trim();
                int timeSweep = (int)(timeSec / info.sweepIntervalSec);
                Tag tag = new Tag(timeSec, timeSweep, comment, abfTag.nTagType);
                info.tags[i] = tag;
            }
        }

        private void EnsureDataArrayExists()
        {
            if (data == null)
            {
                data = new double[info.sweepCount, info.sweepLengthPoints, info.channelCount];
                dataLoaded = new bool[info.sweepCount, info.channelCount];
            }
        }

        private void LoadSweep(int sweep, int channel = 0)
        {
            EnsureDataArrayExists();
            if (!dataLoaded[sweep, channel])
            {
                abffio.ReadChannel(sweep + 1, channel);
                for (int i = 0; i < info.sweepLengthPoints; i++)
                    data[sweep, i, channel] = abffio.sweepBuffer[i];
                dataLoaded[sweep, channel] = true;
            }
        }

        public void LoadAllSweeps()
        {
            EnsureDataArrayExists();
            for (int channel = 0; channel < info.channelCount; channel++)
                for (int sweep = 0; sweep < info.sweepCount; sweep++)
                    LoadSweep(sweep, channel);
        }

        public void SetSweep(int sweepNumber = 0, int channelNumber = 0)
        {
            EnsureDataArrayExists();
            if (!dataLoaded[sweepNumber, channelNumber])
                LoadSweep(sweepNumber, channelNumber);

            for (int i = 0; i < info.sweepLengthPoints; i++)
                sweep.values[i] = data[sweepNumber, i, channelNumber];
        }
    }
}
