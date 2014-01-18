//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//namespace Sudoku
//{
//    class SudokuSolver
//    {
//        int[,] baseValues; //Store the base values of Sudoku game
//        public int[,] solvedValues;
//        //We need an array of possible values for every empty cell
//        //That's why we create new type

//        //class tempCellValue
//        //{
//        //    int row;
//        //    int column;

//        //    int val;

//        //   public List<int> possibleValues;
//        //   public int Row { get { return row; } }
//        //   public int Column { get { return column; } }
//        //   public int Value { get { return val; } set { val = value; } }

//        //    public tempCellValue(int row, int column, int value)
//        //    {
//        //        this.row = row;
//        //        this.column = column;
//        //        this.val = value;
//        //        this.possibleValues = new List<int>();
//        //    }

//        //}

//        int[,] tempSolutions;

//        int width = 9;
//        int height = 9;
//        public  delegate void SolverDelegate();
//        public delegate bool CheckingDelegate(int row, int column);

//        public SudokuSolver(int[,] values)
//        {
//            baseValues = new int[width, height];
//            tempSolutions = new int[width, height];

//            for (int i = 0; i < width; i++)
//            {
//                for (int j = 0; j < height; j++)
//                {
//                    baseValues[i, j] = values[i, j];
//                    tempSolutions[i, j] = values[i, j];

//                }
//            }
            
//        }

//        public void SolveSudoku()
//        {
//            SolverDelegate sd = new SolverDelegate(SolveSudokuTask);
//            IAsyncResult res = sd.BeginInvoke(null, null);
            
//            sd.EndInvoke(res);
//        }

//        private void SolveSudokuTask()
//        {
//            CheckingDelegate checkDel = new CheckingDelegate(getAllCellCandidates);
//            IAsyncResult res;
//            int count = 0;
//            //do
//            //{
//                for (int row = 0; row < width; row++)
//                {
//                    for (int column = 0; column < height; column++)
//                    {
                        
//                            res = checkDel.BeginInvoke(row, column,null,null);
//                          bool flag=  checkDel.EndInvoke(res);
//                            //getAllCellCandidates(row, column);
//                          if (flag)
//                          {
//                              row = 0;
//                              column = 0;
//                          }
                            
                       
//                    }
//                }

//                for (int row = width-1; row >=0; row--)
//                {
//                    for (int column = height-1; column >=0; column--)
//                    {

//                        res = checkDel.BeginInvoke(row, column, null, null);
//                        bool flag = checkDel.EndInvoke(res);
//                        //getAllCellCandidates(row, column);
//                        if (flag)
//                        {
//                            row = 0;
//                            column = 0;
//                        }


//                    }
//                }

//            //    count++;
//            //    //if (count > 81)
//            //    //{
//            //    //    break;
//            //    //}
                

//            //} while (!isComplete);

//            solvedValues=new int [width,height];
//            for (int i = 0; i < width; i++)
//            {
//                for (int j = 0; j < height; j++)
//                {
//                    solvedValues[i, j] = tempSolutions[i, j];
//                }
//            }


//        }

//        private bool getAllCellCandidates(int row, int column)
//        {
//            if (tempSolutions[row, column] == 0)
//            {
//                List<int> possibleValues = new List<int>();


//                int[,] temp = new int[width, height];
//                for (int i = 0; i < width; i++)
//                {
//                    for (int j = 0; j < height; j++)
//                    {
//                        temp[i, j] = tempSolutions[i, j];
//                    }
//                }
//                for (int value = 1; value <= 9; value++)
//                {

//                    if (!checkValueInRow(row, column, value, temp) && !checkValueInColumn(row, column, value, temp) && !checkValueInSection(row, column, value, temp))
//                    {
//                        possibleValues.Add(value);
//                    }
//                }
//                if (possibleValues.Count == 1)
//                {
//                    tempSolutions[row, column] = possibleValues[0];
//                    return true;
//                }
                 
//            }
//            return false;
//        }

//        private bool checkValueInRow(int row, int column, int value, int[,] temp)
//        {
//            bool flag = false;
//            for (int i = 0; i < width; i++)
//            {
//                if (temp[row, i] == value && i != column)
//                {
//                    flag = flag && true;
//                }
//            }
//            return flag;
//        }

//        private bool checkValueInColumn(int row, int column, int value, int[,] temp)
//        {

//            bool flag = false;
//            for (int i = 0; i < width; i++)
//            {
//                if (temp[i,column] == value && i != row)
//                {
//                    flag = flag && true;
//                }
//            }
//            return flag;
//        }

//        private bool checkValueInSection(int row, int column, int value, int[,] temp)
//        {
//            bool flag = false;
//            int secRow = (row / 3) * 3;
//            int secColumn = (row / 3) * 3;

//            for (int i = 0; i < width; i++)
//            {
//                for (int j = 0; j < height; j++)
//                {
//                    if (i != row && j != column && temp[i,j]==value)
//                    {
//                        flag = flag && true;

//                    }
//                }

//            }            
//            return flag;
//        }

//        bool isComplete
//        {
//            get
//            {
//                int number = 0;
//                for (int i = 0; i < width; i++)
//                {
//                    for (int j = 0; j < height; j++)
//                    {
//                        if (tempSolutions[i, j] == 0)
//                            number += 1;
//                    }
//                }
//                if (number > 0)
//                    return false;
//                else return true;
//            }
//        }


//        int this[int i, int j]
//        {
//            get { return tempSolutions[i, j]; }
//            set { tempSolutions[i, j] = value; }
//        }

//    }
//}
