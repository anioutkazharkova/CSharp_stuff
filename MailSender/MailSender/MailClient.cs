using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Web;

namespace MailSender
{
    /// <summary>
    /// Класс почтового клиента
    /// </summary>
    class MailClient:IDisposable
    {
        //Ссылка на основную форму
        MessageForm form;

        //Хост и порт клиента
        string host;
        int port;

        //SMTP-клиент
        SmtpClient smtpClient;

        //Логин и пароль
        string login;
        string password;

        public string Login
        {
            get { return login; }
            set { login = value; }
        }

        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        //Признак необходимости включения SSL
        bool needSSL = false;

        public bool isNeedSSL{
            get { return needSSL; }
            set { needSSL = value; }
    }

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        /// <param name="form">Ссылка на основную форму</param>
        public MailClient(MessageForm form)
        {
            port = 0;
            host = "";
            login = "";
            password = "";
            this.form = form;

            smtpClient = new SmtpClient();
           
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="host">Хост</param>
        /// <param name="port">Порт</param>
        /// <param name="form">Ссылка на основную форму</param>
        public MailClient(string host, int port, MessageForm form)
        {
            this.host = host;
            this.port = port;
            this.form = form;

            //Создание SMTP-клиента
            smtpClient = new SmtpClient(this.host, this.port);
            
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="host">Хост</param>
        /// <param name="port">Порт</param>
        /// <param name="login">Логин</param>
        /// <param name="password">Пароль</param>
        /// <param name="form">Ссылка на основную форму</param>
        public MailClient(string host, int port,string login, string password, MessageForm form)
        {
            this.host = host;
            this.password = password;
            this.login = login;
            this.password = password;
            this.form = form;

            smtpClient = new SmtpClient(this.host, this.port);
           
        }
        /// <summary>
        /// Конструктор с передачей признака
        /// </summary>
        /// <param name="host">Хост</param>
        /// <param name="port">Порт</param>
        /// <param name="NeedSSL">Признак, нужно ли включить SSL</param>
        /// <param name="form">Ссылка на основную форму</param>
        public MailClient(string host, int port,bool NeedSSL, MessageForm form)
        {
            this.host = host;
            this.port = port;
            this.form = form;
            this.needSSL = NeedSSL;
            smtpClient = new SmtpClient(this.host, this.port);
            
        }

        /// <summary>
        /// Конструкто
        /// </summary>
        /// <param name="host">Хост</param>
        /// <param name="port">Порт</param>
        /// <param name="login">Логин</param>
        /// <param name="password">Пароль</param>
        /// <param name="NeedSSL">Признак, нужно ли включить SSL</param>
        /// <param name="form">Ссылка на основную форму</param>
        public MailClient(string host, int port, string login, string password, bool NeedSSL, MessageForm form)
        {
            this.host = host;
            this.password = password;
            this.login = login;
            this.password = password;
            this.form = form;
            this.needSSL = NeedSSL;
            smtpClient = new SmtpClient(this.host, this.port);
            
        }

        /// <summary>
        /// Подготовка SMTP-клиента
        /// </summary>
        private bool PrepareClient()
        {
            //Запрет использовать данные по умолчанию
            smtpClient.UseDefaultCredentials = false;

            //Если логин и пароль не пустые
            if (login != "" && password != "")
            {
                //Они устанавливаются
                smtpClient.Credentials = new NetworkCredential(login, password);

                //Если необходимо, включается поддержка SSL
                if (isNeedSSL)
                {
                    smtpClient.EnableSsl = true;
                }

                //Метод отправки через сеть
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;

                return true;

            }
            else
            {
                //Логин и пароль не введены - вывести уведомление
                form.DisplayMessage("Enter login and password!!!");
                return false;
            }
        }

        public bool SendMessage(MailMessage message)
        {
            try
            {
                //Если SMTP-клиент подготовлен правильно
                if (PrepareClient())
                {
                    //Если адресаты указаны, то отправляем сообщение
                    if (message.From != null && message.To.Count != 0)
                    {
                        smtpClient.Send(message);
                        return true;
                    }
                    else
                    {
                        form.DisplayMessage("Enter addresses!");
                        return false;
                    }
                }
                else
                    return false;

            }
            catch (SmtpException ex)
            {
                form.DisplayMessage("Can't send the message! Some data is wrong");
                return false;
            }
        }

         void System.IDisposable.Dispose()
    {
        smtpClient.Dispose();
    }


    }
}
