using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Sudoku
{
    public enum CellType {statCell=1,inputCell=2 };
    
    class Cell
    {
        CellType type;
        public CellType Type { get { return type; }
            set { type = value; }
        }
        public Image cellImage
        {
            get { return image; }
            set{image=value;}

        }
        public int _X
        {
            get;
            set;
        }
        public int _Y
        {
            get;
            set;
        }

        bool correctCell;

        public bool isCellCorrect
        {
            set { correctCell = value; }
            get { return correctCell; }
        }

        public bool isStatCell
        {
            get {
                if (Type==CellType.statCell)
                {
                    return true;
                }
                else return false;
            }
        }

        public bool isInputCell
        {
            get
            {
                if (Type == CellType.inputCell)
                {
                    return true;
                }
                else return false;
            }
        }

        private Image image;
        public static int imageWidth=40;
        public static int imageHeight=40;

        public Cell(CellType type,int value)
        {
            this.type = type;

            image = new Bitmap(imageWidth, imageHeight);
            Graphics graph = Graphics.FromImage(image);
            Pen pen = new Pen(Color.Black, 2);
            Brush brush = new SolidBrush(Color.GhostWhite);
            Rectangle rect = new Rectangle(0, 0, imageWidth, imageHeight);

            graph.DrawRectangle(new Pen(Color.WhiteSmoke, 1), rect);
            graph.FillRectangle(brush, rect);
            isCellCorrect = true;


            if (type== CellType.statCell)
                    {
                        Font tahoma = new Font("Tahoma", 20, FontStyle.Bold);
                        graph.DrawString(value.ToString(), tahoma, new SolidBrush(Color.Black), new PointF(5, 5));
                        //isCellCorrect = true;
                    }
                 graph.Dispose();     

        }

        public void redrawCell(int value,Color color)
        {
            Graphics graph = Graphics.FromImage(this.cellImage);
            Font tahoma = new Font("Tahoma", 20, FontStyle.Bold);
            Brush brush;
             brush = new SolidBrush(color);
            
            graph.DrawString(value.ToString(), tahoma, brush, new PointF(5, 5));
            
            graph.Dispose();
        }

        public void cleanCell(Color color)
        {
            Graphics graph = Graphics.FromImage(this.cellImage);
            Brush brush = new SolidBrush(Color.GhostWhite);
            Pen pen = new Pen(brush);
            Rectangle rect = new Rectangle(0, 0, image.Width, image.Height);
            graph.DrawRectangle(pen,rect);
            graph.FillRectangle(brush, rect);

            graph.Dispose();
        }

    }
}
