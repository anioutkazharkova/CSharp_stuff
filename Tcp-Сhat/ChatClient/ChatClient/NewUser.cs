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
    //Форма регистрации нового пользователя
    public partial class NewUser : Form
    {
        //Результат нажатия кнопки
        public FormResult Result;

        string login; //логин
        string password;//пароль

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

        public NewUser()
        {
            InitializeComponent();

            //Значения по умолчанию
            login = "";
            password = "";

            //Кнопка по умолчанию неактивна
            registerButton.Enabled = false;
        }

        //Отправка регистрационных данных
        private void registerButton_Click(object sender, EventArgs e)
        {
            //Если длина пароля больше 8, то параметры передаются главной форме
            if (passwordBox.Text.Length >= 8)
            {
                password = passwordBox.Text;
                login = loginBox.Text;
                Result = FormResult.Register;
            }

        }

        private void loginBox_TextChanged(object sender, EventArgs e)
        {
            //Если поля логина и пароля не пустые, то кнопка становится активной
            if (loginBox.Text != "" && passwordBox.Text != "")
            {
                registerButton.Enabled = true;
            }
            else
            {
                registerButton.Enabled = false;
            }

        }

        private void passwordBox_TextChanged(object sender, EventArgs e)
        {

            if (passwordBox.Text != "")
            {
                //Если поля для логина и пароля не пустые, то кнопка становится активной
                if (loginBox.Text != "")
                {
                    registerButton.Enabled = true;
                }

               //Пароль не может быть короче 8 символов
                if (passwordBox.Text.Length < 8)
                {
                    //Вывод информации для пользователя
                    pascheckLabel.Text = "Password length can't be less than 8 symbols";
                }
                else
                {
                    
                    pascheckLabel.Text = "Password has an appropriate length";                    
                }
            }
            else //Иначе кнопка неактивна
            {
                registerButton.Enabled = false;
            }
        }

        //Проверка символов, вводимых в поле
        private void passwordBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Допустимы только цифры и латинские символы
            if (Char.IsDigit(e.KeyChar) || (e.KeyChar >= 'A' && e.KeyChar <= 'Z') || (e.KeyChar >= 'a' && e.KeyChar <= 'z') || (e.KeyChar == (char)Keys.Back) || (e.KeyChar == (char)Keys.Delete))
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

        //Отправка данных об отмене регистрации
        private void cancelButton_Click(object sender, EventArgs e)
        {
            Result = FormResult.Cancel;
        }

        private void loginBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '|')
                e.Handled = true;
        }
    }
}
