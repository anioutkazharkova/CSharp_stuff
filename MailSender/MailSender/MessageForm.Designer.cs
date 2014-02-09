namespace MailSender
{
    partial class MessageForm
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MessageForm));
            this.label1 = new System.Windows.Forms.Label();
            this.serviceComboBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.sentButton = new System.Windows.Forms.Button();
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.passwordTextBox = new System.Windows.Forms.TextBox();
            this.senderMailTextBox = new System.Windows.Forms.TextBox();
            this.recMailTextBox = new System.Windows.Forms.TextBox();
            this.subjectTextBox = new System.Windows.Forms.TextBox();
            this.messageTextBox = new System.Windows.Forms.TextBox();
            this.attachButton = new System.Windows.Forms.Button();
            this.attachLabel = new System.Windows.Forms.Label();
            this.showLinkLabel = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Mail service";
            // 
            // serviceComboBox
            // 
            this.serviceComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.serviceComboBox.FormattingEnabled = true;
            this.serviceComboBox.Items.AddRange(new object[] {
            "Yandex",
            "Mail.ru",
            "Gmail.com"});
            this.serviceComboBox.Location = new System.Drawing.Point(104, 9);
            this.serviceComboBox.Name = "serviceComboBox";
            this.serviceComboBox.Size = new System.Drawing.Size(133, 21);
            this.serviceComboBox.TabIndex = 1;
            this.serviceComboBox.SelectedIndexChanged += new System.EventHandler(this.serviceComboBox_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Sender Name";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 85);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Sender email";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(351, 50);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Password";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 127);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(85, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "Receiption email";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 160);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(43, 13);
            this.label6.TabIndex = 6;
            this.label6.Text = "Subject";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 196);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(31, 13);
            this.label7.TabIndex = 7;
            this.label7.Text = "Text:";
            // 
            // sentButton
            // 
            this.sentButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.sentButton.Location = new System.Drawing.Point(539, 486);
            this.sentButton.Name = "sentButton";
            this.sentButton.Size = new System.Drawing.Size(75, 23);
            this.sentButton.TabIndex = 8;
            this.sentButton.Text = "Send";
            this.sentButton.UseVisualStyleBackColor = true;
            this.sentButton.Click += new System.EventHandler(this.sentButton_Click);
            // 
            // nameTextBox
            // 
            this.nameTextBox.Location = new System.Drawing.Point(104, 47);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.Size = new System.Drawing.Size(206, 20);
            this.nameTextBox.TabIndex = 9;
            this.nameTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.nameTextBox_KeyPress);
            // 
            // passwordTextBox
            // 
            this.passwordTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.passwordTextBox.Location = new System.Drawing.Point(410, 47);
            this.passwordTextBox.Name = "passwordTextBox";
            this.passwordTextBox.PasswordChar = '*';
            this.passwordTextBox.Size = new System.Drawing.Size(204, 20);
            this.passwordTextBox.TabIndex = 10;
            this.passwordTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.passwordTextBox_KeyPress);
            // 
            // senderMailTextBox
            // 
            this.senderMailTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.senderMailTextBox.Location = new System.Drawing.Point(102, 85);
            this.senderMailTextBox.Name = "senderMailTextBox";
            this.senderMailTextBox.Size = new System.Drawing.Size(512, 20);
            this.senderMailTextBox.TabIndex = 11;
            this.senderMailTextBox.TextChanged += new System.EventHandler(this.senderMailTextBox_TextChanged);
            this.senderMailTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.senderMailTextBox_KeyPress);
            // 
            // recMailTextBox
            // 
            this.recMailTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.recMailTextBox.Location = new System.Drawing.Point(102, 124);
            this.recMailTextBox.Name = "recMailTextBox";
            this.recMailTextBox.Size = new System.Drawing.Size(512, 20);
            this.recMailTextBox.TabIndex = 12;
            this.recMailTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.recMailTextBox_KeyPress);
            // 
            // subjectTextBox
            // 
            this.subjectTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.subjectTextBox.Location = new System.Drawing.Point(102, 157);
            this.subjectTextBox.Name = "subjectTextBox";
            this.subjectTextBox.Size = new System.Drawing.Size(512, 20);
            this.subjectTextBox.TabIndex = 13;
            // 
            // messageTextBox
            // 
            this.messageTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.messageTextBox.Location = new System.Drawing.Point(15, 224);
            this.messageTextBox.Multiline = true;
            this.messageTextBox.Name = "messageTextBox";
            this.messageTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.messageTextBox.Size = new System.Drawing.Size(599, 246);
            this.messageTextBox.TabIndex = 14;
            // 
            // attachButton
            // 
            this.attachButton.Image = ((System.Drawing.Image)(resources.GetObject("attachButton.Image")));
            this.attachButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.attachButton.Location = new System.Drawing.Point(102, 196);
            this.attachButton.Name = "attachButton";
            this.attachButton.Size = new System.Drawing.Size(96, 25);
            this.attachButton.TabIndex = 15;
            this.attachButton.Text = "Attachment...";
            this.attachButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.attachButton.UseVisualStyleBackColor = true;
            this.attachButton.Click += new System.EventHandler(this.attachButton_Click);
            // 
            // attachLabel
            // 
            this.attachLabel.AutoSize = true;
            this.attachLabel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.attachLabel.Location = new System.Drawing.Point(0, 498);
            this.attachLabel.Name = "attachLabel";
            this.attachLabel.Size = new System.Drawing.Size(0, 13);
            this.attachLabel.TabIndex = 16;
            // 
            // showLinkLabel
            // 
            this.showLinkLabel.Enabled = false;
            this.showLinkLabel.Image = ((System.Drawing.Image)(resources.GetObject("showLinkLabel.Image")));
            this.showLinkLabel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.showLinkLabel.Location = new System.Drawing.Point(220, 202);
            this.showLinkLabel.Name = "showLinkLabel";
            this.showLinkLabel.Size = new System.Drawing.Size(122, 19);
            this.showLinkLabel.TabIndex = 17;
            this.showLinkLabel.TabStop = true;
            this.showLinkLabel.Text = "Show attachments...";
            this.showLinkLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.showLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.showLinkLabel_LinkClicked);
            // 
            // MessageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(626, 511);
            this.Controls.Add(this.showLinkLabel);
            this.Controls.Add(this.attachLabel);
            this.Controls.Add(this.attachButton);
            this.Controls.Add(this.messageTextBox);
            this.Controls.Add(this.subjectTextBox);
            this.Controls.Add(this.recMailTextBox);
            this.Controls.Add(this.senderMailTextBox);
            this.Controls.Add(this.passwordTextBox);
            this.Controls.Add(this.nameTextBox);
            this.Controls.Add(this.sentButton);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.serviceComboBox);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MessageForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mail sender";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox serviceComboBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button sentButton;
        private System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.TextBox passwordTextBox;
        private System.Windows.Forms.TextBox senderMailTextBox;
        private System.Windows.Forms.TextBox recMailTextBox;
        private System.Windows.Forms.TextBox subjectTextBox;
        private System.Windows.Forms.TextBox messageTextBox;
        private System.Windows.Forms.Button attachButton;
        private System.Windows.Forms.Label attachLabel;
        private System.Windows.Forms.LinkLabel showLinkLabel;
    }
}

