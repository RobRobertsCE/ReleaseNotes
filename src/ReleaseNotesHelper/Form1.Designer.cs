namespace ReleaseNotesHelper
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.btnNewDeployment = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnOpenCurrentVersion = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnCommitGridView = new System.Windows.Forms.ToolStripButton();
            this.btnReleaseNotesView = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnSaveReleaseNotes = new System.Windows.Forms.ToolStripButton();
            this.btnSaveReleaseNotesAs = new System.Windows.Forms.ToolStripButton();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(4, 121);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(157, 264);
            this.listBox1.TabIndex = 0;
            // 
            // btnNewDeployment
            // 
            this.btnNewDeployment.Location = new System.Drawing.Point(27, 80);
            this.btnNewDeployment.Name = "btnNewDeployment";
            this.btnNewDeployment.Size = new System.Drawing.Size(112, 21);
            this.btnNewDeployment.TabIndex = 1;
            this.btnNewDeployment.Text = "New Deployment";
            this.btnNewDeployment.UseVisualStyleBackColor = true;
            this.btnNewDeployment.Click += new System.EventHandler(this.btnNewDeployment_Click);
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(167, 52);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(678, 493);
            this.panel1.TabIndex = 2;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(867, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveToolStripMenuItem,
            this.toolStripMenuItem1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.saveToolStripMenuItem.Text = "&Save";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(95, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnOpenCurrentVersion,
            this.btnSaveReleaseNotes,
            this.btnSaveReleaseNotesAs,
            this.toolStripSeparator1,
            this.btnCommitGridView,
            this.btnReleaseNotesView,
            this.toolStripSeparator2});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(867, 25);
            this.toolStrip1.TabIndex = 4;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnOpenCurrentVersion
            // 
            this.btnOpenCurrentVersion.Image = ((System.Drawing.Image)(resources.GetObject("btnOpenCurrentVersion.Image")));
            this.btnOpenCurrentVersion.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnOpenCurrentVersion.Name = "btnOpenCurrentVersion";
            this.btnOpenCurrentVersion.Size = new System.Drawing.Size(56, 22);
            this.btnOpenCurrentVersion.Text = "Open";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // btnCommitGridView
            // 
            this.btnCommitGridView.AutoSize = false;
            this.btnCommitGridView.Checked = true;
            this.btnCommitGridView.CheckOnClick = true;
            this.btnCommitGridView.CheckState = System.Windows.Forms.CheckState.Checked;
            this.btnCommitGridView.Image = ((System.Drawing.Image)(resources.GetObject("btnCommitGridView.Image")));
            this.btnCommitGridView.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCommitGridView.Name = "btnCommitGridView";
            this.btnCommitGridView.Size = new System.Drawing.Size(100, 22);
            this.btnCommitGridView.Text = "Work Items";
            this.btnCommitGridView.CheckedChanged += new System.EventHandler(this.ViewSelector_CheckedChanged);
            // 
            // btnReleaseNotesView
            // 
            this.btnReleaseNotesView.AutoSize = false;
            this.btnReleaseNotesView.CheckOnClick = true;
            this.btnReleaseNotesView.Image = ((System.Drawing.Image)(resources.GetObject("btnReleaseNotesView.Image")));
            this.btnReleaseNotesView.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnReleaseNotesView.Name = "btnReleaseNotesView";
            this.btnReleaseNotesView.Size = new System.Drawing.Size(100, 22);
            this.btnReleaseNotesView.Text = "Release Notes";
            this.btnReleaseNotesView.CheckedChanged += new System.EventHandler(this.ViewSelector_CheckedChanged);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // btnSaveReleaseNotes
            // 
            this.btnSaveReleaseNotes.Image = ((System.Drawing.Image)(resources.GetObject("btnSaveReleaseNotes.Image")));
            this.btnSaveReleaseNotes.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSaveReleaseNotes.Name = "btnSaveReleaseNotes";
            this.btnSaveReleaseNotes.Size = new System.Drawing.Size(51, 22);
            this.btnSaveReleaseNotes.Text = "Save";
            // 
            // btnSaveReleaseNotesAs
            // 
            this.btnSaveReleaseNotesAs.Image = ((System.Drawing.Image)(resources.GetObject("btnSaveReleaseNotesAs.Image")));
            this.btnSaveReleaseNotesAs.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSaveReleaseNotesAs.Name = "btnSaveReleaseNotesAs";
            this.btnSaveReleaseNotesAs.Size = new System.Drawing.Size(76, 22);
            this.btnSaveReleaseNotesAs.Text = "Save As...";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(867, 560);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnNewDeployment);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button btnNewDeployment;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnOpenCurrentVersion;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnCommitGridView;
        private System.Windows.Forms.ToolStripButton btnReleaseNotesView;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnSaveReleaseNotes;
        private System.Windows.Forms.ToolStripButton btnSaveReleaseNotesAs;
    }
}

