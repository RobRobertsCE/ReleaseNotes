namespace CenterEdge.SourceControl.TestApp
{
    partial class CeDeveloperStatusView
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
            this.components = new System.ComponentModel.Container();
            this.dgvWorkItems = new System.Windows.Forms.DataGridView();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.chkAutoUpdate = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cboUserFilter = new System.Windows.Forms.ComboBox();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.lblAppStatus = new System.Windows.Forms.Label();
            this.dgvSubTasks = new System.Windows.Forms.DataGridView();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.splitter2 = new System.Windows.Forms.Splitter();
            this.dgvPullRequests = new System.Windows.Forms.DataGridView();
            this.pnlWorkItems = new System.Windows.Forms.Panel();
            this.lblWorkItems = new System.Windows.Forms.Label();
            this.pnlSubTasks = new System.Windows.Forms.Panel();
            this.lblSubTasks = new System.Windows.Forms.Label();
            this.pnlPullRequests = new System.Windows.Forms.Panel();
            this.lblPullRequests = new System.Windows.Forms.Label();
            this.timAutoUpdate = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dgvWorkItems)).BeginInit();
            this.pnlTop.SuspendLayout();
            this.pnlBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSubTasks)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPullRequests)).BeginInit();
            this.pnlWorkItems.SuspendLayout();
            this.pnlSubTasks.SuspendLayout();
            this.pnlPullRequests.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvWorkItems
            // 
            this.dgvWorkItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvWorkItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvWorkItems.Location = new System.Drawing.Point(2, 27);
            this.dgvWorkItems.Name = "dgvWorkItems";
            this.dgvWorkItems.Size = new System.Drawing.Size(1082, 227);
            this.dgvWorkItems.TabIndex = 0;
            this.dgvWorkItems.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvWorkItems_DataBindingComplete);
            this.dgvWorkItems.RowHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvWorkItems_RowHeaderMouseClick);
            this.dgvWorkItems.SelectionChanged += new System.EventHandler(this.dgvWorkItems_SelectionChanged);
            // 
            // pnlTop
            // 
            this.pnlTop.Controls.Add(this.chkAutoUpdate);
            this.pnlTop.Controls.Add(this.label1);
            this.pnlTop.Controls.Add(this.cboUserFilter);
            this.pnlTop.Controls.Add(this.btnUpdate);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(1088, 44);
            this.pnlTop.TabIndex = 1;
            // 
            // chkAutoUpdate
            // 
            this.chkAutoUpdate.AutoSize = true;
            this.chkAutoUpdate.Location = new System.Drawing.Point(111, 16);
            this.chkAutoUpdate.Name = "chkAutoUpdate";
            this.chkAutoUpdate.Size = new System.Drawing.Size(86, 17);
            this.chkAutoUpdate.TabIndex = 4;
            this.chkAutoUpdate.Text = "Auto Update";
            this.chkAutoUpdate.UseVisualStyleBackColor = true;
            this.chkAutoUpdate.CheckedChanged += new System.EventHandler(this.chkAutoUpdate_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(230, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Filter by user:";
            // 
            // cboUserFilter
            // 
            this.cboUserFilter.FormattingEnabled = true;
            this.cboUserFilter.Items.AddRange(new object[] {
            "-All-",
            "rroberts",
            "lpowell",
            "kfriedl",
            "bsfrenza",
            "prutledge"});
            this.cboUserFilter.Location = new System.Drawing.Point(305, 14);
            this.cboUserFilter.Name = "cboUserFilter";
            this.cboUserFilter.Size = new System.Drawing.Size(224, 21);
            this.cboUserFilter.TabIndex = 2;
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(12, 12);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 23);
            this.btnUpdate.TabIndex = 1;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // pnlBottom
            // 
            this.pnlBottom.Controls.Add(this.lblAppStatus);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 612);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Padding = new System.Windows.Forms.Padding(2);
            this.pnlBottom.Size = new System.Drawing.Size(1088, 25);
            this.pnlBottom.TabIndex = 2;
            // 
            // lblAppStatus
            // 
            this.lblAppStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblAppStatus.Location = new System.Drawing.Point(2, 2);
            this.lblAppStatus.Name = "lblAppStatus";
            this.lblAppStatus.Size = new System.Drawing.Size(1084, 21);
            this.lblAppStatus.TabIndex = 0;
            this.lblAppStatus.Text = "Loading...";
            this.lblAppStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dgvSubTasks
            // 
            this.dgvSubTasks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSubTasks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSubTasks.Location = new System.Drawing.Point(2, 27);
            this.dgvSubTasks.Name = "dgvSubTasks";
            this.dgvSubTasks.Size = new System.Drawing.Size(1082, 193);
            this.dgvSubTasks.TabIndex = 3;
            this.dgvSubTasks.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvSubTasks_DataBindingComplete);
            this.dgvSubTasks.SelectionChanged += new System.EventHandler(this.dgvSubTasks_SelectionChanged);
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitter1.Location = new System.Drawing.Point(0, 302);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(1088, 3);
            this.splitter1.TabIndex = 4;
            this.splitter1.TabStop = false;
            // 
            // splitter2
            // 
            this.splitter2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitter2.Location = new System.Drawing.Point(0, 529);
            this.splitter2.Name = "splitter2";
            this.splitter2.Size = new System.Drawing.Size(1088, 3);
            this.splitter2.TabIndex = 6;
            this.splitter2.TabStop = false;
            // 
            // dgvPullRequests
            // 
            this.dgvPullRequests.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPullRequests.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPullRequests.Location = new System.Drawing.Point(2, 27);
            this.dgvPullRequests.Name = "dgvPullRequests";
            this.dgvPullRequests.Size = new System.Drawing.Size(1082, 49);
            this.dgvPullRequests.TabIndex = 5;
            this.dgvPullRequests.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvPullRequests_DataBindingComplete);
            // 
            // pnlWorkItems
            // 
            this.pnlWorkItems.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlWorkItems.Controls.Add(this.dgvWorkItems);
            this.pnlWorkItems.Controls.Add(this.lblWorkItems);
            this.pnlWorkItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlWorkItems.Location = new System.Drawing.Point(0, 44);
            this.pnlWorkItems.Name = "pnlWorkItems";
            this.pnlWorkItems.Padding = new System.Windows.Forms.Padding(2);
            this.pnlWorkItems.Size = new System.Drawing.Size(1088, 258);
            this.pnlWorkItems.TabIndex = 7;
            // 
            // lblWorkItems
            // 
            this.lblWorkItems.BackColor = System.Drawing.Color.LightSteelBlue;
            this.lblWorkItems.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblWorkItems.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblWorkItems.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWorkItems.Location = new System.Drawing.Point(2, 2);
            this.lblWorkItems.Name = "lblWorkItems";
            this.lblWorkItems.Padding = new System.Windows.Forms.Padding(4);
            this.lblWorkItems.Size = new System.Drawing.Size(1082, 25);
            this.lblWorkItems.TabIndex = 0;
            this.lblWorkItems.Text = "Work Items";
            this.lblWorkItems.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlSubTasks
            // 
            this.pnlSubTasks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlSubTasks.Controls.Add(this.dgvSubTasks);
            this.pnlSubTasks.Controls.Add(this.lblSubTasks);
            this.pnlSubTasks.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlSubTasks.Location = new System.Drawing.Point(0, 305);
            this.pnlSubTasks.Name = "pnlSubTasks";
            this.pnlSubTasks.Padding = new System.Windows.Forms.Padding(2);
            this.pnlSubTasks.Size = new System.Drawing.Size(1088, 224);
            this.pnlSubTasks.TabIndex = 8;
            // 
            // lblSubTasks
            // 
            this.lblSubTasks.BackColor = System.Drawing.Color.LightSteelBlue;
            this.lblSubTasks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblSubTasks.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblSubTasks.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubTasks.Location = new System.Drawing.Point(2, 2);
            this.lblSubTasks.Name = "lblSubTasks";
            this.lblSubTasks.Padding = new System.Windows.Forms.Padding(4);
            this.lblSubTasks.Size = new System.Drawing.Size(1082, 25);
            this.lblSubTasks.TabIndex = 0;
            this.lblSubTasks.Text = "SubTasks";
            this.lblSubTasks.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlPullRequests
            // 
            this.pnlPullRequests.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlPullRequests.Controls.Add(this.dgvPullRequests);
            this.pnlPullRequests.Controls.Add(this.lblPullRequests);
            this.pnlPullRequests.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlPullRequests.Location = new System.Drawing.Point(0, 532);
            this.pnlPullRequests.Name = "pnlPullRequests";
            this.pnlPullRequests.Padding = new System.Windows.Forms.Padding(2);
            this.pnlPullRequests.Size = new System.Drawing.Size(1088, 80);
            this.pnlPullRequests.TabIndex = 8;
            // 
            // lblPullRequests
            // 
            this.lblPullRequests.BackColor = System.Drawing.Color.LightSteelBlue;
            this.lblPullRequests.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblPullRequests.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblPullRequests.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPullRequests.Location = new System.Drawing.Point(2, 2);
            this.lblPullRequests.Name = "lblPullRequests";
            this.lblPullRequests.Padding = new System.Windows.Forms.Padding(4);
            this.lblPullRequests.Size = new System.Drawing.Size(1082, 25);
            this.lblPullRequests.TabIndex = 0;
            this.lblPullRequests.Text = "Pull Requests";
            this.lblPullRequests.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // timAutoUpdate
            // 
            this.timAutoUpdate.Interval = 60000;
            this.timAutoUpdate.Tick += new System.EventHandler(this.timAutoUpdate_Tick);
            // 
            // CeDeveloperStatusView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1088, 637);
            this.Controls.Add(this.pnlWorkItems);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.pnlSubTasks);
            this.Controls.Add(this.splitter2);
            this.Controls.Add(this.pnlPullRequests);
            this.Controls.Add(this.pnlBottom);
            this.Controls.Add(this.pnlTop);
            this.Name = "CeDeveloperStatusView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CE Developer Status View";
            this.Load += new System.EventHandler(this.CeDeveloperStatusView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvWorkItems)).EndInit();
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.pnlBottom.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSubTasks)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPullRequests)).EndInit();
            this.pnlWorkItems.ResumeLayout(false);
            this.pnlSubTasks.ResumeLayout(false);
            this.pnlPullRequests.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvWorkItems;
        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Panel pnlBottom;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.DataGridView dgvSubTasks;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Splitter splitter2;
        private System.Windows.Forms.DataGridView dgvPullRequests;
        private System.Windows.Forms.Panel pnlWorkItems;
        private System.Windows.Forms.Label lblWorkItems;
        private System.Windows.Forms.Panel pnlSubTasks;
        private System.Windows.Forms.Label lblSubTasks;
        private System.Windows.Forms.Panel pnlPullRequests;
        private System.Windows.Forms.Label lblPullRequests;
        private System.Windows.Forms.Label lblAppStatus;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboUserFilter;
        private System.Windows.Forms.CheckBox chkAutoUpdate;
        private System.Windows.Forms.Timer timAutoUpdate;
    }
}