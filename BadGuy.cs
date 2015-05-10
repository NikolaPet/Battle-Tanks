using ArcadeGamez_Featuring_Marko_and_Nikola.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcadeGamez_Featuring_Marko_and_Nikola
{
    public class BadGuy
    {
        public int X { get; set; }
        public int Y { get; set; }

        public int Velocity { get; set; }

        public Direction direction;

        public Image ImgBottom = Resources.tankBottomPurple;
        public Image ImgTop = Resources.tankTopPurple;
        public Image ImgLeft = Resources.tankLeftPurple;
        public Image ImgRight = Resources.tankRightPurple;

        public Image ImgTank;

        public int parentWidth;
        public int parentHeight;

        public List<Block> blocks;

        public BadGuy() // da se koristi SAMO pri instanciranje lista od bad guys
        {
        }

        public BadGuy(int x, int y, Direction direction, int width, int height, Difficulty dif, List<Block> list, int arcadeVelocity)
        {
            X = x;
            Y = y;
            this.direction = direction;
            parentWidth = width;
            parentHeight = height;
            if (dif == Difficulty.EASY)
                Velocity = 2;
            else if (dif == Difficulty.NORMAL)
                Velocity = 3;
            else if (dif == Difficulty.HARD)
                Velocity = 4;
            else if (dif == Difficulty.IMPOSSIBLE)
                Velocity = 6;
            else if (dif == Difficulty.ARCADE)
                Velocity = arcadeVelocity;

         

            

            blocks = new List<Block>();
            foreach (Block block in list)
                blocks.Add(block);
        }

        public void Draw(Graphics g)
        {
            if (direction == Direction.BOTTOM)
                ImgTank = ImgBottom;
            else if (direction == Direction.TOP)
                ImgTank = ImgTop;
            else if (direction == Direction.LEFT)
                ImgTank = ImgLeft;
            else if (direction == Direction.RIGHT)
                ImgTank = ImgRight;
            g.DrawImageUnscaled(ImgTank, X, Y, 10, 10);
                
        }

        public Boolean checkBlockBounds(Direction dir)
        {
            Boolean canMove = true;

            if (dir == Direction.RIGHT)
            {
                foreach (Block block in blocks)
                {
                    if (block.Type() == BlockType.METAL)
                    {
                        if(block.isWithinArea(X + Velocity, Y))
                        {
                            canMove = false;
                            break;
                        }
                    }
                }
            }
            else if (dir == Direction.LEFT)
            {
                foreach (Block block in blocks)
                {
                    if (block.Type() == BlockType.METAL)
                    {
                        if (block.isWithinArea(X - Velocity, Y))
                        {
                            canMove = false;
                            break;
                        }
                    }
                }
            }
            else if (dir == Direction.TOP)
            {
                foreach (Block block in blocks)
                {
                    if (block.Type() == BlockType.METAL)
                    {
                        if (block.isWithinArea(X, Y - Velocity))
                        {
                            canMove = false;
                            break;
                        }
                    }
                }
            }
            else
            {
                foreach (Block block in blocks)
                {
                    if (block.Type() == BlockType.METAL)
                    {
                        if (block.isWithinArea(X, Y + Velocity))
                        {
                            canMove = false;
                            break;
                        }
                    }
                }
            }

            return canMove;
        }

        public void Move(Direction dir)         // se dvizi na klik na bilo koe kopce
        {
            Boolean canMove = true;
            if (dir == Direction.RIGHT)
            {
                if (X + Velocity <= parentWidth - 75)
                {
                    foreach (Block block in blocks)
                    {
                        if (block.Type() == BlockType.METAL)
                        {
                            if (block.isWithinArea(X + Velocity, Y))
                            {
                                canMove = false;
                                break;
                            }
                        }
                    }
                    if(canMove)
                        X += Velocity;
                }
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

        public Boolean checkCustomBounds(Direction dir, int n)
        {
            /*if (dir == Direction.RIGHT)
            {
                if (X + (n*Velocity) <= parentWidth - 75)
                    return true;
                return false;
            }

            else if (dir == Direction.LEFT)
            {
                if (X - (n*Velocity) >= 15)
                    return true;
                return false;
            }

            else if (dir == Direction.TOP)
            {
                if (Y - (n*Velocity) >= 12)
                    return true;
                return false;
            }

            else
            {
                if (Y + (n*Velocity) <= parentHeight - 90)
                    return true;
                return false;
            }*/
            if (X + (n * Velocity) <= parentWidth - 75 || X - (n * Velocity) >= 15 || Y - (n * Velocity) >= 12 || Y + (n * Velocity) <= parentHeight - 90)
                return true;
            return false;
        }

        public Boolean checkBounds(Direction dir)
        {
            if (dir == Direction.RIGHT)
            {
                if (X + Velocity <= parentWidth - 75)
                    return true;
                return false;
            }

            else if (dir == Direction.LEFT)
            {
                if (X - Velocity >= 15)
                    return true;
                return false;
            }

            else if (dir == Direction.TOP)
            {
                if (Y - Velocity >= 12)
                    return true;
                return false;
            }

            else
            {
                if (Y + Velocity <= parentHeight - 90)
                    return true;
                return false;
            }
        }

        public Boolean isWithinArea(int x, int y)
        {
            return Math.Abs(X + 10 - x) <= (ImgTank.Width+10) / 2 && Math.Abs(Y - 10 - y) <= ImgTank.Height / 2;
        }



       

        
    }
}
