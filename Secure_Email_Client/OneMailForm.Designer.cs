namespace Secure_Email_Client
{
    partial class OneMailForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            mainPanel = new Panel();
            mailBodyPanel = new Panel();
            webView = new Microsoft.Web.WebView2.WinForms.WebView2();
            mailBodyRTB = new RichTextBox();
            attachmentsPanel = new Panel();
            listPanel = new Panel();
            attachmentsList = new ListBox();
            button11 = new Button();
            buttonAttachmentsPanel = new Panel();
            deleteButton = new Button();
            button10 = new Button();
            textAttachmentsPanel = new Panel();
            label3 = new Label();
            button9 = new Button();
            button8 = new Button();
            buttonsPanel = new Panel();
            addButton = new Button();
            sentButton = new Button();
            formatingPanel = new Panel();
            checkESP = new CheckBox();
            checkEncrypt = new CheckBox();
            rightAlignButton = new Button();
            centerAlignButton = new Button();
            leftAlignButton = new Button();
            italicButton = new Button();
            boldButton = new Button();
            subjectPanel = new Panel();
            subjectTextBox = new TextBox();
            label2 = new Label();
            toPanel = new Panel();
            oneUserPanel = new Panel();
            rsaUsersList = new ComboBox();
            label4 = new Label();
            defaultToPanel = new Panel();
            toTextBox = new TextBox();
            addToButton = new Button();
            label1 = new Label();
            mainPanel.SuspendLayout();
            mailBodyPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)webView).BeginInit();
            attachmentsPanel.SuspendLayout();
            listPanel.SuspendLayout();
            buttonAttachmentsPanel.SuspendLayout();
            textAttachmentsPanel.SuspendLayout();
            buttonsPanel.SuspendLayout();
            formatingPanel.SuspendLayout();
            subjectPanel.SuspendLayout();
            toPanel.SuspendLayout();
            oneUserPanel.SuspendLayout();
            defaultToPanel.SuspendLayout();
            SuspendLayout();
            // 
            // mainPanel
            // 
            mainPanel.Controls.Add(mailBodyPanel);
            mainPanel.Controls.Add(attachmentsPanel);
            mainPanel.Controls.Add(buttonsPanel);
            mainPanel.Controls.Add(formatingPanel);
            mainPanel.Controls.Add(subjectPanel);
            mainPanel.Controls.Add(toPanel);
            mainPanel.Dock = DockStyle.Fill;
            mainPanel.Location = new Point(0, 0);
            mainPanel.Name = "mainPanel";
            mainPanel.Size = new Size(484, 396);
            mainPanel.TabIndex = 0;
            // 
            // mailBodyPanel
            // 
            mailBodyPanel.Controls.Add(webView);
            mailBodyPanel.Controls.Add(mailBodyRTB);
            mailBodyPanel.Dock = DockStyle.Fill;
            mailBodyPanel.Location = new Point(0, 130);
            mailBodyPanel.Name = "mailBodyPanel";
            mailBodyPanel.Size = new Size(346, 236);
            mailBodyPanel.TabIndex = 6;
            // 
            // webView
            // 
            webView.AllowExternalDrop = true;
            webView.CreationProperties = null;
            webView.DefaultBackgroundColor = Color.White;
            webView.Dock = DockStyle.Fill;
            webView.Location = new Point(0, 0);
            webView.Name = "webView";
            webView.Size = new Size(346, 236);
            webView.TabIndex = 8;
            webView.ZoomFactor = 1D;
            // 
            // mailBodyRTB
            // 
            mailBodyRTB.BorderStyle = BorderStyle.None;
            mailBodyRTB.Dock = DockStyle.Fill;
            mailBodyRTB.Location = new Point(0, 0);
            mailBodyRTB.Name = "mailBodyRTB";
            mailBodyRTB.Size = new Size(346, 236);
            mailBodyRTB.TabIndex = 0;
            mailBodyRTB.Text = "";
            // 
            // attachmentsPanel
            // 
            attachmentsPanel.Controls.Add(listPanel);
            attachmentsPanel.Controls.Add(buttonAttachmentsPanel);
            attachmentsPanel.Controls.Add(textAttachmentsPanel);
            attachmentsPanel.Controls.Add(button8);
            attachmentsPanel.Dock = DockStyle.Right;
            attachmentsPanel.Location = new Point(346, 130);
            attachmentsPanel.Name = "attachmentsPanel";
            attachmentsPanel.Size = new Size(138, 236);
            attachmentsPanel.TabIndex = 5;
            // 
            // listPanel
            // 
            listPanel.Controls.Add(attachmentsList);
            listPanel.Controls.Add(button11);
            listPanel.Dock = DockStyle.Fill;
            listPanel.Location = new Point(0, 30);
            listPanel.Name = "listPanel";
            listPanel.Size = new Size(138, 176);
            listPanel.TabIndex = 8;
            // 
            // attachmentsList
            // 
            attachmentsList.BorderStyle = BorderStyle.None;
            attachmentsList.Dock = DockStyle.Fill;
            attachmentsList.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            attachmentsList.FormattingEnabled = true;
            attachmentsList.ItemHeight = 21;
            attachmentsList.Location = new Point(0, 0);
            attachmentsList.Name = "attachmentsList";
            attachmentsList.Size = new Size(138, 176);
            attachmentsList.TabIndex = 2;
            attachmentsList.DoubleClick += attachmentsList_DoubleClick;
            // 
            // button11
            // 
            button11.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            button11.BackColor = Color.FromArgb(255, 255, 192);
            button11.FlatAppearance.BorderColor = Color.FromArgb(224, 224, 224);
            button11.FlatStyle = FlatStyle.Flat;
            button11.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            button11.Location = new Point(413, 126);
            button11.Name = "button11";
            button11.Size = new Size(132, 31);
            button11.TabIndex = 1;
            button11.Text = "📎 Прикрепить";
            button11.UseVisualStyleBackColor = false;
            // 
            // buttonAttachmentsPanel
            // 
            buttonAttachmentsPanel.Controls.Add(deleteButton);
            buttonAttachmentsPanel.Controls.Add(button10);
            buttonAttachmentsPanel.Dock = DockStyle.Bottom;
            buttonAttachmentsPanel.Location = new Point(0, 206);
            buttonAttachmentsPanel.Name = "buttonAttachmentsPanel";
            buttonAttachmentsPanel.Size = new Size(138, 30);
            buttonAttachmentsPanel.TabIndex = 7;
            // 
            // deleteButton
            // 
            deleteButton.BackColor = Color.FromArgb(255, 255, 192);
            deleteButton.Dock = DockStyle.Fill;
            deleteButton.FlatAppearance.BorderColor = Color.FromArgb(224, 224, 224);
            deleteButton.FlatStyle = FlatStyle.Flat;
            deleteButton.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            deleteButton.Location = new Point(0, 0);
            deleteButton.Name = "deleteButton";
            deleteButton.Size = new Size(138, 30);
            deleteButton.TabIndex = 2;
            deleteButton.Text = "🗑 Удалить";
            deleteButton.UseVisualStyleBackColor = false;
            deleteButton.Click += deleteButton_Click;
            // 
            // button10
            // 
            button10.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            button10.BackColor = Color.FromArgb(255, 255, 192);
            button10.FlatAppearance.BorderColor = Color.FromArgb(224, 224, 224);
            button10.FlatStyle = FlatStyle.Flat;
            button10.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            button10.Location = new Point(413, 30);
            button10.Name = "button10";
            button10.Size = new Size(132, 31);
            button10.TabIndex = 1;
            button10.Text = "📎 Прикрепить";
            button10.UseVisualStyleBackColor = false;
            // 
            // textAttachmentsPanel
            // 
            textAttachmentsPanel.Controls.Add(label3);
            textAttachmentsPanel.Controls.Add(button9);
            textAttachmentsPanel.Dock = DockStyle.Top;
            textAttachmentsPanel.Location = new Point(0, 0);
            textAttachmentsPanel.Name = "textAttachmentsPanel";
            textAttachmentsPanel.Size = new Size(138, 30);
            textAttachmentsPanel.TabIndex = 6;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Dock = DockStyle.Left;
            label3.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label3.Location = new Point(0, 0);
            label3.Name = "label3";
            label3.Padding = new Padding(3);
            label3.Size = new Size(64, 27);
            label3.TabIndex = 2;
            label3.Text = "Файлы";
            // 
            // button9
            // 
            button9.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            button9.BackColor = Color.FromArgb(255, 255, 192);
            button9.FlatAppearance.BorderColor = Color.FromArgb(224, 224, 224);
            button9.FlatStyle = FlatStyle.Flat;
            button9.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            button9.Location = new Point(413, 100);
            button9.Name = "button9";
            button9.Size = new Size(132, 31);
            button9.TabIndex = 1;
            button9.Text = "📎 Прикрепить";
            button9.UseVisualStyleBackColor = false;
            // 
            // button8
            // 
            button8.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            button8.BackColor = Color.FromArgb(255, 255, 192);
            button8.FlatAppearance.BorderColor = Color.FromArgb(224, 224, 224);
            button8.FlatStyle = FlatStyle.Flat;
            button8.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            button8.Location = new Point(407, 160);
            button8.Name = "button8";
            button8.Size = new Size(132, 31);
            button8.TabIndex = 1;
            button8.Text = "📎 Прикрепить";
            button8.UseVisualStyleBackColor = false;
            // 
            // buttonsPanel
            // 
            buttonsPanel.Controls.Add(addButton);
            buttonsPanel.Controls.Add(sentButton);
            buttonsPanel.Dock = DockStyle.Bottom;
            buttonsPanel.Location = new Point(0, 366);
            buttonsPanel.Name = "buttonsPanel";
            buttonsPanel.Size = new Size(484, 30);
            buttonsPanel.TabIndex = 3;
            // 
            // addButton
            // 
            addButton.BackColor = Color.FromArgb(255, 255, 192);
            addButton.Dock = DockStyle.Right;
            addButton.FlatAppearance.BorderColor = Color.FromArgb(224, 224, 224);
            addButton.FlatStyle = FlatStyle.Flat;
            addButton.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            addButton.Location = new Point(346, 0);
            addButton.Name = "addButton";
            addButton.Size = new Size(138, 30);
            addButton.TabIndex = 1;
            addButton.Text = "📎 Прикрепить";
            addButton.UseVisualStyleBackColor = false;
            addButton.Click += addButton_Click;
            // 
            // sentButton
            // 
            sentButton.BackColor = Color.FromArgb(255, 255, 192);
            sentButton.Dock = DockStyle.Left;
            sentButton.FlatAppearance.BorderColor = Color.FromArgb(224, 224, 224);
            sentButton.FlatStyle = FlatStyle.Flat;
            sentButton.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            sentButton.Location = new Point(0, 0);
            sentButton.Name = "sentButton";
            sentButton.Size = new Size(132, 30);
            sentButton.TabIndex = 0;
            sentButton.Text = "✉ Отправить";
            sentButton.UseVisualStyleBackColor = false;
            sentButton.Click += sentButton_Click;
            // 
            // formatingPanel
            // 
            formatingPanel.Controls.Add(checkESP);
            formatingPanel.Controls.Add(checkEncrypt);
            formatingPanel.Controls.Add(rightAlignButton);
            formatingPanel.Controls.Add(centerAlignButton);
            formatingPanel.Controls.Add(leftAlignButton);
            formatingPanel.Controls.Add(italicButton);
            formatingPanel.Controls.Add(boldButton);
            formatingPanel.Dock = DockStyle.Top;
            formatingPanel.Location = new Point(0, 90);
            formatingPanel.Name = "formatingPanel";
            formatingPanel.Size = new Size(484, 40);
            formatingPanel.TabIndex = 2;
            // 
            // checkESP
            // 
            checkESP.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            checkESP.AutoSize = true;
            checkESP.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            checkESP.Location = new Point(410, 6);
            checkESP.Name = "checkESP";
            checkESP.Size = new Size(62, 25);
            checkESP.TabIndex = 9;
            checkESP.Text = "ЭЦП";
            checkESP.UseVisualStyleBackColor = true;
            // 
            // checkEncrypt
            // 
            checkEncrypt.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            checkEncrypt.AutoSize = true;
            checkEncrypt.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            checkEncrypt.Location = new Point(291, 6);
            checkEncrypt.Name = "checkEncrypt";
            checkEncrypt.Size = new Size(113, 25);
            checkEncrypt.TabIndex = 8;
            checkEncrypt.Text = "Шифровать";
            checkEncrypt.UseVisualStyleBackColor = true;
            checkEncrypt.CheckedChanged += checkEncrypt_CheckedChanged;
            // 
            // rightAlignButton
            // 
            rightAlignButton.FlatAppearance.BorderColor = Color.FromArgb(224, 224, 224);
            rightAlignButton.FlatStyle = FlatStyle.Flat;
            rightAlignButton.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            rightAlignButton.Location = new Point(204, 3);
            rightAlignButton.Name = "rightAlignButton";
            rightAlignButton.Size = new Size(40, 31);
            rightAlignButton.TabIndex = 7;
            rightAlignButton.Text = ">";
            rightAlignButton.UseVisualStyleBackColor = false;
            rightAlignButton.Click += cmdRight_Click;
            // 
            // centerAlignButton
            // 
            centerAlignButton.FlatAppearance.BorderColor = Color.FromArgb(224, 224, 224);
            centerAlignButton.FlatStyle = FlatStyle.Flat;
            centerAlignButton.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            centerAlignButton.Location = new Point(158, 3);
            centerAlignButton.Name = "centerAlignButton";
            centerAlignButton.Size = new Size(40, 31);
            centerAlignButton.TabIndex = 6;
            centerAlignButton.Text = "^";
            centerAlignButton.UseVisualStyleBackColor = false;
            centerAlignButton.Click += cmdCenter_Click;
            // 
            // leftAlignButton
            // 
            leftAlignButton.FlatAppearance.BorderColor = Color.FromArgb(224, 224, 224);
            leftAlignButton.FlatStyle = FlatStyle.Flat;
            leftAlignButton.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            leftAlignButton.Location = new Point(112, 3);
            leftAlignButton.Name = "leftAlignButton";
            leftAlignButton.Size = new Size(40, 31);
            leftAlignButton.TabIndex = 5;
            leftAlignButton.Text = "<";
            leftAlignButton.UseVisualStyleBackColor = false;
            leftAlignButton.Click += cmdLeft_Click;
            // 
            // italicButton
            // 
            italicButton.FlatAppearance.BorderColor = Color.FromArgb(224, 224, 224);
            italicButton.FlatStyle = FlatStyle.Flat;
            italicButton.Font = new Font("Segoe UI", 11.25F, FontStyle.Italic, GraphicsUnit.Point);
            italicButton.Location = new Point(49, 3);
            italicButton.Name = "italicButton";
            italicButton.Size = new Size(40, 31);
            italicButton.TabIndex = 2;
            italicButton.Text = "К";
            italicButton.UseVisualStyleBackColor = false;
            italicButton.Click += cmdItalic_Click;
            // 
            // boldButton
            // 
            boldButton.FlatAppearance.BorderColor = Color.FromArgb(224, 224, 224);
            boldButton.FlatStyle = FlatStyle.Flat;
            boldButton.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            boldButton.Location = new Point(3, 3);
            boldButton.Name = "boldButton";
            boldButton.Size = new Size(40, 31);
            boldButton.TabIndex = 1;
            boldButton.Text = "Ж";
            boldButton.UseVisualStyleBackColor = false;
            boldButton.Click += cmdBold_Click;
            // 
            // subjectPanel
            // 
            subjectPanel.Controls.Add(subjectTextBox);
            subjectPanel.Controls.Add(label2);
            subjectPanel.Dock = DockStyle.Top;
            subjectPanel.Location = new Point(0, 60);
            subjectPanel.Name = "subjectPanel";
            subjectPanel.Size = new Size(484, 30);
            subjectPanel.TabIndex = 1;
            // 
            // subjectTextBox
            // 
            subjectTextBox.BackColor = Color.White;
            subjectTextBox.BorderStyle = BorderStyle.FixedSingle;
            subjectTextBox.Dock = DockStyle.Fill;
            subjectTextBox.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            subjectTextBox.Location = new Point(56, 0);
            subjectTextBox.Name = "subjectTextBox";
            subjectTextBox.Size = new Size(428, 29);
            subjectTextBox.TabIndex = 2;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Dock = DockStyle.Left;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(0, 0);
            label2.Name = "label2";
            label2.Padding = new Padding(3, 3, 5, 3);
            label2.Size = new Size(56, 27);
            label2.TabIndex = 1;
            label2.Text = "Тема:";
            // 
            // toPanel
            // 
            toPanel.AutoSize = true;
            toPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            toPanel.Controls.Add(oneUserPanel);
            toPanel.Controls.Add(defaultToPanel);
            toPanel.Dock = DockStyle.Top;
            toPanel.Location = new Point(0, 0);
            toPanel.Name = "toPanel";
            toPanel.Size = new Size(484, 60);
            toPanel.TabIndex = 0;
            // 
            // oneUserPanel
            // 
            oneUserPanel.Controls.Add(rsaUsersList);
            oneUserPanel.Controls.Add(label4);
            oneUserPanel.Dock = DockStyle.Top;
            oneUserPanel.Location = new Point(0, 30);
            oneUserPanel.Name = "oneUserPanel";
            oneUserPanel.Size = new Size(484, 30);
            oneUserPanel.TabIndex = 6;
            // 
            // rsaUsersList
            // 
            rsaUsersList.Dock = DockStyle.Fill;
            rsaUsersList.DropDownStyle = ComboBoxStyle.DropDownList;
            rsaUsersList.FlatStyle = FlatStyle.Flat;
            rsaUsersList.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            rsaUsersList.FormattingEnabled = true;
            rsaUsersList.Location = new Point(56, 0);
            rsaUsersList.Name = "rsaUsersList";
            rsaUsersList.Size = new Size(428, 29);
            rsaUsersList.TabIndex = 1;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Dock = DockStyle.Left;
            label4.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label4.Location = new Point(0, 0);
            label4.Name = "label4";
            label4.Padding = new Padding(3);
            label4.Size = new Size(56, 27);
            label4.TabIndex = 0;
            label4.Text = "Кому:";
            // 
            // defaultToPanel
            // 
            defaultToPanel.Controls.Add(toTextBox);
            defaultToPanel.Controls.Add(addToButton);
            defaultToPanel.Controls.Add(label1);
            defaultToPanel.Dock = DockStyle.Top;
            defaultToPanel.Location = new Point(0, 0);
            defaultToPanel.Name = "defaultToPanel";
            defaultToPanel.Size = new Size(484, 30);
            defaultToPanel.TabIndex = 5;
            // 
            // toTextBox
            // 
            toTextBox.BackColor = Color.White;
            toTextBox.BorderStyle = BorderStyle.FixedSingle;
            toTextBox.Dock = DockStyle.Fill;
            toTextBox.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            toTextBox.Location = new Point(56, 0);
            toTextBox.Name = "toTextBox";
            toTextBox.ReadOnly = true;
            toTextBox.Size = new Size(290, 29);
            toTextBox.TabIndex = 4;
            // 
            // addToButton
            // 
            addToButton.BackColor = Color.FromArgb(255, 255, 192);
            addToButton.Dock = DockStyle.Right;
            addToButton.FlatAppearance.BorderColor = Color.FromArgb(224, 224, 224);
            addToButton.FlatStyle = FlatStyle.Flat;
            addToButton.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            addToButton.Location = new Point(346, 0);
            addToButton.Name = "addToButton";
            addToButton.Size = new Size(138, 30);
            addToButton.TabIndex = 3;
            addToButton.Text = "👤 Получатели";
            addToButton.UseVisualStyleBackColor = false;
            addToButton.Click += addToButton_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Dock = DockStyle.Left;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(0, 0);
            label1.Name = "label1";
            label1.Padding = new Padding(3);
            label1.Size = new Size(56, 27);
            label1.TabIndex = 0;
            label1.Text = "Кому:";
            // 
            // OneMailForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(484, 396);
            Controls.Add(mainPanel);
            MinimumSize = new Size(500, 435);
            Name = "OneMailForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Письмо";
            mainPanel.ResumeLayout(false);
            mainPanel.PerformLayout();
            mailBodyPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)webView).EndInit();
            attachmentsPanel.ResumeLayout(false);
            listPanel.ResumeLayout(false);
            buttonAttachmentsPanel.ResumeLayout(false);
            textAttachmentsPanel.ResumeLayout(false);
            textAttachmentsPanel.PerformLayout();
            buttonsPanel.ResumeLayout(false);
            formatingPanel.ResumeLayout(false);
            formatingPanel.PerformLayout();
            subjectPanel.ResumeLayout(false);
            subjectPanel.PerformLayout();
            toPanel.ResumeLayout(false);
            oneUserPanel.ResumeLayout(false);
            oneUserPanel.PerformLayout();
            defaultToPanel.ResumeLayout(false);
            defaultToPanel.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel mainPanel;
        private Panel toPanel;
        private Panel buttonsPanel;
        private Panel formatingPanel;
        private Panel subjectPanel;
        private Label label1;
        private Label label2;
        private TextBox subjectTextBox;
        private Button sentButton;
        private Button addButton;
        private Button italicButton;
        private Button boldButton;
        private Button rightAlignButton;
        private Button centerAlignButton;
        private Button leftAlignButton;
        private Panel mailBodyPanel;
        private RichTextBox mailBodyRTB;
        private Panel attachmentsPanel;
        private Button button8;
        private Panel listPanel;
        private ListBox attachmentsList;
        private Button button11;
        private Panel buttonAttachmentsPanel;
        private Button button10;
        private Panel textAttachmentsPanel;
        private Button button9;
        private Button deleteButton;
        private Label label3;
        private Microsoft.Web.WebView2.WinForms.WebView2 webView;
        private TextBox toTextBox;
        private Button addToButton;
        private CheckBox checkESP;
        private CheckBox checkEncrypt;
        private Panel defaultToPanel;
        private Panel oneUserPanel;
        private ComboBox rsaUsersList;
        private Label label4;
    }
}