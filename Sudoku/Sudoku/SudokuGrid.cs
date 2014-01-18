using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;

namespace Sudoku
{
    
    class SudokuGrid
    {
        public int[,] grid;
        int width=9;
        int height=9;

       public int[,] problemSit;

        int[,] pattern;

        public int Width { get { return width; } }

        public int Height { get { return height; } }

        private delegate void AsyncDelegate();

        Diff difficult;

        int hintNumber;
        int emptyNumber;
        int totalNumber = 81;

        int minNumber;
        int maxNumber;

        public Diff Difficulty
        {
            get { return difficult; }
            set { difficult = value; }
        }


        public SudokuGrid(Diff difficult)
        {
            grid = new int[width, height];
            
            AsyncDelegate ad = new AsyncDelegate(createBaseGrid);
            IAsyncResult ar = ad.BeginInvoke(null,null);
            ad.EndInvoke(ar);
            ad = new AsyncDelegate(transformGrid);
            ar = ad.BeginInvoke(null, null);
            ad.EndInvoke(ar);
            Random rand = new Random();

            Difficulty = difficult;
            switch (Difficulty)
            {
                //Difficulty level defines the number of possible empty cells in every Row, Column or Section
                case Diff.easy:
                    {
                        hintNumber = rand.Next(45,56);
                        minNumber = 1; //Number of non-empty cells
                        maxNumber = 4; //Number of nulls
                    }
                    break;
                case Diff.medium:
                    {
                        hintNumber = rand.Next(36, 46);
                        minNumber = 2;
                        maxNumber = 5;
                    }
                    break;
                case Diff.hard:
                    {
                        hintNumber = rand.Next(30, 36);
                        minNumber = 4;
                        maxNumber = 8;
                    }
                    break;
            }
            emptyNumber = totalNumber - hintNumber;
            
            createProblemSit();
        }

        

        private void createBaseGrid()
        {

            //Making first Row
            int[] tempArray = new int[width];
            for (int i = 0; i < width; i++)
            {
                tempArray[i] = i + 1;
                grid[0, i] = tempArray[i];
            }

            int mainShift = 0;
            int simShift = 0;

            //Making other Rows with shifting
            for (int i = 1; i < width; i++)
            {
                //Every Rownumber div to one third
                if (i % 3 == 0)
                {
                    mainShift += 1;
                   int[] temp= shiftArray(tempArray, mainShift);
                    for (int j = 0; j < width; j++)
                    {
                        grid[i, j] = temp[j];
                    }
                    simShift = mainShift;
                }
                else
                {
                    simShift += 3;
                    //simShift += mainShift;
                    simShift = simShift % width;
                    int[] temp = shiftArray(tempArray, simShift);
                    for (int j = 0; j < height; j++)
                    {
                        grid[i, j] = temp[j];
                    }
                }
            }
            //transformGrid();
            
        }

        private int[] shiftArray(int[] array, int shift)
        {
            int[] temp = new int[array.Length];
            for (int i = 0; i < array.Length - shift; i++)
            {
                temp[i] = array[i + shift];
            }
            for (int i = 0, j = array.Length - shift; i < shift; i++)
            {
                temp[j + i] = array[i];
            }

            return temp;
        }

        private void transformGrid()
        {
            transponGrid();
            shiftColumn();
            shiftColumn();
            shiftSectionColumn();
            shiftRows();
            shiftRows();
            shiftSectionRow();
            shiftColumn();
            
        }


        private void transponGrid()
        {
            int[,] temp = new int[width, height];

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    temp[i, j] = grid[j, i];
                }
            }

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    grid[i, j] = temp[i, j];
                }
            }
        }

        private void shiftRows()
        {

            Random rand = new Random();
            int sectNumber = rand.Next(1, 4);
            int rowStep = rand.Next(1, 3);

            int rowNumber1 = (sectNumber-1) * 3;
            int rowNumber2 = rowNumber1 + rowStep;

            int[,] temp = new int[width, height];
            int[] row1 = new int[width];
            int[] row2 = new int[width];

            for (int i = 0; i < width; i++)
            {
                row1[i] = grid[rowNumber1, i];
                row2[i] = grid[rowNumber2, i];
            }

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    temp[i, j] = grid[i, j];
                }
            }
            for (int i = 0; i < width; i++)
            {
                temp[rowNumber1, i] = row2[i];
                temp[rowNumber2, i] = row1[i];
            }

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    grid[i, j] = temp[i, j];
                }
            }
        }

        private void shiftColumn()
        {

            Random rand = new Random();
            int sectNumber = rand.Next(1, 4);
            int columnStep = rand.Next(1, 3);

            int columnNumber1 = (sectNumber - 1) * 3;
            int columnNumber2 = columnNumber1 + columnStep;

            int[,] temp = new int[width, height];
            int[] column1 = new int[width];
            int[] column2 = new int[width];

            for (int i = 0; i < width; i++)
            {
                column1[i] = grid[i,columnNumber1];
                column2[i] = grid[i,columnNumber2];
            }

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    temp[i, j] = grid[i, j];
                }
            }
            for (int i = 0; i < width; i++)
            {
                temp[i,columnNumber1] = column2[i];
                temp[i,columnNumber2] = column1[i];
            }

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    grid[i, j] = temp[i, j];
                }
            }
        }

        private void shiftSectionRow()
        {
            int[,] temp = new int[width, height];

            Random rand = new Random();
            int sectRow = rand.Next(1, 4);
            //int sectColumn = rand.Next(1, 4);

            int sectStep = 0;

            switch (sectRow)
            {
                case 1:
                    sectStep = rand.Next(1, 3);
                    break;
                case 2:
                    sectStep = rand.Next(-1, 2);
                    break;
                case 3:
                    sectStep = rand.Next(-2, 0);
                    break;
            }

            int sectRow2 = sectRow + sectStep;

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    temp[i, j] = grid[i, j];
                }

            }

            for (int i = 0, k = (sectRow - 1) * 3, k2=(sectRow2-1)*3; i < 3; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    temp[i + k, j] = grid[i + k2, j];
                    temp[i + k2, j] = grid[i + k, j];
                }
            }

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    grid[i, j] = temp[i, j];
                }
            }
        }

        private void shiftSectionColumn()
        {
            int[,] temp = new int[width, height];

            Random rand = new Random();
            int sectRow = rand.Next(1, 4);
            int sectColumn = rand.Next(1, 4);

            int sectStep = 0;

            switch (sectColumn)
            {
                case 1:
                    sectStep = rand.Next(1, 3);
                    break;
                case 2:
                    sectStep = rand.Next(-1, 2);
                    break;
                case 3:
                    sectStep = rand.Next(-2, 0);
                    break;
            }

            int sectColumn2 = sectColumn + sectStep;

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    temp[i, j] = grid[i, j];
                }

            }

            for (int i = 0, k = (sectColumn - 1) * 3, k2 = (sectColumn2 - 1) * 3; i < 3; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    temp[j,i+k] = grid[j,i + k2];
                    temp[j,i+k2] = grid[j,i + k];
                }
            }

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    grid[i, j] = temp[i, j];
                }
            }
        }

        private void createProblemSit()
        {
            problemSit = new int[width, height];
            pattern = new int[width, height];

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    problemSit[i, j] = grid[i, j];
                    pattern[i, j] = 0;
                }
            }

            AsyncDelegate ad = new AsyncDelegate(generatePattern);
            IAsyncResult ar = ad.BeginInvoke(null, null);
            ad.EndInvoke(ar);


            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    problemSit[i, j] = problemSit[i, j] * pattern[i, j];
                }
            }
            
        }

        private void generatePattern()
        {
            int counter = 0;
            Random rand=new Random();

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    pattern[i,j]=0;
                }
            }
            while (counter < hintNumber)
            {
                bool flag = false;
                while (!flag)
                {
                    int i = rand.Next(0, width);
                    int j = rand.Next(0, height);
                    if (pattern[i, j] == 0)
                    {
                        pattern[i, j] = 1;
                        flag = true;
                    }
                }
                counter += 1;
            }
        }      
        
    }
}
