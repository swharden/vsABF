namespace ABF_Analyzer
{
    partial class DetectionParamatersUC
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnDefault = new System.Windows.Forms.Button();
            this.btnLoad = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.cbDirection = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.nudArea = new System.Windows.Forms.NumericUpDown();
            this.nudDecayFraction = new System.Windows.Forms.NumericUpDown();
            this.nudBaselineDuration = new System.Windows.Forms.NumericUpDown();
            this.nudBaselineBackUp = new System.Windows.Forms.NumericUpDown();
            this.nudTimeToPeak = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.nudThreshold = new System.Windows.Forms.NumericUpDown();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudArea)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDecayFraction)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBaselineDuration)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBaselineBackUp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTimeToPeak)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudThreshold)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnDefault);
            this.groupBox1.Controls.Add(this.btnLoad);
            this.groupBox1.Controls.Add(this.btnSave);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.cbDirection);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.nudArea);
            this.groupBox1.Controls.Add(this.nudDecayFraction);
            this.groupBox1.Controls.Add(this.nudBaselineDuration);
            this.groupBox1.Controls.Add(this.nudBaselineBackUp);
            this.groupBox1.Controls.Add(this.nudTimeToPeak);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.nudThreshold);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(230, 282);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Detection Parameters";
            // 
            // btnDefault
            // 
            this.btnDefault.Location = new System.Drawing.Point(9, 241);
            this.btnDefault.Name = "btnDefault";
            this.btnDefault.Size = new System.Drawing.Size(71, 30);
            this.btnDefault.TabIndex = 18;
            this.btnDefault.Text = "Default";
            this.btnDefault.UseVisualStyleBackColor = true;
            this.btnDefault.Click += new System.EventHandler(this.BtnDefault_Click);
            // 
            // btnLoad
            // 
            this.btnLoad.Enabled = false;
            this.btnLoad.Location = new System.Drawing.Point(86, 241);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(58, 30);
            this.btnLoad.TabIndex = 17;
            this.btnLoad.Text = "Load";
            this.btnLoad.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.Enabled = false;
            this.btnSave.Location = new System.Drawing.Point(150, 241);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(74, 30);
            this.btnSave.TabIndex = 16;
            this.btnSave.Text = "Save As";
            this.btnSave.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(6, 210);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(138, 25);
            this.label7.TabIndex = 15;
            this.label7.Text = "Direction";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cbDirection
            // 
            this.cbDirection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDirection.FormattingEnabled = true;
            this.cbDirection.Items.AddRange(new object[] {
            "up",
            "down"});
            this.cbDirection.Location = new System.Drawing.Point(150, 210);
            this.cbDirection.Name = "cbDirection";
            this.cbDirection.Size = new System.Drawing.Size(74, 25);
            this.cbDirection.TabIndex = 14;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(6, 179);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(138, 25);
            this.label6.TabIndex = 13;
            this.label6.Text = "Area (pA * ms)";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(6, 148);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(138, 25);
            this.label5.TabIndex = 12;
            this.label5.Text = "Decay Fraction";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(6, 117);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(138, 25);
            this.label4.TabIndex = 11;
            this.label4.Text = "Baseline duration (ms)";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(6, 86);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(138, 25);
            this.label3.TabIndex = 10;
            this.label3.Text = "Basline back-up (ms)";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(6, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(138, 25);
            this.label2.TabIndex = 9;
            this.label2.Text = "Time to peak (ms)";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // nudArea
            // 
            this.nudArea.DecimalPlaces = 1;
            this.nudArea.Location = new System.Drawing.Point(150, 179);
            this.nudArea.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudArea.Name = "nudArea";
            this.nudArea.Size = new System.Drawing.Size(74, 25);
            this.nudArea.TabIndex = 6;
            this.nudArea.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // nudDecayFraction
            // 
            this.nudDecayFraction.DecimalPlaces = 1;
            this.nudDecayFraction.Location = new System.Drawing.Point(150, 148);
            this.nudDecayFraction.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudDecayFraction.Name = "nudDecayFraction";
            this.nudDecayFraction.Size = new System.Drawing.Size(74, 25);
            this.nudDecayFraction.TabIndex = 5;
            this.nudDecayFraction.Value = new decimal(new int[] {
            37,
            0,
            0,
            131072});
            // 
            // nudBaselineDuration
            // 
            this.nudBaselineDuration.DecimalPlaces = 1;
            this.nudBaselineDuration.Location = new System.Drawing.Point(150, 117);
            this.nudBaselineDuration.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudBaselineDuration.Name = "nudBaselineDuration";
            this.nudBaselineDuration.Size = new System.Drawing.Size(74, 25);
            this.nudBaselineDuration.TabIndex = 4;
            this.nudBaselineDuration.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // nudBaselineBackUp
            // 
            this.nudBaselineBackUp.DecimalPlaces = 1;
            this.nudBaselineBackUp.Location = new System.Drawing.Point(150, 86);
            this.nudBaselineBackUp.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudBaselineBackUp.Name = "nudBaselineBackUp";
            this.nudBaselineBackUp.Size = new System.Drawing.Size(74, 25);
            this.nudBaselineBackUp.TabIndex = 3;
            this.nudBaselineBackUp.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // nudTimeToPeak
            // 
            this.nudTimeToPeak.DecimalPlaces = 1;
            this.nudTimeToPeak.Location = new System.Drawing.Point(150, 55);
            this.nudTimeToPeak.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudTimeToPeak.Name = "nudTimeToPeak";
            this.nudTimeToPeak.Size = new System.Drawing.Size(74, 25);
            this.nudTimeToPeak.TabIndex = 2;
            this.nudTimeToPeak.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(6, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(138, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "Threshold (pA)";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // nudThreshold
            // 
            this.nudThreshold.DecimalPlaces = 1;
            this.nudThreshold.Location = new System.Drawing.Point(150, 24);
            this.nudThreshold.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudThreshold.Name = "nudThreshold";
            this.nudThreshold.Size = new System.Drawing.Size(74, 25);
            this.nudThreshold.TabIndex = 0;
            this.nudThreshold.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // DetectionParamatersUC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "DetectionParamatersUC";
            this.Size = new System.Drawing.Size(230, 282);
            this.Load += new System.EventHandler(this.DetectionParamatersUC_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudArea)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDecayFraction)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBaselineDuration)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBaselineBackUp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTimeToPeak)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudThreshold)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cbDirection;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown nudArea;
        private System.Windows.Forms.NumericUpDown nudDecayFraction;
        private System.Windows.Forms.NumericUpDown nudBaselineDuration;
        private System.Windows.Forms.NumericUpDown nudBaselineBackUp;
        private System.Windows.Forms.NumericUpDown nudTimeToPeak;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nudThreshold;
        private System.Windows.Forms.Button btnDefault;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.Button btnSave;
    }
}
