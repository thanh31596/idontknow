using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Stephen
{
    
    public class HumanFirst : Player
    {
        public override Tuple<int, double[]> make_move(Board board, double temp = 0.001, bool returnProb = false)
        {
            
            int move;
            Console.WriteLine("Input 0 if you want to save the state or input this format [a,b] to plays");
            
            

                    try
            {
                Console.Write("Your move: ");
                var loc = Console.ReadLine();
                switch (loc)
                {
                    case "0":
                        Console.WriteLine("Your File Is Saved!!!!");
                        Data.WriteToBinaryFile<Board>("C:/Board.pfcsheet", board);
                        break;
                    
                }

                var ss = loc.Split(',');
                var location = new[] { int.Parse(ss[0]), int.Parse(ss[1]) };
                move = board.locate_move(location);


            }
            catch (Exception)
            {
                move = -1;
            }
            if (move == -1 || !board.emptySquares.Contains(move))
            {
                Console.WriteLine("Wrong Input, please try again using [a,b] index!");
                move = make_move(board).Item1;
            }
            return Tuple.Create<int, double[]>(move, null);
        }

        public override void switch_player()
        {
           
        }

        public override string ToString()
        {
            return $"Human {player} 1";
        }

    }
    public class HumanSecond : Player
    {
        public override Tuple<int, double[]> make_move(Board board, double temp = 0.001, bool returnProb = false)
        {
            int move;
            
            
            try
            {
                Console.Write("Your move: ");
                var loc = Console.ReadLine();
                switch (loc)
                {
                    case "0":
                        Console.WriteLine("Your File Is Saved!!!!");
                        Data.WriteToBinaryFile<Board>("C:/Board.pfcsheet", board);
                        break;

                }
                var ss = loc.Split(',');
                var location = new[] { int.Parse(ss[0]), int.Parse(ss[1]) };
                move = board.locate_move(location);
            }
            catch (Exception)
            {
                move = -1;
            }
            if (move == -1 || !board.emptySquares.Contains(move))
            {
                Console.WriteLine("invalid move");
                move = make_move(board).Item1;
            }
            return Tuple.Create<int, double[]>(move, null);
        }

        public override void switch_player()
        {
            
        }

        public override string ToString()
        {
            return $"Human {player} 2 ";
        }
    }
        public class Computer : Player
        {
            static Random rnd = new Random();
            public override Tuple<int, double[]> make_move(Board board, double temp = 0.001, bool returnProb = false)
            {
                int move;
                try
                {
                    int row = rnd.Next(14);
                    int col = rnd.Next(14);

                    var loc = Tuple.Create(row, col);
                    var ss = loc;
                    var location = new[] { loc.Item1, loc.Item2 };
                    move = board.locate_move(location);
                }
                catch (Exception)
                {
                    move = -1;
                }
                if (move == -1 || !board.emptySquares.Contains(move))
                {
                    move = make_move(board).Item1;
                }
                return Tuple.Create<int, double[]>(move, null);
            }

            public override void switch_player()
            {
            }

            public override string ToString()
            {
                return $"Computer {player} ";
            }
        }
    public class ComputerStraight : Player
    {
        static Random rnd = new Random();
        public override Tuple<int, double[]> make_move(Board board, double temp = 0.001, bool returnProb = false)
        {
            int move;
            try
            {
                int row = rnd.Next(7);
                int column = rnd.Next(7);

                var loc = Tuple.Create(row, column);
                var ss = loc;
                var location = new[] { loc.Item1, loc.Item2 };
                move = board.straight_move(location);
            }
            catch (Exception)
            {
                move = -1;
            }
            if (move == -1 || !board.emptySquares.Contains(move))
            {
                move = make_move(board).Item1;
            }
            return Tuple.Create<int, double[]>(move, null);
        }

        public override void switch_player()
        {
        }

        public override string ToString()
        {
            return $"ComputerStraight {player} ";
        }
    }

}
