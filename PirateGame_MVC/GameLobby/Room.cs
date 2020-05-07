using Newtonsoft.Json;
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

		public Room(string roomName, int maxPlayers, ref Player creator, int id)
		{
			RoomId = id;
			RoomName = roomName;
			MaxPlayers = maxPlayers;

			Players = new List<Player>
			{
				creator
			};
		}

		public void AddPlayer(Player player)
		{
			Players.Add(player);
		}
	}
}