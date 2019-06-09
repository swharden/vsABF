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
            {
                System.Diagnostics.Stopwatch stopwatch = System.Diagnostics.Stopwatch.StartNew();
                tester.TestFolder(pyAbfDataFolder);
                double elapsedMsec = stopwatch.ElapsedTicks * 1000.0 / System.Diagnostics.Stopwatch.Frequency;
                System.Console.WriteLine(string.Format("\n\nAll tests completed in {0:0.000} ms", elapsedMsec));
            }

            Console.WriteLine("\npress ENTER to exit...");
            Console.ReadLine();

        }
    }
}
