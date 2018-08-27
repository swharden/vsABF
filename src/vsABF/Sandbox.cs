using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vsABF
{
    class Sandbox
    {
        public static void ReadFile()
        {

        }

        public class HeaderV1
        {
            public HeaderV1()
            {
                ReadFile();
            }
        }

        public class HeaderV2
        {
            public HeaderV2()
            {

            }
        }

        public class SectionMap
        {
            public class Section
            {
                public int firstByte;
                public int itemSize;
                public int itemCount;
            }
            public Section ProtocolSection;
            public Section ADCSection;
            public Section DACSection;
        }

        public class ABFversion1
        {
            public ABFversion1()
            {

            }
        }

        public class ABFversion2
        {
            public ABFversion2()
            {

            }
        }
    }
}