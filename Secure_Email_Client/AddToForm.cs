using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Secure_Email_Client
{
    public partial class AddToForm : Form
    {
        List<string> users;

        public AddToForm(ref List<string> users)
        {
            InitializeComponent();

            this.users = users;

            foreach (var user in users)
            {
                namesToList.Items.Add(user);
            }
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            StringClass user = new("");

            using (AddUserForm form = new AddUserForm(ref user))
            {
                form.ShowDialog();
            }

            if (!string.IsNullOrEmpty(user.Str))
            {
                namesToList.Items.Add(user.Str);
                users.Add(user.Str);
            }
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            int index = namesToList.SelectedIndex;

            if (index != -1)
            {
                namesToList.Items.RemoveAt(index);
                users.RemoveAt(index);
            }
        }
    }
}
