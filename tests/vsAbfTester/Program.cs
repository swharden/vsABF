using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vsAbfTester
{
    class Program
    {
        static void Main(string[] args)
        {
            Tester tester = new Tester();

            string pyAbfDataFolder = @"C:\Users\scott\Documents\GitHub\pyABF\data\abfs";
            if (System.IO.Directory.Exists(pyAbfDataFolder))
                tester.testFolder(pyAbfDataFolder);

            Console.WriteLine("\npress ENTER to exit...");
            Console.ReadLine();

        }
    }
}
