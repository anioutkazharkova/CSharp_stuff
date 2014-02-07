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
    //Перечисление результатов нажатия кнопок
    public enum FormResult { Login, Cancel, Register};

    //Форма входа пользователя
    public partial class IntoForm : Form
    {

        string login; //Логин
        string password; //Пароль

        //Свойства для логина и пароля
        public string Login
        {
            get
            {
                return login;
            }
            set
            {
                login = value;
            }
        }

        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
            }
        }
        public FormResult Result; //Результат нажатия кнопки


        
        public IntoForm()
        {
            InitializeComponent();

            //Задание значений по умолчанию
            password = "";
            login = "";

            //Кнопка входа неактивна
            loginButton.Enabled = false;
            Result = FormResult.Cancel;
        }

        //Передача параметров главной форме
        private void loginButton_Click(object sender, EventArgs e)
        {

            Result = FormResult.Login;
            login = loginBox.Text;
            password = passwordBox.Text;
        }

        //Отмена входа
        private void cancelButton_Click(object sender, EventArgs e)
        {
            Result = FormResult.Cancel;
        }       

        private void loginBox_TextChanged(object sender, EventArgs e)
        {
            //Если поля логина и пароля не пустые, то кнопка активируется
            if (loginBox.Text != "" && passwordBox.Text != "")
            {
                loginButton.Enabled = true;
            }
            else //Иначе кнопка становится неактивной
            {
                loginButton.Enabled = false;
            }
        }

        private void passwordBox_TextChanged(object sender, EventArgs e)
        {
            //Если поля логина и пароля не пустые, то кнопка активируется
            if (loginBox.Text != "" && passwordBox.Text != "")
            {
                loginButton.Enabled = true;
            }
            else//Иначе кнопка становится неактивной
            {
                loginButton.Enabled = false;
            }

        }

        private void passwordBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            //В поле пароля можно вводить только цифры и латинские буквы
              if (Char.IsDigit(e.KeyChar) || (e.KeyChar >= 'A' && e.KeyChar <= 'Z') || (e.KeyChar >= 'a' && e.KeyChar <= 'z')||(e.KeyChar==(char)Keys.Back)||(e.KeyChar==(char)Keys.Delete))
            {
                return;
            }
            else
            {
                e.Handled = true;
                ToolTip tip = new ToolTip();
                tip.SetToolTip(passwordBox, "Enter digits and latin symbols only");
            }
        }

        //Передаче главной форме необходимости вызова формы регистрации
        private void regButton_Click(object sender, EventArgs e)
        {
            Result = FormResult.Register;
            this.Close();
        }

        private void loginBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '|')
                e.Handled = true;
        }
    }
}
