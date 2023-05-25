using System.Security.Cryptography;
using ScissorsGame.Enums;
using ScissorsGame.Models;

//Rock Paper Scissors Game

namespace ScissorsGame
{
	internal class Program
	{
		private static SecurityService _security;

		static bool ValidateArgs(string[] args)
		{
			if (args.Length < 3 || args.Length % 2 == 0)
			{
				Console.WriteLine("Invalid options: please pass an odd number of moves (3 or more).");
				return false;
			}

			if (args.Length != args.Distinct().Count())
			{
				Console.WriteLine("Invalid options: all moves must be distinct.");
				return false;
			}

			return true;
		}

		static void PrintMenu(string[] moves)
		{
			Console.WriteLine("Available Moves:");

			for (int i = 0; i < moves.Length; i++)
			{
				Console.WriteLine($"{i + 1} - {moves[i]}");
			}

			Console.WriteLine("0 - Exit");
			Console.WriteLine("? - Help");
		}

		static void Main(string[] args)
		{
			if (!ValidateArgs(args)) return;

			 _security = new SecurityService();

			var table = new MoveTable(args);
			var gameJudge = new GameJudge(args.Length);
			bool isActive = true;

			while (isActive)
			{
				string key = _security.GenerateKey();
				int computerMove = RandomNumberGenerator.GetInt32(args.Length);
				string hmac = _security.GenerateHMAC(key, args[computerMove]);

				Console.WriteLine("HMAC: " + hmac);

				PrintMenu(args);

				Console.Write("Enter your move: ");
				string input = Console.ReadLine()?.Trim() ?? string.Empty;

				if (input == "?")
				{
					table.Print();
					Console.Write("\n\n\n");
					continue;
				}
				else if (input == "0") isActive = false;

				if (!int.TryParse(input, out var playerMove) || playerMove <= 0 || playerMove > args.Length)
				{
					Console.Write("\n\n\n");
					continue;
				}

				Console.WriteLine($"Your move: {args[playerMove - 1]}\n" +
								  $"Computer move: {args[computerMove]}");

				Result result = gameJudge.DecideWinner(computerMove, playerMove - 1);

				string answer = result switch
				{
					Result.WIN => "You won!",
					Result.LOSE => "You lost!",
					_ => "Draw!"
				};

				Console.Write($"{answer}\nHMAC key: {key}\n\n" +
								$"Press enter to continue...");

				Console.ReadKey();
				Console.Clear();
			}
		}
	}

}
