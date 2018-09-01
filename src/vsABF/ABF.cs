using System;
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
        public int abfVersionMajor = 0;
        public ABFreader.HeaderV1 headerV1;
        public ABFreader.HeaderV2 headerV2;
        public ABFreader.SectionMap sectionMap;

        public ABF(string abfFilePath)
        {
            // set up our logger, paths, and ensure file exists
            log = new Logger("ABF");
            abfFilePath = Path.GetFullPath(abfFilePath);

            if (!File.Exists(abfFilePath))
            {
                log.Critical($"File does not exist: {abfFilePath}");
                return;
            }

            log.Info($"Loading ABF file: {abfFilePath}");            
            ABFreader abfReader = new ABFreader(abfFilePath, log);
            if (abfReader.fileSignature=="ABF ")
            {
                abfVersionMajor = 1;
                headerV1 = abfReader.headerV1;
            } else if (abfReader.fileSignature == "ABF2")
            {
                abfVersionMajor = 2;
                headerV2 = abfReader.headerV2;
                sectionMap = abfReader.sectionMap;
            } else
            {
                log.Critical("Unrecognized file format");
                return;
            }           

        }

        public string GetHeaderInfo()
        {
            string info = "";
            if (abfVersionMajor == 1)
            {
                info += headerV1.GetInfo();
            }
            else if (abfVersionMajor == 2)
            {
                info += headerV2.GetInfo();
                info += sectionMap.GetInfo();
            }
            return info;
        }


    }
}
