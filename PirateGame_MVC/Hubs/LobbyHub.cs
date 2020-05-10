﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PirateGame_MVC.GameLobby;
using Microsoft.AspNetCore.Mvc;

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

		public void JoinRoom(int roomId, string playerNickname)
		{
			var player = _gameLobby.GetPlayer(playerNickname);
			var room = _gameLobby.Rooms.FirstOrDefault(r => r.RoomId == roomId);

			room.AddPlayer(player);
			player.IsInRoom = true;
		}

		public void LeaveRoom(string playerNickname)
		{
			_gameLobby.LeaveRoom(_gameLobby.GetPlayer(playerNickname));
		}

		public async Task CreateRoom(string roomName, int maxPlayers, string playerNickname)
		{
			Player player = _gameLobby.GetPlayer(playerNickname);
			Room room = _gameLobby.CreateRoom(roomName, maxPlayers, ref player);

			_gameLobby.AddRoom(room);
			player.IsInRoom = true;

			await Clients.Caller.SendAsync("GetRoomId", room.RoomId);
			await GetAvailableRooms();
		}

		public async Task GetAvailableRooms()
		{
			string rooms = JsonConvert.SerializeObject(_gameLobby.FindRooms(x => x.Players.Count() < x.MaxPlayers));
			await Clients.Caller.SendAsync("ReceiveRoomList", rooms);
		}
	}
}