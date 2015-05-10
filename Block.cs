using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcadeGamez_Featuring_Marko_and_Nikola
{
    public enum BlockType
    {
        METAL,
        GRASS,
        ICE
    }

    public abstract class Block
    {
        public int X;
        public int Y;
        public int width;
        public int height;

        public Block(int x, int y, int width, int height)
        {
            X = x;
            Y = y;
            this.width = width;
            this.height = height;
        }

        public Boolean isWithinArea(int x, int y)
        {
            return (Math.Abs(X - x) <= width / 2) && Math.Abs(Y + 2 - y) <= height / 2; // + 10 bea
        }

        public abstract BlockType Type();

    }
}
