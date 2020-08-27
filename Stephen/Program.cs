using System;
using System.IO;



namespace Stephen
{
	class Program
	{
		static void Main(string[] args)
		{			
			var stillPlaying = true;
			const int w = 15;
			var comp = new Computer();
			var str = new ComputerStraight();
			var fist = new HumanFirst();
			var sec = new HumanSecond();
			var game = new Game(new Board(w, w, 5));
			Console.ForegroundColor = ConsoleColor.DarkGreen;
			Console.WriteLine("-----------------------");
			Console.WriteLine("Vu Kim Thanh - Stephen Vu 10648771!");
			Console.WriteLine("-----------------------\n");
			Console.ResetColor();
			string url = "https://google.com";
			while (stillPlaying)
			{
				Console.ForegroundColor = ConsoleColor.DarkYellow;
				Console.WriteLine("Welcome to my game, please choose your game type:");
				Console.WriteLine("1. Start a new single-player game");
				Console.WriteLine("2. Start a new multiplayer game");
				Console.WriteLine("3. Start a new single-player straight game");
				Console.WriteLine("4. Open Saved Game");
				Console.WriteLine("5. Direct to an Online help system ");
				Console.WriteLine("6. Quit\n");
				Console.ResetColor();
				Console.Write("Choose your option please: ");

				var choice = GetUserInput("[123456]");

				switch (choice)
				{
					case "1":
						game.start_play(comp, fist, 0);
						Console.Clear();
						break;
					case "2":
						game.start_play(fist, sec, 0);
						break;
					case "3":
						game.start_play(fist, str, 0);
						
						break;
					case "4":
						Board board = Data.ReadFromBinaryFile<Board>("C:/Board.pfcsheet");
						break;
					case "5":
						Helper.OpenBrowser(url);
						break;
					case "6":
						stillPlaying = false;
						break;

				}
			}



		}
		public static string GetUserInput(string validPattern = null)
		{
			var input = Console.ReadLine();
			input = input.Trim();

			if (validPattern != null && !System.Text.RegularExpressions.Regex.IsMatch(input, validPattern))
			{
				Console.ForegroundColor = ConsoleColor.DarkRed;
				Console.WriteLine("\"" + input + "\" is not valid.\n");
				Console.ResetColor();
				return null;
			}

			return input;
		}

	}
}

