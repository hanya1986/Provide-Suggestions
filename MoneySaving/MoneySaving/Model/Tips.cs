using System;
using System.Text;
using System.Collections.Generic;

namespace MoneySaving
{
	public class Tips
	{
		private Dictionary<string,List<Money>> recommendation = new Dictionary<string, List<Money>> ();
		public int totalMonth { get; set; }
		
		public Tips ()
		{
		}

		/// <summary>
		/// Adds the recommendation.
		/// </summary>
		/// <param name="type">Type.</param>
		/// <param name="spend">Spend.</param>
		/// <param name="save">Save.</param>
		public void AddRecommendation(string type, double spend, double save){
			if (recommendation.ContainsKey (type)) {
				recommendation [type].Add (new Money (spend, save));
			} else {
				recommendation.Add (type, new List<Money> ());
				recommendation [type].Add (new Money (spend, save));
			}
		}

		/// <summary>
		/// Publishs the final statement.
		/// </summary>
		public void PublishFinalStatement(){
			if (recommendation.Count != 0) {
				Console.WriteLine ("Average total unnecessary cost/month: $");
				foreach (KeyValuePair<string,List<Money>> pair in recommendation) {
					double total = 0;
					double totalSave = 0;
					StringBuilder sb = new StringBuilder ();
					switch (pair.Key) {
						case WithoutAPIList.RESTAURANT:
							sb.Append (pair.Key + " expences that exceed $15 total = $");
							break;
						case WithoutAPIList.TRANSPORTATION:
							sb.Append (pair.Key + " expences taking taxi = $");
							break;
						default:
							sb.Append (pair.Key + " total expence = $");
							break;
					}
					foreach (Money money in pair.Value) {
						total += money.spend;
						totalSave += money.save;
					}
					sb.Append (total / totalMonth + " || " + "minimum save = $" + totalSave / totalMonth);

					Console.WriteLine (sb.ToString());
				}
			}
		}

		/// <summary>
		/// Creates the tips and publish them.
		/// </summary>
		public void CreateAndPublishTips(){
			if (recommendation.Count != 0) {
				StringBuilder sb = new StringBuilder ();
				sb.Append ("Tips: \n");
				foreach (KeyValuePair<string,List<Money>> pair in recommendation) {
					double total = 0;
					double totalSave = 0;
					foreach (Money money in pair.Value) {
						total += money.spend;
						totalSave += money.save;
					}
					sb.Append ("\t\u25C9" + "For " + pair.Key + " you can minimun save $" + totalSave / totalMonth * -1);
					switch (pair.Key) {
						case WithoutAPIList.RESTAURANT:
							sb.Append (" if you spend less than 15/meal.");
							break;
						case WithoutAPIList.TRANSPORTATION:
							sb.Append (" if you take public transportation.");
							break;
						case WithoutAPIList.SHOPPING:
							sb.Append (" if you no buying them (excluded Walmart)");
							break;
						case WithoutAPIList.ENTERTAINMENT:
							sb.Append (" if you are not going.");
							break;
						default:
							sb.Append (pair.Key + " total expence = ");
							break;
					}
					Console.WriteLine(sb.ToString());
					sb.Clear ();
				}
			}
		}
	}

	public class Money{
		public double spend;
		public double save;

		public Money(double spend, double save){
			this.spend = spend;
			this.save = save;
		}
	}
}

