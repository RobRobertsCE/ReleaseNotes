using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReleaseNotesHelper.Views
{
    public partial class ReleaseNotesContentView : UserControl
    {
        #region events

        #endregion

        #region fields

        #endregion

        #region properties

        public string ReleaseNotesContent
        {
            get
            {
                return textBox1.Text;
            }
            set
            {
                textBox1.Text = value;
            }
        }

        #endregion

        #region ctor

        public ReleaseNotesContentView()
        {
            InitializeComponent();
        }

        #endregion

        #region public

        #endregion

        #region protected

        #endregion

        #region private

        #endregion
    }
}
