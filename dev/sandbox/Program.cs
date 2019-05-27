using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sandbox
{
    class Program
    {
        static void Main(string[] args)
        {
            vsABF.ABF abf = new vsABF.ABF(@"D:\demoData\abfs-pyabf\16d05007_vc_tags.abf");
            foreach (vsABF.Tag tag in abf.tags)
                Console.WriteLine(tag);
        }
    }
}
