using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CenterEdge.ReleaseNotes.Models;

namespace ReleaseNotesHelper.Views
{
    public partial class CommitView : UserControl
    {
        #region properties

        public Commit Commit { get; set; }

        #endregion

        #region ctor / load

        public CommitView()
        {
            InitializeComponent();
        }

        private void CommitView_Load(object sender, EventArgs e)
        {
            if (Commit != null)
            {
                txtIssueKey.Text = Commit.IssueKey;
                txtCommitTitle.Text = Commit.CommitMessage;
            }
        }

        #endregion
    }
}
