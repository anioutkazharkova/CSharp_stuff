using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Sudoku
{
    class GameField:PictureBox
    {
        Pen penBoard;
        Pen penGrid;
        Brush brushGrid;

        public List<Cell> Cells;

        public GameField()
        {
            this.BackColor = Color.White;
            brushGrid = new SolidBrush(Color.Black);

            penBoard = new Pen(brushGrid, 3);
            penGrid = new Pen(brushGrid, 2);
            //this.DoubleBuffered = true;           
            Cells = new List<Cell>();
        }

        

        
    }
}
