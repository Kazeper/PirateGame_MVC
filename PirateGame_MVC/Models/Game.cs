using PirateGame_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PirateBayMVC.Models
{
	public class Game
	{
		public List<Player> Players { get; set; }
		public List<int> AvailableFields { get; set; }

		public Game()
		{
			AvailableFields = GameSettings.CreateAvailableFieldsList();
		}

		//private void
	}
}