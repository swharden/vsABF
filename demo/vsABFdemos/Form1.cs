using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace vsABFdemos
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string abfFolder = @"C:\Users\scott\Documents\GitHub\pyABF\data\abfs";
            string abfFileName = "171116sh_0013.abf"; // voltage-clamp steps
            string abfFilePath = System.IO.Path.Combine(abfFolder, abfFileName);
            vsABF.ABF abf = new vsABF.ABF(abfFilePath);
        }
    }
}
