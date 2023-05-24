using ConsoleTables;

namespace ScissorsGame.Models
{
	internal class MoveTable
	{
		private readonly string[] _moveNames;
		private readonly GameJudge _gameJudge;

		public MoveTable(string[] moveNames)
		{
			_moveNames = moveNames;
			_gameJudge = new GameJudge(_moveNames.Length);
		}

		public void Print()
		{
			var headerItems = _moveNames.Prepend("PC \\ User");
			var table = new ConsoleTable(headerItems.ToArray());

			for (int i = 0; i < _moveNames.Length; i++)
			{
				var currentRow = new string[_moveNames.Length + 1];
				currentRow[0] = _moveNames[i];

				for (int j = 0; j < _moveNames.Length; j++)
				{
					currentRow[j + 1] = _gameJudge.DecideWinner(i, j).ToString();
				}

				table.AddRow(currentRow.ToArray());
			}

			table.Write(Format.Alternative);
		}
	}
}
