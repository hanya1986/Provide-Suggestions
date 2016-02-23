using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text;
using System.Linq;
using YelpSharp;
using YelpSharp.Data.Options;

namespace MoneySaving
{
	public class DataReaderPresenter : DataReaderInterface
	{
		private TransactionData currentData;
		
		public DataReaderPresenter()
		{
		}

		public TransactionData getCurrentData{
			get{ return currentData; }
		}

		/// <summary>
		/// Reads CSV file in and store at currentData.
		/// </summary>
		/// <returns><c>true</c>, if in CSV file was  read, <c>false</c> otherwise.</returns>
		/// <param name="path">Path.</param>
		public bool ReadInCSVFile(string path){
			TransactionData data = new TransactionData();
			Dictionary<string, string> populatedList = new Dictionary<string, string>();
			try{
				if(File.Exists(path)){
			        using (StreamReader reader = new StreamReader (path)) {
				        while (!reader.EndOfStream) {
							string[] line = reader.ReadLine().Split(',');
							int output;
							DateTime test;
							double test2;
							if(line.Count() == 3){
								if(DateTime.TryParse(line[0],out test) && Double.TryParse(line[2],out test2)){
							        if(int.TryParse(line[1].Split(' ').Last(), out output)){
								        if(int.Parse(line[1].Split(' ').Last()) > 10000){
							                line[1] = string.Join(" ",line[1].Split(' ').Take(line[1].Split(' ').Count() - 1).ToArray());
								        }
							        }
									if(CheckAPIExist()){
								        if(!populatedList.ContainsKey(line[1])){
									        string temp = line[1];
									        line[1] = FilterWithAPI(line[1]);
									        populatedList.Add(temp,line[1]);
								        }else{
									        line[1] = populatedList[line[1]];
								        }
							        }else{
							            line[1] = FilterWithoutAPI(line[1]);
							        }
							        if(!string.IsNullOrEmpty(line[1])){
								        Statement s = new Statement(Convert.ToDateTime(line[0]),line[1],Convert.ToDouble(line[2]));
								        data.AddStatement(s);
							        }
								}else{
									Console.WriteLine("The data does not match the existing format.");
									return false;
								}
							}else{
								Console.WriteLine("The data does not match the existing format.");
								return false;
							}
				        }
					}
					currentData = data;
					return true;
				}else{
					Console.WriteLine("File does not exist in the current path");
					return false;
				}
			}catch(IOException e){
				Console.WriteLine ("Cannot open " + e.GetType ().Name);
			}
			return false;
		}

		/// <summary>
		/// Checks the API existance.
		/// </summary>
		/// <returns><c>true</c>, if API exist , <c>false</c> otherwise.</returns>
		public bool CheckAPIExist(){
			if (!string.IsNullOrEmpty (ConfigFileForYelpAPI.Options.AccessToken) || !string.IsNullOrEmpty (ConfigFileForYelpAPI.Options.ConsumerKey) ||
			   !string.IsNullOrEmpty (ConfigFileForYelpAPI.Options.AccessTokenSecret) || !string.IsNullOrEmpty (ConfigFileForYelpAPI.Options.ConsumerSecret)) {
				return true;
			}
			return false;
		}

		/// <summary>
		/// Provide tips according to the given data, by searching the key word on internet to catagorize
		/// all the transactions.
		/// </summary>
		/// <returns>Tips</returns>
		/// <param name="data">TransactionData</param>
		public Tips ProvideSolution(TransactionData data){
			Tips tip = new Tips ();
			foreach (Statement state in data.GetStatements()) {
				if (state.log == WithoutAPIList.RESTAURANT) {
					if (state.amount > 15) {
						tip.AddRecommendation (state.log, state.amount, 15 - state.amount);
					}
				} else if(state.log == WithoutAPIList.TRANSPORTATION){
					tip.AddRecommendation (state.log, state.amount, 2.25 - state.amount);
				}
				else {
					tip.AddRecommendation (state.log, state.amount, 0 - state.amount);
				}
			}
			tip.totalMonth = data.TotalPeriod ();
			return tip;
		}

		/// <summary>
		/// Filters the without AP.
		/// </summary>
		/// <returns><c>true</c>, if without AP was filtered, <c>false</c> otherwise.</returns>
		/// <param name="description">Description.</param>
		public string FilterWithoutAPI(string description){
			if (WithoutAPIList.UnVoidableCost.Any (u => description.StartsWith (u))) {
				return null;
			}
			string newDescription = WithoutAPIList.AvoidableCost.SingleOrDefault (a => description.StartsWith (a.Key)).Value;
			if (!string.IsNullOrEmpty (newDescription)) {
				return newDescription;
			} else {
				newDescription = WithoutAPIList.RESTAURANT;
			}
			return newDescription;
		}

		/// <summary>
		/// Filter the specified description using YelpSharp API, but it is much slower than without API.
		/// </summary>
		/// <param name="description">Description.</param>
		public string FilterWithAPI(string description){
			if (WithoutAPIList.UnVoidableCost.Any (u => description.StartsWith (u))) {
				return null;
			}
			string newDescription = WithoutAPIList.AvoidableCost.SingleOrDefault (a => description.StartsWith (a.Key)).Value;
			if (!string.IsNullOrEmpty (newDescription)) {
				return newDescription;
			} else {
				var y = new Yelp (ConfigFileForYelpAPI.Options);
				var results = y.Search (description, "Indianapolis, IN").Result;
				if (results.businesses != null) {
					if (results.businesses.Count != 0) {
						if (results.businesses [0].categories.Any (c => c.Any (item => WithAPIList.categories.ContainsKey (item)))) {
							return WithoutAPIList.RESTAURANT;
						}
					}
				}
			}
			return null;
		}

		/// <summary>
		/// Repeat this instance.
		/// </summary>
		public bool Repeat(){
			Console.Write ("Do you want to load another file(Yes or No)? ");
			string input = Console.ReadLine ();
			switch (input.ToLower()) {
			    case "yes":
				    return true;
			    case "no":
				    return false;
				default:
					Repeat ();
					break;
			}
			return false;
		}
	}
}

