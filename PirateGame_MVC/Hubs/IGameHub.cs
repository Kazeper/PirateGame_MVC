using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace PirateGame_MVC.Hubs
{
	public interface IGameHub
	{
		Task AskForTarget(int playerField);

		Task ReceiveTarget(string targetNickname);

		Task ReceiveNotification(string connectionId, string message);
	}
}