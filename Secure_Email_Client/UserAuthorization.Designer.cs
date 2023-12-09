namespace Secure_Email_Client
{
    partial class UserAuthorization
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
            splitContainer1 = new SplitContainer();
            usersList = new ListBox();
            buttonPanel = new Panel();
            saveEditButton = new Button();
            deleteButton = new Button();
            newMessageButton = new Button();
            IMAPPanel = new Panel();
            useSsl = new CheckBox();
            imapPort = new TextBox();
            imapServer = new TextBox();
            imapServerLabel = new Label();
            imapPortLabel = new Label();
            SMTPPanel = new Panel();
            smtpPort = new TextBox();
            smtpServer = new TextBox();
            smtpServerLabel = new Label();
            smtpPortLabel = new Label();
            loginPasswordPanel = new Panel();
            password = new TextBox();
            login = new TextBox();
            loginLabel = new Label();
            passwordLabel = new Label();
            mainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            buttonPanel.SuspendLayout();
            IMAPPanel.SuspendLayout();
            SMTPPanel.SuspendLayout();
            loginPasswordPanel.SuspendLayout();
            SuspendLayout();
            // 
            // mainPanel
            // 
            mainPanel.Controls.Add(splitContainer1);
            mainPanel.Dock = DockStyle.Fill;
            mainPanel.Location = new Point(0, 0);
            mainPanel.Name = "mainPanel";
            mainPanel.Size = new Size(484, 303);
            mainPanel.TabIndex = 0;
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(usersList);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(buttonPanel);
            splitContainer1.Panel2.Controls.Add(IMAPPanel);
            splitContainer1.Panel2.Controls.Add(SMTPPanel);
            splitContainer1.Panel2.Controls.Add(loginPasswordPanel);
            splitContainer1.Size = new Size(484, 303);
            splitContainer1.SplitterDistance = 173;
            splitContainer1.TabIndex = 0;
            // 
            // usersList
            // 
            usersList.BorderStyle = BorderStyle.None;
            usersList.Dock = DockStyle.Fill;
            usersList.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            usersList.FormattingEnabled = true;
            usersList.ItemHeight = 17;
            usersList.Location = new Point(0, 0);
            usersList.Name = "usersList";
            usersList.Size = new Size(173, 303);
            usersList.TabIndex = 0;
            usersList.SelectedIndexChanged += usersList_SelectedIndexChanged;
            usersList.DoubleClick += usersList_DoubleClick;
            // 
            // buttonPanel
            // 
            buttonPanel.BackColor = Color.White;
            buttonPanel.Controls.Add(saveEditButton);
            buttonPanel.Controls.Add(deleteButton);
            buttonPanel.Controls.Add(newMessageButton);
            buttonPanel.Dock = DockStyle.Bottom;
            buttonPanel.Location = new Point(0, 263);
            buttonPanel.MaximumSize = new Size(3122221, 40);
            buttonPanel.MinimumSize = new Size(100, 40);
            buttonPanel.Name = "buttonPanel";
            buttonPanel.Size = new Size(307, 40);
            buttonPanel.TabIndex = 10;
            // 
            // saveEditButton
            // 
            saveEditButton.BackColor = Color.FromArgb(255, 255, 192);
            saveEditButton.Dock = DockStyle.Left;
            saveEditButton.FlatAppearance.BorderColor = Color.FromArgb(224, 224, 224);
            saveEditButton.FlatStyle = FlatStyle.Flat;
            saveEditButton.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            saveEditButton.Location = new Point(40, 0);
            saveEditButton.Name = "saveEditButton";
            saveEditButton.Size = new Size(40, 40);
            saveEditButton.TabIndex = 3;
            saveEditButton.Text = "✔";
            saveEditButton.UseVisualStyleBackColor = false;
            saveEditButton.Click += saveEditButton_Click;
            // 
            // deleteButton
            // 
            deleteButton.BackColor = Color.FromArgb(255, 255, 192);
            deleteButton.Dock = DockStyle.Left;
            deleteButton.FlatAppearance.BorderColor = Color.FromArgb(224, 224, 224);
            deleteButton.FlatStyle = FlatStyle.Flat;
            deleteButton.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            deleteButton.Location = new Point(0, 0);
            deleteButton.Name = "deleteButton";
            deleteButton.Size = new Size(40, 40);
            deleteButton.TabIndex = 2;
            deleteButton.Text = "🗑";
            deleteButton.UseVisualStyleBackColor = false;
            deleteButton.Click += deleteButton_Click;
            // 
            // newMessageButton
            // 
            newMessageButton.BackColor = Color.FromArgb(255, 255, 192);
            newMessageButton.Dock = DockStyle.Right;
            newMessageButton.FlatAppearance.BorderColor = Color.FromArgb(224, 224, 224);
            newMessageButton.FlatStyle = FlatStyle.Flat;
            newMessageButton.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            newMessageButton.Location = new Point(152, 0);
            newMessageButton.Name = "newMessageButton";
            newMessageButton.Size = new Size(155, 40);
            newMessageButton.TabIndex = 1;
            newMessageButton.Text = "⎆ Авторизоваться";
            newMessageButton.UseVisualStyleBackColor = false;
            newMessageButton.Click += newMessageButton_Click;
            // 
            // IMAPPanel
            // 
            IMAPPanel.Controls.Add(useSsl);
            IMAPPanel.Controls.Add(imapPort);
            IMAPPanel.Controls.Add(imapServer);
            IMAPPanel.Controls.Add(imapServerLabel);
            IMAPPanel.Controls.Add(imapPortLabel);
            IMAPPanel.Dock = DockStyle.Top;
            IMAPPanel.Location = new Point(0, 158);
            IMAPPanel.Name = "IMAPPanel";
            IMAPPanel.Size = new Size(307, 106);
            IMAPPanel.TabIndex = 9;
            // 
            // useSsl
            // 
            useSsl.AutoSize = true;
            useSsl.Checked = true;
            useSsl.CheckState = CheckState.Checked;
            useSsl.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            useSsl.Location = new Point(15, 69);
            useSsl.Name = "useSsl";
            useSsl.Size = new Size(55, 25);
            useSsl.TabIndex = 9;
            useSsl.Text = "SSL";
            useSsl.UseVisualStyleBackColor = true;
            // 
            // imapPort
            // 
            imapPort.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            imapPort.Location = new Point(125, 45);
            imapPort.Name = "imapPort";
            imapPort.Size = new Size(170, 23);
            imapPort.TabIndex = 8;
            imapPort.TextChanged += integer_TextChanged;
            // 
            // imapServer
            // 
            imapServer.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            imapServer.Location = new Point(125, 16);
            imapServer.Name = "imapServer";
            imapServer.Size = new Size(170, 23);
            imapServer.TabIndex = 7;
            // 
            // imapServerLabel
            // 
            imapServerLabel.AutoSize = true;
            imapServerLabel.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            imapServerLabel.Location = new Point(13, 16);
            imapServerLabel.Name = "imapServerLabel";
            imapServerLabel.Size = new Size(103, 21);
            imapServerLabel.TabIndex = 4;
            imapServerLabel.Text = "IMAP сервер:";
            // 
            // imapPortLabel
            // 
            imapPortLabel.AutoSize = true;
            imapPortLabel.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            imapPortLabel.Location = new Point(13, 45);
            imapPortLabel.Name = "imapPortLabel";
            imapPortLabel.Size = new Size(88, 21);
            imapPortLabel.TabIndex = 5;
            imapPortLabel.Text = "IMAP порт:";
            // 
            // SMTPPanel
            // 
            SMTPPanel.Controls.Add(smtpPort);
            SMTPPanel.Controls.Add(smtpServer);
            SMTPPanel.Controls.Add(smtpServerLabel);
            SMTPPanel.Controls.Add(smtpPortLabel);
            SMTPPanel.Dock = DockStyle.Top;
            SMTPPanel.Location = new Point(0, 75);
            SMTPPanel.Name = "SMTPPanel";
            SMTPPanel.Size = new Size(307, 83);
            SMTPPanel.TabIndex = 8;
            // 
            // smtpPort
            // 
            smtpPort.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            smtpPort.Location = new Point(125, 43);
            smtpPort.Name = "smtpPort";
            smtpPort.Size = new Size(170, 23);
            smtpPort.TabIndex = 5;
            smtpPort.TextChanged += integer_TextChanged;
            // 
            // smtpServer
            // 
            smtpServer.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            smtpServer.Location = new Point(125, 14);
            smtpServer.Name = "smtpServer";
            smtpServer.Size = new Size(170, 23);
            smtpServer.TabIndex = 4;
            // 
            // smtpServerLabel
            // 
            smtpServerLabel.AutoSize = true;
            smtpServerLabel.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            smtpServerLabel.Location = new Point(13, 14);
            smtpServerLabel.Name = "smtpServerLabel";
            smtpServerLabel.Size = new Size(106, 21);
            smtpServerLabel.TabIndex = 2;
            smtpServerLabel.Text = "SMTP сервер:";
            // 
            // smtpPortLabel
            // 
            smtpPortLabel.AutoSize = true;
            smtpPortLabel.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            smtpPortLabel.Location = new Point(13, 43);
            smtpPortLabel.Name = "smtpPortLabel";
            smtpPortLabel.Size = new Size(91, 21);
            smtpPortLabel.TabIndex = 3;
            smtpPortLabel.Text = "SMTP порт:";
            // 
            // loginPasswordPanel
            // 
            loginPasswordPanel.Controls.Add(password);
            loginPasswordPanel.Controls.Add(login);
            loginPasswordPanel.Controls.Add(loginLabel);
            loginPasswordPanel.Controls.Add(passwordLabel);
            loginPasswordPanel.Dock = DockStyle.Top;
            loginPasswordPanel.Location = new Point(0, 0);
            loginPasswordPanel.Name = "loginPasswordPanel";
            loginPasswordPanel.Size = new Size(307, 75);
            loginPasswordPanel.TabIndex = 7;
            // 
            // password
            // 
            password.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            password.Location = new Point(85, 40);
            password.Name = "password";
            password.PasswordChar = '*';
            password.Size = new Size(210, 23);
            password.TabIndex = 3;
            // 
            // login
            // 
            login.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            login.Location = new Point(85, 11);
            login.Name = "login";
            login.Size = new Size(210, 23);
            login.TabIndex = 2;
            // 
            // loginLabel
            // 
            loginLabel.AutoSize = true;
            loginLabel.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            loginLabel.Location = new Point(13, 11);
            loginLabel.Name = "loginLabel";
            loginLabel.Size = new Size(57, 21);
            loginLabel.TabIndex = 0;
            loginLabel.Text = "Логин:";
            // 
            // passwordLabel
            // 
            passwordLabel.AutoSize = true;
            passwordLabel.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            passwordLabel.Location = new Point(13, 40);
            passwordLabel.Name = "passwordLabel";
            passwordLabel.Size = new Size(66, 21);
            passwordLabel.TabIndex = 1;
            passwordLabel.Text = "Пароль:";
            // 
            // UserAuthorization
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(484, 303);
            Controls.Add(mainPanel);
            MinimumSize = new Size(500, 342);
            Name = "UserAuthorization";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Авторизация";
            FormClosing += UserAuthorization_FormClosing;
            mainPanel.ResumeLayout(false);
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            buttonPanel.ResumeLayout(false);
            IMAPPanel.ResumeLayout(false);
            IMAPPanel.PerformLayout();
            SMTPPanel.ResumeLayout(false);
            SMTPPanel.PerformLayout();
            loginPasswordPanel.ResumeLayout(false);
            loginPasswordPanel.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel mainPanel;
        private SplitContainer splitContainer1;
        private Label imapPortLabel;
        private Label imapServerLabel;
        private Label smtpPortLabel;
        private Label smtpServerLabel;
        private Label passwordLabel;
        private Label loginLabel;
        private Panel SMTPPanel;
        private Panel loginPasswordPanel;
        private Panel buttonPanel;
        private Panel IMAPPanel;
        private CheckBox useSsl;
        private TextBox imapPort;
        private TextBox imapServer;
        private TextBox smtpPort;
        private TextBox smtpServer;
        private TextBox password;
        private TextBox login;
        private Button newMessageButton;
        private ListBox usersList;
        private Button saveEditButton;
        private Button deleteButton;
    }
}