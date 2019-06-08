using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vsAbfTester
{
    class Tester
    {
        public Tester()
        {

        }

        public void testAbf(string abfFilePath)
        {
            var abf = new vsABF.ABF(abfFilePath);
            Console.WriteLine(abf);
            abf.Close();
        }

        public void testFolder(string abfFolderPath)
        {
            foreach (string abfFilePath in System.IO.Directory.GetFiles(abfFolderPath, "*.abf"))
                testAbf(abfFilePath);
        }
    }
}
