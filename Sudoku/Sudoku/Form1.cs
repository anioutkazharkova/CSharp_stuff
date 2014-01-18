using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;

namespace Sudoku
{
    public enum Diff { easy = 1, medium = 2, hard = 3 };
    

    public partial class Form1 : Form
    {
        SudokuGrid sudoku; //Generated sudoku game

        public int[,] baseValues;    //Problem situation     
        public int[,] userSolution;  //Array of user solution 

        List<Cell> boardCell; //The list of cells 

        Diff difficulty; //Difficulty level

        int wrongCells; //Number of wrong values
        int emptyCells; //Number of unfilled cells

        //Parameters of board
        int boardWidth=9;
        int boardHeight=9;

        bool gameIsStarted;

        System.Windows.Forms.Timer timer;

        public delegate void DrawDelegate(object sender, PaintEventArgs e);
        public delegate void FirstDraw(object sender, PaintEventArgs e, int[,] values);
        public delegate bool SolvingDelegate(int i, int j);

        private int[,] loadValues;
        private int[,] searchSolution;
        
        //public Thread timerThread;
        int timerCounter = 0;
        string Time = "";

        public Form1()
        {
            InitializeComponent();
           
            InitializeParameters();

        }

        private void InitializeParameters()
        {
            wrongCells = 0;
            emptyCells = 0;
            gameIsStarted = false;
            timer = new System.Windows.Forms.Timer();
            timer.Interval = 1000;
            timer.Tick += new EventHandler(timer_Tick);
            medRadioButton.Checked = true;
            easyRadioButton.Checked = false;
            hardRadioButton.Checked = false;
            difficulty = Diff.medium;
        }

        void timer_Tick(object sender, EventArgs e)
        {
            timerCounter += 1;
            int hours = 0;
            int min = 0;
            int sec = timerCounter;
            formateTime(out hours, out min, ref sec);
            string Hours = "";
            string Minutes = "";
            string Seconds = "";

            if (hours < 10)
                Hours = "0" + hours.ToString();
            else Hours = hours.ToString();

            if (min < 10)
                Minutes = "0" + min.ToString();
            else Minutes = min.ToString();

            if (sec < 10)
            {
                Seconds = "0" + sec.ToString();
            }
            else Seconds = sec.ToString();
            if (tabControl1.SelectedIndex == 0)
            {
                timerTextBox.Text = Hours + ":" + Minutes + ":" + Seconds;
            }
            else
            {
                Time = Hours + ":" + Minutes + ":" + Seconds;
            }
        }
        private void formateTime(out int hours, out int min, ref int sec)
        {
            hours = 0;
            min = 0;

            hours = sec / 3600;
            min = (sec - hours * 3600) / 60;
            sec = sec % 60;

        }
       

        //First drawing the Board
        public  void drawBoard(object sender, PaintEventArgs e, int[,] values)
        {
            Graphics gr = e.Graphics;
            int dif = 40;
            

            //Making the boarders
            Pen pen = new Pen(Color.Black, 5);
            gr.DrawRectangle(pen, 0, 0, gameField.Width, gameField.Height);

            //Drawing the grid
                for(int i=0;i<values.GetLength(0);i++)
                {
                    for(int j=0;j<values.GetLength(1);j++)
                    {
                        gr.DrawLine(new Pen(Color.Black,2), dif * (j + 1)+3,0,dif*(j+1)+3, gameField.Height);
                    }
                    gr.DrawLine(new Pen(Color.Black, 2),0,dif*(i+1)+3,gameField.Width,dif*(i+1)+3);
                }

            //Drawing cells in GameField
                for (int i = 0; i < values.GetLength(0); i++)
                {
                    for (int j = 0; j < values.GetLength(1); j++)
                    {
                        //Check if the Cell is empty
                        if (values[i, j] != 0)
                        {
                            //Creating new statCell
                            Cell cell = new Cell(CellType.statCell, values[i, j]);
                            gr.DrawImage(cell.cellImage, new Point(Cell.imageWidth * j+2, Cell.imageHeight * i+2));
                            cell._X = j;
                            cell._Y = i;
                            gameField.Cells.Add(cell);
                        }
                        else
                        {
                            //Creating new inputCell
                            Cell cell = new Cell(CellType.inputCell, 0);
                            gr.DrawImage(cell.cellImage, new Point(Cell.imageWidth * j+2, Cell.imageHeight * i+2));
                            cell._X = j;
                            cell._Y = i;
                            gameField.Cells.Add(cell);
                        }

                    }

                }

            //Making overlay with grid
                for (int i = 0; i < values.GetLength(0); i++)
                {
                    for (int j = 0; j < values.GetLength(1); j++)
                    {
                        if (j % 3 == 2)
                            pen = new Pen(Color.Black, 5);
                        else pen = new Pen(Color.Black, 2);

                            gr.DrawLine(pen, dif * (j + 1) + 3, 0, dif * (j + 1) + 3, gameField.Height);
                        
                    }
                    if (i % 3 == 2)
                        pen = new Pen(Color.Black, 5);
                    else pen = new Pen(Color.Black, 2);
                    gr.DrawLine(pen, 0, dif * (i + 1) + 3, gameField.Width, dif * (i + 1) + 3);
                }
            gr.Dispose();


        }
        

        private void gameField_MouseClick(object sender, MouseEventArgs e)
        {
            if (gameIsStarted)
            {
                
                if (e.Button==MouseButtons.Left)
                {
                    //Get cell's coordinates
                    int _x = e.X;
                    int _y = e.Y;

                    //Get the coordinates of the value in array
                    int x = (_x - 2) / 40;
                    int y = (_y - 2) / 40;

                    //Get the cell by it's coordinates
                    Cell goalCell = getCellByXY(x, y);

                    //if the Cell is statCell, do nothing
                    if (goalCell.isStatCell)
                    {
                        //MessageBox.Show(x.ToString() + " " + y.ToString());
                    }
                    //Else 
                    if (goalCell.isInputCell)
                    {
                        int[] numbers;
                        //Getting the numbers in other Cells in Section
                        getDataByXY(x, y, out numbers);

                        int value = -1;
                        ChoseNumber chose = new ChoseNumber(numbers);
                        //Showing to user dialog where he can choose the number
                        if (chose.ShowDialog() == DialogResult.OK)
                        {
                            value = chose.btnCode;
                            //MessageBox.Show(value.ToString());
                        }

                        //If user has chosed something, check if there no such value in Section-Row-Column and drawing new Cell
                        if (value > 0)
                        {
                            int[,] tempArray = userSolution;
                            tempArray[y, x] = value;
                            DrawDelegate draw = new DrawDelegate(redrawBoard);
                            IAsyncResult ar;

                            if (!checkSection(x, y, value, tempArray) && !checkColumn(x, y, value, tempArray) && !checkRow(x, y, value, tempArray))
                            {

                                int index = boardCell.IndexOf(goalCell);
                                Cell cell = new Cell(CellType.inputCell, value);
                                cell.isCellCorrect = true;
                                cell._X = x;
                                cell._Y = y;
                                boardCell.RemoveAt(index);
                                boardCell.Insert(index, cell);
                                userSolution[y, x] = value;
                                ar = draw.BeginInvoke(sender, new PaintEventArgs(gameField.CreateGraphics(), gameField.Bounds), null, null);
                                draw.EndInvoke(ar);
                                //redrawBoard(sender, new PaintEventArgs(gameField.CreateGraphics(), gameField.Bounds));


                            }
                            else
                            {
                                int index = boardCell.IndexOf(goalCell);
                                Cell cell = new Cell(CellType.inputCell, value);
                                cell._X = x;
                                cell._Y = y;
                                cell.isCellCorrect = false;
                                boardCell.RemoveAt(index);
                                boardCell.Insert(index, cell);
                                userSolution[y, x] = value;
                                ar = draw.BeginInvoke(sender, new PaintEventArgs(gameField.CreateGraphics(), gameField.Bounds), null, null);
                                draw.EndInvoke(ar);
                                //redrawBoard(sender, new PaintEventArgs(gameField.CreateGraphics(), gameField.Bounds));
                            }

                            emptyCells = getEmptyNumber();
                            wrongCells = getWrongNumber();

                            if (emptyCells == 0 && wrongCells == 0)
                            {
                                bool gameIsReady = checkTheGame();
                                if (gameIsReady)
                                {
                                    timer.Stop();
                                    MessageBox.Show("Поздравляем! Игра закончена");
                                }
                                else
                                    MessageBox.Show("Что-то пошло не так. Проверьте еще раз");
                            }

                        }
                        else if (value == 0)
                        {
                            userSolution[y, x] = 0;
                            int index = boardCell.IndexOf(goalCell);
                            Cell cell = new Cell(CellType.inputCell, value);
                            cell._X = x;
                            cell._Y = y;
                            cell.isCellCorrect = true;
                            boardCell.RemoveAt(index);
                            boardCell.Insert(index, cell);
                            DrawDelegate draw = new DrawDelegate(redrawBoard);
                            IAsyncResult ar;
                            ar = draw.BeginInvoke(sender, new PaintEventArgs(gameField.CreateGraphics(), gameField.Bounds), null, null);
                            draw.EndInvoke(ar);
                           
                        }
                    }
                }
               
            }
        }

       //Checking if solution is right
        private bool checkTheGame()
        {
            bool flag = true;
            for (int i = 0; i < boardWidth; i++)
            {
                for (int j = 0; j < boardHeight; j++)
                {
                    if (userSolution[i, j] == sudoku.grid[i, j])
                    {
                        flag = flag && true;
                    }
                    else
                    {
                        flag = flag && false;
                    }
                }
            }
            return flag;
        }
       

        //Checking the section if it contains value
        private bool checkSection(int x, int y,int value,int[,] tempArray)
        {
            //Getting section coordinates
            int sectHor = (x / 3) * 3;
            int sectVer = (y / 3) * 3;

            bool isAlready = false;

            for (int i = sectVer; i < sectVer + 3; i++)
            {
                for (int j = sectHor; j < sectHor + 3; j++)
                {
                    if (tempArray[i, j] == value && i!=y && j!=x)
                    {
                        isAlready = true;
                    }
                }
            }

            return isAlready;
        }

        //Checking row if it contains value
        private bool checkRow(int x,int y, int value,int[,] tempArray)
        {
              bool isAlready = false;
            
                for (int j = 0; j < tempArray.GetLength(1); j++)
                {
                    if (tempArray[y,j ] == value && j!=x)
                    {
                        isAlready = true;
                    }
                }            

            return isAlready;

        }

        //Checking column if it contains value
        private bool checkColumn(int x,int y, int value,int[,] tempArray)
        {

            bool isAlready = false;

            for (int i = 0; i < tempArray.GetLength(0); i++)
            {
                if (tempArray[i, x] == value && i!=y)
                {
                    isAlready = true;
                }
            }
            return isAlready;

        }

        //Getting the cell and it's parameters by (x,y) coordinates
        private Cell getCellByXY(int x,int y)
        {
            //List contains all cells of the GameField
            List<Cell> list = boardCell;
            Cell goalCell = null;
            foreach (Cell cell in list)
            {
                //Return cell if it is found
                if (cell._X == x && cell._Y == y)
                {
                    goalCell = cell;
                }
            }
            return goalCell;
        }

        //Getting cell data (neighbours names) by it's coordinates
        private void getDataByXY(int x, int y, out int[] numbers)
        {
            int value = 0;
            value = sudoku.grid[y,x];
            //MessageBox.Show(value.ToString());
            int sectHor = ((x) / 3)*3;
            int sectVert = ((y) / 3)*3;
            int k = 0;
            
            List<int> temp = new List<int>();

            for (int i = sectVert; i < sectVert + 3; i++)
            {
                for (int j = sectHor; j < sectHor+ 3; j++)
                {
                    if (baseValues[i,j] == 0)
                    {
                        temp.Add(sudoku.grid[i,j]);
                        k++;
                    }
                }
            }

            numbers = new int[k];
            for (int i = 0; i < k; i++)
            {
                numbers[i] = temp[i];
            }
        }

        private int getEmptyNumber()
        {
            int number = 0;
            foreach (Cell cell in boardCell)
            {
                int i = cell._Y;
                int j = cell._X;
                if (userSolution[i, j] == 0)
                    number += 1;
            }
            return  number;
        }
        private int getWrongNumber()
        {
            int number = 0;
            foreach (Cell cell in boardCell)
            {
                if (!cell.isCellCorrect)
                    number += 1;
            }

            return number;
        }

        //Refreshing the GameBoard after user's move
        public void redrawBoard(object sender, PaintEventArgs e)
        {
            Graphics gr = e.Graphics;
            int dif = 40;
            Pen pen;
            
            //Drawing cells in GameField
            foreach (Cell cell in boardCell)
            {
                if (cell.isStatCell)
                {
                    int j = cell._X;
                    int i = cell._Y;
                    //gr.DrawImage(cell.cellImage, new Point(Cell.imageWidth * j + 2, Cell.imageHeight * i + 2));

                }
                else if (cell.isInputCell)
                {
                    int j = cell._X;
                    int i = cell._Y;
                    if (userSolution[i, j] != 0)
                    {
                        Color color=Color.Black;
                        if (cell.isCellCorrect && cell.isInputCell)
                            color = Color.Gray;
                        else
                        {
                            if (!cell.isCellCorrect && cell.isInputCell)
                                color = Color.DarkRed;
                        }
                        cell.redrawCell(userSolution[i, j], color);
                        gr.DrawImage(cell.cellImage, new Point(Cell.imageWidth * j + 2, Cell.imageHeight * i + 2));
                    }
                    else
                    {
                        Color color = Color.GhostWhite;
                        cell.redrawCell(userSolution[i, j], color);
                        gr.DrawImage(cell.cellImage, new Point(Cell.imageWidth * j + 2, Cell.imageHeight * i + 2));
                    }
                }
            }

            pen = new Pen(Color.Black, 5);
            gr.DrawRectangle(pen, 0, 0, gameField.Width, gameField.Height);           

            //Making overlay with grid
            for (int i = 0; i < userSolution.GetLength(0); i++)
            {
                for (int j = 0; j < userSolution.GetLength(1); j++)
                {
                    if (j % 3 == 2)
                        pen = new Pen(Color.Black, 5);
                    else pen = new Pen(Color.Black, 2);

                    gr.DrawLine(pen, dif * (j + 1) + 3, 0, dif * (j + 1) + 3, gameField.Height);

                }
                if (i % 3 == 2)
                    pen = new Pen(Color.Black, 5);
                else pen = new Pen(Color.Black, 2);
                gr.DrawLine(pen, 0, dif * (i + 1) + 3, gameField.Width, dif * (i + 1) + 3);


            }
            //Making the boarders
            pen = new Pen(Color.Black, 5);
            gr.DrawRectangle(pen, 0, 0, gameField.Width, gameField.Height);
            gr.Dispose();


        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            sudoku = new SudokuGrid(difficulty);

            boardWidth = sudoku.Width;
            boardHeight = sudoku.Height;

            baseValues = new int[boardWidth, boardHeight];
            userSolution = new int[boardWidth, boardHeight];

            //Filling arrays with problem situation values
            for (int i = 0; i < boardWidth; i++)
            {
                for (int j = 0; j < boardHeight; j++)
                {
                    baseValues[i, j] = sudoku.problemSit[i, j];
                    userSolution[i, j] = sudoku.problemSit[i, j];
                }
            }
            
            //Drawing first values
            FirstDraw fd = new FirstDraw(drawBoard);
            IAsyncResult ar=fd.BeginInvoke(sender,new PaintEventArgs(gameField.CreateGraphics(), gameField.Bounds), baseValues,null,null);            
            fd.EndInvoke(ar);

            //Preparing list of values of cells
            boardCell = new List<Cell>();
            foreach (Cell cell in gameField.Cells)
                boardCell.Add(cell);

            gameIsStarted = true;

            timerCounter = 0;
            timer.Start();
            

        }

        //Selecting difficulty level
        private void easyRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (easyRadioButton.Checked)
            {
                difficulty = Diff.easy;
            }
        }

        private void medRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (medRadioButton.Checked)
            {
                difficulty = Diff.medium;
            }
        }

        private void hardRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (hardRadioButton.Checked)
            {
                difficulty = Diff.hard;
            }
        }

        //Cleaning the board
        private void clearButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < boardWidth; i++)
            {
                for (int j = 0; j < boardHeight; j++)
                {
                    userSolution[i, j] = baseValues[i, j];
                }
            }
            boardCell = null;
            
            FirstDraw fd = new FirstDraw(drawBoard);
            IAsyncResult ar = fd.BeginInvoke(sender, new PaintEventArgs(gameField.CreateGraphics(), gameField.Bounds), baseValues, null, null);
            
            fd.EndInvoke(ar);
            boardCell = new List<Cell>();
            foreach (Cell cell in gameField.Cells)
                boardCell.Add(cell);
        }

        //Initializing parameters for each of tabs
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
            {
                
                InitializeParameters();
            }
            if (tabControl1.SelectedIndex == 1)
            {
                if (timer != null)
                {
                    timer.Stop();
                    timerTextBox.Text = "";
                    gameIsStarted = false;
                }                
                timer = new System.Windows.Forms.Timer();
                timer.Interval = 1000;
                timer.Tick += new EventHandler(timer_Tick);
                solveButton.Enabled = false;
            }
        }

        //Saving generated Sudoku game in a file
        private void saveButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Sudoku file (*.sud)|*.sud";
            if (gameIsStarted)
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    using (FileStream file = new FileStream(dialog.FileName, FileMode.OpenOrCreate))
                    {
                        using (StreamWriter sw = new StreamWriter(file))
                        {
                            for (int i = 0; i < boardWidth; i++)
                            {
                                for (int j = 0; j < boardHeight; j++)
                                {
                                    sw.Write(baseValues[i, j] + " ");
                                }
                                sw.WriteLine();
                            }
                        }
                    }
                }
            }
        }

        //Loading saved Sudoku game
        private void loadButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Sudoku file (*.sud)|*.sud";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                //Reading data from source file
                using (FileStream file = new FileStream(dialog.FileName, FileMode.Open))
                {
                    try
                    {
                        using (StreamReader sr = new StreamReader(file))
                        {
                            try
                            {
                                loadValues=new int [boardWidth,boardHeight];
                                searchSolution = new int[boardWidth, boardHeight];
                                int i = 0;
                                while (!sr.EndOfStream)
                                {
                                    string temp = sr.ReadLine();
                                    
                                    string[] tmp = temp.Split(' ');

                                    //Filling work array
                                    for (int j = 0; j < boardHeight; j++)
                                    {
                                        loadValues[i, j] = int.Parse(tmp[j]);
                                        searchSolution[i, j] = int.Parse(tmp[j]);
                                    }
                                    i++;
                                }
                               
                                //Drawing the board
                                FirstDraw fd = new FirstDraw(drawBoard);
                                IAsyncResult ar = fd.BeginInvoke(sender, new PaintEventArgs(sudokuField.CreateGraphics(), sudokuField.Bounds), loadValues, null, null);
                                fd.EndInvoke(ar);
                                solveButton.Enabled = true;
                                

                            }
                            catch (IOException ex)
                            {
                                MessageBox.Show("Ошибка чтения!");
                            }
                        }
                    }
                    catch (FileNotFoundException ex)
                    {
                        MessageBox.Show("Указанный файл не найден");
                    }
                }
            }
        }

        //Checking if the Board is full
        bool isComplete(int[,] temp)
        {
            bool flag = true;
            for (int i = 0; i < boardWidth; i++)
            {
                for (int j = 0; j < boardHeight; j++)
                {
                    if (temp[i, j] == 0)
                    {
                        flag = flag && false;
                    }
                    
                }
            }
            return flag;
        }

        //Need to progressBar
        private int getNullsNumber(int[,] array)
        {
            int number = 0;
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    if (array[i, j] == 0)
                    {
                        number += 1;
                    }
                }
            }

            return number;
        }

        //Function to solve Sudoku Task
        private void solveSudoku(object sender)
        {
            //Creating delegate to launch processing single values
            SolvingDelegate solver = new SolvingDelegate(findingSingleValue);
            IAsyncResult res;

            labelProgress.Text = "Прогресс решения:";

            //Prepatring progressBar
            int steps = getNullsNumber(searchSolution);
            solvingProgressBar.Value = 0;
            solvingProgressBar.Maximum = steps; //The number of steps is equal to number of unknown values
            solvingProgressBar.Step = 1;

            //Looking from 4 corners-point
                for (int i = 0; i < boardWidth; i++)
                {
                    for (int j = 0; j < boardHeight; j++)
                    {
                        res = solver.BeginInvoke(i, j, null, null);
                        bool flag = solver.EndInvoke(res);
                        //If the value was found, reseting parameters
                        if (flag)
                        {
                            solvingProgressBar.Value += 1;
                            System.Threading.Thread.Sleep(200);
                            i = 0; j = 0;
                        }
                    }
                }
                for (int i =boardWidth-1; i >=0; i--)
                {
                    for (int j = boardHeight-1; j >=0; j--)
                    {
                        res = solver.BeginInvoke(i, j, null, null);
                        bool flag = solver.EndInvoke(res);
                        //If the value was found, reseting parameters
                        if (flag)
                        {
                            solvingProgressBar.Value += 1;
                            System.Threading.Thread.Sleep(200);
                            i = 0; j = 0;
                        }
                    }
                }
                for (int i = boardWidth - 1; i >= 0; i--)
                {
                    for (int j = 0; j < boardHeight; j++)
                    {
                        res = solver.BeginInvoke(i, j, null, null);
                        bool flag = solver.EndInvoke(res);
                        //If the value was found, reseting parameters
                        if (flag)
                        {
                            solvingProgressBar.Value += 1;
                            System.Threading.Thread.Sleep(200);
                            i = 0; j = 0;
                        }
                    }
                }
                for (int i = 0; i <boardWidth; i++)
                {
                    for (int j =boardHeight-1; j >=0; j--)
                    {
                        res = solver.BeginInvoke(i, j, null, null);
                        bool flag = solver.EndInvoke(res);
                        if (flag)
                        {
                            solvingProgressBar.Value += 1;
                            System.Threading.Thread.Sleep(200);
                            i = 0; j = 0;
                        }
                    }
                }

                timer.Stop();
            
            if (isComplete(searchSolution))
            {
                labelProgress.Text = "Прогресс решения:";
                MessageBox.Show("Решение найдено");
                

            }
            else MessageBox.Show("Решение не найдено. Выберите другую задачу");
            solvingProgressBar.Value = 0;

            //Drawing the board
            FirstDraw fd = new FirstDraw(drawBoard);
            IAsyncResult ar = fd.BeginInvoke(sender, new PaintEventArgs(sudokuField.CreateGraphics(), sudokuField.Bounds), searchSolution, null, null);
            fd.EndInvoke(ar);
            
        }


        //Processing every single value
        private bool findingSingleValue(int i, int j)
        {
            List<CellValue> posValues = new List<CellValue>();

            if (searchSolution[i, j] == 0)
            {
                //Check all possible values
                for (int value = 1; value <= 9; value++)
                {
                    //Checking that the value is unique in it's row, column and section
                    if (!checkRow(j, i, value, searchSolution))
                        if (!checkColumn(j, i, value, searchSolution))
                            if (!checkSection(j, i, value, searchSolution))
                            {
                                //Adding at the list of possible values
                                posValues.Add(new CellValue(i, j, value));
                                
                            }
                }

            }
            //If it is a only one solution for this cell, putting it at place
            if (posValues.Count == 1)
            {
                int row = posValues[0].Row;
                int column = posValues[0].Column;
                int value = posValues[0].Value;

                searchSolution[row, column] = value;
                return true;

            }

            return false;
        }
        

        private void solveButton_Click(object sender, EventArgs e)
        {
            //Launching Sudoku solver
            solveSudoku(sender);
            timer.Start();
            
            
        }



        
    }
}
