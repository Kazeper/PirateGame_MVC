using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using PirateGame_MVC.GameLobby;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PirateGame_MVC.Hubs
{
	public class GameHub : Hub<IGameHub>
	{
		private readonly Lobby _gamelobby;
		private string targetNickname;

		public delegate void ReceivedTargetEventHandler(object o, EventArgs e);

		public event ReceivedTargetEventHandler ReceivedTarget;

		public GameHub(Lobby gameLobby)
		{
			_gamelobby = gameLobby;
			targetNickname = "";
		}

		public async Task AddToGroup(string roomId)
		{
			await Groups.AddToGroupAsync(Context.ConnectionId, roomId);
		}

		public void SaveConnectionId(string playerNickname)
		{
			var player = _gamelobby.GetPlayer(playerNickname);
			player.ConnectionId = Context.ConnectionId;
		}

		public async void StartGame(int roomId)
		{
			var room = _gamelobby.Rooms.Find(r => r.RoomId == roomId);

			if (!room.GameStarted)
			{
				room.Game.StartGame();
				room.GameStarted = true;
			}
		}

		public void AskForTarget(string connectionId, int playerField)
		{
			//Clients.Client(connectionId).SendAsync("SelectTarget", playerField);
		}

		//public async Task SendNotification(string connectionId, string message)
		//{
		//	//await Clients.Client(connectionId).SendAsync("NotifyClient", message);
		//}

		//protected virtual void OnReceivedTarget(string targetNickname)
		//{
		//	if (ReceivedTarget != null)
		//	{
		//		ReceivedTarget(this, EventArgs.Empty, targetNickname);
		//	}
		//}

		public void ReceiveTarget(string targetNickname, int roomId)
		{
			var room = _gamelobby.Rooms.Find(r => r.RoomId == roomId);
			room.Game.TargetNickname = targetNickname;
			room.Game.TargetHasBeenReceived = true;
		}

		public string LoadTarget()
		{
			return targetNickname;
		}
	}
}