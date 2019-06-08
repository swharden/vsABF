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
            scottPlotUC1.plt.PlotSignal(abf.sweepY, abf.sampleRate, markerSize: 2);
            scottPlotUC1.plt.Title(abf.abfFilename);
            scottPlotUC1.plt.YLabel($"Channel {abf.sweepChannel} (pA)");
            scottPlotUC1.plt.XLabel($"Time (milliseconds)");
            scottPlotUC1.plt.AxisAuto(0, .1);
            scottPlotUC1.plt.Axis(7.0, 7.75, -100, 50);
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

        private void ApplyFilterToSweep()
        {
            System.Diagnostics.Stopwatch stopwatch = System.Diagnostics.Stopwatch.StartNew();

            // start by re-loading the current sweep to ensure it's fresh
            abf.SetSweep(abf.sweepNumber, abf.sweepChannel);
            if ((cbFilter.Checked == false) || (nudFilterMs.Value <= 0))
            {
                scottPlotUC1.Render();
                return;
            }


            // set aside memory to do this
            double[] smooth = new double[abf.sweepY.Length];

            /// create the kernal to be a shape you want

            // create a triangle kernel
            int triangleRampPoints = (int)(20 * (double)nudFilterMs.Value); // I know there are 20 points per msec
            double[] kernel = new double[triangleRampPoints * 2 - 1];
            for (int i = 0; i < triangleRampPoints; i++)
                kernel[i] = 1 + i;
            for (int i = triangleRampPoints; i < kernel.Length; i++)
                kernel[i] = kernel.Length - i;
            Console.WriteLine($"Triangle kernel size: {kernel.Length} points");

            // normalize the kernel so its sum is 1
            double kernelSum = 0;
            for (int i = 0; i < kernel.Length; i++)
                kernelSum += kernel[i];
            for (int i = 0; i < kernel.Length; i++)
                kernel[i] /= kernelSum;

            // apply the kernel manually (this would be faster with a deconvolution)
            for (int i = kernel.Length; i < abf.sweepY.Length - kernel.Length; i++)
            {
                smooth[i] = 0;
                for (int j = 0; j < kernel.Length; j++)
                    smooth[i] += kernel[j] * abf.sweepY[i + j];
            }
            // clean up the ends
            for (int i = 0; i < kernel.Length; i++)
            {
                smooth[i] = smooth[kernel.Length+1];
                smooth[smooth.Length - i - 1] = smooth[smooth.Length - kernel.Length - 2];
            }
            Array.Copy(smooth, abf.sweepY, smooth.Length);

            double elapsedMsec = stopwatch.ElapsedTicks * 1000.0 / System.Diagnostics.Stopwatch.Frequency;
            System.Console.WriteLine(string.Format("filter applied in {0:0.00} ms", elapsedMsec));

            scottPlotUC1.Render();
        }

        private void CbFilter_CheckedChanged(object sender, EventArgs e)
        {
            ApplyFilterToSweep();
        }

        private void NudFilterMs_ValueChanged(object sender, EventArgs e)
        {
            ApplyFilterToSweep();
        }
    }
}
