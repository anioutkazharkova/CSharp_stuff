using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ChatClient
{
    /// <summary>
    /// Форма отображения логов
    /// </summary>
    public partial class ClientHistory : Form
    {
        //Ссылка на клиент
        Client client;
        //Список пользователей из БД
        List<string> userList=new List<string>();

        //Автор сообщения
        string user;
        //Начальная дата
        DateTime startTime;
        //Конечная дата
        DateTime endTime;

        string defItem = "All users";

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="client">Клиент</param>
        /// <param name="users">Список пользователей</param>
        public ClientHistory(Client client,List<string>users)
        {
            //Инициализация параметров
            this.client = client;
            user = "";
            startTime = DateTime.Now;
            endTime = DateTime.Now;

            foreach (string u in users)
            {
                userList.Add(u);
            }

            InitializeComponent();

            userListComboBox.Items.Add(defItem);
            for(int i=0;i<userList.Count;i++)
            {
                userListComboBox.Items.Add(userList[i]);
            }
            userListComboBox.SelectedIndex = 0;
        }

        private void ClientHistory_Load(object sender, EventArgs e)
        {
            //Установка форматов отображения даты и времени
            startDtPicker.CustomFormat = "dd MM yyyy HH:mm:ss";
            startDtPicker.Format = DateTimePickerFormat.Custom;

            endDtPicker.CustomFormat = "dd MM yyyy HH:mm:ss";
            endDtPicker.Format = DateTimePickerFormat.Custom;        
            
          
        }

        //Запрос логов
        private void getLogButton_Click(object sender, EventArgs e)
        {
            //Формирование сообщения для запроса с сервера
            string message = string.Format("flag:meslog;message:{0}|{1}|{2}|",startTime.ToString(),endTime.ToString(),user);

            //Отправка и получения результатов
           client.SendMessageResponse(message);
           message= client.ReadUserMessage();           

            //Обработка списка сообщений
           List<ChatMessage> list = client.ProcessLog(message);
            //Загрузка сообщений в таблицу
           FillDataGrid(list);
          
        }

        private void startDtPicker_ValueChanged(object sender, EventArgs e)
        {
            //Установка начальной даты
            startTime = startDtPicker.Value;
        }

        private void endDtPicker_ValueChanged(object sender, EventArgs e)
        {
            //Установка конечной даты
            endTime = endDtPicker.Value;
        }

        private void userListComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Установка пользователя для отбора
            if (userListComboBox.SelectedIndex != 0)
                user = userListComboBox.SelectedItem.ToString();
            else
                user = " ";
            }

        /// <summary>
        /// Загрузка сообщений в таблицу
        /// </summary>
        /// <param name="mesList">Список сообщений с параметрами</param>
        public void FillDataGrid(List<ChatMessage> mesList)
        {

            logGridView.Rows.Clear();
            if (mesList.Count != 0)
            {
                foreach (ChatMessage mes in mesList)
                {
                    logGridView.Rows.Add(mes.UserName, mes.MesText.Replace("!^","\r\n"), mes.MesDate);
                }
            }
        }
        
    }
}
