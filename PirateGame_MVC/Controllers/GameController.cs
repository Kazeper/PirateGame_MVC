using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PirateGame_MVC.Models;

namespace PirateGame_MVC.Controllers
{
	public class GameController : Controller
	{
		public IActionResult Index()
		{
			GameRoomViewModel gameRoom = null;
			if (TempData["gameRoom"] != null)
			{
				gameRoom = JsonConvert.DeserializeObject<GameRoomViewModel>((string)TempData["gameRoom"]);

				//if (gameRoom.Room.Players[0].Nickname == HttpContext.Session.GetString("playerNickname"))
				//{
				//	gameRoom.Room.Game = new Game(gameRoom.Room.Players);
				//}
			}
			return View(gameRoom);
		}
	}
}