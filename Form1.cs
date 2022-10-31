using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Algoritms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }      

        //Статический метод, реализующий отрисовку 8-связной линии по алгоритму Брезенхема
        static public void Bresenham8Line(Graphics g, Color clr, int x0, int y0, int x1, int y1)
        {
            //Изменения координат
            int dx = (x1 > x0) ? (x1 - x0) : (x0 - x1);
            int dy = (y1 > y0) ? (y1 - y0) : (y0 - y1);
            //Направление приращения
            int sx = (x1 >= x0) ? (1) : (-1);
            int sy = (y1 >= y0) ? (1) : (-1);

            if (dy < dx)
            {
                int d = (dy << 1) - dx;
                int d1 = dy << 1;
                int d2 = (dy - dx) << 1;
                PutPixel(g, clr, x0, y0, 255);
                int x = x0 + sx;
                int y = y0;
                for (int i = 1; i <= dx; i++)
                {
                    if (d > 0)
                    {
                        d += d2;
                        y += sy;
                    }
                    else
                        d += d1;
                    PutPixel(g, clr, x, y, 255);
                    x++;
                }
            }
            else
            {
                int d = (dx << 1) - dy;
                int d1 = dx << 1;
                int d2 = (dx - dy) << 1;
                PutPixel(g, clr, x0, y0, 255);
                int x = x0;
                int y = y0 + sy;
                for (int i = 1; i <= dy; i++)
                {
                    if (d > 0)
                    {
                        d += d2;
                        x += sx;
                    }
                    else
                        d += d1;
                    PutPixel(g, clr, x, y, 255);
                    y++;
                }
            }
        }

        //Статический метод, реализующий отрисовку 4-связной линии по алгоритму Брезенхема
        public static void Bresenham4Line(Graphics g, Color clr, int x0, int y0, int x1, int y1)
        {
            int dx = x1 - x0;
            int dy = y1 - y0;
            int d = 0;
            int d1 = dy << 1;
            int d2 = -(dx << 1);
            PutPixel(g, clr, x0, y0, 255);
            int x = x0;
            int y = y0;

            for (int i = 1; i <= dx + dy; i++)
            {
                if (d > 0)
                {
                    d += d2;
                    y++;
                }
                else
                {
                    d += d1;
                    x++;
                }
                PutPixel(g, clr, x, y, 255);
            }
        }

        public 4Line ()
             {

             }
        //Статический метод, реализующий отрисовку окружности по алгоритму Брезенхема
        public static void BresenhamCircle(Graphics g, Color clr, int _x, int _y, int radius)
        {
            int x = 0, y = radius, gap = 0, delta = (2 - 2 * radius);
            while (y >= 0)
            {
                PutPixel(g, clr, _x + x, _y + y, 255);
                PutPixel(g, clr, _x + x, _y - y, 255);
                PutPixel(g, clr, _x - x, _y - y, 255);
                PutPixel(g, clr, _x - x, _y + y, 255);
                gap = 2 * (delta + y) - 1;
                if (delta < 0 && gap <= 0)
                {
                    x++;
                    delta += 2 * x + 1;
                    continue;
                }
                if (delta > 0 && gap > 0)
                {
                    y--;
                    delta -= 2 * y + 1;
                    continue;
                }
                x++;
                delta += 2 * (x - y);
                y--;
            }
        }

        //Метод, устанавливающий пиксел на форме с заданными цветом и прозрачностью
        private static void PutPixel(Graphics g, Color col, int x, int y, int alpha)
        {
            g.FillRectangle(new SolidBrush(Color.FromArgb(alpha, col)), x, y, 1, 1);
        }

        static int algoritmns = 0;

        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                algoritmns = 1;
                pictureBox1.Invalidate();
            }
            else if (radioButton2.Checked)
            {
                algoritmns = 2;
                pictureBox1.Invalidate();
            }
            else if (radioButton3.Checked)
            {
                algoritmns = 3;
                pictureBox1.Invalidate();
            }
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            pictureBox1.InitialImage = null;
            int x0 = Convert.ToInt32(textBox1.Text);
            int y0 = Convert.ToInt32(textBox2.Text);
            int x1 = Convert.ToInt32(textBox3.Text);
            int y1 = Convert.ToInt32(textBox4.Text);
            int r = Convert.ToInt32(textBox5.Text);
            if (algoritmns == 1)
            {
                // Отрисовка 4 - связной линии, построенной по алгоритму Брезенхема
                Bresenham4Line(
                        e.Graphics,
                        Color.Black,
                        x0, y0, x1, y1);
            }
            else if (algoritmns == 2)
            {
                // Отрисовка 8-связной линии, построенной по алгоритму Брезенхема
                Bresenham8Line(
                    e.Graphics,
                    Color.Black,
                    x0, y0, x1, y1);
            }
            else if (algoritmns == 3)
            {
                // Отрисовка окружности, построенной по алгоритму Брезенхема
                BresenhamCircle(
                    e.Graphics,
                    Color.Black,
                    x1, y1, r);
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            textBox1.ReadOnly = true;
            textBox2.ReadOnly = true;
            textBox3.ReadOnly = false;
            textBox4.ReadOnly = false;
            textBox5.ReadOnly = false;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            textBox1.ReadOnly = false;
            textBox2.ReadOnly = false;
            textBox3.ReadOnly = false;
            textBox4.ReadOnly = false;
            textBox5.ReadOnly = true;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            textBox1.ReadOnly = false;
            textBox2.ReadOnly = false;
            textBox3.ReadOnly = false;
            textBox4.ReadOnly = false;
            textBox5.ReadOnly = true;
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar <= 47 || e.KeyChar >= 58) && e.KeyChar != 8)
                e.Handled = true;
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar <= 47 || e.KeyChar >= 58) && e.KeyChar != 8)
                e.Handled = true;
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar <= 47 || e.KeyChar >= 58) && e.KeyChar != 8)
                e.Handled = true;
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar <= 47 || e.KeyChar >= 58) && e.KeyChar != 8)
                e.Handled = true;
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar <= 47 || e.KeyChar >= 58) && e.KeyChar != 8)
                e.Handled = true;
        }
    }
}
