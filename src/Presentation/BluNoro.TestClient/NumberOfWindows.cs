using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BluChat.TestClient
{
    public partial class NumberOfWindows : Form
    {
        public NumberOfWindows()
        {
            InitializeComponent();
            NumberOfWindowsResult = 1;
            DialogResult = DialogResult.Abort;
        }

        public int NumberOfWindowsResult;

        private void btn_start_Click(object sender, EventArgs e)
        {
            NumberOfWindowsResult = (int)nmb_number.Value;
            DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
