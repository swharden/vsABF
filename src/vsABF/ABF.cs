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
        public Logger log = new Logger("ABF");
        public string abfFilePath;
        private BinaryReader br;

        /// <summary>
        /// The ABF class provides simple access to ABF header and signal data.
        /// </summary>
        /// <param name="abfFilePath">path to an ABF file</param>
        public ABF(string abfFilePath="./someFile.abf")
        {
            this.abfFilePath = System.IO.Path.GetFullPath(abfFilePath);
            log.Info($"loading ABF File: {this.abfFilePath}");

            // ensure the ABF is valid and determine if it is ABF1 or ABF2 format
            FileOpen();
            string firstFewStr = FileReadString("", 0, 4);
            if (firstFewStr=="ABF ")
            {
                log.Debug("ABF format 1 detected");
                ReadHeaderV1();
            } else if (firstFewStr == "ABF2")
            {
                return;
                log.Debug("ABF format 2 detected");
            } else
            {
                log.Critical("file is not in ABF format");
            }
            FileClose();
        }

        private void FileOpen()
        {
            log.Debug("opening ABF for reading");
            br = new BinaryReader(File.Open(this.abfFilePath, FileMode.Open));
        }

        private void FileClose()
        {
            log.Debug("closing ABF for reading");
            br.Close();
        }

    }
}
