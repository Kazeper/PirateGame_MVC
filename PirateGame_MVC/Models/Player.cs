using PirateGame_MVC.Models;
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

		public bool HasMirror { get; private set; }
		public bool HasShield { get; private set; }

		public int Bank { get; set; }

		public int Wallet { get; set; }

		public int[] GameField { get; set; }

		#region GameField Description

		/*
		 * one number at GameField Array represents single field.
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
			GameField = new int[GameSettings.NumberOfRows * GameSettings.NumberOfColumns];
		}

		public void SetGameField(int[] gameField)
		{
			this.GameField = gameField;
		}

		public void SetGameFieldRandomly(int? seed)
		{
			Random random;

			if (seed is null)
			{
				random = new Random();
			}
			else
			{
				random = new Random((int)seed);
			}

			List<int> availableFields = GameSettings.CreateAvailableFieldsList();
			List<int[]> availableFieldTypes = new List<int[]>
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

			foreach (int[] field in availableFieldTypes)
			{
				for (int i = field[0]; i > 0; i--)
				{
					int randomPosition = random.Next(0, availableFields.Count);
					this.GameField[availableFields[randomPosition]] = field[1];

					availableFields.Remove(availableFields[randomPosition]);
				}
			}
		}

		public void ExecuteAction(int drawField)
		{
			switch (GameField[drawField])
			{
				case 1:
					RobSomeone();
					break;

				case 2:
					KillSomeone();
					break;

				case 3:
					GivePresent();
					break;

				case 4:
					WipeOutField();
					break;

				case 5:
					SwapMoney();
					break;

				case 6:
					ChooseNextField();
					break;

				case 7:
					VerifyShieldUse();
					break;

				case 8:
					VerifyMirrorUse();
					break;

				case 9:
					DetonateBomb();
					break;

				case 10:
					DoubleTheWallet();
					break;

				case 11:
					DepositCash();
					break;

				case 12:
					AddCash(5000);
					break;

				case 13:
					AddCash(3000);
					break;

				case 14:
					AddCash(1000);
					break;

				case 15:
					AddCash(200);
					break;

				default:
					break;
			}
		}

		private void AddCash(int money)
		{
			Wallet += money;
		}

		private void DepositCash()
		{
			Bank = Wallet;
			Wallet = 0;
		}

		private void DoubleTheWallet()
		{
			Wallet *= 2;
		}

		private void DetonateBomb()
		{
			if (HasShield)
			{
				VerifyShieldUse();
			}
		}

		private void ChooseNextField()
		{
			throw new NotImplementedException();
		}

		private void SwapMoney()
		{
			int tempMoney = this.Wallet;
			//TODO finish
		}

		private void WipeOutField()
		{
			throw new NotImplementedException();
		}

		private void GivePresent()
		{
			throw new NotImplementedException();
		}

		private void KillSomeone()
		{
			throw new NotImplementedException();
		}

		private void RobSomeone()
		{
			throw new NotImplementedException();
		}

		public void SetFieldAsEmpty(int drawField)
		{
			GameField[drawField] = 0;
		}

		public void VerifyMirrorUse()
		{
			if (HasMirror)
			{
				HasMirror = false;
			}
		}

		public void VerifyShieldUse()
		{
			if (HasShield)
			{
				HasShield = false;
			}
		}
	}
}