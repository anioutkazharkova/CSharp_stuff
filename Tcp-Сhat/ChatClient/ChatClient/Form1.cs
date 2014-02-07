using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using Message;

namespace ChatClient
{
    public partial class Form1 : Form
    {
        //Клиент чата
        Client client;

        string formTitle = "Chat Client";

        //Список активных пользователей
        List<string> usersOfChat;

        //Форма запроса лога
        ClientHistory hisForm;

        //Делегат на обработку сообщения
        public delegate string MesProcDel(string message, out string flag,string split);
        //Делегат на запуск работы
        public delegate bool RunDel();

        public Form1()
        {
            //Основная форма невидима
            this.Visible = false;
            InitializeComponent();
                        
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Запуск диалоговых окон для аутентификации пользователя или создания новых
            Entering();
        }

        /// <summary>
        /// Запуск форм на вход пользователя и регистрацию
        /// </summary>
        private void Entering()
        {           
            this.Visible = false;      
            //Отображение формы на вход
            IntoForm inform = new IntoForm();
            inform.ShowDialog();

            //Если пользователь отменил вход или закрыл форму
            if (inform.Result == FormResult.Cancel)
            {
                //Закрываем основную форму
                    this.Close();
            }
            else
            { //Если пользователь выбрал создание нового пользователя
                if (inform.Result == FormResult.Register)
                {
                    //Отображение формы регистрации
                    NewUser nform = new NewUser();
                    nform.ShowDialog();

                    //Если пользователь отменил регистрацию
                    if (nform.Result == FormResult.Cancel)
                    {
                        //Отобразить форму для входа
                        Entering();
                    } //Если пользователь выбрал регистрацию
                    else if (nform.Result == FormResult.Register)
                    {
                        //Инициализируем клиента
                        client = new Client(nform.Login, nform.Password, true);
                        //Создаем сообщение запроса регистрации у сервера
                        string loginMessage = string.Format("flag:register;message:{0}|{1}",client.Login,client.Password);

                        //Если соединение с сервером есть
                        if (client.isClientConnected)
                        {
                            //Отправляем запрос серверу
                            client.SendMessageResponse(loginMessage);

                            //Регистрируем пользователя
                            RunDel del = new RunDel(LoginRegClient);
                            IAsyncResult res = del.BeginInvoke(null, null);
                            bool isRegistered = del.EndInvoke(res);

                            //Если регистрация не произошла
                            if (!isRegistered)
                            {
                                this.Visible = false;
                                //Уведомить пользователя, что такой логин уже используется
                                MessageBox.Show("User with such login exists. Enter with your login and password or try again");
                                //Отображение формы для входа
                                Entering();
                            }
                            else
                            {
                                //Если все успешно, сообщить пользователю
                                MessageBox.Show(string.Format("User {0} created successfully! Enter with your login and password", client.Login));
                                //Отображение формы для входа
                                Entering();
                            }
                        }
                        else
                        {//Иначе уведомить, что связи с сервером нет
                            MessageBox.Show("No connection with Server", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            //Отображение формы для входа
                            Entering();
                        }

                    }
                } //Если пользователь выбрал вход
                else if (inform.Result == FormResult.Login)
                {
                    //Инициализируем клиента
                    client = new Client(inform.Login,inform.Password,false);

                    //Создаем сообщение для сервера
                    string loginMessage = string.Format("flag:login;message:{0}|{1}",client.Login,client.Password);
                    //Если клиент подключен
                    if (client.isClientConnected)
                    {
                        //Отправка запроса серверу
                        client.SendMessageResponse(loginMessage);

                        //Провера успешности входа пользователя в чат
                        RunDel del = new RunDel(LoginRegClient);
                        IAsyncResult res = del.BeginInvoke(null, null);
                        bool isLogined = del.EndInvoke(res);

                        //Если вход не совершен
                        if (!isLogined)
                        {
                            this.Visible = false;
                            MessageBox.Show("Wrong login or password! Try again");
                            //Вывод формы для входа
                            Entering();
                        }
                        else
                        {
                            //Иначе отображаем главную форму
                            this.Visible = true;
                            MessageBox.Show("Welcome to Chat!");
                            //Отображаем имя пользователя в заголовке формы
                            this.Text = formTitle + " - " + client.Login;
                            //Запускаем работу клиента в отдельном потоке
                            Thread runThread = new Thread(Run);
                            runThread.Start();
                        }
                    }
                    else
                    {//Если соединения с сервером нет, вывести форму для входа
                        MessageBox.Show("No connection with Server","Information",MessageBoxButtons.OK,MessageBoxIcon.Error);
                        Entering();
                    }
                }
            }
        }

        /// <summary>
        /// Проверка успешности входа
        /// </summary>
        /// <returns></returns>
        private bool LoginRegClient()
        {
            bool isLogined = true;
            //Получаем уведомление от сервера
            string message = client.ReadUserMessage();           
            
            if (message != "OK")
            {
               
                isLogined = false;
            }
            return isLogined;
        }

        /// <summary>
        /// Запуск приема сообщений от других пользователей
        /// </summary>
        public void Run()
        {
            //Пока клиент подключен
            while (client.isClientConnected)
                {
                //Получение сообщения от сервера
                   string message = client.ReadUserMessage();
                   string tempMess = message;

                //Обработка сообщения, выделение текста и флага
                   string flag;
                   MesProcDel del = new MesProcDel(client.ProcessMessage);

                //Пустой разделительный символ в общем случае
               string split="";
                   IAsyncResult res = del.BeginInvoke(message, out flag,split, null, null);
                   message = del.EndInvoke(out flag, res);
                   
                switch(flag)
                {
                        //Если сообщение содержит флаг простого сообщения
                    case "message":
                        {
                            //Отображение сообщения в текстовом поле
                            BeginInvoke(new MethodInvoker(delegate
                            {

                                messageDisplayBox.Text += PrepareMessageAfter(message);
                                messageDisplayBox.Text += "\r\n";
                            }));
                        }
                        break;
                        //Если сообщение содержит список активных пользователей
                    case "userlist":
                        {
                            //Выделяем имена пользователей
                            string[] usList = message.Split('|');

                            //Выводим их в поле listView
                            BeginInvoke(new MethodInvoker(delegate
                            {
                                usersListView.Items.Clear();
                                for (int i = 0; i < usList.Length-1; i++)
                                {
                                    ListViewItem item = new ListViewItem();
                                    item.Text = usList[i];                                    
                                    usersListView.Items.Add(item);
                                }
                                
                            }));
                        }
                        break;
                        //Если сообщение содержит всех зарегистрированных пользователей
                    case "allusers":
                        {
                            //Выделяем имена
                            string[] usList = message.Split('|');
                            
                            //Формируем список
                            for (int i = 0; i < usList.Length - 1; i++)
                            {
                                usersOfChat.Add(usList[i]);
                            }
                            //Запускаем форму отображения лога, куда передаем всех имеющихся в базе пользователей
                            hisForm  = new ClientHistory(client, usersOfChat);
                            hisForm.ShowDialog();

                        }
                        break;
                        //Если сообщение содержит завершение работы сервера
                    case "bye":
                        {
                            //Остановка работы клиента
                            client.StopConnection();
                            client.isClientConnected = false;
                            MessageBox.Show("Server has disconnected","Information",MessageBoxButtons.OK,MessageBoxIcon.Error);
                           //Вывод формы входа
                            BeginInvoke(new MethodInvoker(delegate
                            {
                                Entering();
                            }));
                        }
                        break;
                }
        }               
            }
              

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //При закрытии формы отправляем серверу уведомление
            string byeMessage = "flag:bye;message:disconnected";
            if (client != null && client.isClientConnected)
            {
                client.SendMessageResponse(byeMessage);
                //Останавливаем работу клиента
                client.StopConnection();
                client.isClientConnected = false;
            }
        }
        
        private void messageDisplayBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Запрещаем вводить символы в поле отображения сообщений
            e.Handled = true;
        }

        private void historyButton_Click(object sender, EventArgs e)
        {
            if (client.isClientConnected)
            {
                //Запросить список зарегистрированных пользователей на сервере
                usersOfChat = new List<string>();
                string message = string.Format("flag:allusers;message:{0}", "");
                client.SendMessageResponse(message);
            }
            
        }

        //Очистка поля сообщений
        private void cleanButton_Click(object sender, EventArgs e)
        {
            messageDisplayBox.Text = "";
        }

        //Отправка сообщений
        private void sendButton_Click(object sender, EventArgs e)
        {
            if (client.isClientConnected)
            {
                //Если текст не пустой, то формируем сообщение и отправляем серверу
                if (mesSendBox.Text != "")
                {

                    string message = string.Format("flag:message;message:{0}", PrepareMessageBefore(mesSendBox.Text));
                    client.SendMessageResponse(message);
                    mesSendBox.Text = "";
                }
            }
            else {
                MessageBox.Show("Server has disconnected", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //Вывод формы входа                
                    Entering();               
            }
        }

        //Обработка сообщения - замена символов перехода на новую строку для записи в БД
        private string PrepareMessageBefore(string message)
        {
          return  message.Replace("\r\n", "!^");            
        }

        //Обработка сообщения - возврат символов перехода на новую строку
        private string PrepareMessageAfter(string message)
        {
            return message.Replace("!^", "\r\n");
        }
        
    }
}
