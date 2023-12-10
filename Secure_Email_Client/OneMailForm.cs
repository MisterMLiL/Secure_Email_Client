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
using Microsoft.Web.WebView2.WinForms;
using Microsoft.Web.WebView2.Wpf;
using Microsoft.Web.WebView2.Core;
using System.Net.Mail;
using System.Net;
using MailKit.Net.Imap;
using System.Diagnostics.Eventing.Reader;
using MailKit.Security;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.IO;
using MailKit;
using System.Security.Cryptography;
using System.Diagnostics.Metrics;
using Microsoft.VisualBasic.ApplicationServices;
using System.Diagnostics;
using Org.BouncyCastle.Crypto;

namespace Secure_Email_Client
{
    public partial class OneMailForm : Form
    {
        MimeMessage read;
        string smtpServer;
        int port;
        bool useSsl;
        string login;
        string password;
        List<FileAttachment> fileAttachments;
        IMailFolder sentFolder;

        bool isEnc = false;

        (byte[]? key, byte[]? iv) des;
        string? userFrom;

        string userSeparator = "; ";

        #region Конструкторы

        // Для нового сообщения
        public OneMailForm(string smtpServer, int port, bool useSsl, string login, string password, List<RSAUsers> rsaUsers, IMailFolder sentFolder)
        {
            InitializeComponent();

            subjectTextBox.ReadOnly = false;
            mailBodyRTB.ReadOnly = false;

            formatingPanel.Visible = true;
            buttonsPanel.Visible = true;
            buttonAttachmentsPanel.Visible = true;
            attachmentsPanel.Visible = false;
            oneUserPanel.Visible = false;

            webView.Visible = false;

            this.smtpServer = smtpServer;
            this.port = port;
            this.useSsl = useSsl;
            this.login = login;
            this.password = password;

            fileAttachments = new List<FileAttachment>();

            this.sentFolder = sentFolder;

            foreach (var user in rsaUsers)
            {
                rsaUsersList.Items.Add(user.userName);
            }
        }

        // Для чтения сообщения
        public OneMailForm(MimeMessage message, bool isEnc, (byte[]? key, byte[]? iv) des, string userFrom, string login)
        {
            InitializeComponent();

            subjectTextBox.ReadOnly = true;
            mailBodyRTB.ReadOnly = true;

            formatingPanel.Visible = false;
            buttonsPanel.Visible = false;
            buttonAttachmentsPanel.Visible = false;
            addToButton.Visible = false;
            oneUserPanel.Visible = false;

            this.read = message ?? throw new ArgumentNullException(nameof(message));

            var attachments = this.read.Attachments.ToList();
            for (int j = 0; j < attachments.Count; j++)
            {
                var attachment = attachments[j] as MimePart;
                if (attachment != null)
                {
                    if (attachment.FileName == "Signature.enc" || attachment.FileName == "SignatureKey.enc")
                        continue;

                    attachmentsList.Items.Add(attachment.ContentId + attachment.FileName);
                }
            }

            attachmentsPanel.Visible = attachmentsList.Items.Count > 0;

            toTextBox.Text = this.read.From.ToString();
            subjectTextBox.Text = this.read.Subject.ToString();

            if (this.read.HtmlBody != null)
            {
                // Инициализация WebView2
                webView.CoreWebView2InitializationCompleted += WebView_CoreWebView2InitializationCompleted;
                webView.EnsureCoreWebView2Async(null);
                mailBodyRTB.Visible = false;
            }
            else if (this.read.TextBody != null)
            {
                mailBodyRTB.Text = this.read.TextBody.ToString();
                webView.Visible = false;
            }
            else
            {
                mailBodyRTB.Text = "**Пустое сообщение**";
                webView.Visible = false;
            }

            this.isEnc = isEnc;
            this.des = des;
            this.userFrom = userFrom;
            this.login = login;
        }

        //// Для черновика
        //public OneMailForm(string smtpServer, message message)
        //{
        //    InitializeComponent();

        //    toTextBox.ReadOnly = false;
        //    subjectTextBox.ReadOnly = false;
        //    mailBodyRTB.ReadOnly = false;

        //    formatingPanel.Visible = true;
        //    buttonsPanel.Visible = true;
        //    buttonAttachmentsPanel.Visible = true;
        //    attachmentsPanel.Visible = false;

        //    webView.Visible = false;

        //    this.message = message;

        //    this.smtpServer = smtpServer;
        //}

        #endregion

        #region WebView2

        private void WebView_CoreWebView2InitializationCompleted(object sender, CoreWebView2InitializationCompletedEventArgs e)
        {
            if (e.IsSuccess)
            {
                // Успешная инициализация, теперь можно загрузить HTML из строки
                LoadHtml(this.read.HtmlBody);
            }
            else
            {
                // Обработка ошибок инициализации
                MessageBox.Show($"WebView2 initialization failed. Error: {e.InitializationException.Message}");
            }
        }

        private void LoadHtml(string htmlContent)
        {
            // Загрузка HTML из строки
            webView.CoreWebView2.NavigateToString(htmlContent);
        }

        #endregion

        #region Прикрепленные файлы

        // Добавить новый файл
        private void addButton_Click(object sender, EventArgs e)
        {
            GetAttachment();

            ChangeList();
        }

        // Выбрать новый файл
        private void GetAttachment()
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    var splitname = openFileDialog.FileName.Split('\\');
                    var filename = splitname.Last();
                    fileAttachments.Add(new FileAttachment(filename, openFileDialog.FileName));
                }
            }
        }

        // Обновить список файлов
        private void ChangeList()
        {
            attachmentsList.Items.Clear();

            foreach (FileAttachment item in fileAttachments)
                attachmentsList.Items.Add(item.FileName);

            if (attachmentsList.Items.Count > 0)
            {
                attachmentsPanel.Visible = true;
            }
            else
            {
                attachmentsPanel.Visible = false;
            }
        }

        // Удалить файл
        private void deleteButton_Click(object sender, EventArgs e)
        {
            int index = attachmentsList.SelectedIndex;

            if (index != -1)
            {
                fileAttachments.RemoveAt(index);
            }

            ChangeList();
        }

        #endregion

        // Добавить получателя
        private void addToButton_Click(object sender, EventArgs e)
        {
            List<string> users = new List<string>();

            if (!string.IsNullOrEmpty(toTextBox.Text))
            {
                foreach (string item in toTextBox.Text.Split(userSeparator))
                {
                    users.Add(item);
                }
            }

            using (AddToForm add = new AddToForm(ref users))
            {
                add.ShowDialog();
            }

            if (users.Count > 0)
            {
                toTextBox.Text = string.Join(userSeparator, users.ToArray());
            }
        }

        // Отправить готовое письмо
        private bool CreateMessage()
        {
            List<Stream> streams = new List<Stream>();

            // кому отправляем
            string to = toTextBox.Text;

            // создаем объект сообщения
            MimeMessage m = new MimeMessage();

            m.From.Add(new MailboxAddress("", login));

            foreach (string item in to.Split(userSeparator))
                m.To.Add(new MailboxAddress("", item));

            // тема письма
            m.Subject = subjectTextBox.Text;

            // текст письма
            var text = new TextPart("html")
            {
                Text = HTMLFormater.ConvertRichTextBoxToHtml(mailBodyRTB)
            };

            // создаем составное тело сообщения
            var body = new Multipart("mixed")
            {
                text
            };

            if (checkESP.Checked)
            {
                byte[] textBytes = Encoding.UTF8.GetBytes(text.Text);
                byte[] attachmentsBytes = fileAttachments.SelectMany(item => File.ReadAllBytes(item.FilePathName)).ToArray();

                byte[] combinedBytes = textBytes.Concat(attachmentsBytes).ToArray();

                string privateKey;
                string publicKey;

                using (var dsa = new DSACryptoServiceProvider())
                {
                    privateKey = dsa.ToXmlString(true);
                    publicKey = dsa.ToXmlString(false);
                }

                var signature = EncryptedMessage.CreateSignature(combinedBytes, privateKey);

                var msData = new MemoryStream(signature);
                streams.Add(msData);

                var att = new MimePart()
                {
                    Content = new MimeContent(msData),
                    ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
                    ContentTransferEncoding = ContentEncoding.Base64,
                    FileName = "Signature.enc"
                };

                body.Add(att);

                MemoryStream msKey = new MemoryStream(Encoding.UTF8.GetBytes(publicKey));
                streams.Add(msKey);

                att = new MimePart()
                {
                    Content = new MimeContent(msKey),
                    ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
                    ContentTransferEncoding = ContentEncoding.Base64,
                    FileName = "SignatureKey.enc"
                };

                body.Add(att);
            }

            foreach (var item in fileAttachments)
            {
                var stream = File.OpenRead(item.FilePathName);
                streams.Add(stream);

                var attachment = new MimePart()
                {
                    Content = new MimeContent(stream),
                    ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
                    ContentTransferEncoding = ContentEncoding.Base64,
                    FileName = item.FileName
                };

                body.Add(attachment);
            }

            m.Body = body;

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

                    foreach (var item in streams)
                    {
                        try
                        {
                            item.Close();
                        }
                        catch
                        {
                            continue;
                        }
                    }

                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Сообщение не отправлено.\n" + ex.Message);

                    foreach (var item in streams)
                    {
                        try
                        {
                            item.Close();
                        }
                        catch
                        {
                            continue;
                        }
                    }

                    return false;
                }
            }

            return false;
        }

        // Отправить готовое шифрованное письмо
        private bool CreateEncryptedMessage()
        {
            List<Stream> streams = new List<Stream>();

            // кому отправляем
            string to = rsaUsersList.Text;

            // создаем объект сообщения
            MimeMessage m = new MimeMessage();

            m.From.Add(new MailboxAddress("", login));

            m.To.Add(new MailboxAddress("", to));

            // тема письма
            m.Subject = "%%%" + subjectTextBox.Text + "%%%";

            string bodyText = HTMLFormater.ConvertRichTextBoxToHtml(mailBodyRTB);

            (byte[] key, byte[] iv) des = EncryptedMessage.GenerateDESKeyIV();

            var text = Encoding.UTF8.GetBytes(bodyText);

            string bodyTextPath = EncryptedMessage.EncryptMessage(text, to, login, DateTime.Now.Millisecond, des.key, des.iv);

            // создаем составное тело сообщения
            var body = new Multipart("mixed");

            var attachment = new MimePart();

            var bodyStream = File.OpenRead(bodyTextPath);
            streams.Add(bodyStream);

            (string key, string iv) encryptedDES = EncryptedMessage.EncryptDES(to, login, des.key, des.iv);

            var desKeyStream = File.OpenRead(encryptedDES.key);
            streams.Add(desKeyStream);

            var desIVStream = File.OpenRead(encryptedDES.iv);
            streams.Add(desIVStream);

            List<(string FileName, string FilePath)> listOfStreamAttachments = new List<(string, string)>();

            foreach (var item in fileAttachments)
            {
                byte[] data = File.ReadAllBytes(item.FilePathName);
                string currentAttachmentPath = EncryptedMessage.EncryptMessage(data, to, login, DateTime.Now.Millisecond, des.key, des.iv);

                listOfStreamAttachments.Add((item.FileName, currentAttachmentPath));
            }

            attachment = new MimePart()
            {
                Content = new MimeContent(bodyStream),
                ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
                ContentTransferEncoding = ContentEncoding.Base64,
                FileName = "BodyText.ef"
            };

            body.Add(attachment);

            attachment = new MimePart()
            {
                Content = new MimeContent(desKeyStream),
                ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
                ContentTransferEncoding = ContentEncoding.Base64,
                FileName = "DESKey.ef"
            };

            body.Add(attachment);

            attachment = new MimePart()
            {
                Content = new MimeContent(desIVStream),
                ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
                ContentTransferEncoding = ContentEncoding.Base64,
                FileName = "DESIV.ef"
            };

            body.Add(attachment);

            if (checkESP.Checked)
            {
                long length = bodyStream.Length;
                byte[] textBytes = new byte[length];
                bodyStream.Read(textBytes, 0, (int)length);

                byte[] combinedBytes = textBytes;

                length = desKeyStream.Length;
                textBytes = new byte[length];
                desKeyStream.Read(textBytes, 0, (int)length);

                combinedBytes = combinedBytes.Concat(textBytes).ToArray();

                length = desIVStream.Length;
                textBytes = new byte[length];
                desIVStream.Read(textBytes, 0, (int)length);

                combinedBytes = combinedBytes.Concat(textBytes).ToArray();

                byte[] attachmentsBytes = listOfStreamAttachments.SelectMany(item => File.ReadAllBytes(item.FilePath)).ToArray();

                combinedBytes = combinedBytes.Concat(attachmentsBytes).ToArray();

                string privateKey;
                string publicKey;

                using (var dsa = new DSACryptoServiceProvider())
                {
                    privateKey = dsa.ToXmlString(true);
                    publicKey = dsa.ToXmlString(false);
                }

                var signature = EncryptedMessage.CreateSignature(combinedBytes, privateKey);

                MemoryStream msData = new MemoryStream(signature);
                streams.Add(msData);

                var att = new MimePart()
                {
                    Content = new MimeContent(msData),
                    ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
                    ContentTransferEncoding = ContentEncoding.Base64,
                    FileName = "Signature.enc"
                };

                body.Add(att);

                MemoryStream msKey = new MemoryStream(Encoding.UTF8.GetBytes(publicKey));
                streams.Add(msKey);

                att = new MimePart()
                {
                    Content = new MimeContent(msKey),
                    ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
                    ContentTransferEncoding = ContentEncoding.Base64,
                    FileName = "SignatureKey.enc"
                };

                body.Add(att);
            }

            List<FileAttachment> encFileAtt = new List<FileAttachment>();

            foreach (var item in listOfStreamAttachments)
            {
                encFileAtt.Add(new FileAttachment(item.FileName, item.FilePath));
            }

            foreach (var item in encFileAtt)
            {
                var stream = File.OpenRead(item.FilePathName);
                streams.Add(stream);

                attachment = new MimePart()
                {
                    Content = new MimeContent(stream),
                    ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
                    ContentTransferEncoding = ContentEncoding.Base64,
                    FileName = item.FileName
                };

                body.Add(attachment);
            }

            m.Body = body;

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

                    foreach (var item in streams)
                    {
                        try
                        {
                            item.Close();
                        }
                        catch
                        {
                            continue;
                        }
                    }

                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Сообщение не отправлено.\n" + ex.Message);

                    foreach (var item in streams)
                    {
                        try
                        {
                            item.Close();
                        }
                        catch
                        {
                            continue;
                        }
                    }

                    return false;
                }
            }

            return false;
        }

        private void sentButton_Click(object sender, EventArgs e)
        {
            if (!checkEncrypt.Checked)
            {
                CreateMessage();
            }
            else
            {
                CreateEncryptedMessage();
            }
        }

        private void attachmentsList_DoubleClick(object sender, EventArgs e)
        {
            int index = attachmentsList.SelectedIndex;

            if (index == -1)
                return;

            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string attachmentPath = saveFileDialog.FileName;
                    int counter = 0;

                    if (isEnc)
                    {
                        foreach (var att in read.Attachments.OfType<MimePart>())
                        {
                            if (counter != index)
                            {
                                counter++;
                                continue;
                            }

                            var tempFilePath = Path.Combine("temp_emails", login, att.FileName);

                            using (var stream = File.Create(tempFilePath))
                            {
                                // Сохранение файла
                                att.Content.DecodeTo(stream);
                            }

                            var decodeBytes = EncryptedMessage.DecryptMessage(tempFilePath, des.key, des.iv, login, userFrom);

                            var attachment = new MimePart();
                            using (var stream = File.OpenRead(tempFilePath))
                            {
                                using (var streamWrite = new BinaryWriter(File.Open(attachmentPath, FileMode.Create)))
                                {
                                    streamWrite.Write(decodeBytes);
                                }
                            }

                            if (File.Exists(tempFilePath))
                            {
                                File.Delete(tempFilePath);
                            }

                            return;
                        }
                    }
                    else
                    {
                        foreach (var att in read.Attachments.OfType<MimePart>())
                        {
                            if (counter != index)
                            {
                                counter++;
                                continue;
                            }

                            using (var stream = File.Create(attachmentPath))
                            {
                                // Сохранение файла
                                att.Content.DecodeTo(stream);
                            }

                            return;
                        }
                    }
                }
            }
        }

        #region Форматирование

        private void cmdBold_Click(object sender, EventArgs e)
        {
            Font newFont, oldFont;
            oldFont = mailBodyRTB.SelectionFont;
            if (oldFont.Bold)
                newFont = new Font(oldFont, oldFont.Style & ~FontStyle.Bold);
            else
                newFont = new Font(oldFont, oldFont.Style | FontStyle.Bold);

            mailBodyRTB.SelectionFont = newFont;
            mailBodyRTB.Focus();
        }

        private void cmdItalic_Click(object sender, EventArgs e)
        {
            Font newFont, oldFont;
            oldFont = mailBodyRTB.SelectionFont;
            if (oldFont.Italic)
                newFont = new Font(oldFont, oldFont.Style & ~FontStyle.Italic);
            else
                newFont = new Font(oldFont, oldFont.Style | FontStyle.Italic);

            mailBodyRTB.SelectionFont = newFont;
            mailBodyRTB.Focus();
        }

        private void cmdLeft_Click(object sender, EventArgs e)
        {
            mailBodyRTB.SelectionAlignment = HorizontalAlignment.Left;
            mailBodyRTB.Focus();
        }

        private void cmdRight_Click(object sender, EventArgs e)
        {
            mailBodyRTB.SelectionAlignment = HorizontalAlignment.Right;
            mailBodyRTB.Focus();
        }

        private void cmdCenter_Click(object sender, EventArgs e)
        {
            mailBodyRTB.SelectionAlignment = HorizontalAlignment.Center;
            mailBodyRTB.Focus();
        }

        #endregion

        private void checkEncrypt_CheckedChanged(object sender, EventArgs e)
        {
            defaultToPanel.Visible = !defaultToPanel.Visible;
            oneUserPanel.Visible = !oneUserPanel.Visible;
        }
    }
}
