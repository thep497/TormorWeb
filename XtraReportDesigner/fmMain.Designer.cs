namespace XtraReportDesigner
{
    partial class fmMain
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
            this.btnOpenFromFile = new DevExpress.XtraEditors.SimpleButton();
            this.btnDesigner = new DevExpress.XtraEditors.SimpleButton();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.SuspendLayout();
            // 
            // btnOpenFromFile
            // 
            this.btnOpenFromFile.Location = new System.Drawing.Point(12, 174);
            this.btnOpenFromFile.Name = "btnOpenFromFile";
            this.btnOpenFromFile.Size = new System.Drawing.Size(156, 22);
            this.btnOpenFromFile.TabIndex = 1;
            this.btnOpenFromFile.Text = "Design Report from Repx File";
            this.btnOpenFromFile.Click += new System.EventHandler(this.btnOpenFromFile_Click);
            // 
            // btnDesigner
            // 
            this.btnDesigner.Location = new System.Drawing.Point(12, 146);
            this.btnDesigner.Name = "btnDesigner";
            this.btnDesigner.Size = new System.Drawing.Size(156, 22);
            this.btnDesigner.TabIndex = 2;
            this.btnDesigner.Text = "Design Report From Source";
            this.btnDesigner.Click += new System.EventHandler(this.btnDesigner_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(12, 45);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(300, 95);
            this.listBox1.TabIndex = 4;
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(12, 17);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(156, 22);
            this.simpleButton1.TabIndex = 5;
            this.simpleButton1.Text = "Load List";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // fmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(324, 208);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.btnDesigner);
            this.Controls.Add(this.btnOpenFromFile);
            this.Name = "fmMain";
            this.Text = "NNS XtraReport Designer";
            this.Load += new System.EventHandler(this.fmMain_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnOpenFromFile;
        private DevExpress.XtraEditors.SimpleButton btnDesigner;
        private System.Windows.Forms.ListBox listBox1;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
    }
}

