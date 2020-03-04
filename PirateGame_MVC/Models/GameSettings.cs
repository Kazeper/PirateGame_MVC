using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PirateGame_MVC.Models
{
	public static class GameSettings //TODO zastanowić się nad inną nazwą klasy
	{
		public static int Rows { get; } = 7;
		public static int Columns { get; } = 7;
		public static int MaxPlayers { get; } = 7;

		public static List<int> CreateAvailableFieldsList()
		{
			List<int> availableFields = new List<int>();

			for (int i = 0; i < GameSettings.Rows * GameSettings.Columns; i++)
			{
				availableFields.Add(i);
			}

			return availableFields;
		}
	}
}