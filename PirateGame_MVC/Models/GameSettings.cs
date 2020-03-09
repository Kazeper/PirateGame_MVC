using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PirateGame_MVC.Models
{
	public static class GameSettings //TODO zastanowić się nad inną nazwą klasy
	{
		public static int NumberOfRows { get; } = 7;
		public static int NumberOfColumns { get; } = 7;
		public static int MaxPlayers { get; } = 7;

		public static List<int> CreateAvailableFieldsList()
		{
			List<int> availableFields = new List<int>();

			for (int i = 0; i < GameSettings.NumberOfRows * GameSettings.NumberOfColumns; i++)
			{
				availableFields.Add(i);
			}

			return availableFields;
		}
	}
}