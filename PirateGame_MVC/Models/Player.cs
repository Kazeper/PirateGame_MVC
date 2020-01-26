using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PirateBayMVC.Models
{
	public class Player
	{
		public string Nickname { get; set; }

		public bool gameFieldIsSet;
		//private long[] _GameField = new long[7];

		public bool hasMirror { get; private set; }
		public bool hasShield { get; private set; }

		public int Bank { get; set; }

		public int Wallet { get; set; }

		public long[] GameField { get; set; }

		#region GameField Description

		/*
		 * one number at GameField Array contains 14 digits and represents whole row. 2 digits represent single field.
		 * Field types:
		 * 00 - empty field
		 * 01 - Rob someone's points [action field]
		 * 02 - Kill someone [action field]
		 * 03 - Present [action field]
		 * 04 - skull and crossbones - wipe out a field [action field]
		 * 05 - swap scores [action field]
		 * 06 - Choose next field [action field]
		 * 07 - Shield - block the bad [boost field]
		 * 08 - Mirror - reflect the bad []
		 * 09 - Bomb - your wallet go to 0
		 * 10 - double your wallet
		 * 11 - deposit your cash at wallet to the bank
		 * 12 - add 5000$ x1
		 * 13 - add 3000$ x2
		 * 14 - add 1000$ x10
		 * 15 - add 200$  x25
		 *
		 */

		#endregion GameField Description

		public Player()
		{
		}

		public void SetGameField(long[] gameField)
		{
			this.GameField = gameField;
		}

		public void SetGameFieldRandomly()
		{
			List<int[]> availableFields = new List<int[]>
			{
				//		{amount, type of field }
				new int[] { 1,1},
				new int[] { 1,2},
				new int[] { 1,3},
				new int[] { 1,4},
				new int[] { 1,5},
				new int[] { 1,6},
				new int[] { 1,7},
				new int[] { 1,8},
				new int[] { 1,9},
				new int[] { 1,10},
				new int[] { 1,11},
				new int[] { 1,12},
				new int[] { 2,13},
				new int[] { 10,14},
				new int[] { 25,15}
			};
		}

		public void UseMirror()
		{
		}

		public void UseShield()
		{
		}
	}
}