using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RectangleByClick
{
    public partial class Form1 : Form
    {
        Graphics g;
        List<Figure> rectangles_lst = new List<Figure>();
        int indexRect;
        public Form1()
        {
            InitializeComponent();
            initCanvas();
        }
        private void initCanvas()
        {
            this.Size = new Size(900, 900);
        }

        private int searchIndexCell(MouseEventArgs e) {
            int index = -1;            
            for (int i = 0; i < rectangles_lst.Count; i++) {
                if (rectangles_lst[i].rectangle.Contains(e.Location))
                {
                    return i;
                }
            }
            return index;
        }
        private void addCell(Point point, Brush brush)
        {
            Rectangle alarm = new Rectangle(0, 0, 135, 135);
            if (alarm.Contains(point))
            {
                return;
            }
            g = CreateGraphics();
            int height = 90;
            int width = 90;
            Rectangle rectCell = new Rectangle(
                point.X - (width/2), point.Y - (height/2)
                , width, height);
            Figure figure = new Figure(rectCell, brush);
            rectangles_lst.Add(figure);
            g.Clear(BackColor);
            printCells();
        }
        private void printCells() {
            foreach (var item in rectangles_lst)
            {
                g.FillRectangle(item.brush, item.rectangle);
            }
        }
        private bool searchOldCell( MouseEventArgs e) {
            bool res= true;
            foreach (var item in rectangles_lst) {
                int length = item.rectangle.Width;
                if (e.Location.X + item.rectangle.Width >= item.rectangle.Location.X + length / 2
                && e.Location.X - item.rectangle.Width <= item.rectangle.Location.X + length / 2
                && e.Location.Y + item.rectangle.Height >= item.rectangle.Location.Y + length / 2
                && e.Location.Y - item.rectangle.Height <= item.rectangle.Location.Y + length / 2)
                {
                    res= false;
                    break;
                }
                else
                {
                    res= true;
                }
            }
            return res;
        }
        private void Form1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right & searchIndexCell(e)>=0)
            {
                delCell(e);
            }
            else
                if (searchOldCell(e))
            {
                if (rectangles_lst.Count % 2 == 0)
                    addCell(e.Location, Brushes.DarkGreen);
                else
                    addCell(e.Location, Brushes.Red);
            }
            else
                MessageBox.Show("Область для квадрата занята");
        }
        private void delCell(MouseEventArgs e) {
            rectangles_lst.RemoveAt(searchIndexCell(e));
            g.Clear(BackColor);
            printCells();
        }
        
    }
}
