using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vsABF
{

    public struct AbfTag
    {
        double sweep;
        double timeMin;
        double timeSec;
        int position;
        int tagType;
        string comment;
    }

    public class AbfVersion
    {
        int major;
        int minor;
        int bugfix;

        public override string ToString()
        {
            return $"{major}.{minor}.{bugfix}";
        }
    }

    public class AbfInfo
    {
        // ABF info which never changes as sweeps/channels change

        DateTime dateTime;

        string filePath;
        string fileName;
        string id;
        AbfVersion version;
        double fileSizeMb;
        int channelCount;
        int sweepCount;
        string protocol;
        string protocolPath;
        AbfTag[] tags;
    }
}
