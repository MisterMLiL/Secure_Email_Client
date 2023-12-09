namespace Secure_Email_Client
{
    partial class ClientMailsForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            mainPanel = new Panel();
            mailPanel = new Panel();
            dataMailPanel = new Panel();
            mailData = new DataGridView();
            senderMail = new DataGridViewTextBoxColumn();
            subjectMail = new DataGridViewTextBoxColumn();
            dataMail = new DataGridViewTextBoxColumn();
            foldersPanel = new Panel();
            mailFoldersPanel = new Panel();
            foldersList = new ListBox();
            buttonPanel = new Panel();
            refreshButton = new Button();
            newMessageButton = new Button();
            mailCountPanel = new Panel();
            contactButton = new Button();
            mailsCountLabel = new Label();
            labelAllMails = new Label();
            mailNamePanel = new Panel();
            mailNameLabel = new Label();
            contextMenuStrip1 = new ContextMenuStrip(components);
            открытьToolStripMenuItem = new ToolStripMenuItem();
            удалитьToolStripMenuItem = new ToolStripMenuItem();
            mainPanel.SuspendLayout();
            mailPanel.SuspendLayout();
            dataMailPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)mailData).BeginInit();
            foldersPanel.SuspendLayout();
            mailFoldersPanel.SuspendLayout();
            buttonPanel.SuspendLayout();
            mailCountPanel.SuspendLayout();
            mailNamePanel.SuspendLayout();
            contextMenuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // mainPanel
            // 
            mainPanel.Controls.Add(mailPanel);
            mainPanel.Controls.Add(foldersPanel);
            mainPanel.Dock = DockStyle.Fill;
            mainPanel.Location = new Point(0, 0);
            mainPanel.Name = "mainPanel";
            mainPanel.Size = new Size(800, 461);
            mainPanel.TabIndex = 0;
            // 
            // mailPanel
            // 
            mailPanel.BorderStyle = BorderStyle.FixedSingle;
            mailPanel.Controls.Add(dataMailPanel);
            mailPanel.Dock = DockStyle.Fill;
            mailPanel.Location = new Point(150, 0);
            mailPanel.Name = "mailPanel";
            mailPanel.Size = new Size(650, 461);
            mailPanel.TabIndex = 1;
            // 
            // dataMailPanel
            // 
            dataMailPanel.Controls.Add(mailData);
            dataMailPanel.Dock = DockStyle.Fill;
            dataMailPanel.Location = new Point(0, 0);
            dataMailPanel.Name = "dataMailPanel";
            dataMailPanel.Size = new Size(648, 459);
            dataMailPanel.TabIndex = 2;
            // 
            // mailData
            // 
            mailData.AllowUserToAddRows = false;
            mailData.AllowUserToResizeRows = false;
            mailData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            mailData.BackgroundColor = Color.White;
            mailData.BorderStyle = BorderStyle.None;
            mailData.CellBorderStyle = DataGridViewCellBorderStyle.None;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = Color.White;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            mailData.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            mailData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            mailData.Columns.AddRange(new DataGridViewColumn[] { senderMail, subjectMail, dataMail });
            mailData.Dock = DockStyle.Fill;
            mailData.GridColor = Color.White;
            mailData.Location = new Point(0, 0);
            mailData.MultiSelect = false;
            mailData.Name = "mailData";
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.White;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            mailData.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            mailData.RowHeadersVisible = false;
            mailData.RowTemplate.Height = 25;
            mailData.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            mailData.Size = new Size(648, 459);
            mailData.TabIndex = 0;
            mailData.CellDoubleClick += mailData_CellDoubleClick;
            mailData.CellMouseDown += mailData_CellMouseDown;
            // 
            // senderMail
            // 
            senderMail.FillWeight = 25F;
            senderMail.HeaderText = "Отправитель";
            senderMail.Name = "senderMail";
            senderMail.ReadOnly = true;
            // 
            // subjectMail
            // 
            subjectMail.FillWeight = 35F;
            subjectMail.HeaderText = "Тема";
            subjectMail.Name = "subjectMail";
            subjectMail.ReadOnly = true;
            // 
            // dataMail
            // 
            dataMail.FillWeight = 35F;
            dataMail.HeaderText = "Дата";
            dataMail.Name = "dataMail";
            dataMail.ReadOnly = true;
            // 
            // foldersPanel
            // 
            foldersPanel.BorderStyle = BorderStyle.FixedSingle;
            foldersPanel.Controls.Add(mailFoldersPanel);
            foldersPanel.Controls.Add(buttonPanel);
            foldersPanel.Controls.Add(mailCountPanel);
            foldersPanel.Controls.Add(mailNamePanel);
            foldersPanel.Dock = DockStyle.Left;
            foldersPanel.Location = new Point(0, 0);
            foldersPanel.Name = "foldersPanel";
            foldersPanel.Size = new Size(150, 461);
            foldersPanel.TabIndex = 0;
            // 
            // mailFoldersPanel
            // 
            mailFoldersPanel.Controls.Add(foldersList);
            mailFoldersPanel.Dock = DockStyle.Fill;
            mailFoldersPanel.Location = new Point(0, 54);
            mailFoldersPanel.Name = "mailFoldersPanel";
            mailFoldersPanel.Size = new Size(148, 365);
            mailFoldersPanel.TabIndex = 4;
            // 
            // foldersList
            // 
            foldersList.BorderStyle = BorderStyle.None;
            foldersList.Dock = DockStyle.Fill;
            foldersList.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            foldersList.FormattingEnabled = true;
            foldersList.ItemHeight = 21;
            foldersList.Items.AddRange(new object[] { "Входящие", "Отправленные", "Черновики", "Корзина" });
            foldersList.Location = new Point(0, 0);
            foldersList.Name = "foldersList";
            foldersList.Size = new Size(148, 365);
            foldersList.TabIndex = 0;
            // 
            // buttonPanel
            // 
            buttonPanel.Controls.Add(refreshButton);
            buttonPanel.Controls.Add(newMessageButton);
            buttonPanel.Dock = DockStyle.Top;
            buttonPanel.Location = new Point(0, 24);
            buttonPanel.Name = "buttonPanel";
            buttonPanel.Size = new Size(148, 30);
            buttonPanel.TabIndex = 3;
            // 
            // refreshButton
            // 
            refreshButton.BackColor = Color.FromArgb(255, 255, 192);
            refreshButton.Dock = DockStyle.Right;
            refreshButton.FlatAppearance.BorderColor = Color.FromArgb(224, 224, 224);
            refreshButton.FlatStyle = FlatStyle.Flat;
            refreshButton.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            refreshButton.Location = new Point(118, 0);
            refreshButton.Name = "refreshButton";
            refreshButton.Size = new Size(30, 30);
            refreshButton.TabIndex = 1;
            refreshButton.Text = "↺";
            refreshButton.UseVisualStyleBackColor = false;
            refreshButton.Click += refreshButton_Click;
            // 
            // newMessageButton
            // 
            newMessageButton.BackColor = Color.FromArgb(255, 255, 192);
            newMessageButton.Dock = DockStyle.Left;
            newMessageButton.FlatAppearance.BorderColor = Color.FromArgb(224, 224, 224);
            newMessageButton.FlatStyle = FlatStyle.Flat;
            newMessageButton.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            newMessageButton.Location = new Point(0, 0);
            newMessageButton.Name = "newMessageButton";
            newMessageButton.Size = new Size(115, 30);
            newMessageButton.TabIndex = 0;
            newMessageButton.Text = "✉ Написать";
            newMessageButton.UseVisualStyleBackColor = false;
            newMessageButton.Click += newMessageButton_Click;
            // 
            // mailCountPanel
            // 
            mailCountPanel.Controls.Add(contactButton);
            mailCountPanel.Controls.Add(mailsCountLabel);
            mailCountPanel.Controls.Add(labelAllMails);
            mailCountPanel.Dock = DockStyle.Bottom;
            mailCountPanel.Location = new Point(0, 419);
            mailCountPanel.Name = "mailCountPanel";
            mailCountPanel.Size = new Size(148, 40);
            mailCountPanel.TabIndex = 1;
            // 
            // contactButton
            // 
            contactButton.BackColor = Color.FromArgb(255, 255, 192);
            contactButton.Dock = DockStyle.Right;
            contactButton.FlatAppearance.BorderColor = Color.FromArgb(224, 224, 224);
            contactButton.FlatStyle = FlatStyle.Flat;
            contactButton.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            contactButton.Location = new Point(108, 0);
            contactButton.Name = "contactButton";
            contactButton.Size = new Size(40, 40);
            contactButton.TabIndex = 1;
            contactButton.Text = "👤";
            contactButton.UseVisualStyleBackColor = false;
            contactButton.Click += contactButton_Click;
            // 
            // mailsCountLabel
            // 
            mailsCountLabel.AutoSize = true;
            mailsCountLabel.Dock = DockStyle.Left;
            mailsCountLabel.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            mailsCountLabel.Location = new Point(47, 0);
            mailsCountLabel.Margin = new Padding(0);
            mailsCountLabel.Name = "mailsCountLabel";
            mailsCountLabel.Padding = new Padding(3);
            mailsCountLabel.Size = new Size(19, 21);
            mailsCountLabel.TabIndex = 2;
            mailsCountLabel.Text = "0";
            // 
            // labelAllMails
            // 
            labelAllMails.AutoSize = true;
            labelAllMails.Dock = DockStyle.Left;
            labelAllMails.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            labelAllMails.Location = new Point(0, 0);
            labelAllMails.Margin = new Padding(0);
            labelAllMails.Name = "labelAllMails";
            labelAllMails.Padding = new Padding(3);
            labelAllMails.Size = new Size(47, 21);
            labelAllMails.TabIndex = 1;
            labelAllMails.Text = "Всего:";
            // 
            // mailNamePanel
            // 
            mailNamePanel.Controls.Add(mailNameLabel);
            mailNamePanel.Dock = DockStyle.Top;
            mailNamePanel.Location = new Point(0, 0);
            mailNamePanel.Name = "mailNamePanel";
            mailNamePanel.Size = new Size(148, 24);
            mailNamePanel.TabIndex = 0;
            // 
            // mailNameLabel
            // 
            mailNameLabel.AutoSize = true;
            mailNameLabel.Dock = DockStyle.Left;
            mailNameLabel.Location = new Point(0, 0);
            mailNameLabel.Margin = new Padding(0);
            mailNameLabel.Name = "mailNameLabel";
            mailNameLabel.Padding = new Padding(3);
            mailNameLabel.Size = new Size(51, 21);
            mailNameLabel.TabIndex = 0;
            mailNameLabel.Text = "yandex";
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.Items.AddRange(new ToolStripItem[] { открытьToolStripMenuItem, удалитьToolStripMenuItem });
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(122, 48);
            // 
            // открытьToolStripMenuItem
            // 
            открытьToolStripMenuItem.Name = "открытьToolStripMenuItem";
            открытьToolStripMenuItem.Size = new Size(121, 22);
            открытьToolStripMenuItem.Text = "Открыть";
            открытьToolStripMenuItem.Click += открытьToolStripMenuItem_Click;
            // 
            // удалитьToolStripMenuItem
            // 
            удалитьToolStripMenuItem.Name = "удалитьToolStripMenuItem";
            удалитьToolStripMenuItem.Size = new Size(121, 22);
            удалитьToolStripMenuItem.Text = "Удалить";
            удалитьToolStripMenuItem.Click += удалитьToolStripMenuItem_Click;
            // 
            // ClientMailsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(800, 461);
            Controls.Add(mainPanel);
            MinimumSize = new Size(600, 400);
            Name = "ClientMailsForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Почтовый клиент";
            mainPanel.ResumeLayout(false);
            mailPanel.ResumeLayout(false);
            dataMailPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)mailData).EndInit();
            foldersPanel.ResumeLayout(false);
            mailFoldersPanel.ResumeLayout(false);
            buttonPanel.ResumeLayout(false);
            mailCountPanel.ResumeLayout(false);
            mailCountPanel.PerformLayout();
            mailNamePanel.ResumeLayout(false);
            mailNamePanel.PerformLayout();
            contextMenuStrip1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel mainPanel;
        private Panel mailPanel;
        private DataGridView mailData;
        private Panel foldersPanel;
        private ListBox foldersList;
        private Panel mailCountPanel;
        private Panel mailNamePanel;
        private Label mailNameLabel;
        private Label mailsCountLabel;
        private Label labelAllMails;
        private Panel mailFoldersPanel;
        private Panel buttonPanel;
        private Button refreshButton;
        private Button newMessageButton;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem открытьToolStripMenuItem;
        private ToolStripMenuItem удалитьToolStripMenuItem;
        private Panel dataMailPanel;
        private Button contactButton;
        private DataGridViewTextBoxColumn senderMail;
        private DataGridViewTextBoxColumn subjectMail;
        private DataGridViewTextBoxColumn dataMail;
    }
}