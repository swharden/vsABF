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
        public Logging log = new Logging();

        public ABF(string abfFilePath)
        {
            var abf = new ABFFIO(abfFilePath);
        }
    }
}
