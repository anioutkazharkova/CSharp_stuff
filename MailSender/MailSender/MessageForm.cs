using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.Mail;
using System.IO;
using System.Net.Mime;


namespace MailSender
{
    public partial class MessageForm : Form
    {
        //Хост и порт
        string host;
        int port;

        //Необходимость включить SSL
        bool isNeedSSL = false;

        //Делегат для отправки почты
        public delegate bool SendDel(MailMessage message);
        IAsyncResult res=null;

        //Сообщение
        MailMessage message;
        //Вложение
        List<string> attachments;

        int maxFileSize = 20;

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public MessageForm()
        {
            host = "smtp.yandex.ru";
            port = 25;
            attachments = new List<string>();
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            serviceComboBox.SelectedIndex = 0;
        }

     
        
        private void nameTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Запрещаем ввод неподходящих символов
            if ((e.KeyChar >= 'a' && e.KeyChar <= 'z') || (e.KeyChar >= '0' && e.KeyChar <= '9') || e.KeyChar =='.' || e.KeyChar=='-' ||e.KeyChar==(char)Keys.Back || e.KeyChar==(char)Keys.Delete) 
            {
            }
            else
            {
                e.Handled = true;
                ToolTip tip = new ToolTip();
                tip.SetToolTip(nameTextBox, "You can use only low latin symbols, numbers,'.' and '-'");
            }
        }

        private void passwordTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Запрещаем ввод неподходящих символов
            if ((e.KeyChar >= 'a' && e.KeyChar <= 'z') || (e.KeyChar >= 'A' && e.KeyChar <= 'Z') || (e.KeyChar >= '0' && e.KeyChar <= '9') || (e.KeyChar>=32 && e.KeyChar<=127) || e.KeyChar==(char)Keys.Back || e.KeyChar==(char)Keys.Delete) 
            {
            }
            else
            {
                e.Handled = true;
                ToolTip tip = new ToolTip();
                tip.SetToolTip(passwordTextBox, "You cannot use non-latin symbols");
            }
        }

        private void senderMailTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Запрещаем ввод неподходящих символов
            if ((e.KeyChar >= 'a' && e.KeyChar <= 'z')  || (e.KeyChar >= '0' && e.KeyChar <= '9') || e.KeyChar == '@' || e.KeyChar=='.'||e.KeyChar == '_' || e.KeyChar == '-' || e.KeyChar == (char)Keys.Back || e.KeyChar == (char)Keys.Delete)
            {
            }
            else
            {
                e.Handled = true;
                ToolTip tip = new ToolTip();
                tip.SetToolTip(senderMailTextBox, "You can use only low latin symbols, numbers,'.','@' and '-'");
            }
        }

        private void recMailTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Запрещаем ввод неподходящих символов
            if ((e.KeyChar >= 'a' && e.KeyChar <= 'z') || (e.KeyChar >= '0' && e.KeyChar <= '9') || e.KeyChar == '@' || e.KeyChar=='.'||e.KeyChar == '_' || e.KeyChar == '-' || e.KeyChar == (char)Keys.Back || e.KeyChar == (char)Keys.Delete)
            {
            }
            else
            {
                e.Handled = true;
                ToolTip tip = new ToolTip();
                tip.SetToolTip(recMailTextBox, "You can use only low latin symbols, numbers,'.','@' and '-'");
            }
        }

        /// <summary>
        /// Отображение сообщений от клиента
        /// </summary>
        /// <param name="message">Текст сообщения</param>
        public void DisplayMessage(string message)
        {
            
            if (message != "")
            {
                MessageBox.Show(message);
            }
        }

        //Установка параметров в зависимости от выбранного сервиса
        private void serviceComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string service = serviceComboBox.SelectedItem.ToString();
            senderMailTextBox.Text = "";
            //recMailTextBox.Text = "";
            nameTextBox.Text = "";
            passwordTextBox.Text = "";

            switch (service)
            {
                case "Yandex":
                    {
                        host = "smtp.yandex.ru";
                        port = 25;
                        isNeedSSL = false;
                    }
                    break;
                case "Mail.ru":
                    {
                        host = "smtp.mail.ru";
                        port = 25;
                        isNeedSSL = false;
                    }
                    break;
                case "Gmail.com":
                    {
                        host = "smtp.gmail.com";
                        port = 587;
                        isNeedSSL = true;
                    }
                    break;
            }
        }

        //Отправка сообщения
        private void sentButton_Click(object sender, EventArgs e)
        {
            //Если данные введены, создаем сообщение
            if (senderMailTextBox.Text != "" && nameTextBox.Text != "" && passwordTextBox.Text != "" && recMailTextBox.Text != "")
            {
                //Устанавливаем параметры сообщения
                message = new MailMessage();
                message.From = new MailAddress(senderMailTextBox.Text);
                message.To.Add(new MailAddress(recMailTextBox.Text));

                message.Subject = subjectTextBox.Text;
                message.Body = messageTextBox.Text;
                message.BodyEncoding = Encoding.GetEncoding("windows-1251");
                message.SubjectEncoding = message.BodyEncoding;

                //Прикрепляем вложения
                foreach (string item in attachments)
                {
                    Attachment attach = new Attachment(item, MediaTypeNames.Application.Octet);

                    ContentDisposition disposition = attach.ContentDisposition;
                    disposition.CreationDate = File.GetCreationTime(item);
                    disposition.ModificationDate = File.GetLastWriteTime(item);
                    disposition.ReadDate = File.GetLastAccessTime(item);

                    message.Attachments.Add(attach);

                }

                //Отправляем подготовленное сообщение
                using (MailClient client = new MailClient(host, port, this))
                {
                    client.Login = nameTextBox.Text;
                    client.Password = passwordTextBox.Text;
                    client.isNeedSSL = isNeedSSL;

                    bool isReady = false;
                  
                        SendDel del = new SendDel(client.SendMessage);
                        res = del.BeginInvoke(message, null, null);
                        isReady=del.EndInvoke(res);
                        if (isReady)
                        {
                            MessageBox.Show("Mail delivered successfully!");
                            CleanFields();
                        }

                }
               
            }
            else
            {
                DisplayMessage("Fill the input boxes with valid data!");
            }
        }

        private void senderMailTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        //Отправка вложения
        private void attachButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();

            if (dialog.ShowDialog() == DialogResult.OK)
            {

                //Выбор файла
                FileInfo file = new FileInfo(dialog.FileName);

                //Допустимый размер файла не должен превышать максимального
                if (file.Length / (1024 * 1024) <= maxFileSize)
                {
                    long totalSize = 0;
                    foreach (string item in attachments)
                    {
                        totalSize += (new FileInfo(item)).Length;
                    }
                    //Все вложения не должны превышать максимального
                    if (totalSize / (1024 * 1024) <= maxFileSize)
                    {
                        attachments.Add(dialog.FileName);
                        //attachLabel.Text += file.Name + ";\r\n";
                    }
                    else
                    {
                        DisplayMessage("Total size of all attachments shoudn't exceed 20 MB");
                    }
                }
                else
                {
                    DisplayMessage("Total size of all attachments shoudn't exceed 20 MB");
                }
                //Если число вложений больше 0, то можно просматривать их
                if (attachments.Count != 0)
                {
                    showLinkLabel.Enabled = true;
                    //showLinkLabel.Visible = true;
                }
            }
        }

       

        private void showLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //Отображаем окно вложений
            AttachForm attach = new AttachForm(attachments,this);
            //Регистрируем делегат действий при удалении вложения
            attach.Register(AttachmentDeleted);
            attach.ShowDialog();
        }

        /// <summary>
        /// При удалении вложения
        /// </summary>
        /// <param name="sender">Указатель на отправителя-инициатора события</param>
        private void AttachmentDeleted(Object sender)
        {
            if (attachments.Count == 0)
            {
                showLinkLabel.Enabled = false;
            }
        }
        /// <summary>
        /// Удаление вложения
        /// </summary>
        /// <param name="index">Индекс элемента</param>
        public void RemoveAttachment(int index)
        {
            if (index < attachments.Count)
            {
                attachments.RemoveAt(index);
            }
        }

        /// <summary>
        /// Очистка полей
        /// </summary>
        private void CleanFields()
        {
            nameTextBox.Text = "";
            passwordTextBox.Text = "";
            senderMailTextBox.Text = "";
            recMailTextBox.Text = "";
            messageTextBox.Text = "";

            attachments = new List<string>();
        }
        
    }
}
