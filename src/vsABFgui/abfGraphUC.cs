using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace vsABFgui
{
    public partial class abfGraphUC : UserControl
    {
        public vsABF.ABF abf;
        public string currentFolder = null;
        public string currentFilename = null;

        public string currentFilePath
        {
            get
            {
                if (currentFolder == null || currentFilename == null)
                    return null;
                else
                    return Path.Combine(currentFolder, currentFilename);
            }
        }

        public abfGraphUC()
        {
            InitializeComponent();
            GuiReset();
        }

        public void GuiReset()
        {
            comboSweep.Items.Clear();
            comboSweep.Text = "";
            comboChannel.Items.Clear();
            comboChannel.Text = "";
            comboFilename.Items.Clear();
            comboFilename.Text = "";
        }

        // load a sweep using the single sweep view
        public void SetSweep(int sweep, int channel)
        {
            if (sweep < 1)
                return;
            if (sweep > abf.sweepCount)
                return;

            abf.SetSweep(sweep, channel);
            scottPlotUC1.PlotSignal(abf.sweepY, abf.sampleRate);

            // sweep info and limits
            comboSweep.Text = sweep.ToString();
            btnSweepPrev.Enabled = (sweep == 1) ? false : true;
            btnSweepNext.Enabled = (sweep == abf.sweepCount) ? false : true;

            // channel info and limits
            comboChannel.Text = channel.ToString();
            btnChannelPrev.Enabled = (channel == 0) ? false : true;
            btnChannelNext.Enabled = (channel == abf.channelCount - 1) ? false : true;
        }

        public void SetFolder(string folderPath)
        {
            currentFolder = folderPath;
            comboFilename.Items.Clear();
            string[] abfFilePaths = Directory.GetFiles(folderPath, "*.abf");
            foreach (string abfFilePath in abfFilePaths)
                comboFilename.Items.Add(Path.GetFileName(abfFilePath));
        }

        public void LoadABF(string abfFilePath)
        {
            // if an ABF is already open, close it
            if (abf != null)
                abf.Close();

            // load the ABF
            abf = new vsABF.ABF(abfFilePath);

            // update the GUI
            //GuiReset();
            comboChannel.Items.Clear();
            for (int i = 0; i < abf.channelCount; i++)
                comboChannel.Items.Add((i).ToString());
            comboSweep.Items.Clear();
            for (int i = 0; i < abf.sweepCount; i++)
                comboSweep.Items.Add((i + 1).ToString());
            if (Path.GetDirectoryName(abfFilePath) != currentFolder)
                SetFolder(Path.GetDirectoryName(abfFilePath));
            comboFilename.Text = abf.abfFilename;

            // update the plot
            scottPlotUC1.Clear();
            scottPlotUC1.fig.labelTitle = abf.abfID;
            SetSweep(1, 0);
            scottPlotUC1.AxisAuto();

        }

        private void scottPlotUC1_Load(object sender, EventArgs e)
        {
            scottPlotUC1.fig.labelTitle = "Uninitialized ABF Graph";
        }

        private void btnSetFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog diag = new OpenFileDialog();
            diag.Filter += "ABF Files (*.abf)|*.abf;";
            diag.Filter += "|ATF Files (*.atf)|*.atf;";
            if (diag.ShowDialog() == DialogResult.OK)
            {
                LoadABF(diag.FileName);
            }
        }

        private void btnSweepPrev_Click(object sender, EventArgs e)
        {
            SetSweep(abf.sweepNumber - 1, abf.sweepChannel);
        }

        private void btnSweepNext_Click(object sender, EventArgs e)
        {
            SetSweep(abf.sweepNumber + 1, abf.sweepChannel);
        }

        private void comboSweep_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (int.TryParse(comboSweep.Text, out int sweep) && int.TryParse(comboChannel.Text, out int channel))
                SetSweep(sweep, channel);
        }

        private void btnChannelPrev_Click(object sender, EventArgs e)
        {
            SetSweep(abf.sweepNumber, abf.sweepChannel - 1);
            scottPlotUC1.AxisAuto();
        }

        private void btnChannelNext_Click(object sender, EventArgs e)
        {
            SetSweep(abf.sweepNumber, abf.sweepChannel + 1);
            scottPlotUC1.AxisAuto();
        }

        private void comboChannel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (int.TryParse(comboSweep.Text, out int sweep) && int.TryParse(comboChannel.Text, out int channel))
                SetSweep(sweep, channel);
            scottPlotUC1.AxisAuto();
        }

        private void btnFilePrev_Click(object sender, EventArgs e)
        {
            string previousFilename = comboFilename.Items[comboFilename.SelectedIndex - 1].ToString();
            LoadABF(Path.Combine(currentFolder, previousFilename));
        }

        private void btnFileNext_Click(object sender, EventArgs e)
        {
            string nextFilename = comboFilename.Items[comboFilename.SelectedIndex - 1].ToString();
            LoadABF(Path.Combine(currentFolder, nextFilename));
        }

        private void comboFilename_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentFilename = comboFilename.Text;
            LoadABF(currentFilePath);
        }
    }
}
