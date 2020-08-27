using System;
using System.Collections.Generic;
using System.Text;

namespace Stephen
{
    class ComputerLogics
    {
        public static Random rand = new Random();

        public int GetRandomMove(Board b, int searchRange = 1)
        {
            int[] moves = b.GetInterestingMoves(searchRange);
            if (moves.Length == 0 && b.squares[0] == 0)
            {
                return (b.row * b.row) / 2;
            }
            else if (moves.Length == 0)
            {
                return -1;
            }
            return moves[rand.Next(moves.Length)];
        }
        public virtual int GetAI(int[] move)
        {
            int i = 0;
            int p = 0;

            while (true)
            {
                move[i] = p;
                i++;
            }

        }
    }
}
