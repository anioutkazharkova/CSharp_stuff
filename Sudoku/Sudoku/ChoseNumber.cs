using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sudoku
{
    public partial class ChoseNumber : Form
    {
        int[] numbersToDisplay;

        public enum btnStatus {btnClicked=1,noClicked=2 };
        public static btnStatus status;
        int btnWidth = 30;
        int btnHeight = 30;
        int startX = 20;
        int startY = 20;
        public int btnCode;


        public ChoseNumber(int[] numbers)
        {
            numbersToDisplay = numbers;
            status = btnStatus.noClicked;
            btnCode = 0;
            Button[] btnNumbers = new Button[numbers.Length+1];
            Array.Sort(numbers);

            for (int i = 0; i < numbers.Length; i++)
            {
                if (i % 5 == 0 && i!=0)
                {
                    startY = startY + btnWidth + 10;
                    startX = 20;
                }
                btnNumbers[i] = new Button();
                btnNumbers[i].Width = btnWidth;
                btnNumbers[i].Height = btnHeight;
                btnNumbers[i].Location = new Point(startX, startY);
                btnNumbers[i].Text = numbers[i].ToString();
                btnNumbers[i].Click+=new EventHandler(ChoseNumber_Click);
                btnNumbers[i].DialogResult = DialogResult.OK;
                this.Controls.Add(btnNumbers[i]);
                startX = startX + btnWidth + 10;              
                
            }
            btnNumbers[numbers.Length] = new Button();
            btnNumbers[numbers.Length].Width = btnWidth;
            btnNumbers[numbers.Length].Height = btnHeight;
            btnNumbers[numbers.Length].Location = new Point(startX, startY);
            btnNumbers[numbers.Length].Text = "0";
            btnNumbers[numbers.Length].Click += new EventHandler(ChoseNumber_Click);
            btnNumbers[numbers.Length].DialogResult = DialogResult.OK;
            this.Controls.Add(btnNumbers[numbers.Length]);
            
            InitializeComponent();


        }

        private void ChoseNumber_Click(object sender, EventArgs e)
        {
            Button btn=(Button)sender;
            btnCode = int.Parse(btn.Text);
            status = btnStatus.btnClicked;

        }
    }
}
