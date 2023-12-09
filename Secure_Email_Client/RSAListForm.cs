using MailKit;
using Microsoft.VisualBasic.ApplicationServices;
using MimeKit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Secure_Email_Client
{
    public partial class RSAListForm : Form
    {
        string smtpServer;
        int port;
        bool useSsl;
        string login;
        string password;

        List<RSAUsers> users;

        IMailFolder sentFolder;

        public RSAListForm(string smtpServer, int port, bool useSsl, string login, string password, ref List<RSAUsers> users, IMailFolder sentFolder)
        {
            InitializeComponent();

            this.smtpServer = smtpServer;
            this.port = port;
            this.useSsl = useSsl;
            this.login = login;
            this.password = password;

            this.sentFolder = sentFolder;

            this.users = users;

            foreach (var item in this.users)
            {
                namesList.Items.Add(item.userName);
            }
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            StringClass str = new("");

            using (AddUserForm form = new AddUserForm(ref str))
            {
                form.ShowDialog();
            }

            if (string.IsNullOrEmpty(str.Str))
                return;

            if (MessageBox.Show($"Вы точно хотите отправить пользователю {str.Str} ключ для шифрования сообщений?", "Уведомление", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                // кому отправляем
                string to = str.Str;

                // создаем объект сообщения
                MimeMessage m = new MimeMessage();
                m.From.Add(new MailboxAddress("", login));
                m.To.Add(new MailboxAddress("", to));
                // тема письма
                m.Subject = "^$";
                // создаем составное тело сообщения
                var body = new Multipart("mixed");

                (string pub, string priv) rsa = EncryptedMessage.GenerateRSAKeys();

                string filePublicKeyPath = Path.Combine(EncryptedMessage.tempEmails, to + "_rsa_key_temp");

                if (!Directory.Exists(Path.Combine(EncryptedMessage.tempEmails)))
                {
                    Directory.CreateDirectory(Path.Combine(EncryptedMessage.tempEmails));
                }

                File.WriteAllText(filePublicKeyPath, rsa.pub);

                var attachment = new MimePart()
                {
                    Content = new MimeContent(File.OpenRead(filePublicKeyPath)),
                    ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
                    ContentTransferEncoding = ContentEncoding.Base64,
                    FileName = "RSAKey.key"
                };

                body.Add(attachment);

                m.Body = body;

                // Добавьте заголовок "X-Sent-To"
                m.Headers.Add("X-Sent-To", login);

                // адрес smtp-сервера и порт, с которого будем отправлять письмо
                using (MailKit.Net.Smtp.SmtpClient client = new MailKit.Net.Smtp.SmtpClient())
                {
                    client.Connect(smtpServer, port, useSsl);

                    client.Authenticate(login, password);

                    // отправляем сообщение
                    try
                    {
                        client.Send(m);

                        sentFolder.Open(FolderAccess.ReadWrite);

                        sentFolder.Append(m);

                        sentFolder.Close();

                        MessageBox.Show("Сообщение отправлено!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Сообщение не отправлено.\n" + ex.Message);
                    }
                }

                namesList.Items.Add(str.Str);

                var user = new RSAUsers()
                {
                    userName = str.Str,
                    publicKey = "",
                    privateKey = rsa.priv
                };

                users.Add(user);
            }
            else
            {
                return;
            }
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            int index = namesList.SelectedIndex;

            if (index != -1)
            {
                if (MessageBox.Show($"Вы точно хотите удалить пользователя {namesList.Items[index]} из СВОЕГО списка контактов?", "Уведомление", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    namesList.Items.RemoveAt(index);
                    users.RemoveAt(index);
                }
            }
        }
    }
}
