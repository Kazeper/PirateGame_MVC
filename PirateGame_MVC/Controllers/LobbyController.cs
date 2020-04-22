using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PirateGame_MVC.GameLobby;

namespace PirateGame_MVC.Controllers
{
	public class LobbyController : Controller
	{
		private IHttpContextAccessor _accessor;
		private Lobby _gameLobby;

		public LobbyController(Lobby gameLobby, IHttpContextAccessor accessor)
		{
			_accessor = accessor;
			_gameLobby = gameLobby;
		}

		public IActionResult Index()
		{
			return View();
		}

		//private Player GetConnectedPlayer()
		//{
		//	string ip = _accessor.HttpContext.Connection.RemoteIpAddress.ToString();

		//	return _gameLobby.Players.Find(m => m.Ip.Equals(ip));
		//}
	}
}