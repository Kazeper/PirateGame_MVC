using Microsoft.AspNetCore.SignalR;
using PirateGame_MVC.GameLobby;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PirateGame_MVC.Hubs
{
	public class GameHub : Hub
	{
		private readonly Lobby _gamelobby;

		public GameHub(Lobby gameLobby)
		{
			_gamelobby = gameLobby;
		}
	}
}