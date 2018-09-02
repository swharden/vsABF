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
            //ScanAbfFolder(@"C:\Users\scott\Documents\GitHub\pyABF\data\abfs");
        }

        private class AbfPath
        {
            public string abfPath { get; set; }
            public string abfFileName { get; set; }
            public string abfID { get; set; }
        }

        private BindingList<AbfPath> abfPaths = new BindingList<AbfPath>();

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

        ABF abf;
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            abf = new ABF(listBox1.SelectedValue.ToString());
            Replot();
        }

        public void Replot()
        {
            scottPlotUC1.Clear();
            scottPlotUC1.fig.labelTitle = abf.abfID;
            scottPlotUC1.fig.labelX = "time (seconds)";
            scottPlotUC1.fig.labelY = $"{abf.adcNames[0]} ({abf.adcUnits[0]})";
            if (cbAllSweeps.Checked)
            {
                for (int sweep=0; sweep<abf.sweepCount; sweep++)
                {
                    abf.SetSweep(sweep);
                    double offsetY = (double)nudVertOffset.Value * sweep;
                    double offsetX = 0;
                    if (cbContinuous.Checked)
                        offsetX = sweep * abf.sweepPointCount / abf.dataRate;
                    scottPlotUC1.PlotSignal(abf.sweepY, abf.dataRate, Color.Blue, offsetX, offsetY);
                    Console.WriteLine(abf.sweepY[0]);
                }                
            } else
            {
                abf.SetSweep(0);
                scottPlotUC1.PlotSignal(abf.sweepY, abf.dataRate);
            }            
            scottPlotUC1.AxisAuto();
        }

        private void cbAllSweeps_CheckedChanged(object sender, EventArgs e)
        {
            if (cbAllSweeps.Checked)
            {
                cbContinuous.Enabled = true;
            } else
            {
                cbContinuous.Enabled = false;
                cbContinuous.Checked = false;
            }
            Replot();
        }

        private void nudVertOffset_ValueChanged(object sender, EventArgs e)
        {
            Replot();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var diag = new FolderBrowserDialog();
            if (diag.ShowDialog() == DialogResult.OK)
            {
                ScanAbfFolder(diag.SelectedPath);
            }
        }

        private void cbContinuous_CheckedChanged(object sender, EventArgs e)
        {
            Replot();
        }
    }
}
