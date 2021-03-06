﻿using System;
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
        public bool busy = false;
        public double sweepTimeSecStart = -1;
        public double sweepTimeSecEnd = -1;
        public double sweepbaselineStart = -1;
        public double sweepbaselineEnd = -1;
        public void PlotSweep(int sweep, int channel, bool clearFirst = true, bool render = true,
                              double offsetX = 0, double offsetY = 0)
        {
            if (busy)
                return;
            if (sweep < 1)
                return;
            if (sweep > abf.sweepCount)
                return;

            Console.WriteLine($"Setting Ch: {channel} Sw: {sweep}");
            busy = true;
            abf.SetSweep(sweep, channel);

            // prepare to copy the sweep array into a local array
            int trimmedFirstIndex = 0;
            int trimmedPointCount = abf.sweepY.Length;

            // if baseline subtraction is enabled, adjust offsetY by the baseline region average
            if (sweepbaselineStart >= 0 && sweepbaselineEnd > sweepbaselineStart)
            {
                int baselineIndex1 = (int)(sweepbaselineStart * abf.sampleRate);
                baselineIndex1 = Math.Max(baselineIndex1, 0);
                int baselineIndex2 = (int)(sweepbaselineEnd * abf.sampleRate);
                baselineIndex2 = Math.Min(baselineIndex2, abf.sweepPointCount);
                double baselineSum = 0;
                for (int i = baselineIndex1; i < baselineIndex2; i++)
                    baselineSum += abf.sweepY[i];
                int baselinePointCount = baselineIndex2 - baselineIndex1;
                double baselineValue = baselineSum / baselinePointCount;
                offsetY -= baselineValue;
            }

            // if trimming enabled, trim appropriately
            if (sweepTimeSecStart >= 0 && sweepTimeSecEnd > sweepTimeSecStart)
            {
                double trimmedTimeSpanSec = sweepTimeSecEnd - sweepTimeSecStart;
                trimmedFirstIndex = (int)(sweepTimeSecStart * abf.sampleRate);
                trimmedFirstIndex = Math.Max(trimmedFirstIndex, 0);
                trimmedPointCount = (int)(trimmedTimeSpanSec * abf.sampleRate);
                trimmedPointCount = Math.Min(trimmedPointCount, abf.sweepPointCount - trimmedFirstIndex);
                offsetX += sweepTimeSecStart;
            }

            // copy data from the ABF object into the local space
            double[] sweepY = new double[trimmedPointCount];
            Array.Copy(abf.sweepY, trimmedFirstIndex, sweepY, 0, trimmedPointCount);

            // make the plot
            if (clearFirst)
                scottPlotUC1.plt.Clear();
            scottPlotUC1.plt.PlotSignal(sweepY, abf.sampleRate, xOffset: offsetX, yOffset: offsetY);
            scottPlotUC1.Render();

            // sweep info and limits
            comboSweep.Text = sweep.ToString();
            btnSweepPrev.Enabled = (sweep == 1) ? false : true;
            btnSweepNext.Enabled = (sweep == abf.sweepCount) ? false : true;

            // channel info and limits
            comboChannel.Text = channel.ToString();
            btnChannelPrev.Enabled = (channel == 0) ? false : true;
            btnChannelNext.Enabled = (channel == abf.channelCount - 1) ? false : true;

            // ensure new sweep redraws
            Application.DoEvents();
            busy = false;
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
            scottPlotUC1.plt.Clear();
            scottPlotUC1.plt.Title(abf.abfID);
            PlotSweep(1, 0);
            scottPlotUC1.plt.AxisAuto(0);
            scottPlotUC1.Render();
        }

        private void scottPlotUC1_Load(object sender, EventArgs e)
        {
            scottPlotUC1.plt.Title("Uninitialized ABF Graph");
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
            PlotSweep(abf.sweepNumber - 1, abf.sweepChannel);
        }

        private void btnSweepNext_Click(object sender, EventArgs e)
        {
            PlotSweep(abf.sweepNumber + 1, abf.sweepChannel);
        }

        private void comboSweep_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (int.TryParse(comboSweep.Text, out int sweep) && int.TryParse(comboChannel.Text, out int channel))
                PlotSweep(sweep, channel);
        }

        private void btnChannelPrev_Click(object sender, EventArgs e)
        {
            PlotSweep(abf.sweepNumber, abf.sweepChannel - 1);
            scottPlotUC1.plt.AxisAuto(0);
            scottPlotUC1.Render();
        }

        private void btnChannelNext_Click(object sender, EventArgs e)
        {
            PlotSweep(abf.sweepNumber, abf.sweepChannel + 1);
            scottPlotUC1.plt.AxisAuto(0);
            scottPlotUC1.Render();
        }

        private void comboChannel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (int.TryParse(comboSweep.Text, out int sweep) && int.TryParse(comboChannel.Text, out int channel))
                PlotSweep(sweep, channel);
            scottPlotUC1.plt.AxisAuto(0);
            scottPlotUC1.Render();
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

        private void btnViewSweep_Click(object sender, EventArgs e)
        {
            if (abf == null)
                return;
            scottPlotUC1.plt.Clear();
            PlotSweep(abf.sweepNumber, abf.sweepChannel);
        }

        public double DialogGetNumber(string text, string caption, int valMin = 0, int valMax = 1000)
        {
            Form prompt = new Form();
            prompt.Width = 350;
            prompt.Height = 150;
            prompt.Text = caption;
            Label textLabel = new Label() { Left = 10, Top = 10, Text = text };
            NumericUpDown nudVal = new NumericUpDown() { Left = 10, Top = 40, Width = 100 };
            nudVal.Minimum = valMin;
            nudVal.Maximum = valMax;
            Button btnOk = new Button() { Text = "Ok", Left = 120, Top = 40, Width = 50 };
            btnOk.Click += (sender, e) => { prompt.Close(); };
            prompt.Controls.Add(btnOk);
            prompt.Controls.Add(textLabel);
            prompt.Controls.Add(nudVal);
            prompt.ShowDialog();
            return (double)nudVal.Value;
        }

        private void btnViewStacked_Click(object sender, EventArgs e)
        {
            if (abf == null)
                return;
            scottPlotUC1.plt.Clear();
            double offsetY = DialogGetNumber("what spacing?", "spacing");
            for (int i = 0; i < abf.sweepCount; i++)
            {
                PlotSweep(i + 1, abf.sweepChannel, clearFirst: false, render: false, offsetY: offsetY * i);
            }
            scottPlotUC1.plt.AxisAuto(0);
            scottPlotUC1.Render();
        }

        private void btnViewContinuous_Click(object sender, EventArgs e)
        {
            if (abf == null)
                return;
            scottPlotUC1.plt.Clear();
            for (int i = 0; i < abf.sweepCount; i++)
            {
                PlotSweep(i + 1, abf.sweepChannel, clearFirst: false, render: false, offsetX: i * abf.sweepIntervalSec);
            }
            scottPlotUC1.plt.AxisAuto(0);
            scottPlotUC1.Render();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (busy)
                return true;
            if ((keyData == Keys.Right) || (keyData == Keys.Up))
            {
                btnSweepNext_Click(null, null);
                return true;
            }
            else if ((keyData == Keys.Left) || (keyData == Keys.Down))
            {
                btnSweepPrev_Click(null, null);
                return true;
            }
            else
            {
                return base.ProcessCmdKey(ref msg, keyData);
            }
        }

        private void btnBaseline_Click(object sender, EventArgs e)
        {
            sweepbaselineStart = DialogGetNumber("baseline start (sec)", "trim start");
            sweepbaselineEnd = DialogGetNumber("baseline end (sec)", "trim end");
            PlotSweep(1, 0);
        }

        private void btnTrim_Click(object sender, EventArgs e)
        {
            sweepTimeSecStart = DialogGetNumber("trim start (sec)", "trim start");
            sweepTimeSecEnd = DialogGetNumber("trim end (sec)", "trim end");
            PlotSweep(1, 0);
        }

        private void btnUndoSettings_Click(object sender, EventArgs e)
        {
            sweepTimeSecStart = -1;
            sweepTimeSecEnd = -1;
            sweepbaselineStart = -1;
            sweepbaselineEnd = -1;
            PlotSweep(1, 0);
        }
    }
}
