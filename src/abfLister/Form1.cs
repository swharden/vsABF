using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using vsABF;

namespace abfLister
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        
        private void btnSelectFolder_Click(object sender, EventArgs e)
        {
            //string abfFolder = @"C:\abfs";
            var diag = new FolderBrowserDialog();
            if (diag.ShowDialog() == DialogResult.OK)
            {
                dataGridView1.DataSource = AbfTable(diag.SelectedPath);
                label1.Text = diag.SelectedPath;
            }            
        }

        public DataTable AbfTable(string abfFolder)
        {
            if (!System.IO.Directory.Exists(abfFolder)){
                return null;
            }

            DataTable table = new DataTable();
            table.Columns.Add("abfID", typeof(string));
            table.Columns.Add("path", typeof(string));
            table.Columns.Add("protocol", typeof(string));
            table.Columns.Add("sweepCount", typeof(int));
            table.Columns.Add("channelCount", typeof(int));
            table.Columns.Add("lengthSec", typeof(double));
            table.Columns.Add("units", typeof(string));
            table.Columns.Add("tags", typeof(string));

            string[] filePaths = System.IO.Directory.GetFiles(abfFolder, "*.abf");
            foreach (string filePath in filePaths)
            {
                ABF abf = new ABF(filePath, false);
                string protocol = System.IO.Path.GetFileNameWithoutExtension(abf.protocolPath);
                double lengthSec = abf.dataPointCount / abf.dataRate;
                string units = String.Join(", ", abf.adcUnits);
                string tags = String.Join(", ", abf.tagComments);
                table.Rows.Add(abf.abfID, abf.abfFilePath, protocol, abf.sweepCount, abf.channelCount, lengthSec, units, tags);
            }

            return table;

        }
    }
}
