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
        private BinaryReader br;
        public Logger log;

        public ABF(string abfFilePath)
        {
            log = new Logger("ABFTEST");
            log.Debug($"instantiating on: {abfFilePath}");
            br = new BinaryReader(File.Open(abfFilePath, FileMode.Open));

            // determine if ABF is ABF1 or ABF2
            var genericReader = new AbfHeaderReader(br, log);
            string fileSignature = genericReader.FileReadString("fFileSignature", 0, 4);
            if (fileSignature == "ABF ")
            {
                AbfHeaderV1 abfHeaderv1 = new AbfHeaderV1(br, log);
            }
            else if (fileSignature == "ABF2")
            {
                AbfHeaderV2 abfHeaderv2 = new AbfHeaderV2(br, log);
            }
            else
            {
                log.Critical("Invalid ABF file.");
            }
            br.Close();
        }

    }
}
