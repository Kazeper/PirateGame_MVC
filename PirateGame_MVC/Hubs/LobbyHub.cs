using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PirateGame_MVC.GameLobby;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.CodeAnalysis.Host;

namespace PirateGame_MVC.Hubs
{
	public class LobbyHub : Hub
	{
		private readonly Lobby _gameLobby;
		private readonly IHubContext<RoomHub> _roomHub;

		public LobbyHub(Lobby gameLobby, IHubContext<RoomHub> roomHub)
		{
			_gameLobby = gameLobby;
			_roomHub = roomHub;
		}

		public async Task SendMessage(string user, string message)
		{
			await Clients.All.SendAsync("ReceiveMessage", user, message);
		}

		public async Task JoinRoom(int roomId, string playerNickname)
		{
			var player = _gameLobby.GetPlayer(playerNickname);
			var room = _gameLobby.Rooms.FirstOrDefault(r => r.RoomId == roomId);

			//TODO verify is there enough space for new player
			room.AddPlayer(player);
			player.IsInRoom = true;

			string players = JsonConvert.SerializeObject(room.Players);

			await _roomHub.Clients.Group(roomId.ToString()).SendAsync("ReceivePlayers", players);
		}

		public void LeaveRoom(string playerNickname)
		{
			_gameLobby.LeaveRoom(_gameLobby.GetPlayer(playerNickname));
		}

		public async Task CreateRoom(string roomName, int maxPlayers, string playerNickname)
		{
			Player player = _gameLobby.GetPlayer(playerNickname);
			Room room;
			if (_gameLobby.CreateRoom(roomName, maxPlayers, ref player))
			{
				room = _gameLobby.Rooms.Find(r => r.Players.Contains(player));
				player.IsInRoom = true;
				await Clients.Caller.SendAsync("GoToCreatedRoom", room.RoomId);
				await GetAvailableRooms();
			}
			else
			{
				string message = "unable to create room.";
				await Clients.Caller.SendAsync("CreateRoomError", message);
			}
		}

		public async Task GetAvailableRooms()
		{
			string rooms = JsonConvert.SerializeObject(_gameLobby.FindRooms(x => x.Players.Count() < x.MaxPlayers));
			await Clients.All.SendAsync("ReceiveRoomList", rooms);
		}
	}
}