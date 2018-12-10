using System;
using System.Windows.Forms;

namespace AxonDLL
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //string abfFilePath = @"C:\Users\scott\Documents\GitHub\pyABF\data\abfs\180415_aaron_temp.abf";
            //string abfFilePath = @"C:\Users\scott\Documents\GitHub\pyABF\data\abfs\171117_HFMixFRET.abf";
            //string abfFilePath = @"C:\Users\scott\Documents\GitHub\pyABF\data\abfs\14o08011_ic_pair.abf";
            //string abfFilePath = @"C:\Users\scott\Documents\GitHub\pyABF\data\abfs\171116sh_0013.abf";
            //string abfFilePath = @"C:\Users\scott\Documents\GitHub\pyABF\data\abfs\16d22006_kim_gapfree.abf";

            OpenFileDialog diag = new OpenFileDialog();
            diag.Filter = "ABF files (*.abf)|*.abf";
            if (diag.ShowDialog() == DialogResult.OK)
            {
                var abf = new AbfReader(diag.FileName);
                richTextBox1.Text = abf.logString;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            button1_Click(null, null);
        }
    }
}