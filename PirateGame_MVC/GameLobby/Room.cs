using Newtonsoft.Json;
using PirateGame_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PirateGame_MVC.GameLobby
{
	public class Room
	{
		public int RoomId { get; set; }
		public string RoomName { get; set; }
		public int MaxPlayers { get; set; }
		public List<Player> Players { get; set; }
		public Game Game { get; set; }

		public bool GameStarted { get; set; }

		public Room(string roomName, int maxPlayers, ref Player creator, int id)
		{
			RoomId = id;
			RoomName = roomName;
			MaxPlayers = maxPlayers;

			Players = new List<Player>
			{
				creator
			};

			GameStarted = false;
		}

		public void AddPlayer(Player player)
		{
			Players.Add(player);
		}

		public bool AllPlayersAreReady()
		{
			foreach (var player in Players)
			{
				if (!player.GameFieldIsSet)
				{
					return false;
				}
			}

			return true;
		}

		public bool AllPlayersJoinedGame()
		{
			bool result = true;

			foreach (var player in Players)
			{
				if (player.ConnectionId == null)
				{
					result = false;
				}
			}

			return result;
		}
	}
}