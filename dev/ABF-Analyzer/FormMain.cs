using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ABF_Analyzer
{
    public partial class FormMain : Form
    {
        private vsABF.ABF abf;

        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            GuiLoadAbf(@"D:\demoData\abfs-pyabf\17o05026_vc_stim.abf");
        }

        private void ScottPlotUC1_Load(object sender, EventArgs e)
        {

        }

        private void GuiLoadAbf(string abfFileName)
        {
            abf = new vsABF.ABF(abfFileName);
            scottPlotUC1.plt.Clear();
            scottPlotUC1.plt.PlotSignal(abf.sweepY, abf.sampleRate, markerSize: 0);
            scottPlotUC1.plt.Title(abf.abfFilename);
            scottPlotUC1.plt.YLabel($"Channel {abf.sweepChannel} (pA)");
            scottPlotUC1.plt.XLabel($"Time (milliseconds)");
            scottPlotUC1.plt.AxisAuto(0, .1);
            scottPlotUC1.Render();
        }

        private void BtnAnalyzeSweep_Click(object sender, EventArgs e)
        {

            var detectionSettings = detectionParamatersUC1.GetSettings();
            var events = Detector.Psc(abf.sweepY, abf.sampleRate, detectionSettings);
            Console.WriteLine($"Detected {events.Count} events");

            foreach (var evnt in events)
            {
                double eventTimeSec = evnt.peakI / (double)abf.sampleRate;
                scottPlotUC1.plt.PlotPoint(eventTimeSec, abf.sweepY[evnt.peakI], color: Color.Red);

                double baselineTimeSec1 = evnt.baselineStartI / (double)abf.sampleRate;
                double baselineTimeSec2 = evnt.baselineEndI / (double)abf.sampleRate;
                double[] xs = new double[] { baselineTimeSec1, baselineTimeSec2 };
                double[] ys = new double[] { evnt.baselineLevel, evnt.baselineLevel };
                scottPlotUC1.plt.PlotScatter(xs, ys, color: Color.DarkGreen, markerSize: 0, lineWidth: 2);
            }

            scottPlotUC1.Render();
        }
    }
}
