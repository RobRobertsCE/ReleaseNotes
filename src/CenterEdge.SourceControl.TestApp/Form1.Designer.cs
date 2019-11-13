namespace CenterEdge.SourceControl.TestApp
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
            this.btnCurrentRelease = new System.Windows.Forms.Button();
            this.lstStartRelease = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lstEndRelease = new System.Windows.Forms.ListBox();
            this.btnSelectedRange = new System.Windows.Forms.Button();
            this.pnlControls = new System.Windows.Forms.Panel();
            this.txtReleaseNotes = new System.Windows.Forms.TextBox();
            this.btnSelectedRelease = new System.Windows.Forms.Button();
            this.pnlControls.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCurrentRelease
            // 
            this.btnCurrentRelease.Location = new System.Drawing.Point(386, 12);
            this.btnCurrentRelease.Name = "btnCurrentRelease";
            this.btnCurrentRelease.Size = new System.Drawing.Size(75, 44);
            this.btnCurrentRelease.TabIndex = 0;
            this.btnCurrentRelease.Text = "Current Release";
            this.btnCurrentRelease.UseVisualStyleBackColor = true;
            this.btnCurrentRelease.Click += new System.EventHandler(this.btnCurrentRelease_Click);
            // 
            // lstStartRelease
            // 
            this.lstStartRelease.FormattingEnabled = true;
            this.lstStartRelease.Location = new System.Drawing.Point(12, 36);
            this.lstStartRelease.Name = "lstStartRelease";
            this.lstStartRelease.Size = new System.Drawing.Size(181, 121);
            this.lstStartRelease.TabIndex = 1;
            this.lstStartRelease.SelectedIndexChanged += new System.EventHandler(this.LstStartRelease_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Start Release";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(200, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "End Release";
            // 
            // lstEndRelease
            // 
            this.lstEndRelease.FormattingEnabled = true;
            this.lstEndRelease.Location = new System.Drawing.Point(199, 36);
            this.lstEndRelease.Name = "lstEndRelease";
            this.lstEndRelease.Size = new System.Drawing.Size(181, 121);
            this.lstEndRelease.TabIndex = 3;
            this.lstEndRelease.SelectedIndexChanged += new System.EventHandler(this.lstEndRelease_SelectedIndexChanged);
            // 
            // btnSelectedRange
            // 
            this.btnSelectedRange.Enabled = false;
            this.btnSelectedRange.Location = new System.Drawing.Point(386, 113);
            this.btnSelectedRange.Name = "btnSelectedRange";
            this.btnSelectedRange.Size = new System.Drawing.Size(75, 44);
            this.btnSelectedRange.TabIndex = 5;
            this.btnSelectedRange.Text = "Selected Range";
            this.btnSelectedRange.UseVisualStyleBackColor = true;
            this.btnSelectedRange.Click += new System.EventHandler(this.btnSelectedRange_Click);
            // 
            // pnlControls
            // 
            this.pnlControls.Controls.Add(this.btnSelectedRelease);
            this.pnlControls.Controls.Add(this.btnCurrentRelease);
            this.pnlControls.Controls.Add(this.btnSelectedRange);
            this.pnlControls.Controls.Add(this.lstStartRelease);
            this.pnlControls.Controls.Add(this.label2);
            this.pnlControls.Controls.Add(this.label1);
            this.pnlControls.Controls.Add(this.lstEndRelease);
            this.pnlControls.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlControls.Enabled = false;
            this.pnlControls.Location = new System.Drawing.Point(0, 0);
            this.pnlControls.Name = "pnlControls";
            this.pnlControls.Size = new System.Drawing.Size(788, 177);
            this.pnlControls.TabIndex = 6;
            // 
            // txtReleaseNotes
            // 
            this.txtReleaseNotes.AcceptsReturn = true;
            this.txtReleaseNotes.AcceptsTab = true;
            this.txtReleaseNotes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtReleaseNotes.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReleaseNotes.Location = new System.Drawing.Point(0, 177);
            this.txtReleaseNotes.Multiline = true;
            this.txtReleaseNotes.Name = "txtReleaseNotes";
            this.txtReleaseNotes.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtReleaseNotes.Size = new System.Drawing.Size(788, 372);
            this.txtReleaseNotes.TabIndex = 7;
            // 
            // btnSelectedRelease
            // 
            this.btnSelectedRelease.Enabled = false;
            this.btnSelectedRelease.Location = new System.Drawing.Point(386, 63);
            this.btnSelectedRelease.Name = "btnSelectedRelease";
            this.btnSelectedRelease.Size = new System.Drawing.Size(75, 44);
            this.btnSelectedRelease.TabIndex = 6;
            this.btnSelectedRelease.Text = "Selected Release";
            this.btnSelectedRelease.UseVisualStyleBackColor = true;
            this.btnSelectedRelease.Click += new System.EventHandler(this.btnSelectedRelease_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(788, 549);
            this.Controls.Add(this.txtReleaseNotes);
            this.Controls.Add(this.pnlControls);
            this.Name = "Form1";
            this.Text = "Release Notes Helper";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.pnlControls.ResumeLayout(false);
            this.pnlControls.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCurrentRelease;
        private System.Windows.Forms.ListBox lstStartRelease;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox lstEndRelease;
        private System.Windows.Forms.Button btnSelectedRange;
        private System.Windows.Forms.Panel pnlControls;
        private System.Windows.Forms.TextBox txtReleaseNotes;
        private System.Windows.Forms.Button btnSelectedRelease;
    }
}

