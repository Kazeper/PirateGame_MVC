using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PirateGame_MVC.GameLobby;
using PirateGame_MVC.Models;

namespace PirateGame_MVC.Controllers
{
	public class LobbyController : Controller
	{
		private readonly IHttpContextAccessor _accessor;
		private readonly Lobby _gameLobby;

		public LobbyController(Lobby gameLobby, IHttpContextAccessor accessor)
		{
			_accessor = accessor;
			_gameLobby = gameLobby;
		}

		public IActionResult Index()
		{
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
			if (TempData["roomId"] is null)
			{
				RedirectToAction("Index", "Home");
			}
			int id = int.Parse(TempData["roomId"].ToString());

			return View(GetSelectedRoomViewModel(id));
		}

		private SelectedRoomViewModel GetSelectedRoomViewModel(int id)
		{
			string playerNickname = HttpContext.Session.GetString("playerNickname");

			SelectedRoomViewModel selectedRoom = new SelectedRoomViewModel
			{
				RoomId = id,
				Room = _gameLobby.Rooms.FirstOrDefault(x => x.RoomId == id),
				Player = _gameLobby.Players.FirstOrDefault(p => p.Nickname.Equals(playerNickname))
			};

			return selectedRoom;
		}
	}
}