using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcadeGamez_Featuring_Marko_and_Nikola
{
    public class MetalBlock : Block
    {

        public MetalBlock(int x, int y, int width, int height) : base(x, y, width, height) { }

        public override BlockType Type()
        {
            return BlockType.METAL;
        }


        
    }
}
