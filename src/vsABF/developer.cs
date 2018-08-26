using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vsABF
{
    public class ABFdev
    {
        public List<string> abfFileFolders;
        public ABFdev()
        {
            abfFileFolders = new List<string>();
            abfFileFolders.Add(@"C:\Users\scott\Documents\important\abfs");
            abfFileFolders.Add(@"C:\Users\scott\Documents\important\demo_ABF_folder\demoFolder");
            abfFileFolders.Add(@"C:\Users\scott\Documents\GitHub\pyABF\data\abfs");            
            abfFileFolders.Add(@"C:\abfs");
        }

        /// <summary>
        /// scan the list of pre-programmed possible ABF file locations and return the first one that exists
        /// </summary>
        public string GetAbfFolder()
        {
            foreach (string abfFolder in abfFileFolders)
            {
                if (System.IO.Directory.Exists(abfFolder))
                {
                    return abfFolder;
                }
            }
            return "./";
        }

    }
}
