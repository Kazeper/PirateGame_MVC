using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using PirateGame_MVC.GameLobby;
using PirateGame_MVC.Hubs;
using PirateGame_MVC.Models;

namespace PirateGame_MVC.Controllers
{
	public class LobbyController : Controller
	{
		private readonly IHttpContextAccessor _accessor;
		private readonly Lobby _gameLobby;
		private readonly IHubContext<RoomHub> _roomHub;
		private readonly string _playerNickname;

		public LobbyController(Lobby gameLobby, IHttpContextAccessor accessor, IHubContext<RoomHub> roomHub)
		{
			_accessor = accessor;
			_gameLobby = gameLobby;
			_roomHub = roomHub;
			_playerNickname = _accessor.HttpContext.Session.GetString("playerNickname");
		}

		public IActionResult Index()
		{
			if (String.IsNullOrEmpty(_playerNickname))
			{
				return RedirectToAction("Index", "Home");
			}

			Player player = _gameLobby.GetPlayer(_playerNickname);
			if (player.IsInRoom)
			{
				_gameLobby.LeaveRoom(player);
			}

			return View();
		}

		[HttpPost]
		public IActionResult Index(SelectedRoomViewModel selectedRoom)
		{
			TempData["roomId"] = selectedRoom.RoomId;

			return RedirectToAction("Room");
		}

		public IActionResult Room()
		{
			if (TempData["roomId"] == null)
			{
				var room = _gameLobby.Rooms.Find(r => r.Players.Exists(p => p.Nickname.Equals(_playerNickname)));

				if (room == null) return RedirectToAction("Index");
				else return View(GetGameViewModel(room.RoomId));
			}

			int id = int.Parse(TempData["roomId"].ToString());
			TempData.Remove("roomId");

			return View(GetGameViewModel(id));
		}

		private GameRoomViewModel GetGameViewModel(int roomId)
		{
			GameRoomViewModel gameRoom = new GameRoomViewModel
			{
				RoomId = roomId,
				Room = _gameLobby.Rooms.FirstOrDefault(x => x.RoomId == roomId),
				Player = _gameLobby.GetPlayer(_playerNickname)
			};

			gameRoom.GameField = gameRoom.Player.GameField;

			return gameRoom;
		}

		public IActionResult LeaveRoom()
		{
			return RedirectToAction("Index");
		}

		[HttpPost]
		public IActionResult Room(GameRoomViewModel gameRoom)
		{
			var room = _gameLobby.Rooms.Find(r => r.RoomId == gameRoom.RoomId);

			if (ModelState.IsValid && room.AllPlayersAreReady())
			{
				var player = _gameLobby.GetPlayer(_playerNickname);
				player.SetGameField(gameRoom.GameField);

				gameRoom.Player = player;
				gameRoom.Room = room;
				gameRoom.Room.Game = gameRoom.Room.Game ?? new Game(gameRoom.Room.Players);
				TempData["gameRoom"] = JsonConvert.SerializeObject(gameRoom);

				return RedirectToAction("Index", "Game");
			}

			return View(GetGameViewModel(gameRoom.RoomId));
		}
	}
}