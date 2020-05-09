using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using PirateGame_MVC.GameLobby;
using PirateGame_MVC.Hubs;
using PirateGame_MVC.Models;

namespace PirateGame_MVC.Controllers
{
	public class LobbyController : Controller
	{
		private readonly IHttpContextAccessor _accessor;
		private readonly Lobby _gameLobby;
		private string _playerNickname;

		public LobbyController(Lobby gameLobby, IHttpContextAccessor accessor)
		{
			_accessor = accessor;
			_gameLobby = gameLobby;

			_playerNickname = _accessor.HttpContext.Session.GetString("playerNickname");
		}

		public IActionResult Index()
		{
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
				else return View(GetSelectedRoomViewModel(room.RoomId));
			}

			int id = int.Parse(TempData["roomId"].ToString());

			return View(GetSelectedRoomViewModel(id));
		}

		private SelectedRoomViewModel GetSelectedRoomViewModel(int id)
		{
			SelectedRoomViewModel selectedRoom = new SelectedRoomViewModel
			{
				RoomId = id,
				Room = _gameLobby.Rooms.FirstOrDefault(x => x.RoomId == id),
				Player = _gameLobby.Players.FirstOrDefault(p => p.Nickname.Equals(_playerNickname))
			};

			return selectedRoom;
		}
	}
}