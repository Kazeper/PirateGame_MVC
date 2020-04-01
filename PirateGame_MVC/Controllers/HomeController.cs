using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PirateGame_MVC.Models;
using PirateGame_MVC.GameLobby;
using System.Net;

namespace PirateGame_MVC.Controllers
{
	public class HomeController : Controller
	{
		private IHttpContextAccessor _accessor;
		private Lobby _gameLobby;

		public HomeController(IHttpContextAccessor accessor, Lobby gameLobby)
		{
			_accessor = accessor;
			_gameLobby = gameLobby;
		}

		public IActionResult Index()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Index(Player player)
		{
			IActionResult result;

			if (ModelState.IsValid)
			{
				player.Ip = _accessor.HttpContext.Connection.RemoteIpAddress.ToString();
				_gameLobby.Players.Add(player);

				result = RedirectToAction(nameof(SetGameFields));
			}
			else result = View(player);

			return result;
		}

		public IActionResult SetGameFields()
		{
			Player player = GetConnectedPlayer();

			return View(player);
		}

		private Player GetConnectedPlayer()
		{
			string ip = _accessor.HttpContext.Connection.RemoteIpAddress.ToString();

			return _gameLobby.Players.Find(m => m.Ip.Equals(ip));
		}

		[HttpPost]
		public IActionResult SetGameFields(Player player)
		{
			if (ModelState.IsValid)
			{
				Player connPlayer = GetConnectedPlayer();//TODO sprawdzic Gamefield
				connPlayer.SetGameField(player.GameField);

				return RedirectToAction("Index", "Lobby");
			}
			else return View(player);
		}

		public IActionResult Privacy()
		{
			return View();
		}

		public IActionResult Rules()
		{
			return View();
		}

		public IActionResult HowToPlay()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}