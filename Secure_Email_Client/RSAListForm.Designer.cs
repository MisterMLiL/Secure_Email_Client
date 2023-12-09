namespace Secure_Email_Client
{
    partial class RSAListForm
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
            namesPanel = new Panel();
            namesList = new ListBox();
            buttonPanel = new Panel();
            deleteButton = new Button();
            addButton = new Button();
            textPanel = new Panel();
            label1 = new Label();
            mainPanel.SuspendLayout();
            namesPanel.SuspendLayout();
            buttonPanel.SuspendLayout();
            textPanel.SuspendLayout();
            SuspendLayout();
            // 
            // mainPanel
            // 
            mainPanel.Controls.Add(namesPanel);
            mainPanel.Controls.Add(buttonPanel);
            mainPanel.Controls.Add(textPanel);
            mainPanel.Dock = DockStyle.Fill;
            mainPanel.Location = new Point(0, 0);
            mainPanel.Name = "mainPanel";
            mainPanel.Size = new Size(284, 261);
            mainPanel.TabIndex = 0;
            // 
            // namesPanel
            // 
            namesPanel.Controls.Add(namesList);
            namesPanel.Dock = DockStyle.Fill;
            namesPanel.Location = new Point(0, 30);
            namesPanel.Name = "namesPanel";
            namesPanel.Size = new Size(284, 191);
            namesPanel.TabIndex = 2;
            // 
            // namesList
            // 
            namesList.Dock = DockStyle.Fill;
            namesList.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            namesList.FormattingEnabled = true;
            namesList.ItemHeight = 21;
            namesList.Location = new Point(0, 0);
            namesList.Name = "namesList";
            namesList.Size = new Size(284, 191);
            namesList.TabIndex = 0;
            // 
            // buttonPanel
            // 
            buttonPanel.Controls.Add(deleteButton);
            buttonPanel.Controls.Add(addButton);
            buttonPanel.Dock = DockStyle.Bottom;
            buttonPanel.Location = new Point(0, 221);
            buttonPanel.Name = "buttonPanel";
            buttonPanel.Size = new Size(284, 40);
            buttonPanel.TabIndex = 1;
            // 
            // deleteButton
            // 
            deleteButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            deleteButton.BackColor = Color.FromArgb(255, 255, 192);
            deleteButton.FlatAppearance.BorderColor = Color.FromArgb(224, 224, 224);
            deleteButton.FlatStyle = FlatStyle.Flat;
            deleteButton.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            deleteButton.Location = new Point(149, 3);
            deleteButton.Name = "deleteButton";
            deleteButton.Size = new Size(132, 31);
            deleteButton.TabIndex = 3;
            deleteButton.Text = "Удалить";
            deleteButton.UseVisualStyleBackColor = false;
            deleteButton.Click += deleteButton_Click;
            // 
            // addButton
            // 
            addButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            addButton.BackColor = Color.FromArgb(255, 255, 192);
            addButton.FlatAppearance.BorderColor = Color.FromArgb(224, 224, 224);
            addButton.FlatStyle = FlatStyle.Flat;
            addButton.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            addButton.Location = new Point(3, 3);
            addButton.Name = "addButton";
            addButton.Size = new Size(132, 31);
            addButton.TabIndex = 2;
            addButton.Text = "Новый";
            addButton.UseVisualStyleBackColor = false;
            addButton.Click += addButton_Click;
            // 
            // textPanel
            // 
            textPanel.Controls.Add(label1);
            textPanel.Dock = DockStyle.Top;
            textPanel.Location = new Point(0, 0);
            textPanel.Name = "textPanel";
            textPanel.Size = new Size(284, 30);
            textPanel.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Dock = DockStyle.Left;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(0, 0);
            label1.Name = "label1";
            label1.Padding = new Padding(3);
            label1.Size = new Size(84, 27);
            label1.TabIndex = 1;
            label1.Text = "Контакты";
            // 
            // RSAListForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(284, 261);
            Controls.Add(mainPanel);
            MaximumSize = new Size(300, 300);
            MinimumSize = new Size(300, 300);
            Name = "RSAListForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Список получателей";
            mainPanel.ResumeLayout(false);
            namesPanel.ResumeLayout(false);
            buttonPanel.ResumeLayout(false);
            textPanel.ResumeLayout(false);
            textPanel.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel mainPanel;
        private Panel namesPanel;
        private ListBox namesList;
        private Panel buttonPanel;
        private Panel textPanel;
        private Button addButton;
        private Button deleteButton;
        private Label label1;
    }
}