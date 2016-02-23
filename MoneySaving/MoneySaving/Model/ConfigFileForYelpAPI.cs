using System;
using YelpSharp;

namespace MoneySaving
{
	public class ConfigFileForYelpAPI
	{
		public string ConsumerKey { get; set;}
		public string ConsumerSecret {get; set;}
		public string Token {get; set;}
		public string TokenSecret {get; set;}

		/// <summary>
		/// Gets and sets the API OAthu.
		/// </summary>
		/// <value>The options.</value>
		public static Options Options{
			get{
				    Options yelp = new Options () {
					//Put the Key here accordingly.
					ConsumerKey = "",
					ConsumerSecret = "",
					AccessToken = "",
					AccessTokenSecret = ""
				};
				return yelp;
			}
		}
	}
}

