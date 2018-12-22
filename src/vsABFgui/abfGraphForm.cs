using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace vsABFgui
{
    public partial class abfGraphForm : Form
    {
        public abfGraphForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string defaultAbfPath = @"C:\Users\scott\Documents\GitHub\pyABF\data\abfs\18808025.abf";
            abfGraph1.LoadABF(defaultAbfPath);
        }
    }
}
