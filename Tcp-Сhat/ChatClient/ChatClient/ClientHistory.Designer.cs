namespace ChatClient
{
    partial class ClientHistory
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ClientHistory));
            this.logGridView = new System.Windows.Forms.DataGridView();
            this.userName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.userMessage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MessageDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.startDtPicker = new System.Windows.Forms.DateTimePicker();
            this.endDtPicker = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.userListComboBox = new System.Windows.Forms.ComboBox();
            this.getLogButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.logGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // logGridView
            // 
            this.logGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.logGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
            this.logGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.logGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.userName,
            this.userMessage,
            this.MessageDate});
            this.logGridView.Location = new System.Drawing.Point(24, 129);
            this.logGridView.Name = "logGridView";
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.logGridView.RowsDefaultCellStyle = dataGridViewCellStyle1;
            this.logGridView.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.logGridView.Size = new System.Drawing.Size(565, 220);
            this.logGridView.TabIndex = 0;
            // 
            // userName
            // 
            this.userName.HeaderText = "User";
            this.userName.Name = "userName";
            this.userName.ReadOnly = true;
            // 
            // userMessage
            // 
            this.userMessage.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            this.userMessage.HeaderText = "Message";
            this.userMessage.MinimumWidth = 200;
            this.userMessage.Name = "userMessage";
            this.userMessage.ReadOnly = true;
            this.userMessage.Width = 200;
            // 
            // MessageDate
            // 
            this.MessageDate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            this.MessageDate.HeaderText = "Date";
            this.MessageDate.MinimumWidth = 100;
            this.MessageDate.Name = "MessageDate";
            this.MessageDate.ReadOnly = true;
            // 
            // startDtPicker
            // 
            this.startDtPicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.startDtPicker.Location = new System.Drawing.Point(91, 32);
            this.startDtPicker.Name = "startDtPicker";
            this.startDtPicker.Size = new System.Drawing.Size(168, 20);
            this.startDtPicker.TabIndex = 1;
            this.startDtPicker.ValueChanged += new System.EventHandler(this.startDtPicker_ValueChanged);
            // 
            // endDtPicker
            // 
            this.endDtPicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.endDtPicker.Location = new System.Drawing.Point(91, 60);
            this.endDtPicker.Name = "endDtPicker";
            this.endDtPicker.Size = new System.Drawing.Size(168, 20);
            this.endDtPicker.TabIndex = 2;
            this.endDtPicker.ValueChanged += new System.EventHandler(this.endDtPicker_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Start time:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(31, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "End time:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(88, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Time interval";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(323, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "User";
            // 
            // userListComboBox
            // 
            this.userListComboBox.FormattingEnabled = true;
            this.userListComboBox.Location = new System.Drawing.Point(326, 31);
            this.userListComboBox.Name = "userListComboBox";
            this.userListComboBox.Size = new System.Drawing.Size(157, 21);
            this.userListComboBox.TabIndex = 7;
            this.userListComboBox.SelectedIndexChanged += new System.EventHandler(this.userListComboBox_SelectedIndexChanged);
            // 
            // getLogButton
            // 
            this.getLogButton.Location = new System.Drawing.Point(34, 95);
            this.getLogButton.Name = "getLogButton";
            this.getLogButton.Size = new System.Drawing.Size(75, 23);
            this.getLogButton.TabIndex = 8;
            this.getLogButton.Text = "Read logs";
            this.getLogButton.UseVisualStyleBackColor = true;
            this.getLogButton.Click += new System.EventHandler(this.getLogButton_Click);
            // 
            // ClientHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(622, 361);
            this.Controls.Add(this.getLogButton);
            this.Controls.Add(this.userListComboBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.endDtPicker);
            this.Controls.Add(this.startDtPicker);
            this.Controls.Add(this.logGridView);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ClientHistory";
            this.Text = "Message logs";
            this.Load += new System.EventHandler(this.ClientHistory_Load);
            ((System.ComponentModel.ISupportInitialize)(this.logGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView logGridView;
        private System.Windows.Forms.DateTimePicker startDtPicker;
        private System.Windows.Forms.DateTimePicker endDtPicker;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox userListComboBox;
        private System.Windows.Forms.Button getLogButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn userName;
        private System.Windows.Forms.DataGridViewTextBoxColumn userMessage;
        private System.Windows.Forms.DataGridViewTextBoxColumn MessageDate;
    }
}