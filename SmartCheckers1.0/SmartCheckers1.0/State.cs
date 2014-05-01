using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartCheckers1._0
{
    class State
    {
        public String[,] board;
        public List<Move> moves;

        public State(String[,] board, List<Move> moves)
        {
            this.board = board;
            this.moves = moves;
        }
    }
}
