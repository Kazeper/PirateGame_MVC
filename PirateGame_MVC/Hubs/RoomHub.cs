using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using PirateGame_MVC.GameLobby;
using System.Linq;
using System.Threading.Tasks;

namespace PirateGame_MVC
{
	public class RoomHub : Hub
	{
		private readonly Lobby _gameLobby;

		public RoomHub(Lobby gameLobby)
		{
			_gameLobby = gameLobby;
		}

		public async Task AddToGroup(string roomId)
		{
			await Groups.AddToGroupAsync(Context.ConnectionId, roomId);
		}

		public async Task SendMessage(string user, string message, string roomId)
		{
			await Clients.Group(roomId).SendAsync("ReceiveMessage", user, message);
		}

		public async Task GetPlayers(int roomId)//TODO move to abstract class
		{
			var room = _gameLobby.Rooms.FirstOrDefault(r => r.RoomId == roomId);
			string players = JsonConvert.SerializeObject(room.Players);

			await Clients.Group(roomId.ToString()).SendAsync("ReceivePlayers", players);
		}
	}
}