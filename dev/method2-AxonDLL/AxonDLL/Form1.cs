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
            //string abfFilePath = @"C:\Users\scott\Documents\GitHub\pyABF\data\abfs\14o08011_ic_pair.abf";
            string abfFilePath = @"C:\Users\scott\Documents\GitHub\pyABF\data\abfs\171116sh_0013.abf";
            //string abfFilePath = @"C:\Users\scott\Documents\GitHub\pyABF\data\abfs\16d22006_kim_gapfree.abf";
            
            // https://github.com/swharden/pyABF/blob/master/data/headers/171116sh_0013.md

            var abf = new AbfReader(abfFilePath);
            richTextBox1.Text = abf.logString;
            richTextBox1.SelectionStart = richTextBox1.Text.Length;
            richTextBox1.ScrollToCaret();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            button1_Click(null, null);
        }
    }
}