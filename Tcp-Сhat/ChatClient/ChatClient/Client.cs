using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Xml;
using System.IO;


namespace ChatClient
{
    /// <summary>
    /// Класс клиента чата
    /// </summary>
  public  class Client
    {
      //Tcp-клиент для подключения
       TcpClient client;

      
       public TcpClient tcpClient
       {
           get { return client; }
       }

      //Порт и адрес 
        int port;
        string address;

      //Потоки чтения, записи и сетевой
        BinaryWriter streamWriter;
        BinaryReader streamReader;
        NetworkStream socketStream;

      //Признаки, подключен ли пользователь и вошел ли в чат
        bool isLogined;
        bool connected = false;

        //Логин и пароль
        string login;
        string password;

      //Свойства для получения логина и пароля
        public string Login
        {
            get
            {
                return login;
            }
        }
        public string Password
        {
            get
            {
                return password;
            }
        }

      //Свойство для получения признака, подключен ли пользователь
       public bool isClientConnected
        {
            get { return connected; }
            set { connected = value; }
        }

      /// <summary>
      /// Конструктор по умолчанию
      /// </summary>
        public Client()
        {
            //Пользователь не вошел в чат
            isLogined = false;
            //Настройка параметров
            SetUpConfiguration();
            try
            {
                //Подключение клиента к серверу по данному адресу и на данном порте
                client = new TcpClient(address, port);
            }
            catch (SocketException ex)
            {

            }                        
        }

      /// <summary>
      /// Конструктор с параметрами
      /// </summary>
      /// <param name="login">Логин</param>
      /// <param name="password">Пароль</param>
      /// <param name="newUser">Признак, пользователь является новым или нет</param>
        public Client(string login, string password, bool newUser)
        {
            isLogined = false;
            //Настройка параметров
            SetUpConfiguration();
            this.login = login;
            this.password = password;
            //Подключение клиента и инициализация потоков
            try
            {
                client = new TcpClient(address, port);

                socketStream = client.GetStream();
                streamWriter = new BinaryWriter(socketStream);
                streamReader = new BinaryReader(socketStream);
                //Клиент подключен
                connected = true;
            }
            catch (SocketException ex)
            {
                connected = false;
            }
            
        }

      /// <summary>
      /// Настройка параметров - считывание из xml-файла
      /// </summary>
        private void SetUpConfiguration()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("ServerConfig.xml");
            XmlElement root = doc.DocumentElement;
            XmlNodeList nodes = root.ChildNodes;

            port = int.Parse(nodes[0].InnerText);
            address = nodes[1].InnerText;
        }
                
      /// <summary>
      /// Чтение сообщения от сервера и других пользователей
      /// </summary>
      /// <returns>Текст сообщения</returns>
        public string ReadUserMessage()
        {
            string message="";
            try
            {
                //Если клиент подключен, то производится считывание сообщения
                if (connected)
                {
                    message = streamReader.ReadString();
                }
            }
            catch (IOException ex)
            {
            }
            return message;
        }

      /// <summary>
      /// Отправка сообщения
      /// </summary>
      /// <param name="message">Текст сообщения</param>
        public void SendMessageResponse(string message)
        {
            try
            {
                //Если клиент подключен, то сообщение отправляется
                if (connected)
                {
                    streamWriter.Write(message);
                }
                
            }
            catch (IOException ex)
            {
            }
        }

      /// <summary>
      /// Обработка сообщения
      /// </summary>
      /// <param name="message">Входящее сообщение</param>
      /// <param name="flag">Флаг сообщения</param>
      /// <param name="split">Разделитель</param>
      /// <returns>Внутренний текст</returns>
        public string ProcessMessage(string message, out string flag,string split)
        {
            string innerText = "";
            flag = "";
            if (message != "")
            {
                
                flag = "";
                string[] pack = message.Split(';');

                flag = pack[0].Split(':')[1];
                for (int i = 1; i < pack.Length; i++)
                    innerText += pack[i];
                pack = innerText.Split(':');
                innerText = "";
                for (int i = 1; i < pack.Length; i++)
                    innerText += pack[i]+split;
            }
            return innerText;
        }

      /// <summary>
      /// Выделение записей лога
      /// </summary>
      /// <param name="message">Входящее сообщение</param>
      /// <returns>Список сообщений с параметров</returns>
        public List<ChatMessage> ProcessLog(string message)
        {
            //Записи разделяются символами ^^
            string[] logstrings = message.Split(new string[] { "^^" }, StringSplitOptions.RemoveEmptyEntries);
            List<ChatMessage> mesList = new List<ChatMessage>();

            string flag;
            //Обработка первой записи, выделение флага
            message = ProcessMessage(logstrings[0], out flag, ":");
            message = message.TrimEnd(new char[] { ':' });

            //Если полученное внутренне сообщение непустое (удовлетворяющие записи в БД есть)
            if (message != "")
            {
                //Выделяем данные из записей (разделяющий символ ^?)
                string[] data = message.Split(new string[] { "^?" }, StringSplitOptions.None);
                //На основе выделенных данных создаем сообщение с параметрами и сохраняем в список
                mesList.Add(new ChatMessage(data[1], data[0], DateTime.Parse(data[2])));

                for (int i = 1; i < logstrings.Length; i++)
                {
                    message = logstrings[i];
                    data = message.Split(new string[] { "^?" }, StringSplitOptions.None);
                    mesList.Add(new ChatMessage(data[1], data[0], DateTime.Parse(data[2])));

                }
            }
            return mesList;
        }
      
      /// <summary>
      /// Остановка соединения с сервером
      /// </summary>
        public void StopConnection()
        {
            connected = false;    
       
            streamWriter.Close();
            streamReader.Close();
            socketStream.Close();           
            client.Close();
        }
    }

}
