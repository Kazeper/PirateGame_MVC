using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PirateGame_MVC.GameLobby
{
	public class Lobby
	{
		public List<Player> Players { get; set; }

		public List<Room> Rooms { get; set; }

		public Lobby()
		{
			Players = new List<Player>();
			Rooms = new List<Room>();
		}
	}
}