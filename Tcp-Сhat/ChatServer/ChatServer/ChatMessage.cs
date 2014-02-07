using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChatServer
{
    /// <summary>
    /// Сообщение с параметрами
    /// </summary>
    class ChatMessage
    {
        //Текст
        string mesText;
        //Имя пользователя
        string userName;
        //Дата отправки
        DateTime mesDate;

        //Свойства для получения параметров
        public string MesText
        {
            get { return mesText; }
            set { mesText = value; }
        }

        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }

        public DateTime MesDate
        {
            get { return mesDate; }
            set { mesDate = value; }
        }

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public ChatMessage()
        {
            mesText = "";
            userName = "";
            //По умолчанию текущая дата
            mesDate = DateTime.Now;
        }

        /// <summary>
        /// Конструктор с параметрами
        /// </summary>
        /// <param name="text">Текст</param>
        /// <param name="name">Имя пользователя</param>
        /// <param name="date">Дата</param>
        public ChatMessage(string text, string name, DateTime date)
        {
            mesText = text;
            userName = name;
            mesDate = date;
        }

        //Вывод отформатированного сообщения
        public override string ToString()
        {
            string message = string.Format("{0} ({1}) > {2} ", userName,mesDate.ToShortDateString()+" "+mesDate.ToLongTimeString(), mesText);

            return message;
        }

    }
}
