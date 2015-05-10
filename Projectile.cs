using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcadeGamez_Featuring_Marko_and_Nikola
{
    public class Projectile
    {
        public int X;
        public int Y;
        public Direction direction { get; set; }

        public int parentWidth;
        public int parentHeight;

        public Boolean deleteMe;    // proveri

        public int Velocity;

        public Projectile(int x, int y, Direction dir, int width, int height, Difficulty dif, int arcadeVelocity)
        {
            X = x;
            Y = y;
            direction = dir;
            parentWidth = width;
            parentHeight = height;
            deleteMe = false;
            if (dif == Difficulty.EASY)
                Velocity = 4;
            else if (dif == Difficulty.NORMAL)
                Velocity = 6;
            else if (dif == Difficulty.HARD)
                Velocity = 9;
            else if (dif == Difficulty.IMPOSSIBLE)
                Velocity = 14;
            else
                Velocity = arcadeVelocity;
           
        }

        public void Draw(Pen pen, Graphics g)      
        {
            if (direction == Direction.TOP)
            {
                if (Y - 20 >= -10)
                {
                    Point B = new Point(X + 9, Y + 15);
                    Point C = new Point(X + 9, Y + 5);
                    g.DrawLine(pen, B, C);
                }
                else
                    deleteMe = true;
            }
            else if (direction == Direction.BOTTOM)
            {
                if (Y + 45 <= parentHeight - 55)
                {
                    Point B = new Point(X + 9, Y + 45);
                    Point C = new Point(X + 9, Y + 55);
                    g.DrawLine(pen, B, C);
                }
                else
                    deleteMe = true;
            }
            else if (direction == Direction.LEFT)
            {
                if (X - 10 >= 10)
                {
                    Point B = new Point(X, Y + 30);
                    Point C = new Point(X - 10, Y + 30);
                    g.DrawLine(pen, B, C);
                }
                else
                    deleteMe = true;
            }
            else if (direction == Direction.RIGHT)
            {
                if (X + 30 <= parentWidth - 30)
                {
                    Point B = new Point(X + 20, Y + 30);
                    Point C = new Point(X + 30, Y + 30);
                    g.DrawLine(pen, B, C);
                }
                else
                    deleteMe = true;
            }
        }


        public void Move(Direction dir)
        {
            if (dir == Direction.TOP)
            {
                Y -= Velocity;
            }
            else if (dir == Direction.BOTTOM)
            {
                Y += Velocity;
            }
            else if (dir == Direction.LEFT)
            {
                X -= Velocity;
            }
            else if (dir == Direction.RIGHT)
            {
                X += Velocity;
            }
        }



        



    }
}
