using ArcadeGamez_Featuring_Marko_and_Nikola.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcadeGamez_Featuring_Marko_and_Nikola
{
    public enum Direction
    {
        LEFT,
        RIGHT,
        TOP,
        BOTTOM,
        NONE
    }

    public class GoodGuy
    {
        public int X { get; set; }
        public int Y { get; set; }

        public int Velocity { get; set; }

        public Direction direction;

        public Image ImgTank = Resources.tankBottom;

        public int parentWidth;
        public int parentHeight;

        public int keepVelocity;

        public List<Block> blocks;

        public GoodGuy(int x, int y, Direction direction, int width, int height, List<Block> list)
        {
            X = x;
            Y = y;
            this.direction = direction;
            Velocity = 3;
            parentWidth = width;
            parentHeight = height;
            blocks = new List<Block>();
            foreach (Block block in list)
                blocks.Add(block);
            
        }

        public void Draw(Graphics g)
        {
            g.DrawImageUnscaled(ImgTank, X, Y, 10, 10);
        }

        public void Move(Direction dir)         // se dvizi na klik na bilo koe kopce
        {
            if (dir == Direction.RIGHT)
            {
                if (X + Velocity <= parentWidth - 75) 
                    X += Velocity;
                ImgTank = Resources.tankRight;
                direction = dir;

            }
            else if (dir == Direction.LEFT)
            {
                if (X - Velocity >= 15)
                    X -= Velocity;
                ImgTank = Resources.tankLeft;
                direction = dir;

            }
            else if (dir == Direction.TOP)
            {
                if (Y - Velocity >= 12)
                    Y -= Velocity;
                ImgTank = Resources.tankTop;
                direction = dir;

            }
            else if (dir == Direction.BOTTOM)
            {
                if (Y + Velocity <= parentHeight - 90)
                    Y += Velocity;
                ImgTank = Resources.tankBottom;
                direction = dir;

            }
     
        }

        public Boolean isWithinArea(int x, int y)
        {
            return Math.Abs(X - x) <= (ImgTank.Width) / 2 && Math.Abs(Y - y) <= ImgTank.Height / 2;
        }

        public void increaseVelocity()
        {
            Velocity *= 3;
        }

        public void revertVelocityToNormal()
        {
            Velocity = 3;
        }

       


    }
}
