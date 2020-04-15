using Microsoft.AspNetCore.Http;
using System;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using PirateGame_MVC.Sockets.Abstract;

namespace PirateGame_MVC.Sockets
{
	public class SocketMiddelware
	{
		private readonly RequestDelegate _next;
		private SocketHandler Handler { get; set; }

		public SocketMiddelware(SocketHandler handler, RequestDelegate next)
		{
			_next = next;
			Handler = handler;
		}

		public async Task InvokeAsync(HttpContext context)
		{
			if (!context.WebSockets.IsWebSocketRequest)
			{
				return;
			}
			else
			{
				var socket = await context.WebSockets.AcceptWebSocketAsync();
				await Handler.OnConnected(socket);

				await Receive(socket, async (result, buffer) =>
				{
					if (result.MessageType == WebSocketMessageType.Text)
					{
						await Handler.Receive(socket, result, buffer);
					}
					else if (result.MessageType == WebSocketMessageType.Close)
					{
						await Handler.OnDisconnected(socket);
					}
				});
			}
		}

		private async Task Receive(WebSocket socket, Action<WebSocketReceiveResult, byte[]> messageHandler)
		{
			var buffer = new byte[1024 * 4];

			while (socket.State == WebSocketState.Open)
			{
				var result = await socket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
				messageHandler(result, buffer);
			}
		}
	}
}