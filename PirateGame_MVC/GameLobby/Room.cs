using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PirateGame_MVC.GameLobby
{
	public class Room
	{
		public List<Player> Players { get; set; }

		public Room()
		{
			Players = new List<Player>();
		}
	}
}