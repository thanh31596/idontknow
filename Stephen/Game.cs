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



        //This function for PVP mode: 
        public int start_play(Player player1, Player player2, int initialUser = 0, bool isShown = true)
        {
            if (initialUser < 0 || initialUser > 1)
            {
                throw new Exception("start_player should be either 0 (player1 first) or 1 (player2 first)");
            }
            board.SetupBoard(initialUser);
            var p1 = board.players[0];
            var p2 = board.players[1];
            player1.SetIndex_player(p1);
            player2.SetIndex_player(p2);
            var players = new Dictionary<int, Player> { { p1, player1 }, { p2, player2 } };
            if (isShown)
            {
                graphic(board, player1.player, player2.player);
            }
            while (true)
            {
                var currentPlayer = board.get_current_player();
                var playerInTurn = players[currentPlayer];
                var timer = new Stopwatch();
                timer.Start();
                var move = playerInTurn.make_move(board);
                timer.Stop();
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
                            Console.ReadKey();
                        }
                        else
                        {
                            Console.WriteLine("Game end. Tie");
                            Console.ReadKey();
                        }
                    }
                    return ew.Item2;
                }
            }
        }
        public void graphic(Board board, int player1, int player2)
        {
            var row = board.row;
            var height = board.column;
            int scorex = 0;
            int scoreo = 0;
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
                        scorex++;

                    }
                    else if (p == player2)
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write("\tO");
                        Console.ResetColor();
                        scoreo++;
                    }
                    else
                    {
                        Console.Write("\t[ ]");
                    }
                }
                Console.WriteLine("\r\n\r\n");
                
            }
            Console.WriteLine("scoreX: " + scorex);
            Console.WriteLine("scoreO: " + scoreo);
        }
    }       
}
