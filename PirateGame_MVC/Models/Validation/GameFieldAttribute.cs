using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PirateGame_MVC.Models.Validation
{
	public class GameFieldAttribute : ValidationAttribute
	{
		public int[] GameField { get; set; }

		public GameFieldAttribute()
		{
		}

		public string GetErrorMessage() => "values of some fields are invalid.";

		protected override ValidationResult IsValid(object value, ValidationContext validationContext)

		{
			//var roomViewModel = (IRoomViewModel)validationContext.ObjectInstance;
			this.GameField = (int[])value;

			List<int[]> amountOfIndividualFieldType = new List<int[]>()
			{
				//		{amount, type of field }
				new int[] { 0,1},
				new int[] { 0,2},
				new int[] { 0,3},
				new int[] { 0,4},
				new int[] { 0,5},
				new int[] { 0,6},
				new int[] { 0,7},
				new int[] { 0,8},
				new int[] { 0,9},
				new int[] { 0,10},
				new int[] { 0,11},
				new int[] { 0,12},
				new int[] { 0,13},
				new int[] { 0,14},
				new int[] { 0,15}
			};

			if (IsGameFieldInRange())
			{
				CountAmountOfIndividualFields(amountOfIndividualFieldType);

				if (ListsAreEqual(amountOfIndividualFieldType, GameSettings.AvailableFieldTypes))
				{
					return ValidationResult.Success;
				}
			}

			return new ValidationResult(GetErrorMessage());
		}

		private bool ListsAreEqual(List<int[]> amountOfIndividualFieldType, List<int[]> AvailableFieldTypes)
		{
			bool result = true;
			bool isEqual;
			int index = 0;
			int amountIndex = 0;
			int valueIndex = 1;

			foreach (int[] tab in AvailableFieldTypes)
			{
				isEqual = (tab[amountIndex] == amountOfIndividualFieldType[index][amountIndex]) && (tab[valueIndex] == amountOfIndividualFieldType[index][valueIndex]);

				if (!isEqual)
				{
					result = false;
					break;
				}

				index++;
			}
			return result;
		}

		protected bool IsGameFieldInRange()
		{
			bool result = true;

			foreach (int fieldValue in GameField)
			{
				if (!(fieldValue > 0 && fieldValue < 16))
				{
					result = false;
				}
			}

			return result;
		}

		protected List<int[]> CountAmountOfIndividualFields(List<int[]> amountAndFieldTypes)
		{
			int amountIndex = 0;
			int valueIndex = 1;
			int index;
			foreach (int fieldValue in GameField)
			{
				index = amountAndFieldTypes.IndexOf(amountAndFieldTypes.Find(m => m[valueIndex] == fieldValue));

				if (amountAndFieldTypes[index][valueIndex] == fieldValue)
				{
					amountAndFieldTypes[index][amountIndex]++;
				}
			}

			return amountAndFieldTypes;
		}
	}
}