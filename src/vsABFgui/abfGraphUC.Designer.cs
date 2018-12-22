namespace vsABFgui
{
    partial class abfGraphUC
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(abfGraphUC));
            this.scottPlotUC1 = new ScottPlot.ScottPlotUC();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnSetFile = new System.Windows.Forms.ToolStripButton();
            this.comboFilename = new System.Windows.Forms.ToolStripComboBox();
            this.btnFilePrev = new System.Windows.Forms.ToolStripButton();
            this.btnFileNext = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.comboChannel = new System.Windows.Forms.ToolStripComboBox();
            this.btnChannelPrev = new System.Windows.Forms.ToolStripButton();
            this.btnChannelNext = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel4 = new System.Windows.Forms.ToolStripLabel();
            this.comboSweep = new System.Windows.Forms.ToolStripComboBox();
            this.btnSweepPrev = new System.Windows.Forms.ToolStripButton();
            this.btnSweepNext = new System.Windows.Forms.ToolStripButton();
            this.tableLayoutPanel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // scottPlotUC1
            // 
            this.scottPlotUC1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scottPlotUC1.Location = new System.Drawing.Point(2, 27);
            this.scottPlotUC1.Margin = new System.Windows.Forms.Padding(2);
            this.scottPlotUC1.Name = "scottPlotUC1";
            this.scottPlotUC1.Size = new System.Drawing.Size(697, 331);
            this.scottPlotUC1.TabIndex = 0;
            this.scottPlotUC1.Load += new System.EventHandler(this.scottPlotUC1_Load);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.toolStrip1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.scottPlotUC1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(701, 360);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnSetFile,
            this.comboFilename,
            this.btnFilePrev,
            this.btnFileNext,
            this.toolStripSeparator1,
            this.toolStripLabel3,
            this.comboChannel,
            this.btnChannelPrev,
            this.btnChannelNext,
            this.toolStripSeparator3,
            this.toolStripLabel4,
            this.comboSweep,
            this.btnSweepPrev,
            this.btnSweepNext});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(701, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnSetFile
            // 
            this.btnSetFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSetFile.Image = ((System.Drawing.Image)(resources.GetObject("btnSetFile.Image")));
            this.btnSetFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSetFile.Name = "btnSetFile";
            this.btnSetFile.Size = new System.Drawing.Size(23, 22);
            this.btnSetFile.Text = "toolStripButton7";
            this.btnSetFile.Click += new System.EventHandler(this.btnSetFile_Click);
            // 
            // comboFilename
            // 
            this.comboFilename.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboFilename.Name = "comboFilename";
            this.comboFilename.Size = new System.Drawing.Size(121, 25);
            this.comboFilename.SelectedIndexChanged += new System.EventHandler(this.comboFilename_SelectedIndexChanged);
            // 
            // btnFilePrev
            // 
            this.btnFilePrev.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnFilePrev.Image = ((System.Drawing.Image)(resources.GetObject("btnFilePrev.Image")));
            this.btnFilePrev.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnFilePrev.Name = "btnFilePrev";
            this.btnFilePrev.Size = new System.Drawing.Size(23, 22);
            this.btnFilePrev.Text = "toolStripButton1";
            this.btnFilePrev.Click += new System.EventHandler(this.btnFilePrev_Click);
            // 
            // btnFileNext
            // 
            this.btnFileNext.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnFileNext.Image = ((System.Drawing.Image)(resources.GetObject("btnFileNext.Image")));
            this.btnFileNext.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnFileNext.Name = "btnFileNext";
            this.btnFileNext.Size = new System.Drawing.Size(23, 22);
            this.btnFileNext.Text = "toolStripButton2";
            this.btnFileNext.Click += new System.EventHandler(this.btnFileNext_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel3
            // 
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.Size = new System.Drawing.Size(54, 22);
            this.toolStripLabel3.Text = "Channel:";
            // 
            // comboChannel
            // 
            this.comboChannel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboChannel.Name = "comboChannel";
            this.comboChannel.Size = new System.Drawing.Size(75, 25);
            this.comboChannel.SelectedIndexChanged += new System.EventHandler(this.comboChannel_SelectedIndexChanged);
            // 
            // btnChannelPrev
            // 
            this.btnChannelPrev.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnChannelPrev.Image = ((System.Drawing.Image)(resources.GetObject("btnChannelPrev.Image")));
            this.btnChannelPrev.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnChannelPrev.Name = "btnChannelPrev";
            this.btnChannelPrev.Size = new System.Drawing.Size(23, 22);
            this.btnChannelPrev.Text = "toolStripButton4";
            this.btnChannelPrev.Click += new System.EventHandler(this.btnChannelPrev_Click);
            // 
            // btnChannelNext
            // 
            this.btnChannelNext.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnChannelNext.Image = ((System.Drawing.Image)(resources.GetObject("btnChannelNext.Image")));
            this.btnChannelNext.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnChannelNext.Name = "btnChannelNext";
            this.btnChannelNext.Size = new System.Drawing.Size(23, 22);
            this.btnChannelNext.Text = "toolStripButton3";
            this.btnChannelNext.Click += new System.EventHandler(this.btnChannelNext_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel4
            // 
            this.toolStripLabel4.Name = "toolStripLabel4";
            this.toolStripLabel4.Size = new System.Drawing.Size(44, 22);
            this.toolStripLabel4.Text = "Sweep:";
            // 
            // comboSweep
            // 
            this.comboSweep.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboSweep.Name = "comboSweep";
            this.comboSweep.Size = new System.Drawing.Size(75, 25);
            this.comboSweep.SelectedIndexChanged += new System.EventHandler(this.comboSweep_SelectedIndexChanged);
            // 
            // btnSweepPrev
            // 
            this.btnSweepPrev.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSweepPrev.Image = ((System.Drawing.Image)(resources.GetObject("btnSweepPrev.Image")));
            this.btnSweepPrev.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSweepPrev.Name = "btnSweepPrev";
            this.btnSweepPrev.Size = new System.Drawing.Size(23, 22);
            this.btnSweepPrev.Text = "toolStripButton5";
            this.btnSweepPrev.Click += new System.EventHandler(this.btnSweepPrev_Click);
            // 
            // btnSweepNext
            // 
            this.btnSweepNext.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSweepNext.Image = ((System.Drawing.Image)(resources.GetObject("btnSweepNext.Image")));
            this.btnSweepNext.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSweepNext.Name = "btnSweepNext";
            this.btnSweepNext.Size = new System.Drawing.Size(23, 22);
            this.btnSweepNext.Text = "toolStripButton6";
            this.btnSweepNext.Click += new System.EventHandler(this.btnSweepNext_Click);
            // 
            // abfGraphUC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "abfGraphUC";
            this.Size = new System.Drawing.Size(701, 360);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private ScottPlot.ScottPlotUC scottPlotUC1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnFilePrev;
        private System.Windows.Forms.ToolStripButton btnFileNext;
        private System.Windows.Forms.ToolStripComboBox comboFilename;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel3;
        private System.Windows.Forms.ToolStripButton btnChannelPrev;
        private System.Windows.Forms.ToolStripButton btnChannelNext;
        private System.Windows.Forms.ToolStripComboBox comboChannel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripLabel toolStripLabel4;
        private System.Windows.Forms.ToolStripComboBox comboSweep;
        private System.Windows.Forms.ToolStripButton btnSweepPrev;
        private System.Windows.Forms.ToolStripButton btnSweepNext;
        private System.Windows.Forms.ToolStripButton btnSetFile;
    }
}
