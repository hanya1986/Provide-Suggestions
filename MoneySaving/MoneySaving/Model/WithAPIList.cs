using System;
using System.Collections.Generic;

namespace MoneySaving
{
	public class WithAPIList
	{
		private const string RESTAURANT = "Restaurant";
		private const string SHOPPING = "Shopping";
		public const string UBER = "Uber";

		public static readonly Dictionary<string,string> categories = new Dictionary<string, string>();

		static WithAPIList(){
			#region Restaurant
			categories ["breakfast_brunch"] = RESTAURANT;
			categories ["italian"] = RESTAURANT;
			categories ["pubs"] = RESTAURANT;
			categories ["french"] = RESTAURANT;
			categories ["newamerican"] = RESTAURANT;
			categories ["cajun"] = RESTAURANT;
			categories ["bars"] = RESTAURANT;
			categories ["thai"] = RESTAURANT;
			categories ["delis"] = RESTAURANT;
			categories ["pizza"] = RESTAURANT;
			categories ["cafes"] = RESTAURANT;
			categories ["steak"] = RESTAURANT;
			categories ["seafood"] = RESTAURANT;
			categories ["mexican"] = RESTAURANT;
			categories ["tradamerican"] = RESTAURANT;
			categories ["latin"] = RESTAURANT;
			categories ["gastropubs"] = RESTAURANT;
			categories ["desserts"] = RESTAURANT;
			categories ["steakhouses"] = RESTAURANT;
			categories ["brazilian"] = RESTAURANT;
			categories ["restaurants"] = RESTAURANT;
			#endregion

		}
	}
}

