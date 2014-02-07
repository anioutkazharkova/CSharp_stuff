using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChatClient
{
    public class ChatMessage
    {
        string mesText;
        string userName;

        DateTime mesDate;

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

        public ChatMessage()
        {
            mesText = "";
            userName = "";
            mesDate = DateTime.Now;
        }

        public ChatMessage(string text, string name, DateTime date)
        {
            mesText = text;
            userName = name;
            mesDate = date;
        }


        public override string ToString()
        {
            string message = string.Format("{0} ({1}) > {2} ", userName, mesDate.ToLongDateString(), mesText);

            return base.ToString();
        }

    }
}
