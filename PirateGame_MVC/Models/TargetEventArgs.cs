using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PirateGame_MVC.Models
{
	public class TargetEventArgs : EventArgs
	{
		public string TargetNickname { get; set; }
	}
}