using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PirateGame_MVC.Models;

namespace PirateGame_MVC.Controllers
{
	public class HomeController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Index(Player player)
		{
			if (ModelState.IsValid)
			{
				player.SetGameFieldRandomly(null);
				return RedirectToAction(nameof(SetGameFields), player);
			}
			else return View(player);
		}

		[HttpGet]
		public IActionResult SetGameFields(Player player)
		{
			return View(player);
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