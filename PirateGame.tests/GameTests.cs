using PirateGame_MVC;
using PirateGame_MVC.Models;
using System;
using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;

namespace PirateGame.tests
{
	//Arrange
	//Act
	//Assert

	public class GameTests
	{
		private readonly ITestOutputHelper _stdOut;
		public int NumberOfPlayers = 5;

		public GameTests(ITestOutputHelper stdOut)
		{
			_stdOut = stdOut;
		}

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
			Game game = new Game(players); //Game class creates playerQueue

			//Assert
			for (int i = 0; i < NumberOfPlayers; i++)
			{
				_stdOut.WriteLine(game.playerQueue[i].ToString());

				if (game.playerQueue[i] == players.Count - 1) // if is the highest NO
				{
					if (i == (game.playerQueue.Length - 1))// if is the last
					{
						Assert.Equal(0, game.playerQueue[0]);
					}
					else // its not last so take the next one
					{
						Assert.Equal(0, game.playerQueue[i + 1]);
					}
					continue;
				}

				if (i == (game.playerQueue.Length - 1))// if is the last
				{
					Assert.Equal(game.playerQueue[i] + 1, game.playerQueue[0]);// the first one should be greater by one
				}
				else
				{
					Assert.Equal(game.playerQueue[i] + 1, game.playerQueue[i + 1]); // next one is greater  by one
				}
			}
		}
	}
}