using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using PirateGame_MVC.GameLobby;

namespace PirateGame_MVC.Models.Validation
{
	public class NicknameAttribute : ValidationAttribute
	{
		private string nickname;
		private Lobby _gameLobby;

		public string GetErrorMessage() => "this nickname is alredy used.";

		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			_gameLobby = (Lobby)validationContext.GetService(typeof(Lobby));
			var player = (Player)validationContext.ObjectInstance;
			this.nickname = (string)value;

			if (NicknameIsTaken(nickname))
			{
				return new ValidationResult(GetErrorMessage());
			}
			else
			{
				return ValidationResult.Success;
			}
		}

		private bool NicknameIsTaken(string nickname)
		{
			foreach (var player in _gameLobby.Players)
			{
				if (player.Nickname.ToLower().Equals(nickname.ToLower()))
				{
					return true;
				}
			}

			return false;
		}
	}
}