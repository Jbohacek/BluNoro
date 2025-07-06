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
    public partial class LogShow : Form
    {
        public LogShow(string text)
        {
            InitializeComponent();

            txt_text.Text = text;
        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
