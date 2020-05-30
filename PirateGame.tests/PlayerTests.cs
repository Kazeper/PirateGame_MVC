using PirateGame_MVC;
using PirateGame_MVC.Models;
using System;
using Xunit;

namespace PirateGame.tests
{
	public class PlayerTests
	{
		public static readonly int SEED = 666;

		#region Verify number of specific fields

		[Theory]
		[InlineData(1, 1)]
		[InlineData(1, 2)]
		[InlineData(1, 3)]
		[InlineData(1, 4)]
		[InlineData(1, 5)]
		[InlineData(1, 6)]
		[InlineData(1, 7)]
		[InlineData(1, 8)]
		[InlineData(1, 9)]
		[InlineData(1, 10)]
		[InlineData(1, 11)]
		[InlineData(1, 12)]
		[InlineData(2, 13)]
		[InlineData(10, 14)]
		[InlineData(25, 15)]
		public void Number_Of_Specific_Field_Is_Valid(int quantity, int FieldType)
		{
			Player sut = new Player();
			int countedQuantity = 0;
			sut.SetGameFieldRandomly(SEED);

			foreach (int field in sut.GameField)
			{
				if (field == FieldType)
				{
					countedQuantity++;
				}
			}

			Console.WriteLine(countedQuantity);
			Assert.Equal(quantity, countedQuantity);
		}

		#endregion Verify number of specific fields
	}
}