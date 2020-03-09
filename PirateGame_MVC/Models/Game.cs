using PirateGame_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PirateBayMVC.Models
{
	public class Game
	{
		public List<Player> Players { get; set; }
		public List<int> AvailableFields { get; set; }
		private Random random = new Random();
		public int currentPlayer;

		public Game()
		{
			AvailableFields = GameSettings.CreateAvailableFieldsList();
			currentPlayer = SetFirstPlayer();
		}

		public int GetNextField()
		{
			int nextFieldIndex = random.Next(0, AvailableFields.Count);

			return AvailableFields[nextFieldIndex];
		}

		public string ReadFieldAsString(int field)
		{
			int x = (int)Math.Floor(((double)(field / GameSettings.NumberOfColumns)));
			int y = (field % GameSettings.NumberOfColumns) + 1;

			return (char)(x + 65) + y.ToString();
		}

		private void DeleteField(int field)
		{
			AvailableFields.Remove(field);
		}

		private int SetFirstPlayer()
		{
			return random.Next(0, Players.Count);
		}

		public void ReadPlayersFields()
		{
			throw new NotImplementedException();
		}
	}
}