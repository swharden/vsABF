namespace vsABFgui
{
    partial class abfGraphForm
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
            this.abfGraph1 = new vsABFgui.abfGraphUC();
            this.SuspendLayout();
            // 
            // abfGraph1
            // 
            this.abfGraph1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.abfGraph1.Location = new System.Drawing.Point(0, 0);
            this.abfGraph1.Name = "abfGraph1";
            this.abfGraph1.Size = new System.Drawing.Size(800, 450);
            this.abfGraph1.TabIndex = 0;
            // 
            // abfGraphForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.abfGraph1);
            this.Name = "abfGraphForm";
            this.Text = "ABF Graph Demo";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private abfGraphUC abfGraph1;
    }
}

