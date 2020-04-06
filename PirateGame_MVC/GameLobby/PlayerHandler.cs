using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading.Tasks;
using WebSocketManager;
using WebSocketManager.Common;

namespace PirateGame_MVC.GameLobby
{
	public class PlayerHandler : WebSocketHandler
	{
		public PlayerHandler(WebSocketConnectionManager webSocketConnectionManager) : base(webSocketConnectionManager)
		{
		}

		public override async Task OnConnected(WebSocket socket)
		{
			await base.OnConnected(socket);

			var socketId = WebSocketConnectionManager.GetId(socket);

			var message = new Message
			{
				MessageType = MessageType.Text,
				Data = " Player with socket id :{socketId} is now connected!"
			};

			await SendMessageToAllAsync(message);
		}

		public override async Task OnDisconnected(WebSocket socket)
		{
			await base.OnDisconnected(socket);

			var socketId = WebSocketConnectionManager.GetId(socket);

			var message = new Message
			{
				MessageType = MessageType.Text,
				Data = " Player with socket id :{socketId} is now connected!"
			};

			await SendMessageToAllAsync(message);
		}
	}
}