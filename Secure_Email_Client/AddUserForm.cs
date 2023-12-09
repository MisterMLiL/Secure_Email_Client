using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Secure_Email_Client
{
    public partial class AddUserForm : Form
    {
        StringClass str;

        public AddUserForm(ref StringClass str)
        {
            InitializeComponent();

            this.str = str;
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox.Text))
            {
                str.Str = textBox.Text;
                this.Close();
            }
        }
    }
}
