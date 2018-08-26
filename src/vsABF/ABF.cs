using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vsABF
{
    public class ABF
    {
        public ABF(string abfFilePath="./someFile.abf")
        {
            abfFilePath = System.IO.Path.GetFullPath(abfFilePath);
            System.Console.WriteLine($"loading ABF File: {abfFilePath}");
        }
    }
}
