using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ABF_Analyzer
{
    public partial class DetectionParamatersUC : UserControl
    {
        public DetectionParamatersUC()
        {
            InitializeComponent();
            cbDirection.SelectedItem = cbDirection.Items[1];
        }
        private void DetectionParamatersUC_Load(object sender, EventArgs e)
        {

        }

        private void BtnDefault_Click(object sender, EventArgs e)
        {

        }

        public DetectionSettingsPsc GetSettings()
        {
            var settings = new DetectionSettingsPsc
            {
                threshold = (double)nudThreshold.Value,
                timeToPeak = (double)nudTimeToPeak.Value,
                baselineBackUp = (double)nudBaselineBackUp.Value,
                baselineDuration = (double)nudBaselineDuration.Value,
                decayFraction = (double)nudDecayFraction.Value,
                area = (double)nudArea.Value,
                upward = (cbDirection.Text == "up")
            };
            return settings;
        }

    }
}
