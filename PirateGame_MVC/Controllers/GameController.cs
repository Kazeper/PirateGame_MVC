﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace PirateGame_MVC.Controllers
{
	public class GameController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}