namespace ReleaseNotesHelper.Views
{
    partial class CommitView
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
            this.txtIssueKey = new System.Windows.Forms.TextBox();
            this.txtCommitTitle = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtIssueKey
            // 
            this.txtIssueKey.Dock = System.Windows.Forms.DockStyle.Left;
            this.txtIssueKey.Location = new System.Drawing.Point(2, 2);
            this.txtIssueKey.Name = "txtIssueKey";
            this.txtIssueKey.Size = new System.Drawing.Size(125, 20);
            this.txtIssueKey.TabIndex = 0;
            // 
            // txtCommitTitle
            // 
            this.txtCommitTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtCommitTitle.Location = new System.Drawing.Point(127, 2);
            this.txtCommitTitle.Name = "txtCommitTitle";
            this.txtCommitTitle.Size = new System.Drawing.Size(559, 20);
            this.txtCommitTitle.TabIndex = 1;
            // 
            // CommitView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtCommitTitle);
            this.Controls.Add(this.txtIssueKey);
            this.Name = "CommitView";
            this.Padding = new System.Windows.Forms.Padding(2);
            this.Size = new System.Drawing.Size(688, 25);
            this.Load += new System.EventHandler(this.CommitView_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtIssueKey;
        private System.Windows.Forms.TextBox txtCommitTitle;
    }
}
