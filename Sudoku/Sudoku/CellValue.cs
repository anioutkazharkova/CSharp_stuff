using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sudoku
{
    class CellValue
    {
        int row;
        int column;
        int value;

        public int Row { get { return row; }
          
        }

        public int Column
        {
            get { return column; }
            
        }

        public int Value
        {
            get { return value; }
            
        }

        public CellValue(int row, int column, int value)
        {
            this.row = row;
            this.column = column;
            this.value = value;
        }
    }
}
