using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using PirateGame_MVC.Hubs;
using PirateGame_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PirateGame_MVC.Models
{
	public class Game
	{
		public List<Player> Players { get; set; }
		public List<int> AvailableFields { get; set; }
		public bool TargetHasBeenReceived { get; set; }
		public string TargetNickname { get; set; }

		private readonly Random random = new Random();
		private readonly IHubContext<GameHub, IGameHub> _gameHub;
		public int[] playerQueue;
		public int currentPlayer;
		public int drawField;

		public Game(List<Player> players, IHubContext<GameHub, IGameHub> gameHub)
		{
			Players = players;

			CreatePlayerQueue();
			FillPlayerQueue();
			AvailableFields = GameSettings.CreateAvailableFieldsList();
			currentPlayer = SetFirstPlayer();
			_gameHub = gameHub;
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
			if (++playerIndex % Players.Count == 0) playerIndex = 0;
			else playerIndex %= Players.Count;
		}

		private int SetFirstPlayer()
		{
			return random.Next(0, Players.Count);
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

		public async void ReadPlayersFields()
		{
			for (int i = 0; i < Players.Count; i++)
			{
				Player player = Players[playerQueue[i]];
				int playerField = player.GameField[drawField];
				bool isActionField = playerField <= GameSettings.ActionFieldLastIndex;

				if (isActionField)
				{
					await _gameHub.Clients.Client(player.ConnectionId).AskForTarget(playerField);
					WaitForResponse();
					//targetNickname = _gameHub.LoadTarget();
					if (TargetNickname.Length > 2)//TODO magic number ANT
					{
						await _gameHub.Clients.Client(player.ConnectionId).ReceiveNotification(player.ConnectionId, "targetNickname received" + TargetNickname);
					}
				}
				ClearActiveActionData();
				player.ExecutePassiveAction(drawField);
			}
		}

		private void ClearActiveActionData()
		{
			TargetNickname = "";
			TargetHasBeenReceived = false;
		}

		private void WaitForResponse()
		{
			while (true)
			{
				Thread.Sleep(1000);
				if (TargetHasBeenReceived) break;
			}
		}

		public void StartGame()
		{
			while (AvailableFields.Count > 0)
			{
				drawField = GetNextField();
				ReadPlayersFields();
				DeleteField(drawField);
			}
		}

		private void ExecuteActionField(string playerConnectionId, int playerField)
		{
			//string targetNickname = "";
			//_gameHub.AskForTarget(playerConnectionId, playerField);
			//string targetNickname = _gameHub.LoadTarget();
		}
	}
}