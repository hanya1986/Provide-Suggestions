using System;

namespace MoneySaving
{
	public class Statement
	{
		public DateTime time {get;}
		public string log {get;}
		public double amount { get;}
		
		public Statement(DateTime time, string log, double amount)
		{
			this.time = time;
			this.log = log;
			this.amount = amount;
		}
	}
}

