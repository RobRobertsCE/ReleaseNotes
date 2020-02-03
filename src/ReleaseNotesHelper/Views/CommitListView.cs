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
    public partial class CommitListView : UserControl
    {
        #region properties

        public IList<Commit> Commits { get; set; } = new List<Commit>();

        #endregion

        #region ctor / load

        public CommitListView()
        {
            InitializeComponent();
        }

        private void CommitListView_Load(object sender, EventArgs e)
        {
            foreach (Commit commit in Commits)
            {
                var commitView = new CommitView()
                {
                    Commit = commit
                };

                pnlCommits.Controls.Add(commitView);
            }
        }

        #endregion
    }
}
