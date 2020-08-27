using System;
using System.Collections.Generic;
using System.Text;

namespace Stephen
{

    public abstract class Player
    {
        public int player { get; private set; }

        public void SetIndex_player(int p)
        {
            player = p;
        }

        public abstract Tuple<int, double[]> make_move(Board board, double temp = 0.001, bool returnProb = false);

        public abstract void switch_player();
    }
}

