using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartCheckers1._0
{
    class Square
    {
        public int x;
        public int y;
        public int color;   //0:light 1:dark
        public Piece piece;

        public Square(int x, int y, int clr)
        {
            this.x = x;
            this.y = y;
            this.color = clr;
            this.piece = null;
        }
    }
}
