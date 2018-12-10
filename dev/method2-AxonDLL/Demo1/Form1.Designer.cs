namespace Demo1
{
    partial class Form1
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
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.nudChannel = new System.Windows.Forms.NumericUpDown();
            this.nudSweep = new System.Windows.Forms.NumericUpDown();
            this.lblChannel = new System.Windows.Forms.Label();
            this.lblSweep = new System.Windows.Forms.Label();
            this.scottPlotUC1 = new ScottPlot.ScottPlotUC();
            ((System.ComponentModel.ISupportInitialize)(this.nudChannel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSweep)).BeginInit();
            this.SuspendLayout();
            // 
            // listBox1
            // 
            this.listBox1.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(12, 51);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(237, 524);
            this.listBox1.TabIndex = 0;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(237, 33);
            this.button1.TabIndex = 1;
            this.button1.Text = "Select ABF Folder";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBox1.Location = new System.Drawing.Point(255, 339);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(789, 236);
            this.richTextBox1.TabIndex = 2;
            this.richTextBox1.Text = "";
            this.richTextBox1.WordWrap = false;
            // 
            // nudChannel
            // 
            this.nudChannel.Location = new System.Drawing.Point(337, 20);
            this.nudChannel.Name = "nudChannel";
            this.nudChannel.Size = new System.Drawing.Size(59, 20);
            this.nudChannel.TabIndex = 3;
            this.nudChannel.ValueChanged += new System.EventHandler(this.nudChannel_ValueChanged);
            // 
            // nudSweep
            // 
            this.nudSweep.Location = new System.Drawing.Point(498, 20);
            this.nudSweep.Name = "nudSweep";
            this.nudSweep.Size = new System.Drawing.Size(75, 20);
            this.nudSweep.TabIndex = 4;
            this.nudSweep.ValueChanged += new System.EventHandler(this.nudSweep_ValueChanged);
            // 
            // lblChannel
            // 
            this.lblChannel.AutoSize = true;
            this.lblChannel.Location = new System.Drawing.Point(269, 22);
            this.lblChannel.Name = "lblChannel";
            this.lblChannel.Size = new System.Drawing.Size(62, 13);
            this.lblChannel.TabIndex = 5;
            this.lblChannel.Text = "4 channels:";
            // 
            // lblSweep
            // 
            this.lblSweep.AutoSize = true;
            this.lblSweep.Location = new System.Drawing.Point(425, 22);
            this.lblSweep.Name = "lblSweep";
            this.lblSweep.Size = new System.Drawing.Size(67, 13);
            this.lblSweep.TabIndex = 6;
            this.lblSweep.Text = "123 sweeps:";
            // 
            // scottPlotUC1
            // 
            this.scottPlotUC1.Location = new System.Drawing.Point(255, 45);
            this.scottPlotUC1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.scottPlotUC1.Name = "scottPlotUC1";
            this.scottPlotUC1.Size = new System.Drawing.Size(789, 289);
            this.scottPlotUC1.TabIndex = 7;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1056, 588);
            this.Controls.Add(this.scottPlotUC1);
            this.Controls.Add(this.lblSweep);
            this.Controls.Add(this.lblChannel);
            this.Controls.Add(this.nudSweep);
            this.Controls.Add(this.nudChannel);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.listBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Form1";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudChannel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSweep)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.NumericUpDown nudChannel;
        private System.Windows.Forms.NumericUpDown nudSweep;
        private System.Windows.Forms.Label lblChannel;
        private System.Windows.Forms.Label lblSweep;
        private ScottPlot.ScottPlotUC scottPlotUC1;
    }
}

