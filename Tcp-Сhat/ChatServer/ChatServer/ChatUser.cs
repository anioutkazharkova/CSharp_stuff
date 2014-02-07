using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;



namespace ChatServer
{
    //Статусы пользователя (глобальное перечисление)
    public enum Status {Logined,Registered,Cancelled};
    
    /// <summary>
    /// Класс для работы с пользователем со стороны сервера
    /// </summary>
    class ChatUser
    {
        //Запущенный tcp-клиент
        internal TcpClient client;
        //Ссылка на сервер
        Server server;

        //Потоки на чтение, запись, сетевой
        BinaryWriter streamWriter;
        BinaryReader streamReader;
        NetworkStream socketStream;

        //Имя пользователя
        string userName;

      public delegate bool UserDel(string login,string password,bool newUser); //Делегат на запуск регистрации или проверки пользователя на вход
      public delegate void SendDel(string message, ChatUser user); //Делегат на рассылку сообщений пользователям
      public delegate void WriteDel(ChatMessage message); //Делегат на запись сообщения в лог
      public  delegate void SendListDel(ChatUser user); //Делегат на рассылку списка активных пользователей
      public delegate void GetUsersDel(out List<string> users); //Делегат на запрос пользователей из базы
      public delegate void SendLogDel(string message); //Делегат на отправку лога

       public delegate string LogDel(DateTime startDate, DateTime endDate, string userName);
        
        public IAsyncResult res;

        //Статус пользователя
        Status userStatus;

        //Свойство для получения статуса
        public Status UserStatus
        {
            get
            {
                return userStatus;
            }
        }

        //Свойство для получения имени пользователя
        public string UserName
        {
            get { return userName; }
        }

        /// <summary>
        /// Конструктор с параметрами для создания пользователя
        /// </summary>
        /// <param name="client">tcp-клиент</param>
        /// <param name="server">Запускающий сервер</param>
        public ChatUser(TcpClient client,Server server)
        {
            try
            {
                //Инициализация всех параметров
                userName = "";
                this.client = client;
                this.server = server;
                userStatus = Status.Cancelled;

                socketStream = this.client.GetStream();
                streamReader = new BinaryReader(socketStream);
                streamWriter = new BinaryWriter(socketStream);
            }
            catch (SocketException ex)
            {
            }
           
        }

        /// <summary>
        /// Конструктор для инициализации пользователя с именем
        /// </summary>
        /// <param name="client">tcp-клиент</param>
        /// <param name="server">Запускающий сервер</param>
        /// <param name="name">Имя пользователя</param>
        public ChatUser(TcpClient client, Server server,string name)
        {
            try
            {
                this.userName = name;
                this.client = client;
                this.server = server;
                userStatus = Status.Cancelled;

                socketStream = client.GetStream();
                streamReader = new BinaryReader(socketStream);
                streamWriter = new BinaryWriter(socketStream);
               
            }
            catch (SocketException ex)
            {
            }            
        }

        /// <summary>
        /// Запуск временного соединения с пользователем для определения дальнейших действий
        /// </summary>
        public void RunClientTemporally()
        {
            
            string flag;
            bool running = true;

            //Чтение сообщения пользователя
            string message = ReadUserMessage();

            //Обработка (выделение текста и флага)
            message = ProcessMessage(message, out flag);

            switch (flag)
                {
                    //Если сообщение с флагом входа
                    case "login":
                        {
                          //Получаем логин и пароль 
                            string[] data = message.Split('|');
                            string login = data[0];
                            string password = data[1];

                            //Запускаем проверку, существует ли пользователь в базе данных
                                UserDel del = new UserDel(server.LoginUser);
                                res = del.BeginInvoke(login, password,false, null, null);

                            //Получаем результат проверки
                                bool isLogExist = del.EndInvoke(res);

                            //Если пользователь существует
                                if (isLogExist)
                                {
                                    //Отправляем ему подтверждающее вход сообщение
                                    SendMessageResponse("OK");

                                    //Сообщаем о его подключении
                                    server.DisplayMessage(string.Format("User {0} connected",login));

                                    //Устанавливаем статус
                                    userStatus = Status.Logined;
                                    //Запоминаем имя
                                    userName = login;
                                }
                                else
                                {
                                    //Останавливаем работу с пользователем
                                    running = false;  
                                    //Статус пользователя - отменен
                                    userStatus = Status.Cancelled;
                                }                            
                        }
                        break;
                    //Если флаг регистрации
                    case "register":
                        {
                            //Выделяем логин и пароль
                            string[] data = message.Split('|');
                            string login = data[0];
                            string password = data[1];

                            //Проверяем данные и создаем пользователя
                            UserDel del = new UserDel(server.RegisterUser);
                            res = del.BeginInvoke(login, password,true, null, null);
                            bool isRegValid = del.EndInvoke(res);

                            //Если пользователь создан
                            if (isRegValid)
                            {
                                //Отправляем ему уведомление об успешной регистрации
                                SendMessageResponse("OK");
                                server.DisplayMessage(string.Format("New user: {0}",login));
                                //Статус пользователя - зарегистрирован
                                userStatus = Status.Registered;
                            }
                            else
                            {
                                //Отмена регистрации
                                server.DisplayMessage(string.Format("Registration failed: user {0} exists already",login));
                                userStatus = Status.Cancelled;
                            }
                            //Остановка работы с пользователем
                            running = false;
                        }
                        break;                 
                }
            //Если работа с пользователем окончена, закрываем все потоки
                if (!running)
                {
                    streamReader.Close();
                    streamWriter.Close();
                    socketStream.Close();
                    client.Close();
                }
           
        }

        //Работа с вошедшим пользователем
        public void RunClient()
        {
            //Рассылаем пользователям список активных пользователей чата
            SendListDel send = new SendListDel(server.SendBroadcastList);
            res = send.BeginInvoke(this, null, null);
            send.EndInvoke(res);

            string flag;
            
            bool running = true;

            //Пока работа с пользователем ведется
            do
            {
                //Чтение и обработка сообщения, выделение флага и текста
                string message = ReadUserMessage();
                string mess = message;
                message = ProcessMessage(message, out flag);

                switch (flag)
                {
                        //Если сообщение имеет флаг сообщения
                   case "message":
                        {
                            //Создаем сообщение с параметрами (текст, автор, дата)
                            ChatMessage mes = new ChatMessage(message, userName, DateTime.Now);

                            //Отображение сообщения у отправителя
                            SendMessageResponse(string.Format("flag:message;message:{0}",mes.ToString()));
                            
                            //Запись сообщения в лог
                            WriteDel wdel = new WriteDel(server.WriteToLog);
                            res = wdel.BeginInvoke(mes, null, null);
                            wdel.EndInvoke(res);

                            //Рассылка сообщения всем активным пользователям
                           SendDel del = new SendDel(server.SendBroadcastMessage);
                           res = del.BeginInvoke(string.Format("flag:message;message:{0}", mes.ToString()), this, null, null);
                            del.EndInvoke(res);                            
                        }
                        break;
                        //Если флаг запроса всех зарегистрированных пользователей
                   case "allusers":
                        {
                            //Запрос в базе данных списка всех пользователей
                            List<string> users;
                            GetUsersDel getdel = new GetUsersDel(server.GetAllUsers);
                            res = getdel.BeginInvoke(out users,null, null);
                            getdel.EndInvoke(out users,res);

                            //Формируем строку и рассылаем запросившему пользователю
                                string userlist = "";
                                foreach (string user in users)
                                {
                                    userlist += user + "|";
                                }
                                SendMessageResponse(string.Format("flag:allusers;message:{0}",userlist));
                            

                        }
                        break;
                        //Если флаг запроса логов сообщений
                   case "meslog":
                        {
                            string[] str = (mess.Split(';')[1]).Split(':');
                            message = "";
                            for (int i = 1; i < str.Length; i++)
                            {
                                message += str[i] + ":";
                            }
                            
                            //Выделяем из сообщения условия запроса
                            string[] param = message.Split('|');
                            DateTime start = DateTime.Parse(param[0]);
                            DateTime end = DateTime.Parse(param[1]);
                            string user = param[2];

                            //Запрос логов по параметрам
                            LogDel logdel = new LogDel(server.GetLogs);
                            res = logdel.BeginInvoke(start, end, user,null,null);
                            string log=logdel.EndInvoke(res);

                            //Отправляем логи пользователю
                            SendLogDel del = new SendLogDel(SendMessageResponse);
                            res = del.BeginInvoke(string.Format("flag:meslog;message:{0}", log), null, null);
                            del.EndInvoke(res);
                            
                        }
                        break;
                        //Если флаг завершения сеанса пользователя
                   case "bye":
                        {
                            //Завершаем работу с пользователем
                            running = false;
                            server.DisplayMessage(string.Format("User {0} has disconnected",userName));

                            //Удаляем клиента из всех списков
                            int index=server.usersNames.IndexOf(userName);
                            
                            server.usersNames.RemoveAt(index);
                            server.Users.RemoveAt(index);
                           

                            //Отправляем пользователям список всех активных пользователей
                            send = new SendListDel(server.SendBroadcastList);
                            res = send.BeginInvoke(this, null, null);
                            send.EndInvoke(res);

                            //Выключаем поток пользователя
                            server.usersThreads.ElementAt(index).Abort();
                            server.usersThreads.RemoveAt(index);
                        }
                        break;
                }

            } while (running);

            //По завершению работы закрываем все потоки
            streamReader.Close();
            streamWriter.Close();
            socketStream.Close();
            client.Close();
        }

        /// <summary>
        /// Чтение сообщения пользователя из потока
        /// </summary>
        /// <returns>Сообщение</returns>
        public string ReadUserMessage()
        {
            string message = "";
            try
            {
                message = streamReader.ReadString();

            }
            catch (IOException ex)
            {
            }
            catch (SocketException e)
            {
            }
            return message;
        }

        /// <summary>
        /// Отправка сообщения пользователю
        /// </summary>
        /// <param name="message">Сообщение</param>
        public void SendMessageResponse(string message)
        {
            try
            {
                streamWriter.Write(message);
            }
            catch (SocketException e)
            {
            }
        }

        /// <summary>
        /// Обработка сообщения
        /// </summary>
        /// <param name="message">Входящее сообщение</param>
        /// <param name="flag">Флаг</param>
        /// <returns>Содержание сообщения </returns>
        private string ProcessMessage(string message, out string flag)
        {
            string innerText="";
            flag = "";
            string[] pack = message.Split(';');

            flag = pack[0].Split(':')[1];
            innerText = pack[1].Split(':')[1];

            return innerText;
        }
        



        
    }
}
