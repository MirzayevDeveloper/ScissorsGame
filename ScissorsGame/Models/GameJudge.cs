using ScissorsGame.Enums;

namespace ScissorsGame.Models
{
	internal class GameJudge
	{
		private int _numberOfMoves;

		public GameJudge(int numberOfMoves)
		{
			_numberOfMoves = numberOfMoves;
		}

		public Result DecideWinner(int firstMove, int secondMove)
		{
			if (firstMove == secondMove) return Result.DRAW;

			int difference = (secondMove - firstMove + _numberOfMoves) % _numberOfMoves;

			return (difference) switch
			{
				1 => Result.WIN,
				2 => Result.WIN,
				_ => Result.LOSE
			};
		}
	}
}
