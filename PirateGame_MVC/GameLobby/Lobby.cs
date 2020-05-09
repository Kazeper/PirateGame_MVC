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
			return new Room(roomName, maxPlayers, ref creator, Rooms.Max(r => r.RoomId) + 1);
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

		public Player GetPlayer(string playerNickname)
		{
			return Players.Find(p => p.Nickname.Equals(playerNickname));
		}

		public void LeaveRoom(Player player)
		{
			Room room = Rooms.Find(r => r.Players.Exists(p => p.Nickname.Equals(player.Nickname)));

			if (room != null)
			{
				room.Players.Remove(player);
				player.IsInRoom = false;

				if (room.Players.Count == 0)
				{
					RemoveRoom(room);
				}
			}
		}

		private void RemoveRoom(Room room)
		{
			Rooms.Remove(room);
		}
	}
}