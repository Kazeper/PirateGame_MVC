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

		public Room CreateRoom(string roomName, int maxPlayers, ref Player creator)
		{
			return new Room(roomName, maxPlayers, ref creator, Rooms.Count + 1);
		}

		public void AddRoom(Room room)
		{
			Rooms.Add(room);
		}

		public List<Room> FindRooms(Predicate<Room> match)
		{
			List<Room> matchingRooms = new List<Room>();
			foreach (Room room in Rooms)
			{
				if (match(room))
				{
					matchingRooms.Add(room);
				}
			}

			return matchingRooms;
		}
	}
}