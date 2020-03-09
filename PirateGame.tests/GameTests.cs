using PirateBayMVC.Models;
using System;
using System.Collections.Generic;
using Xunit;

namespace PirateGame.tests
{
	//Arrange
	//Act
	//Assert

	public class GameTests
	{
		public int NumberOfPlayers = 5;

		[Fact]
		public void PlayersQueueIsCorrect()
		{
			//Arrange
			List<Player> players = new List<Player>();

			for (int i = 0; i < NumberOfPlayers; i++)
			{
				players.Add(new Player());
			}

			//Act
			Game game = new Game(players);

			//Assert
			for (int i = 0; i < NumberOfPlayers - 2; i++)
			{
				Assert.Equal(game.playerQueue[i] + 1, game.playerQueue[i + 1]);
			}
		}
	}
}