using System;
using System.Collections.Generic;
using System.Text;

namespace Stephen
{
    public class Board
    {
        public readonly int row;
        public readonly int column;
        public readonly int size;
        public int[] squares;
        public int[] players;
        public int n_in_row;
        //I will call the constructor for the board later in the program
        public Board(int row = 15, int column = 15, int n_in_row = 5)
        {
            this.row = row;
            this.column = column;
            this.size = row * column;
            movemaded = 0;
            this.squares = new int[size];
            this.n_in_row = n_in_row;
            //declare the number of players
            this.players = new[] { 1, 2 };
        }


        //Declare items on board 
        public List<int> emptySquares;
        public int movemaded;
        public int current_player;
        public int last_move;
        //Initiate a chess board 
        public virtual void init_board(int startPlayer = 0)
        {
            this.current_player = this.players[startPlayer];
            this.emptySquares = new List<int>();
            for (int i = 0; i < size; ++i)
            {
                //for each cell, append to the square
                emptySquares.Add(i);
            }
            this.squares = new int[size];
            this.last_move = -1;
        }

        //Get the location of each cell
        public int GetCell(int move)
        {
            return squares[move];
        }

        public int locate_move(int[] coordinator)
        {
            if (coordinator.Length != 2)
            {
                return -1;
            }
            var x = coordinator[0];
            var y = coordinator[1];
            var move = x * row + y;
            if (move >= size || move < 0)
            {
                return -1;
            }
            return move;
        }
        //get player for making future moves
        public int get_current_player()
        {
            return current_player;
        }
        //Function helps to remain the value of coordinator pairs: 
        public double[,,] current_state()
        {
            var squareState = new double[4, row, column];
            int count = 0;
            for (int i = 0; i < size; i++)
            {
                var p = squares[i];
                if (p != 0)
                {
                    count++;
                    var index = p == current_player ? 0 : 1;
                    squareState[index, i / row, i % column] = 1.0;
                }
            }
            squareState[2, last_move / row, last_move % column] = 1.0;
            if (count % 2 == 0)
            {
                for (int y = 0; y < column; y++)
                {
                    for (int x = 0; x < row; x++)
                    {
                        squareState[3, x, y] = 1.0;
                    }
                }
            }
            return squareState;
        }

        public virtual void perform_move(int move)
        {
            this.squares[move] = this.current_player;
            //When we place move, the empty square must be replace: 
            emptySquares.Remove(move);
            movemaded++;
            this.current_player = current_player == players[1] ? players[0] : players[1];
            //Embed the last move with moves
            this.last_move = move;

        }
        private bool verifyWinner(int start, int end, int procedure, int player)
        {
            int count = 0;
            for (int i = start; i <= end; i += procedure)
            {
                if (squares[i] == player)
                {
                    count++;
                    if (count >= n_in_row)
                    {
                        return true;
                    }
                }
                else
                {
                    count = 0;
                }
            }
            return false;
        }

        public Tuple<bool, int> is_winner()
        {
            if (movemaded < n_in_row + 2)
            {
                return Tuple.Create(false, -1);
            }
            return GetWinner();
        }
        public Tuple<bool, int> game_end()
        {
            var ter = is_winner();
            if (ter.Item1)
            {
                return Tuple.Create(true, ter.Item2);
            }
            if (movemaded == size)
            {
                return Tuple.Create(true, -1);
            }
            return Tuple.Create(false, -1);
        }

        protected virtual Tuple<bool, int> GetWinner()
        {
            var x = last_move % row;
            var y = last_move / row;
            var p = current_player == players[1] ? players[0] : players[1];
            if (verifyWinner(last_move - x, last_move + row - x - 1, 1, p))
            {
                return Tuple.Create(true, p);
            }
            if (verifyWinner(last_move - y * row, size - 1, row, p))
            {
                return Tuple.Create(true, p);
            }
            var a = Math.Min(y, x);
            var b = row - x - 1;
            if (verifyWinner(last_move - a * row - a, Math.Min(size - 1, last_move + b * row + b), row + 1, p))
            {
                return Tuple.Create(true, p);
            }
            a = Math.Min(y, row - x - 1);
            if (verifyWinner(last_move - a * row + a, Math.Min(size - 1, last_move + x * row - x), row - 1, p))
            {
                return Tuple.Create(true, p);
            }
            return Tuple.Create(false, -1);

        }
        public virtual Board DuplicateMoves()
        {
            var b = new Board(row, column, n_in_row)
            {
                movemaded = movemaded,
                current_player = current_player,
                last_move = last_move,
                emptySquares = new List<int>(),
            };
            foreach (var i in emptySquares)
            {
                b.emptySquares.Add(i);
            }
            for (int i = 0; i < size; i++)
            {
                b.squares[i] = squares[i];
            }
            return b;
        }
        public int PosCol(int position)
        {
            return position % row;
        }
        public int PosRow(int position)
        {
            return (position / row);
        }
        public int[] GetInterestingMoves(int range = 1)
        {
            List<int> moves = new List<int>();
            for (int i = 0; i < this.squares.Length; i++)
            {
                if (this.squares[i] != 0)
                {
                    if (this.PosRow(i) - 1 >= 0 && this.PosCol(i) - 1 >= 0 && !moves.Contains(i - 1 - this.row) && this.squares[i - 1 - this.row] == 0)
                    {
                        moves.Add(i - 1 - this.row);
                    }
                    if (this.PosRow(i) - 1 >= 0 && this.PosCol(i) + 1 < this.row && !moves.Contains(i + 1 - this.row) && this.squares[i + 1 - this.row] == 0)
                    {
                        moves.Add(i + 1 - this.row);
                    }
                    if (this.PosRow(i) + 1 < this.row && this.PosCol(i) - 1 >= 0 && !moves.Contains(i - 1 + this.row) && this.squares[i - 1 + this.row] == 0)
                    {
                        moves.Add(i - 1 + this.row);
                    }
                    if (this.PosRow(i) + 1 < this.row && this.PosCol(i) + 1 < this.row && !moves.Contains(i + 1 + this.row) && this.squares[i + 1 + this.row] == 0)
                    {
                        moves.Add(i + 1 + this.row);
                    }
                    if (this.PosRow(i) - 1 >= 0 && !moves.Contains(i - this.row) && this.squares[i - this.row] == 0)
                    {
                        moves.Add(i - this.row);
                    }
                    if (this.PosRow(i) + 1 < this.row && !moves.Contains(i + this.row) && this.squares[i + this.row] == 0)
                    {
                        moves.Add(i + this.row);
                    }
                    if (this.PosCol(i) - 1 >= 0 && !moves.Contains(i - 1) && this.squares[i - 1] == 0)
                    {
                        moves.Add(i - 1);
                    }
                    if (this.PosCol(i) + 1 < this.row && !moves.Contains(i + 1) && this.squares[i + 1] == 0)
                    {
                        moves.Add(i + 1);
                    }
                }
            }
            return moves.ToArray();
        }

        public virtual int GetXOLocation(string[] board)
        {
            int numRows = (int)Math.Sqrt(board.Length);

            int curRow = 0, curCol = 0;

            for (int i = 0; i < board.Length; i++)
            {
                if (board[i] == null)
                {
                    curRow = i / numRows;
                    curCol = i % numRows;
                    break;
                }
            }

            while (true)
            {
                Console.SetCursorPosition(curCol * 4 + 2, curRow * 4 + 3);
                var keyInfo = Console.ReadKey();
                Console.SetCursorPosition(curCol * 4 + 2, curRow * 4 + 3);
                Console.Write(board[curRow * numRows + curCol] ?? " ");

                switch (keyInfo.Key)
                {
                    case ConsoleKey.LeftArrow:
                        if (curCol > 0)
                            curCol--;
                        break;
                    case ConsoleKey.RightArrow:
                        if (curCol + 1 < numRows)
                            curCol++;
                        break;
                    case ConsoleKey.UpArrow:
                        if (curRow > 0)
                            curRow--;
                        break;
                    case ConsoleKey.DownArrow:
                        if (curRow + 1 < numRows)
                            curRow++;
                        break;
                    case ConsoleKey.Spacebar:
                    case ConsoleKey.Enter:
                        if (board[curRow * numRows + curCol] == null)
                            return curRow * numRows + curCol;
                        break;
                }
            }
        }


    }
}
