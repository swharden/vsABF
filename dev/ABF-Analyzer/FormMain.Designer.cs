namespace ABF_Analyzer
{
    partial class FormMain
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.scottPlotUC1 = new ScottPlot.ScottPlotUC();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnAnalyzeSweep = new System.Windows.Forms.Button();
            this.detectionParamatersUC1 = new ABF_Analyzer.DetectionParamatersUC();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // scottPlotUC1
            // 
            this.scottPlotUC1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.scottPlotUC1.Location = new System.Drawing.Point(248, 12);
            this.scottPlotUC1.Name = "scottPlotUC1";
            this.scottPlotUC1.Size = new System.Drawing.Size(833, 427);
            this.scottPlotUC1.TabIndex = 0;
            this.scottPlotUC1.Load += new System.EventHandler(this.ScottPlotUC1_Load);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnAnalyzeSweep);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 298);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(230, 141);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Analyze";
            // 
            // btnAnalyzeSweep
            // 
            this.btnAnalyzeSweep.Location = new System.Drawing.Point(6, 24);
            this.btnAnalyzeSweep.Name = "btnAnalyzeSweep";
            this.btnAnalyzeSweep.Size = new System.Drawing.Size(75, 29);
            this.btnAnalyzeSweep.TabIndex = 0;
            this.btnAnalyzeSweep.Text = "Sweep";
            this.btnAnalyzeSweep.UseVisualStyleBackColor = true;
            this.btnAnalyzeSweep.Click += new System.EventHandler(this.BtnAnalyzeSweep_Click);
            // 
            // detectionParamatersUC1
            // 
            this.detectionParamatersUC1.Location = new System.Drawing.Point(12, 12);
            this.detectionParamatersUC1.Name = "detectionParamatersUC1";
            this.detectionParamatersUC1.Size = new System.Drawing.Size(230, 280);
            this.detectionParamatersUC1.TabIndex = 1;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1093, 451);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.detectionParamatersUC1);
            this.Controls.Add(this.scottPlotUC1);
            this.Name = "FormMain";
            this.Text = "ABF Analyzer";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ScottPlot.ScottPlotUC scottPlotUC1;
        private DetectionParamatersUC detectionParamatersUC1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnAnalyzeSweep;
    }
}

