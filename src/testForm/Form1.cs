using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using vsABF;

namespace testForm
{
    public partial class Form1 : Form
    {
        ABFdev abfDev;
        Logger log;

        public Form1()
        {
            InitializeComponent();
            abfDev = new ABFdev();
            log = new Logger("Form");
            button1_Click(null, null);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //string[] abfFilePaths = System.IO.Directory.GetFiles(abfDev.GetAbfFolder(), "*.abf");
            string[] abfFilePaths = System.IO.Directory.GetFiles(@"C:\Users\scott\Documents\GitHub\pyABF\data\abfs", "*.abf");
            foreach (string abfFilePath in abfFilePaths)
            {
                ABF abf = new ABF(abfFilePath);
                richTextBox1.Text = abf.log.logText;
                break;
            }            
            
        }
    }
}
