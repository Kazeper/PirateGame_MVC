using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PirateBayMVC.Models
{
	public class Player
	{
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
		 * 12 - add 5000$
		 * 13 - add 3000$
		 * 14 - add 1000$
		 * 15 - add 200$
		 *
		 */

		#endregion GameField Description

		public Player()
		{
			gameFieldIsSet = false;
		}

		private void SetGameField()
		{
		}
	}
}