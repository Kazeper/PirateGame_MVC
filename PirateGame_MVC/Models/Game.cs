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
		public int[] playerQueue;
		public int currentPlayer;
		public int drawField;

		public Game(List<Player> players)
		{
			this.Players = players;

			CreatePlayerQueue();
			FillPlayerQueue();
			AvailableFields = GameSettings.CreateAvailableFieldsList();
			currentPlayer = SetFirstPlayer();
		}

		private void CreatePlayerQueue()
		{
			playerQueue = new int[Players.Count];
		}

		private void FillPlayerQueue()
		{
			int playerIndex = SetFirstPlayer();

			for (int i = 0; i < playerQueue.Length; i++)
			{
				playerQueue[i] = playerIndex;

				UpdatePlayerIndex(ref playerIndex);
			}
		}

		private void UpdatePlayerIndex(ref int playerIndex)
		{
			playerIndex = (playerIndex % Players.Count) + 1;
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
			for (int i = 0; i < Players.Count; i++)
			{
				Players[playerQueue[i]].ExecuteAction(drawField);
			}
		}

		public void StartGame()
		{
			while (AvailableFields.Count > 0)
			{
				drawField = GetNextField();

				ReadPlayersFields();
			}
		}
	}
}