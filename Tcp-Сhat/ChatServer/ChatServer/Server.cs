using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Xml;
using System.Threading;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Data;
using System.Configuration;
using System.Collections.Specialized;

namespace ChatServer
{
    /// <summary>
    /// Класс сервера(его основной логики)
    /// </summary>
    class Server
    {
        //Сетевые параметры
        TcpListener listener;
        int port;
        IPAddress address;

        //Списки пользователей, их имен и потоков
      public  List<ChatUser> Users;
      public  List<String> usersNames;
      public  List<Thread> usersThreads;

        //Соединение с БД 
        SqlConnection connection;

        //Строка подключения к БД
        string connectionString;

        bool connected = false;
        public bool isConnected
        {
            get { return connected; }
        }

        //Асинхронный делегат для запуска клиента 
        public delegate void ClientTempDel();        

        /// <summary>
        /// Начальная инициализация параметров 
        /// </summary>
        public Server()
        {

            Users = new List<ChatUser>();
            usersThreads = new List<Thread>();
            usersNames = new List<String>();            
            SetUpConfiguration();
            SetUpConnection();
                       
        }

        /// <summary>
        /// Настройка конфигурации
        /// </summary>
        private void SetUpConfiguration()
        {
            ////Считывание параметров (порт, адрес и строка подключения) из xml-файла
            //XmlDocument doc = new XmlDocument();
            //doc.Load("ServerConfig.xml");
            //XmlElement root = doc.DocumentElement;
            //XmlNodeList nodes = root.ChildNodes;

            //port = int.Parse(nodes[0].InnerText);
            //address = IPAddress.Parse(nodes[1].InnerText);
            //connectionString = nodes[2].InnerText;

            //Getting parametrs from config
            port = int.Parse(ConfigurationManager.AppSettings.Get("port"));
            address = IPAddress.Parse(ConfigurationManager.AppSettings.Get("address"));
            connectionString = ConfigurationManager.AppSettings.Get("dbase");
         }

        /// <summary>
        /// Настройка соединения сервера
        /// </summary>
        private void SetUpConnection()
        {
            try
            {
                //Запуск сервера с параметрами
                listener = new TcpListener(address, port);
                listener.Start();

                DisplayMessage(string.Format("ChatServer listening on {0} port...",port));
                connected = true;
            }
            catch (SocketException ex)
            {
                connected = false;
               DisplayMessage("Cannot set up connection");
            }
        }

        /// <summary>
        /// Запуск сервера на подключение клиентов и работу с ними
        /// </summary>
        public void RunServer()
        {
            //Для остановки сервера нужно ввести команду shutdown
            DisplayMessage("Server is running. Enter \"shutdown\" to stop the Server");
           
            bool running = true;
            
            while (running) 
            {
              
                //Создание временного экземпляра пользователя для считывания идентифицирующей информации
                ChatUser temp = new ChatUser(listener.AcceptTcpClient(), this);

                //Запустить проверку статуса клиента
                ClientTempDel del = new ClientTempDel(temp.RunClientTemporally);
                IAsyncResult result = del.BeginInvoke(null, null);
                del.EndInvoke(result);

                //Если пользователь может войти в чат, то он из временного становится постоянным (его данные заносятся во все списки)
                if (temp.UserStatus == Status.Logined)
                {
                    Users.Add(temp);
                    usersNames.Add(temp.UserName);
                    //Запуск пользователя на прием сообщений от других пользователей и сервера
                    Thread thread = new Thread(Users[Users.Count - 1].RunClient);
                    thread.Start();
                    usersThreads.Add(thread);
                }
            } 

            
            DisplayMessage("Server is stopped");
        }


        /// <summary>
        /// Рассылка сообщения всем пользователям, кроме автора
        /// </summary>
        /// <param name="message">Сообщение</param>
        /// <param name="user">Автор</param>
        public void SendBroadcastMessage(string message,ChatUser user)
        {
            foreach (ChatUser u in Users)
            {
                if (user.UserName != u.UserName)
                {
                    u.SendMessageResponse(message);
                }
            }
        }

        /// <summary>
        /// Рассылка списка  активных пользователей
        /// </summary>
        /// <param name="user"></param>
        public void SendBroadcastList(ChatUser user)
        {
            string list = "";

            //'|' используется как разделяющий символ
            foreach (ChatUser u in Users)
            {
                list += u.UserName + "|";
            }

            //Сообщение уходит с флагом userlist
            foreach (ChatUser u in Users)
            {                
                u.SendMessageResponse(string.Format("flag:userlist;message:{0}",list));
            }

        }

        
        /// <summary>
        /// Вход пользователя
        /// </summary>
        /// <param name="login">Логин</param>
        /// <param name="password">Пароль</param>
        /// <param name="newUser">Признак, является ли пользователь новым или нет</param>
        /// <returns>Существует ли пользователь с такими данными</returns>
        public bool LoginUser(string login, string password,bool newUser)
        {
            bool isUserExist = false;

            //Запрос к базе данных
            using (connection = new SqlConnection(connectionString))
            {
                //Создание и подготовка соединения
                connection.Open();

                //Создание новой команды для запроса
                SqlCommand command = new SqlCommand();
                command.Connection = connection;

                //Если пользователь не является новым, то нужно проверить, существует ли пользователь с таким логином и паролем 
                if (!newUser)
                {
                    command.CommandText = "SELECT COUNT(*) from chb.Users WHERE login=@login AND password=@password";

                    command.Parameters.Add("@password", SqlDbType.NVarChar);
                    command.Parameters["@password"].Value = password;
                }
                else //Если же пользователь новый, нужно проверить, существует ли уже такой логин
                {
                    command.CommandText = "SELECT COUNT(*) from chb.Users WHERE login=@login";
                }

                command.Parameters.Add("@login", SqlDbType.NVarChar);
                command.Parameters["@login"].Value = login;
                
                //Осуществление запроса
                int count = (int)command.ExecuteScalar();
                
                //Если получена одна запись, то пользователь существует в базе
                if (count == 1)
                    isUserExist = true;
                connection.Close();
            }

            return isUserExist;
        }

        /// <summary>
        /// Регистрация пользователя
        /// </summary>
        /// <param name="login">Логин</param>
        /// <param name="password">Пароль</param>
        /// <param name="newUser">Новый пользователь или нет</param>
        /// <returns>Успешно ли создан пользователь</returns>
        public bool RegisterUser(string login, string password,bool newUser)
        {
            bool isRegistrationValid = false;

            //Проверяем существование пользователя с таким логином
            bool isUserExist = LoginUser(login, password,newUser);
            
            //Если логин свободен, то создаем в базе пользователя с данными
            if (!isUserExist)               
            {
                using(connection=new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand();
                    command.Connection = connection;

                    //Запрос на создание нового пользователя
                    command.CommandText = "INSERT INTO chb.Users VALUES(@login,@password);";
                    command.Parameters.Add("@login", SqlDbType.NVarChar);
                    command.Parameters.Add("@password", SqlDbType.NVarChar);
                    command.Parameters["@login"].Value = login;
                    command.Parameters["@password"].Value = password;

                    command.ExecuteNonQuery();

                    //Регистрация произведена успешно
                    isRegistrationValid = true;
                    connection.Close();
                }

            }
            return isRegistrationValid;
        }

        /// <summary>
        /// Запрос списка пользователей
        /// </summary>
        /// <param name="users">Список пользователей для вывода</param>
        public void GetAllUsers(out List<string>users)
        {
            string query = "SELECT login FROM chb.Users;";
             users = new List<string>(); 

            //Создаем подключение и запрашиваем список логинов из таблицы пользователей
            using (connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = query;

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        users.Add(reader.GetValue(0).ToString());
                    }
                }
                connection.Close();
            }

            
        }

        /// <summary>
        /// Запись сообщения в лог
        /// </summary>
        /// <param name="message">Сообщение с параметрами</param>
        public void WriteToLog(ChatMessage message)
        {
            //Получаем параметры сообщения
            SqlDateTime date = new SqlDateTime(message.MesDate);           
            string text = message.MesText;
            string user = message.UserName;            

            string query = "INSERT INTO chb.Logs VALUES(@date,@text,(SELECT userid FROM chb.Users WHERE login=@login ));";

            //Соединяемся с базой данных и создаем в таблице логов запись
            using (connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = connection;

                command.CommandText = query;

                //Запись содержит дату, текст и логин пользователя
                command.Parameters.Add("@date", SqlDbType.DateTime);
                command.Parameters.Add("@text", SqlDbType.NVarChar);
                command.Parameters.Add("@login",SqlDbType.NVarChar);

                command.Parameters["@date"].Value=date;
                command.Parameters["@text"].Value=text;
                command.Parameters["@login"].Value=user;

                command.ExecuteNonQuery();
                               
                connection.Close();
            }
            
        }

        /// <summary>
        /// Запрос логов из базы данных
        /// </summary>
        /// <param name="start">Начальная дата</param>
        /// <param name="end">Конечная дата</param>
        /// <param name="user">Пользователь</param>
        /// <returns>Лог в формате строки</returns>
        public string GetLogs(DateTime start, DateTime end, string user)
        {
            string query = "";
            string Log = "";

            //Создаем подключение к базе данных
            using (connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand();

                command.Connection = connection;

                //Если имя пользователя непустое, то запрашиваем записи определенного пользователя за определенное время
                if (user != " ")
                {
                    query =
                       @"SELECT u.login, l.message,l.mesdate FROM chb.Logs as l 
                        INNER JOIN chb.Users as u ON l.userid=u.userid 
                        WHERE u.userid = (SELECT userid from chb.Users WHERE login=@login)
                        AND l.mesdate>= @start AND l.mesdate<= @end
                        ORDER BY u.login, l.mesdate ASC;";

                    command.Parameters.Add("@login", SqlDbType.NVarChar);
                    command.Parameters["@login"].Value = user;
                }
                else //Иначе запрашиваем записи всех пользователей за период
                {
                    query =
                        @"SELECT   u.login,l.message, l.mesdate  FROM chb.Logs as l 
                        INNER JOIN chb.Users as u ON l.userid = u.userid
                        WHERE l.mesdate >= @start AND l.mesdate <= @end
                        ORDER BY u.login, l.mesdate ASC;";
                }
                command.Parameters.Add("@start", SqlDbType.DateTime);
                command.Parameters.Add("@end", SqlDbType.DateTime);
                command.Parameters["@start"].Value = start;
                command.Parameters["@end"].Value = end;
                
                command.CommandText = query;

                using (SqlDataReader reader = command.ExecuteReader())
                {

                    //Полученные записи сохраняются в строковом формате: ^^ - разделитель между записями, ^? - разделитель полей
                    while (reader.Read())
                    {
                        Log += string.Format("{0}^?{1}^?{2}^^", reader.GetValue(0).ToString(), reader.GetValue(1).ToString(), reader.GetValue(2).ToString());
                    }
                }
                connection.Close();
            }

            return Log;
        }

        /// <summary>
        /// Остановка сервера
        /// </summary>
        public void ServerStop()
        {
            //Рассылка прощального сообщения всем пользователям
            string message = "flag:bye;message:shutdown";
            
            for (int i = 0; i < Users.Count; i++)
            {
                Users[i].SendMessageResponse(message);
                usersThreads[i].Abort();
            }
            //Очистка списков 
            while (Users.Count != 0)
            {
                Users.RemoveAt(Users.Count - 1);
                usersNames.RemoveAt(usersNames.Count - 1);
                usersThreads.RemoveAt(usersThreads.Count - 1);
            }
            
            
        }

        /// <summary>
        /// Отображение сообщений
        /// </summary>
        /// <param name="message">Текст сообщения</param>
        public void DisplayMessage(string message)
        {
            Console.WriteLine(string.Format("> {0}", message));
            Console.WriteLine();
        }
    }
}
