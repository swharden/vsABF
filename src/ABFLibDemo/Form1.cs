using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ABFLib;

namespace ABFLibDemo
{
    public partial class Form1 : Form
    {
        public ABF abf;

        public Form1()
        {
            InitializeComponent();
            button1_Click(null, null);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            LoadABF(@"C:\Users\scott\Documents\GitHub\pyABF\data\abfs\16d05007_vc_tags.abf");
            LoadABF(@"C:\Users\scott\Documents\GitHub\pyABF\data\abfs\05210017_vc_abf1.abf");
        }

        public void LoadABF(string abfFileName)
        {
            abf = new ABF(abfFileName);
            richTextBox1.Text += abf.Info();
        }
    }
}
