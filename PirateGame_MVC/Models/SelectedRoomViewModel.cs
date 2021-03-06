﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PirateGame_MVC.GameLobby;
using PirateGame_MVC.Models.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PirateGame_MVC.Models
{
	public class SelectedRoomViewModel
	{
		[Required]
		public int RoomId { get; set; }
	}
}