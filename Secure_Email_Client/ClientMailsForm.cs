using System.Net.Mail;
using System.Net;
using MailKit.Net.Imap;
using MailKit.Search;
using MailKit.Security;
using MimeKit;
using System.Linq;
using System.Windows.Forms;
using Microsoft.VisualBasic.Logging;
using Org.BouncyCastle.Asn1.X509;
using System;
using MailKit;
using Microsoft.VisualBasic.ApplicationServices;
using System.Text.Json;
using System.Text;
using System.Diagnostics.Metrics;
using System.Reflection;
using System.Collections.Generic;
using Org.BouncyCastle.Crypto.Encodings;

//login = "eugene.laz1@yandex.ru";
//password = "uhmsmrlfzvcskrol";

//login = "eugene.lazurenko@yandex.ru";
//password = "wxfggbdwrxigtnko";

//imapServer = "imap.yandex.ru";
//imapPort = 993;
//useSsl = true;

//smtpServer = "smtp.yandex.ru";
//smtpPort = 465;

//login = "eugenelazurenko@gmail.com";
//password = "vmydllnkjszmcfst";

namespace Secure_Email_Client
{
    public partial class ClientMailsForm : Form
    {
        private ImapClient client;
        readonly string emailsFolderPath = "emails";

        Dictionary<string, IMailFolder> folderNames;
        Dictionary<IMailFolder, string> folderIMail;

        User currentUser;

        List<RSAUsers> rsaList;

        bool isLoggedIn = true;

        public ClientMailsForm(User user, UserAuthorization form)
        {
            InitializeComponent();

            mailData.ContextMenuStrip = contextMenuStrip1;

            currentUser = user;

            mailNameLabel.Text = currentUser.Login;

            try
            {
                client = new ImapClient();

                client.Connect(currentUser.ImapServer, currentUser.ImapPort, currentUser.Ssl);
                client.Authenticate(currentUser.Login, currentUser.Password);

                folderNames = new Dictionary<string, IMailFolder> {
                    { "INBOX", client.Inbox },
                    { "Sent", client.GetFolder(SpecialFolder.Sent) },
                    { "Drafts", client.GetFolder(SpecialFolder.Drafts) },
                    { "Trash", client.GetFolder(SpecialFolder.Trash) }
                };

                folderIMail = new Dictionary<IMailFolder, string> {
                    { client.Inbox, "INBOX" },
                    { client.GetFolder(SpecialFolder.Sent), "Sent" },
                    { client.GetFolder(SpecialFolder.Drafts), "Drafts" },
                    { client.GetFolder(SpecialFolder.Trash), "Trash" }
                };

                SetToolTips();

                foldersList.SelectedIndex = 0;

                foldersList.SelectedIndexChanged += listFolders_SelectedIndexChanged;

                rsaList = new List<RSAUsers>();

                EncryptedMessage.GetAllUserFromJsonFile(ref rsaList, currentUser.Login);

                DownloadAllEmails();

                form.Visible = false;
            }
            catch (Exception exc)
            {
                MessageBox.Show("������ �����������:\n" + exc.Source + ": " + exc.Message, "������", MessageBoxButtons.OK, MessageBoxIcon.Error);
                isLoggedIn = false;
            }
        }

        private void SetToolTips()
        {
            ToolTip tool = new ToolTip();
            tool.SetToolTip(contactButton, "��������");
            tool.SetToolTip(newMessageButton, "�������� ����� ������");
            tool.SetToolTip(refreshButton, "��������");
            //tool.SetToolTip(contactButton, "��������");
        }

        public void DownloadAllEmails()
        {
            GetAllEmailsAndSaveAttachmentsAsync();

            EncryptedMessage.WriteAllUserFromJsonFile(rsaList, currentUser.Login);
        }

        public void GetAllEmails(IMailFolder folder)
        {
            ReceiveEmailAsync(folder);
        }

        #region ������ � ������ �� ����������

        public void GetAllEmailsAndSaveAttachmentsAsync()
        {
            if (!Directory.Exists(emailsFolderPath))
            {
                Directory.CreateDirectory(emailsFolderPath);
            }

            foreach (var folderDict in folderNames)
            {
                var folder = folderDict.Value;

                folder.Open(FolderAccess.ReadOnly);

                var folderName = folderDict.Key;

                var uids = folder.Search(SearchQuery.All);

                List<string> usesDate = new List<string>();

                var path = Path.Combine(emailsFolderPath, currentUser.Login, folderName);

                foreach (var uid in uids)
                {
                    var message = folder.GetMessage(uid);

                    Directory.CreateDirectory(path);

                    string messagePath = Path.Combine(path, $"{message.Date.DateTime.ToString().Replace(':', '.')}.eml");

                    usesDate.Add(messagePath);

                    if (message.Subject == "^%" && folderDict.Key != "Sent" && folderDict.Key != "Trash")
                    {
                        RSAUsers user = new RSAUsers();
                        int index = -1;
                        string from = string.Join("; ", message.From.Select(x => x.ToString()));

                        foreach (var item in rsaList)
                        {
                            index++;
                            if (item.userName == from)
                            {
                                user = item;

                                break;
                            }
                        }

                        if (!string.IsNullOrEmpty(user.userName) & string.IsNullOrEmpty(user.publicKey))
                        {
                            MessageBox.Show($"������������ {from} �������� ��� ���� ���� ����������\n" +
                                $"�� ������ ���������� ��� ����������� ���������", "�����������", MessageBoxButtons.OK, MessageBoxIcon.Question);

                            string fileName = "";

                            foreach (var rsaPart in message.Attachments.OfType<MimePart>())
                            {
                                using (var stream = File.Create(rsaPart.FileName))
                                {
                                    // ���������� �����
                                    rsaPart.Content.DecodeTo(stream);
                                    fileName = rsaPart.FileName;
                                    break;
                                }
                            }

                            string rsaPublicKey = "";

                            if (!string.IsNullOrEmpty(fileName))
                            {
                                rsaPublicKey = File.ReadAllText(fileName);

                                if (File.Exists(fileName))
                                {
                                    File.Delete(fileName);
                                }
                            }

                            user.publicKey = rsaPublicKey;

                            rsaList[index] = user;
                        }
                    }
                    else if (message.Subject == "^$" && folderDict.Key != "Sent" && folderDict.Key != "Trash")
                    {
                        string checkUser = "";
                        string rsaPrivate = "";
                        int index = -1;
                        string from = string.Join("; ", message.From.Select(x => x.ToString()));

                        foreach (var item in rsaList)
                        {
                            index++;
                            if (item.userName == from)
                            {
                                rsaPrivate = item.privateKey;

                                if (!string.IsNullOrEmpty(item.publicKey))
                                {
                                    checkUser = item.userName;
                                }

                                break;
                            }
                        }

                        if (string.IsNullOrEmpty(checkUser))
                        {
                            if (MessageBox.Show($"� ��� ����� ������ �� ����������� ����������� �� ������������ {from}\n" +
                            "������� ��� �������?", "�����������", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                            {
                                continue;
                            }

                            // ���� ����������
                            string to = from;

                            // ������� ������ ���������
                            MimeMessage m = new MimeMessage();
                            m.From.Add(new MailboxAddress("", currentUser.Login));
                            m.To.Add(new MailboxAddress("", to));
                            // ���� ������
                            m.Subject = "^%";
                            // ������� ��������� ���� ���������
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

                            // ����� smtp-������� � ����, � �������� ����� ���������� ������
                            using (MailKit.Net.Smtp.SmtpClient client = new MailKit.Net.Smtp.SmtpClient())
                            {
                                client.Connect(currentUser.SmtpServer, currentUser.SmtpPort, currentUser.Ssl);

                                client.Authenticate(currentUser.Login, currentUser.Password);

                                // ���������� ���������
                                try
                                {
                                    client.Send(m);

                                    var sentFolder = getFolder("������������");

                                    sentFolder.Open(FolderAccess.ReadWrite);

                                    sentFolder.Append(m);

                                    sentFolder.Close();
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("��������� � ������ �� ����������.\n" + ex.Message);
                                }
                            }

                            string fileName = "";

                            foreach (var rsaPart in message.Attachments.OfType<MimePart>())
                            {
                                using (var stream = File.Create(rsaPart.FileName))
                                {
                                    // ���������� �����
                                    rsaPart.Content.DecodeTo(stream);
                                    fileName = rsaPart.FileName;
                                    break;
                                }
                            }

                            string rsaPublicKey = "";

                            if (!string.IsNullOrEmpty(fileName))
                            {
                                rsaPublicKey = File.ReadAllText(fileName);

                                if (File.Exists(fileName))
                                {
                                    File.Delete(fileName);
                                }
                            }

                            string rsaPrivateKey = rsa.priv;

                            if (!string.IsNullOrEmpty(rsaPrivate))
                            {
                                rsaPrivateKey = rsaPrivate;
                            }

                            var user = new RSAUsers()
                            {
                                userName = from,
                                publicKey = rsaPublicKey,
                                privateKey = rsaPrivateKey
                            };

                            if (index == -1)
                            {
                                rsaList.Add(user);
                            }
                            else
                            {
                                rsaList[index] = user;
                            }
                        }
                    }

                    if (File.Exists(messagePath))
                        continue;

                    using (var stream = File.Create(messagePath))
                    {
                        byte[] hashValue = Encription.EncryptMimeMessage(message);

                        stream.Write(hashValue, 0, hashValue.Length);
                    }
                }

                if (folder.IsOpen)
                    folder.Close();

                if (Directory.Exists(path))
                {
                    var files = Directory.GetFiles(path);

                    foreach (var file in files)
                    {
                        if (!usesDate.Contains(file))
                        {
                            File.Delete(file);
                        }
                    }
                }
            }
        }

        public MimeMessage[] ReadAllEmailsAndAttachments(IMailFolder folder)
        {
            List<MimeMessage> messages = new List<MimeMessage>();
            string folderName = folderIMail[folder];

            var folderPath = Path.Combine(emailsFolderPath, currentUser.Login, folderName);
            if (Directory.Exists(folderPath))
            {
                var files = Directory.GetFiles(folderPath, "*.eml");
                foreach (var filePath in files)
                {
                    try
                    {
                        var message = DecryptAndReadMessage(filePath);
                        messages.Add(message);
                    }
                    catch
                    {
                        continue;
                    }
                }
            }

            return messages.ToArray();
        }

        private MimeMessage DecryptAndReadMessage(string filePath)
        {
            byte[] encryptedBytes = File.ReadAllBytes(filePath);
            var message = Encription.DecryptMimeMessage(encryptedBytes);

            return message;
        }

        #endregion

        #region ������ ������ �����

        public void ReceiveEmailAsync(IMailFolder folder)
        {
            if (client == null)
                return;

            mailData.Rows.Clear();

            if (!isLoggedIn)
            {
                var messages = ReadAllEmailsAndAttachments(folder);

                for (int i = 0; i < messages.Length; i++)
                {
                    var message = messages[i];

                    string from = string.Join("; ", message.From.Select(x => x.ToString()));

                    mailData.Rows.Add(from, message.Subject, message.TextBody);
                    mailData.Rows[i].HeaderCell.Value = i.ToString();
                }

                mailsCountLabel.Text = messages.Length.ToString();
            }
            else
            {
                var inbox = folder;
                inbox.Open(FolderAccess.ReadOnly);

                for (int i = 0; i < inbox.Count; i++)
                {
                    var message = inbox.GetMessage(i);

                    string from = string.Join("; ", message.From.Select(x => x.ToString()));

                    string subject = message.Subject;
                    bool isEnc = false;

                    if (subject == "^$" || subject == "^%")
                    {
                        subject = "��������� � ������";
                    }
                    else if (subject.StartsWith("%%%") && subject.EndsWith("%%%"))
                    {
                        subject.Replace("%%%", "");
                    }

                    string date = message.Date.ToString();

                    mailData.Rows.Add(from, subject, date);
                    mailData.Rows[i].HeaderCell.Value = i.ToString();
                }

                mailsCountLabel.Text = inbox.Count.ToString();

                inbox.Close();
            }
        }

        private MimeMessage ReceiveEmailWithAttachments(IMailFolder folder, int index)
        {
            MimeMessage message;
            if (!isLoggedIn)
            {
                var messages = ReadAllEmailsAndAttachments(folder);

                message = messages[index];
            }
            else
            {
                var inbox = folder;
                inbox.Open(MailKit.FolderAccess.ReadOnly);

                message = inbox.GetMessage(index);

                inbox.Close();
            }

            return message;
        }

        private IMailFolder getFolder(string name)
        {
            switch (name)
            {
                case "��������":
                    return folderNames["INBOX"];
                case "������������":
                    return folderNames["Sent"];
                case "���������":
                    return folderNames["Drafts"];
                case "�������":
                    return folderNames["Trash"];
                default:
                    return folderNames["INBOX"];
            }
        }

        private void moveMessageToTrash(IMailFolder folder, int index)
        {
            var trash = client.GetFolder(SpecialFolder.Trash);
            trash.Open(FolderAccess.ReadWrite);

            var inbox = folder;
            inbox.Open(FolderAccess.ReadWrite);

            var message = inbox.GetMessage(index);

            if (folder == client.GetFolder(SpecialFolder.Trash))
            {
                inbox.AddFlags(index, MessageFlags.Deleted, true);
                inbox.Expunge();
            }
            else
            {
                inbox.MoveTo(index, trash);
            }

            if (inbox.IsOpen)
            {
                inbox.Close();
            }

            if (trash.IsOpen)
            {
                trash.Close();
            }
        }

        #endregion

        #region ������

        private void refreshButton_Click(object sender, EventArgs e)
        {
            if (foldersList.SelectedIndex != -1)
            {
                GetAllEmails(getFolder(foldersList.SelectedItem.ToString()));
            }

            DownloadAllEmails();
        }

        private void newMessageButton_Click(object sender, EventArgs e)
        {
            if (client == null)
                return;

            using (var sentForm = new OneMailForm(currentUser.SmtpServer, currentUser.SmtpPort, currentUser.Ssl, currentUser.Login, currentUser.Password, rsaList, getFolder("������������")))
            {
                sentForm.ShowDialog();
            }
        }

        #endregion

        #region ����� �����

        private void listFolders_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (client == null)
                return;

            if (foldersList.SelectedIndex != -1)
            {
                GetAllEmails(getFolder(foldersList.SelectedItem.ToString()));
            }
        }

        #endregion

        #region ����� ������

        private void mailData_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            getMessage();
        }

        private void getMessage()
        {
            bool isEnc = false;

            (byte[] key, byte[] iv) desDecrypted = new(new byte[0], new byte[0]);
            string userFrom = "";

            if (client == null)
                return;

            if (foldersList.SelectedIndex != -1)
            {
                if (mailData.SelectedRows.Count > 0)
                {
                    var selectedRow = mailData.SelectedRows[0];
                    var header = selectedRow.HeaderCell.Value.ToString();

                    var message = ReceiveEmailWithAttachments(getFolder(foldersList.SelectedItem.ToString()), int.Parse(header));

                    if (message.Subject.StartsWith("%%%") && message.Subject.EndsWith("%%%"))
                    {
                        isEnc = true;
                        try
                        {
                            RSAUsers user = rsaList.Find(obj => obj.userName == string.Join("; ", message.From.Select(x => x.ToString())));

                            userFrom = user.userName;

                            if (user == null)
                            {
                                throw new Exception("������� ������������ ��� � ����� ���������");
                            }
                            else if (string.IsNullOrEmpty(user.publicKey))
                            {
                                throw new Exception("������ ������������ �� ��������� ��� ���� ����");
                            }

                            var attachments = message.Attachments.ToList();

                            var messageBodyAttachment = attachments[0] as MimePart;
                            var desKeyAttachment = attachments[1] as MimePart;
                            var desIVAttachment = attachments[2] as MimePart;

                            if (messageBodyAttachment == null && desKeyAttachment == null || desIVAttachment == null)
                            {
                                throw new Exception("������ ������ ������ ������");
                            }

                            string messagePath = Path.Combine("temp_emails", currentUser.Login, "body_temp");
                            string keyPath = Path.Combine("temp_emails", currentUser.Login, "des_key_temp");
                            string ivPath = Path.Combine("temp_emails", currentUser.Login, "des_iv_temp");

                            byte[] desKey;
                            using (var stream = File.Create(keyPath))
                            {
                                // ���������� �����
                                desKeyAttachment.Content.DecodeTo(stream);
                            }

                            desKey = File.ReadAllBytes(keyPath);

                            byte[] desIV;
                            using (var stream = File.Create(ivPath))
                            {
                                // ���������� �����
                                desIVAttachment.Content.DecodeTo(stream);
                            }

                            desIV = File.ReadAllBytes(ivPath);

                            desDecrypted = EncryptedMessage.DecryptDES(user.userName, currentUser.Login, desKey, desIV);

                            byte[] messageBody;
                            using (var stream = File.Create(messagePath))
                            {
                                // ���������� �����
                                messageBodyAttachment.Content.DecodeTo(stream);
                            }

                            messageBody = File.ReadAllBytes(messagePath);

                            byte[] decryptedMessage = EncryptedMessage.DecryptMessage(messagePath, desDecrypted.key, desDecrypted.iv, user.userName, currentUser.Login);

                            string htmlMessage = Encoding.UTF8.GetString(decryptedMessage);

                            // ---------------------------------------------- //

                            // ������� ������ ���������
                            MimeMessage m = new MimeMessage();

                            var from = message.From.ToList()[0];

                            m.From.Add(from);

                            var to = message.To.ToList()[0];

                            m.To.Add(to);

                            // ���� ������
                            m.Subject = message.Subject.Replace("%%%", "");

                            // ����� ������
                            var text = new TextPart("html")
                            {
                                Text = htmlMessage
                            };

                            // ������� ��������� ���� ���������
                            var body = new Multipart("mixed")
                            {
                                text
                            };

                            attachments.RemoveAt(0);
                            attachments.RemoveAt(0);
                            attachments.RemoveAt(0);

                            while (attachments.Count > 0)
                            {
                                var att = attachments[0] as MimePart;
                                body.Add(att);
                                attachments.RemoveAt(0);
                            }

                            m.Body = body;

                            message = m;
                        }
                        catch (Exception exc)
                        {
                            MessageBox.Show("������ ������������� ���������:\n" + exc.Message, "������", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                        EncryptedMessage.RemoveTempFiles();
                    }

                    using (var sentForm = new OneMailForm(message, isEnc, desDecrypted, userFrom, currentUser.Login))
                    {
                        sentForm.ShowDialog();
                    }
                }
            }
        }

        #endregion

        private void mailData_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                // �������� ������� ������ � �������
                int rowIndex = e.RowIndex;
                int colIndex = e.ColumnIndex;

                // ������������� ������ ��� �������
                mailData.CurrentCell = mailData.Rows[rowIndex].Cells[colIndex];
            }
        }

        private void �������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mailData.SelectedRows.Count > 0)
            {
                // �������� ������� ������ � �������
                var selectedRow = mailData.SelectedRows[0];
                var header = int.Parse(selectedRow.HeaderCell.Value.ToString());

                IMailFolder folder = getFolder(foldersList.SelectedItem.ToString());

                moveMessageToTrash(folder, header);

                GetAllEmails(folder);

                return;
            }
        }

        private void contactButton_Click(object sender, EventArgs e)
        {
            using (var rsaForm = new RSAListForm(currentUser.SmtpServer, currentUser.SmtpPort, currentUser.Ssl, currentUser.Login, currentUser.Password, ref rsaList, getFolder("������������")))
            {
                rsaForm.ShowDialog();
            }

            EncryptedMessage.WriteAllUserFromJsonFile(rsaList, currentUser.Login);
        }

        private void �������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            getMessage();
        }
    }
}
