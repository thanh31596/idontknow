﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Stephen
{

    class Node
    {
        public int move;
        public List<int> untriedMoves;
        public double wins = 0;
        public int games = 0;
        public Node parent;
        public List<Node> nodes = new List<Node>();

        public Node(int move, Node parent, Board state, int searchRange = 1)
        {
            this.move = move;
            this.parent = parent;

        }

        public int GetRandomMove()
        {
            int next = ComputerLogics.rand.Next(untriedMoves.Count);
            int move = untriedMoves[next];
            untriedMoves.RemoveAt(next);
            return move;
        }

        public Node AddChild(int move, Board state)
        {
            Node child = new Node(move, this, state);
            nodes.Add(child);
            return child;
        }
    }
}
