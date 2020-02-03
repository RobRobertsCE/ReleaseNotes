namespace ReleaseNotesHelper.Views
{
    partial class CommitListView
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
            this.pnlHeaders = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pnlCommits = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlHeaders.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeaders
            // 
            this.pnlHeaders.Controls.Add(this.label2);
            this.pnlHeaders.Controls.Add(this.label1);
            this.pnlHeaders.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeaders.Location = new System.Drawing.Point(2, 2);
            this.pnlHeaders.Name = "pnlHeaders";
            this.pnlHeaders.Padding = new System.Windows.Forms.Padding(2);
            this.pnlHeaders.Size = new System.Drawing.Size(598, 27);
            this.pnlHeaders.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Location = new System.Drawing.Point(2, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "Jira Key";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(62, 2);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(534, 23);
            this.label2.TabIndex = 1;
            this.label2.Text = "Title";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlCommits
            // 
            this.pnlCommits.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCommits.Location = new System.Drawing.Point(2, 29);
            this.pnlCommits.Name = "pnlCommits";
            this.pnlCommits.Size = new System.Drawing.Size(598, 287);
            this.pnlCommits.TabIndex = 1;
            // 
            // CommitListView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlCommits);
            this.Controls.Add(this.pnlHeaders);
            this.Name = "CommitListView";
            this.Padding = new System.Windows.Forms.Padding(2);
            this.Size = new System.Drawing.Size(602, 318);
            this.Load += new System.EventHandler(this.CommitListView_Load);
            this.pnlHeaders.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeaders;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.FlowLayoutPanel pnlCommits;
    }
}
