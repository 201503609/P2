using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OLC2_Proyecto2.Gramatica
{
    class Dibujar
    {

        public string nom = "FLor";
        public Draw drawForm = new Draw();

        public void dibujo(ArrayList figuras)
        {
            drawForm.Show();
            drawForm.Size = new Size(800, 800);
            drawForm.Text = nom;


            foreach (string hijo in figuras)
            {
                string phrase = hijo;
                string[] words = phrase.Split(',');
                if (words[0].Equals("circulo"))
                {
                    //--Color,Radio,Solido,PosX,PosY
                    //---1   , 2   ,3     , 4  ,5
                    bool flag = false;
                    if (words[3].Contains("verdadero"))
                        flag = true;
                    DrawCircle(Convert.ToInt32(words[2]), Convert.ToInt32(words[4]), Convert.ToInt32(words[5]), flag, words[1]);
                }
                else if (words[0].Equals("triangulo"))
                {
                    //--Color,Solido,PosX,PosY,PosX1,PosY1,PosX2,PosY2
                    //-- 1   , 2    ,3   ,4   ,5    ,6    ,7     ,8
                    bool flag = false;
                    if (words[2].Contains("verdadero"))
                        flag = true;
                    DrawTriangle(Convert.ToInt32(words[3]), Convert.ToInt32(words[4]), Convert.ToInt32(words[5]), Convert.ToInt32(words[6]), Convert.ToInt32(words[7]), Convert.ToInt32(words[8]), flag, words[1]);

                }
                else if (words[0].Equals("rectangulo"))
                {
                    //--Color,Solido,PosX,PosY,Alto,Ancho
                    //--1    ,2     ,3   ,4   ,5   ,6
                    bool flag = false;
                    if (words[2].Contains("verdadero"))
                        flag = true;
                    DrawSquare(Convert.ToInt32(words[3]), Convert.ToInt32(words[4]), Convert.ToInt32(words[6]), Convert.ToInt32(words[5]), flag, words[1]);

                }
                else if (words[0].Equals("linea"))
                {
                    //--Color,InicioX,InicioY,FinX,FinY,Grosor
                    //--1    ,2      ,3      ,4   ,5    ,6
                    DrawLine(Convert.ToInt32(words[2]), Convert.ToInt32(words[3]), Convert.ToInt32(words[4]), Convert.ToInt32(words[5]), Convert.ToInt32(words[6]),words[1]);
                }
            }


            DrawLine(0, 0, 200, 200, 0, "red");
            DrawLine(200, 0, 0, 200, 0, "red");

        }

        public void DrawCircle(int Radio, int PosX, int PosY, bool relleno, string color)
        {
            Graphics g = drawForm.CreateGraphics();
            Color _color = System.Drawing.ColorTranslator.FromHtml(color);
            if (relleno == false)
            {

                System.Drawing.Pen myPen = new System.Drawing.Pen(_color);
                int widthEllipse = 2 * Radio;
                int heightEllipse = 2 * Radio;
                g.DrawEllipse(myPen, new Rectangle(PosX - Radio, PosY - Radio, widthEllipse, heightEllipse));
                myPen.Dispose();
                g.Dispose();
            }
            else if (relleno == true)
            {

                SolidBrush redBrush = new SolidBrush(_color);
                int widthEllipse = 2 * Radio;
                int heightEllipse = 2 * Radio;
                g.FillEllipse(redBrush, PosX - Radio, PosY - Radio, widthEllipse, heightEllipse);
            }
        }


        private void DrawSquare(int PosX, int PosY, int Width, int Height, bool relleno, string color)
        {

            Graphics g = drawForm.CreateGraphics();
            Color _color = System.Drawing.ColorTranslator.FromHtml(color);
            if (relleno == false)
            {
                System.Drawing.Pen myPen = new System.Drawing.Pen(_color);
                int PosXRec = Width / 2;
                int PosYRec = Height / 2;
                g.DrawRectangle(myPen, new Rectangle(PosX - PosXRec, PosY - PosYRec, Width, Height));
                myPen.Dispose();
                g.Dispose();
            }
            else if (relleno == true)
            {
                SolidBrush redBrush = new SolidBrush(_color);
                int PosXRec = Width / 2;
                int PosYRec = Height / 2;
                RectangleF rect = new RectangleF(PosX - PosXRec, PosY - PosYRec, Width, Height);
                g.FillRectangle(redBrush, rect);
            }
        }

        private void DrawTriangle(int PosX1, int PosY1, int PosX2, int PosY2, int PosX3, int PosY3, bool relleno, string color)
        {

            Graphics g = drawForm.CreateGraphics();
            Color _color = System.Drawing.ColorTranslator.FromHtml(color);
            if (relleno == false)
            {
                Pen blackPen = new Pen(_color, 0);
                PointF point1 = new PointF(PosX1, PosY1);
                PointF point2 = new PointF(PosX2, PosY2);
                PointF point3 = new PointF(PosX3, PosY3);
                PointF[] curvePoints =
                {
                    point1,
                    point2,
                    point3
                };
                g.DrawPolygon(blackPen, curvePoints);
            }
            else if (relleno == true)
            {
                SolidBrush blueBrush = new SolidBrush(_color);
                PointF point1 = new PointF(PosX1, PosY1);
                PointF point2 = new PointF(PosX2, PosY2);
                PointF point3 = new PointF(PosX3, PosY3);
                PointF[] curvePoints =
                {
                    point1,
                    point2,
                    point3
                };
                g.FillPolygon(blueBrush, curvePoints);
            }
        }

        private void DrawLine(int PosX1, int PosY1, int PosX2, int PosY2, int Thickness, string color)
        {

            Graphics g = drawForm.CreateGraphics();
            Color _color = System.Drawing.ColorTranslator.FromHtml(color);
            Pen blackPen = new Pen(_color, Thickness);

            PointF point1 = new PointF(PosX1, PosY1);
            PointF point2 = new PointF(PosX2, PosY2);
            PointF[] curvePoints =
                     {
                 point1,
                 point2

             };
            g.DrawPolygon(blackPen, curvePoints);
        }
    }
}
