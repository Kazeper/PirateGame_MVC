using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PirateGame_MVC.GameLobby;

namespace PirateGame_MVC.Hubs
{
	public class LobbyHub : Hub
	{
		private readonly Lobby _gameLobby;

		public LobbyHub(Lobby gameLobby)
		{
			_gameLobby = gameLobby;
		}

		public async Task SendMessage(string user, string message)
		{
			await Clients.All.SendAsync("ReceiveMessage", user, message);
		}

		public async Task CreateRoom(string roomName, int maxPlayers, string playerNickname)
		{
			Player player = _gameLobby.Players.Find(x => x.Nickname == playerNickname);
			Room room = _gameLobby.CreateRoom(roomName, maxPlayers, ref player);
			_gameLobby.AddRoom(room);

			await Clients.All.SendAsync("AddRoomToList", room.RoomId.ToString(), room.RoomName + " max players: " + room.MaxPlayers);
		}

		public async Task GetAvailableRooms()
		{
			string rooms = JsonConvert.SerializeObject(_gameLobby.FindRooms(x => x.Players.Count() < x.MaxPlayers));
			await Clients.Caller.SendAsync("ReceiveRoomList", rooms);
		}
	}
}