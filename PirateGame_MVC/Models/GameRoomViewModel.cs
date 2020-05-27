using PirateGame_MVC.GameLobby;
using PirateGame_MVC.Models.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PirateGame_MVC.Models
{
	public class GameRoomViewModel
	{
		[Required]
		public int RoomId { get; set; }

		public Room Room { get; set; }

		public Player Player { get; set; }

		[GameField()]
		public int[] GameField { get; set; }
	}
}