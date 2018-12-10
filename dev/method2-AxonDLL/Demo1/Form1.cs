using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Demo1
{
    public partial class Form1 : Form
    {
        private string currentFolder = null;
        private string currentFile = null;

        public Form1()
        {
            InitializeComponent();
            SetFolder(@"C:\Users\scott\Documents\GitHub\pyABF\data\abfs");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            scottPlotUC1.fig.labelTitle = "C# ABFFIO.DLL DEMO";
            scottPlotUC1.AxisAuto();
        }

        private void SetFolder(string path)
        {
            if (!Directory.Exists(path))
                return;
            currentFolder = path;
            listBox1.Items.Clear();
            string[] abfFiles = Directory.GetFiles(path, "*.abf");
            foreach (string abfFile in abfFiles)
                listBox1.Items.Add(Path.GetFileName(abfFile));
        }

        private AxonDLL.AbfReader abf;
        private void SetFile(string abfFilename)
        {
            string abfFilePath = Path.Combine(currentFolder, abfFilename);
            if (!File.Exists(abfFilePath))
                return;

            // update titles
            this.Text = abfFilename;
            scottPlotUC1.fig.labelTitle = abfFilename;

            // load the ABF
            abf = new AxonDLL.AbfReader(abfFilePath);
            richTextBox1.Text = abf.logString;

            // set the NUD boxes and labels
            lblChannel.Text = $"{abf.channelCount} channels:";
            nudChannel.Minimum = 0;
            nudChannel.Maximum = abf.channelCount-1;
            lblSweep.Text = $"{abf.sweepCount} sweeps:";
            nudSweep.Minimum = 1;
            nudSweep.Maximum = abf.sweepCount;

            PlotSweep((int)nudSweep.Value, (int)nudChannel.Value);
        }

        private void PlotSweep(int sweepNumber, int channelNumber)
        {
            // plot the data
            if (abf == null)
                return;
            scottPlotUC1.Clear();
            scottPlotUC1.PlotSignal(abf.GetSweepDouble(sweepNumber, channelNumber), 20000);
            scottPlotUC1.AxisAuto();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var diag = new FolderBrowserDialog();
            if (diag.ShowDialog() == DialogResult.OK)
            {
                string selectedPath = diag.SelectedPath;
                SetFolder(selectedPath);
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                SetFile(listBox1.SelectedItem.ToString());
            } catch
            {
                Console.WriteLine("crash");
            }
            
        }

        private void nudChannel_ValueChanged(object sender, EventArgs e)
        {
            PlotSweep((int)nudSweep.Value, (int)nudChannel.Value);
        }

        private void nudSweep_ValueChanged(object sender, EventArgs e)
        {
            PlotSweep((int)nudSweep.Value, (int)nudChannel.Value);
        }
    }
}
