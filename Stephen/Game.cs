using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;


namespace Stephen
{
    public class Game
    {
        public Board board;

        public Game(Board board)
        {
            this.board = board;
        }

        public void graphic(Board board, int player1, int player2)
        {
            var row = board.row;
            var height = board.column;

            Console.WriteLine($"Player {player1} with X");
            Console.WriteLine($"Player {player2} with O");
            Console.WriteLine();
            Console.Write("\t");
            for (int x = 0; x < row; x++)
            {
                Console.Write($"\t{x}");
            }
            Console.WriteLine("\r\n");
            for (int i = height - 1; i > -1; i -= 1)
            {

                Console.Write($"\t{i}");
                for (int j = 0; j < row; j++)
                {
                    var loc = i * row + j;
                    var p = board.GetCell(loc);
                    if (p == player1)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("\tX");
                        Console.ResetColor();

                    }
                    else if (p == player2)
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write("\tO");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.Write("\t[ ]");
                    }
                }
                Console.WriteLine("\r\n\r\n");
            }
        }

        //This function for PVP mode: 
        public int start_play(Player player1, Player player2, int startPlayer = 0, bool isShown = true)
        {
            if (startPlayer < 0 || startPlayer > 1)
            {
                throw new Exception("start_player should be either 0 (player1 first) or 1 (player2 first)");
            }
            board.init_board(startPlayer);
            var p1 = board.players[0];
            var p2 = board.players[1];
            player1.SetIndex_player(p1);
            player2.SetIndex_player(p2);
            var players = new Dictionary<int, Player> { { p1, player1 }, { p2, player2 } };
            if (isShown)
            {
                graphic(board, player1.player, player2.player);
            }
            int pScore = 0;
            while (true)
            {

                var currentPlayer = board.get_current_player();
                var playerInTurn = players[currentPlayer];
                var timer = new Stopwatch();
                timer.Start();
                var move = playerInTurn.make_move(board);
                timer.Stop();
                Console.WriteLine($"Total game point:{0}", ++pScore);
                board.perform_move(move.Item1);
                if (isShown)
                {
                    graphic(board, player1.player, player2.player);
                }
                var ew = board.game_end();
                if (ew.Item1)
                {
                    if (isShown)
                    {
                        if (ew.Item2 != -1)
                        {
                            Console.WriteLine($"Game end. Winner is {players[ew.Item2]}");
                        }
                        else
                        {
                            Console.WriteLine("Game end. Tie");
                        }
                    }
                    return ew.Item2;
                }
            }
        }    
    }       
}
