using System;
using System.Collections.Generic;

namespace MoneySaving
{
	public class WithoutAPIList
	{
		public const string RESTAURANT = "Restaurant";
		public const string TRANSPORTATION = "Transportation";
		public const string SHOPPING = "Shopping";
		public const string ENTERTAINMENT = "Entertainment";

		private static Dictionary<string,string> avoidableCost = new Dictionary<string, string>();
		private static List<string> unAvoidableCost = new List<string>();

		static WithoutAPIList ()
		{
			#region UnAvoidable
			unAvoidableCost.Add ("BKOFAMERICA");
			unAvoidableCost.Add ("ATM");
			unAvoidableCost.Add ("FirstService");
			unAvoidableCost.Add ("WALMART");
			unAvoidableCost.Add ("CHEVRON");
			unAvoidableCost.Add ("EXXON");
			unAvoidableCost.Add ("SHELL");
			unAvoidableCost.Add ("MOBILE");
			unAvoidableCost.Add ("PAYPAL");
			#endregion

			#region Avoidable
			avoidableCost.Add ("Uber","Transportation");
			avoidableCost.Add ("TARGET","Shopping");
			avoidableCost.Add ("NORDSTROM","Shopping");
			avoidableCost.Add ("AMAZON","Shopping");
			avoidableCost.Add ("ADY*Netflix","Entertainment");
			#endregion
		}

		/// <summary>
		/// Gets the unvoidable cost list.
		/// </summary>
		/// <value>The un voidable cost.</value>
		public static IEnumerable<string> UnVoidableCost{ 
			get{ return unAvoidableCost; }
		}

		/// <summary>
		/// Gets the avoidable cost list.
		/// </summary>
		/// <value>The avoidable cost.</value>
		public static Dictionary<string,string> AvoidableCost{
			get{ return avoidableCost; }
		}
	}
}

