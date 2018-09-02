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
using System.IO;

namespace abfViewer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ScanAbfFolder(@"C:\Users\scott\Documents\GitHub\pyABF\data\abfs");
        }

        private class AbfPath
        {
            public string abfPath { get; set; }
            public string abfFileName { get; set; }
            public string abfID { get; set; }
        }

        private List<AbfPath> abfPaths = new List<AbfPath>();

        public void ScanAbfFolder(string abfFolder)
        {
            abfPaths.Clear();
            foreach (string abfFilePath in System.IO.Directory.GetFiles(abfFolder, "*.abf"))
            {
                AbfPath abfPath = new AbfPath()
                {
                    abfPath = abfFilePath,
                    abfFileName = Path.GetFileName(abfFilePath),
                    abfID = Path.GetFileNameWithoutExtension(abfFilePath)
                };
                abfPaths.Add(abfPath);
            }
            listBox1.DisplayMember = "abfID";
            listBox1.ValueMember = "abfPath";
            listBox1.DataSource = abfPaths;
        }
        
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ABF abf = new ABF(listBox1.SelectedValue.ToString());
            scottPlotUC1.Clear();
            scottPlotUC1.PlotSignal(abf.sweepY, abf.dataRate);
            scottPlotUC1.AxisAuto();
        }
    }
}
