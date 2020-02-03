namespace ReleaseNotesHelper.Views
{
    partial class IssueView
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
            this.txtType = new System.Windows.Forms.TextBox();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtIssueKey
            // 
            this.txtIssueKey.Location = new System.Drawing.Point(5, 5);
            this.txtIssueKey.Name = "txtIssueKey";
            this.txtIssueKey.Size = new System.Drawing.Size(79, 20);
            this.txtIssueKey.TabIndex = 0;
            // 
            // txtType
            // 
            this.txtType.Location = new System.Drawing.Point(90, 5);
            this.txtType.Name = "txtType";
            this.txtType.Size = new System.Drawing.Size(79, 20);
            this.txtType.TabIndex = 1;
            // 
            // txtDescription
            // 
            this.txtDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDescription.Location = new System.Drawing.Point(175, 5);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(474, 20);
            this.txtDescription.TabIndex = 2;
            // 
            // IssueView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.txtType);
            this.Controls.Add(this.txtIssueKey);
            this.Name = "IssueView";
            this.Padding = new System.Windows.Forms.Padding(2);
            this.Size = new System.Drawing.Size(654, 124);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtIssueKey;
        private System.Windows.Forms.TextBox txtType;
        private System.Windows.Forms.TextBox txtDescription;
    }
}
