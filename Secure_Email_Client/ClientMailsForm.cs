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
using static System.Net.Mime.MediaTypeNames;

namespace Secure_Email_Client
{
    public partial class ClientMailsForm : Form
    {
        private ImapClient client;
        readonly string emailsFolderPath = "emails";

        Dictionary<string, IMailFolder> folderNames;
        Dictionary<IMailFolder, string> folderIMail;
        Dictionary<string, string> withoutLoggedInFolders;

        User currentUser;

        List<RSAUsers> rsaList;

        bool isLoggedIn = true;

        UserAuthorization form;

        public ClientMailsForm(User user, UserAuthorization form)
        {
            InitializeComponent();

            mailData.ContextMenuStrip = contextMenuStrip1;

            currentUser = user;

            mailNameLabel.Text = currentUser.Login;

            try
            {
                client = new ImapClient();

                this.form = form;

                client.Connect(currentUser.ImapServer, currentUser.ImapPort, currentUser.Ssl);
                client.Authenticate(currentUser.Login, currentUser.Password);
            }
            catch (Exception exc)
            {
                MessageBox.Show("Ошибка авторизации:\n" + exc.Source + ": " + exc.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                isLoggedIn = false;
            }
        }

        private void ClientMailsForm_Load(object sender, EventArgs e)
        {
            if (isLoggedIn)
            {
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
            }
            else
            {
                withoutLoggedInFolders = new Dictionary<string, string>
                {
                    { "Входящие", "INBOX" },
                    { "Отправленные", "Sent" },
                    { "Черновики", "Drafts" },
                    { "Корзина", "Trash" }
                };
            }

            SetToolTips();

            foldersList.SelectedIndex = 0;

            foldersList.SelectedIndexChanged += listFolders_SelectedIndexChanged;

            rsaList = new List<RSAUsers>();

            EncryptedMessage.GetAllUserFromJsonFile(ref rsaList, currentUser.Login);

            newMessageButton.Visible = isLoggedIn;

            contactButton.Visible = isLoggedIn;

            if (isLoggedIn)
                DownloadAllEmails();

            form.Visible = false;
        }

        private void SetToolTips()
        {
            ToolTip tool = new ToolTip();
            tool.SetToolTip(contactButton, "Контакты");
            tool.SetToolTip(newMessageButton, "Написать новое письмо");
            tool.SetToolTip(refreshButton, "Обновить");
            //tool.SetToolTip(contactButton, "Контакты");
        }

        public void DownloadAllEmails()
        {
            GetAllEmailsAndSaveAttachmentsAsync();

            EncryptedMessage.WriteAllUserFromJsonFile(rsaList, currentUser.Login);
        }

        public void GetAllEmails(string folder)
        {
            ReceiveEmailAsync(folder);
        }

        #region Работа с почтой на компьютере

        public void GetAllEmailsAndSaveAttachmentsAsync()
        {
            if (!Directory.Exists(emailsFolderPath))
            {
                Directory.CreateDirectory(emailsFolderPath);
            }

            foreach (var folderDict in folderNames)
            {
                var folder = folderDict.Value;

                folder.Open(FolderAccess.ReadWrite);

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
                            MessageBox.Show($"Пользователь {from} отправил Вам свой ключ шифрования\n" +
                                $"Вы можете отправлять ему шифрованные сообщения", "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Question);

                            string fileName = "";

                            foreach (var rsaPart in message.Attachments.OfType<MimePart>())
                            {
                                using (var stream = File.Create(rsaPart.FileName))
                                {
                                    // Сохранение файла
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
                            if (MessageBox.Show($"У вас новый запрос на шифрованное подключение от пользователя {from}\n" +
                            "Желаете его принять?", "Уведомление", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                            {
                                continue;
                            }

                            // кому отправляем
                            string to = from;

                            // создаем объект сообщения
                            MimeMessage m = new MimeMessage();
                            m.From.Add(new MailboxAddress("", currentUser.Login));
                            m.To.Add(new MailboxAddress("", to));
                            // тема письма
                            m.Subject = "^%";
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

                            // адрес smtp-сервера и порт, с которого будем отправлять письмо
                            using (MailKit.Net.Smtp.SmtpClient client = new MailKit.Net.Smtp.SmtpClient())
                            {
                                client.Connect(currentUser.SmtpServer, currentUser.SmtpPort, currentUser.Ssl);

                                client.Authenticate(currentUser.Login, currentUser.Password);

                                // отправляем сообщение
                                try
                                {
                                    client.Send(m);

                                    var sentFolder = getFolder("Отправленные");

                                    sentFolder.Open(FolderAccess.ReadWrite);

                                    sentFolder.Append(m);

                                    sentFolder.Close();
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("Сообщение с ключем не отправлено.\n" + ex.Message);
                                }
                            }

                            string fileName = "";

                            foreach (var rsaPart in message.Attachments.OfType<MimePart>())
                            {
                                using (var stream = File.Create(rsaPart.FileName))
                                {
                                    // Сохранение файла
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
                    else if (File.Exists(messagePath + ".deleted"))
                    {
                        var trash = client.GetFolder(SpecialFolder.Trash);
                        trash.Open(FolderAccess.ReadWrite);

                        if (!folder.IsOpen)
                            folder.Open(FolderAccess.ReadWrite);

                        var mess = folder.GetMessage(uid);

                        if (folder == client.GetFolder(SpecialFolder.Trash))
                        {
                            folder.AddFlags(uid, MessageFlags.Deleted, true);
                            folder.Expunge();
                        }
                        else
                        {
                            folder.MoveTo(uid, trash);
                        }

                        if (trash.IsOpen)
                            trash.Close();

                        File.Delete(messagePath + ".deleted");

                        continue;
                    }

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

        public MimeMessage[] ReadAllEmailsAndAttachments(string folder)
        {
            List<MimeMessage> messages = new List<MimeMessage>();
            string folderName = withoutLoggedInFolders[folder];

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

        #region Методы работы почты

        public void ReceiveEmailAsync(string folder)
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

                    string date = message.Date.DateTime.ToString();

                    mailData.Rows.Add(from, message.Subject, date);
                    mailData.Rows[i].HeaderCell.Value = i.ToString();
                }

                mailsCountLabel.Text = messages.Length.ToString();
            }
            else
            {
                var inbox = getFolder(folder);
                inbox.Open(FolderAccess.ReadOnly);

                for (int i = 0; i < inbox.Count; i++)
                {
                    var message = inbox.GetMessage(i);

                    string from = string.Join("; ", message.From.Select(x => x.ToString()));

                    string subject = message.Subject;
                    bool isEnc = false;

                    if (subject == "^$" || subject == "^%")
                    {
                        subject = "Сообщение с ключем";
                    }
                    else if (subject.StartsWith("%%%") && subject.EndsWith("%%%"))
                    {
                        subject.Replace("%%%", "");
                    }

                    string date = message.Date.DateTime.ToString();

                    mailData.Rows.Add(from, subject, date);
                    mailData.Rows[i].HeaderCell.Value = i.ToString();
                }

                mailsCountLabel.Text = inbox.Count.ToString();

                inbox.Close();
            }
        }

        private MimeMessage ReceiveEmailWithAttachments(string folder, int index)
        {
            MimeMessage message;
            if (!isLoggedIn)
            {
                var messages = ReadAllEmailsAndAttachments(folder);

                message = messages[index];
            }
            else
            {
                var inbox = getFolder(folder);
                inbox.Open(FolderAccess.ReadOnly);

                message = inbox.GetMessage(index);

                inbox.Close();
            }

            return message;
        }

        private IMailFolder getFolder(string name)
        {
            switch (name)
            {
                case "Входящие":
                    return folderNames["INBOX"];
                case "Отправленные":
                    return folderNames["Sent"];
                case "Черновики":
                    return folderNames["Drafts"];
                case "Корзина":
                    return folderNames["Trash"];
                default:
                    return folderNames["INBOX"];
            }
        }

        private void moveMessageToTrash(string folder, int index, string date)
        {
            if (isLoggedIn)
            {
                var trash = client.GetFolder(SpecialFolder.Trash);
                trash.Open(FolderAccess.ReadWrite);

                var inbox = getFolder(folder);
                inbox.Open(FolderAccess.ReadWrite);

                var message = inbox.GetMessage(index);

                if (folder == "Корзина")
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
            else
            {
                var folderPath = Path.Combine(emailsFolderPath, currentUser.Login, withoutLoggedInFolders[folder], date.Replace(':', '.') + ".eml");

                if (folder == "Корзина")
                {
                    string newFileName = Path.Combine(emailsFolderPath, currentUser.Login, withoutLoggedInFolders[folder], date.Replace(':', '.') + ".eml.deleted");

                    if (File.Exists(folderPath))
                    {
                        File.Move(folderPath, newFileName);
                        File.Delete(folderPath);
                    }
                }
                else
                {
                    string newFolderPath = Path.Combine(emailsFolderPath, currentUser.Login, "Trash", date.Replace(':', '.') + ".eml");
                    string newFileName = Path.Combine(emailsFolderPath, currentUser.Login, withoutLoggedInFolders[folder], date.Replace(':', '.') + ".eml.deleted");

                    if (File.Exists(folderPath))
                    {
                        File.Copy(folderPath, newFolderPath);
                        File.Move(folderPath, newFileName);
                        File.Delete(folderPath);
                    }
                }
            }
        }

        #endregion

        #region Кнопки

        private void refreshButton_Click(object sender, EventArgs e)
        {
            if (foldersList.SelectedIndex != -1)
            {
                GetAllEmails(foldersList.SelectedItem.ToString());
            }

            if (isLoggedIn)
                DownloadAllEmails();
        }

        private void newMessageButton_Click(object sender, EventArgs e)
        {
            if (client == null)
                return;

            using (var sentForm = new OneMailForm(currentUser.SmtpServer, currentUser.SmtpPort, currentUser.Ssl, currentUser.Login, currentUser.Password, rsaList, getFolder("Отправленные")))
            {
                sentForm.ShowDialog();
            }

            EncryptedMessage.DeleteDirectory(EncryptedMessage.tempEmails);
        }

        #endregion

        #region Выбор папки

        private void listFolders_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (client == null)
                return;

            if (foldersList.SelectedIndex != -1)
            {
                GetAllEmails(foldersList.SelectedItem.ToString());
            }
        }

        #endregion

        #region Выбор письма

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
                    var createPath = Path.Combine("temp_emails", currentUser.Login);

                    if (!Directory.Exists(createPath))
                    {
                        Directory.CreateDirectory(createPath);
                    }

                    var selectedRow = mailData.SelectedRows[0];
                    var header = selectedRow.HeaderCell.Value.ToString();

                    var message = ReceiveEmailWithAttachments(foldersList.SelectedItem.ToString(), int.Parse(header));

                    var attachments = message.Attachments.ToList();

                    var espFile = attachments.Find(obj => (obj as MimePart).FileName == "Signature.enc") as MimePart;
                    var espKey = attachments.Find(obj => (obj as MimePart).FileName == "SignatureKey.enc") as MimePart;

                    if (espFile != null && espKey != null)
                    {
                        string signatureFilePath = Path.Combine("temp_emails", currentUser.Login, "signature_file_temp");
                        string signatureKeyPath = Path.Combine("temp_emails", currentUser.Login, "signature_key_temp");

                        byte[] signFile;
                        using (var stream = File.Create(signatureFilePath))
                        {
                            espFile.Content.DecodeTo(stream);
                        }

                        signFile = File.ReadAllBytes(signatureFilePath);

                        byte[] signKey;
                        using (var stream = File.Create(signatureKeyPath))
                        {
                            espKey.Content.DecodeTo(stream);
                        }

                        signKey = File.ReadAllBytes(signatureKeyPath);

                        string publicKey = Encoding.UTF8.GetString(signKey);

                        attachments.Remove(espFile);
                        attachments.Remove(espKey);

                        List<string> filePaths = new List<string>();

                        byte[] array = new byte[0];

                        if (message.HtmlBody != null)
                        {
                            array = Encoding.UTF8.GetBytes(message.HtmlBody);
                        }

                        foreach (var item in attachments)
                        {
                            var att = item as MimePart;

                            using (var ms = new MemoryStream())
                            {
                                att.Content.DecodeTo(ms);

                                array = array.Concat(ms.ToArray()).ToArray();
                            }
                        }

                        var isRight = EncryptedMessage.VerifySignature(array, signFile, publicKey);

                        if (!isRight)
                        {
                            if (MessageBox.Show("Сигнатура письма не верифицирована, дальнейшее открытие сообщения может быть опасно\n" +
                                "Открыть письмо?", "Ошибка", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.No)
                            {
                                EncryptedMessage.DeleteDirectory(EncryptedMessage.tempEmails);
                                return;
                            }
                        }
                    }

                    if (message.Subject.StartsWith("%%%") && message.Subject.EndsWith("%%%"))
                    {
                        isEnc = true;
                        try
                        {
                            RSAUsers user = rsaList.Find(obj => obj.userName == string.Join("; ", message.From.Select(x => x.ToString())));

                            userFrom = user.userName;

                            if (user == null)
                            {
                                throw new Exception("Данного пользователя нет в Ваших контактах");
                            }
                            else if (string.IsNullOrEmpty(user.publicKey))
                            {
                                throw new Exception("Данный пользователь не отправлял Вам свой ключ");
                            }

                            var messageBodyAttachment = attachments[0] as MimePart;
                            var desKeyAttachment = attachments[1] as MimePart;
                            var desIVAttachment = attachments[2] as MimePart;

                            if (messageBodyAttachment == null && desKeyAttachment == null || desIVAttachment == null)
                            {
                                throw new Exception("Ошибка чтения данных письма");
                            }

                            string messagePath = Path.Combine("temp_emails", currentUser.Login, "body_temp");
                            string keyPath = Path.Combine("temp_emails", currentUser.Login, "des_key_temp");
                            string ivPath = Path.Combine("temp_emails", currentUser.Login, "des_iv_temp");

                            byte[] desKey;
                            using (var stream = File.Create(keyPath))
                            {
                                // Сохранение файла
                                desKeyAttachment.Content.DecodeTo(stream);
                            }

                            desKey = File.ReadAllBytes(keyPath);

                            byte[] desIV;
                            using (var stream = File.Create(ivPath))
                            {
                                // Сохранение файла
                                desIVAttachment.Content.DecodeTo(stream);
                            }

                            desIV = File.ReadAllBytes(ivPath);

                            desDecrypted = EncryptedMessage.DecryptDES(user.userName, currentUser.Login, desKey, desIV);

                            byte[] messageBody;
                            using (var stream = File.Create(messagePath))
                            {
                                // Сохранение файла
                                messageBodyAttachment.Content.DecodeTo(stream);
                            }

                            messageBody = File.ReadAllBytes(messagePath);

                            byte[] decryptedMessage = EncryptedMessage.DecryptMessage(messagePath, desDecrypted.key, desDecrypted.iv, user.userName, currentUser.Login);

                            string htmlMessage = Encoding.UTF8.GetString(decryptedMessage);

                            // ---------------------------------------------- //

                            // создаем объект сообщения
                            MimeMessage m = new MimeMessage();

                            var from = message.From.ToList()[0];

                            m.From.Add(from);

                            var to = message.To.ToList()[0];

                            m.To.Add(to);

                            // тема письма
                            m.Subject = message.Subject.Replace("%%%", "");

                            // текст письма
                            var text = new TextPart("html")
                            {
                                Text = htmlMessage
                            };

                            // создаем составное тело сообщения
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
                            MessageBox.Show("Ошибка расшифрования сообщения:\n" + exc.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                        EncryptedMessage.DeleteDirectory(EncryptedMessage.tempEmails);
                    }

                    using (var sentForm = new OneMailForm(message, isEnc, desDecrypted, userFrom, currentUser.Login))
                    {
                        sentForm.ShowDialog();
                    }

                    EncryptedMessage.DeleteDirectory(EncryptedMessage.tempEmails);
                }
            }
        }

        #endregion

        private void mailData_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                // Получаем индексы строки и столбца
                int rowIndex = e.RowIndex;
                int colIndex = e.ColumnIndex;

                // Устанавливаем ячейку как текущую
                mailData.CurrentCell = mailData.Rows[rowIndex].Cells[colIndex];
            }
        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mailData.SelectedRows.Count > 0)
            {
                // Получаем индексы строки и столбца
                var selectedRow = mailData.SelectedRows[0];
                var header = int.Parse(selectedRow.HeaderCell.Value.ToString());
                var date = selectedRow.Cells[2].Value.ToString();

                var folder = foldersList.SelectedItem.ToString();

                moveMessageToTrash(folder, header, date);

                GetAllEmails(folder);

                return;
            }
        }

        private void contactButton_Click(object sender, EventArgs e)
        {
            using (var rsaForm = new RSAListForm(currentUser.SmtpServer, currentUser.SmtpPort, currentUser.Ssl, currentUser.Login, currentUser.Password, ref rsaList, getFolder("Отправленные")))
            {
                rsaForm.ShowDialog();
            }

            EncryptedMessage.WriteAllUserFromJsonFile(rsaList, currentUser.Login);
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            getMessage();
        }
    }
}
