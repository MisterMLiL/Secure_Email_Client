using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;
using Microsoft.VisualBasic.Logging;
using System.Runtime.Intrinsics.X86;
using System.Text.RegularExpressions;
using Microsoft.VisualBasic.ApplicationServices;

namespace Secure_Email_Client
{
    public partial class UserAuthorization : Form
    {
        private bool isAutorization;
        private List<string> users;
        private User currentUser;
        string path = @".users.acc";

        public UserAuthorization()
        {
            InitializeComponent();

            isAutorization = false;
            readUsers();

            usersList.Items.Add("новый пользователь...");

            usersList.SelectedIndex = usersList.Items.Count - 1;
        }

        private void newMessageButton_Click(object sender, EventArgs e)
        {
            if (usersList.SelectedItem == "новый пользователь...")
            {
                if (!string.IsNullOrEmpty(login.Text)
                    && !string.IsNullOrEmpty(password.Text)
                    && !string.IsNullOrEmpty(smtpServer.Text)
                    && !string.IsNullOrEmpty(smtpPort.Text)
                    && !string.IsNullOrEmpty(imapServer.Text)
                    && !string.IsNullOrEmpty(imapPort.Text)
                    )
                {
                    User user = new User
                    {
                        Login = login.Text,
                        Password = password.Text,
                        SmtpServer = smtpServer.Text,
                        SmtpPort = int.Parse(smtpPort.Text),
                        ImapServer = imapServer.Text,
                        ImapPort = int.Parse(imapPort.Text),
                        Ssl = useSsl.Checked
                    };

                    foreach (string str in users)
                    {
                        if (decodingUser(str).Login == user.Login)
                        {
                            MessageBox.Show("Пользователь с таким Логином уже существует", "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }

                    currentUser = user;

                    string enc = encodingUser(currentUser);

                    users.Add(enc);

                    writeUsers();

                    isAutorization = true;

                    this.Close();
                }
                else
                {
                    MessageBox.Show("Не все поля заполнены", "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                int index = usersList.SelectedIndex;

                if (index == -1)
                    return;

                currentUser = decodingUser(users[index]);

                isAutorization = true;

                this.Close();
            }
        }

        private string encodingUser(User user)
        {
            string jsonString = JsonSerializer.Serialize(user);

            byte[] hashValue = Encription.EncryptStringToBytes_Aes(jsonString);
            string hashedJson = Convert.ToHexString(hashValue);

            return hashedJson;
        }

        private User decodingUser(string hashedJson)
        {
            byte[] hashValue = Convert.FromHexString(hashedJson);
            string jsonString = Encription.DecryptStringFromBytes_Aes(hashValue).ToString();

            User user = JsonSerializer.Deserialize<User>(jsonString);

            return user;
        }

        private bool writeUsers()
        {
            try
            {
                File.WriteAllLines(path, users, Encoding.UTF8);

                return true;
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Source + ": " + exc.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }
        }

        private bool readUsers()
        {
            try
            {
                users = new List<string>();

                foreach (string line in File.ReadLines(path))
                {
                    users.Add(line);
                    usersList.Items.Add(decodingUser(line).Login);
                }

                return true;
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Source + ": " + exc.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }
        }

        private void usersList_DoubleClick(object sender, EventArgs e)
        {
            int index = usersList.SelectedIndex;

            if (index == -1 || index == usersList.Items.Count - 1)
                return;

            currentUser = decodingUser(users[index]);

            if (currentUser != null)
                isAutorization = true;
            else
                MessageBox.Show("Ошибка авторизации пользователя", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);

            this.Close();
        }

        private void integer_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null)
            {
                string text = textBox.Text;
                string newText = new string(text.Where(c => Char.IsDigit(c) || c == '\b').ToArray());
                if (text != newText)
                {
                    textBox.Text = newText;
                    textBox.SelectionStart = newText.Length;
                }
            }
        }

        private void UserAuthorization_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isAutorization)
            {
                this.Enabled = false;

                using (ClientMailsForm form = new ClientMailsForm(currentUser, this))
                {
                    form.ShowDialog();
                }
            }
        }

        private void usersList_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = usersList.SelectedIndex;

            if (index == -1)
                return;

            if (index == usersList.Items.Count - 1)
            {
                login.Text = "";
                password.Text = "";
                smtpServer.Text = "";
                smtpPort.Text = "";
                imapServer.Text = "";
                imapPort.Text = "";
                useSsl.Checked = true;
                return;
            }

            currentUser = decodingUser(users[index]);

            if (currentUser == null)
                return;

            login.Text = currentUser.Login;
            password.Text = currentUser.Password;
            smtpServer.Text = currentUser.SmtpServer;
            smtpPort.Text = currentUser.SmtpPort.ToString();
            imapServer.Text = currentUser.ImapServer;
            imapPort.Text = currentUser.ImapPort.ToString();
            useSsl.Checked = currentUser.Ssl;
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            int index = usersList.SelectedIndex;

            if (index == -1 || index == usersList.Items.Count - 1)
                return;

            currentUser = decodingUser(users[index]);

            if (currentUser == null)
                return;

            if (MessageBox.Show("Вы точно хотите удалить пользователя:\n" + currentUser.Login, "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                users.RemoveAt(index);
                usersList.Items.RemoveAt(index);

                writeUsers();
            }
        }

        private void saveEditButton_Click(object sender, EventArgs e)
        {
            int index = usersList.SelectedIndex;

            if (index == -1 || index == usersList.Items.Count - 1)
                return;

            User user = new User
            {
                Login = login.Text,
                Password = password.Text,
                SmtpServer = smtpServer.Text,
                SmtpPort = int.Parse(smtpPort.Text),
                ImapServer = imapServer.Text,
                ImapPort = int.Parse(imapPort.Text),
                Ssl = useSsl.Checked
            };

            int counter = 0;
            foreach (string str in users)
            {
                if (decodingUser(str).Login == user.Login && counter != index)
                {
                    MessageBox.Show("Пользователь с таким Логином уже существует", "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                counter++;
            }

            string enc = encodingUser(user);

            users[index] = enc;

            writeUsers();

            MessageBox.Show("Данные пользователя изменены", "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
