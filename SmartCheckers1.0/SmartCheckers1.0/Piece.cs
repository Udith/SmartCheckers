using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartCheckers1._0
{
    class Piece
    {
        public Square sqr;
        public int color;   //-1:white 1:black
        public bool isKing;

        public Piece(Square sqr, int clr)
        {
            this.sqr = sqr;
            this.color = clr;
            this.isKing = false;
            sqr.piece = this;
        }
    }
}
