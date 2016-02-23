/// <summary>
/// Author: Yihao Cheng / yc7816@rit.edu
/// </summary>

using System;
using System.Linq;
using System.Collections.Generic;

namespace MoneySaving
{
	public class TransactionData
	{
		private readonly List<Statement> Statements = new List<Statement>();

		public TransactionData()
		{			
		}

		public void AddStatement(Statement newStatement){
			Statements.Add(newStatement);
		}

		public List<Statement> GetStatements(){
			return Statements;
		}

		/// <summary>
		/// Get the transaction data total month.
		/// </summary>
		/// <returns>The period.</returns>
		public int TotalPeriod(){
			if (Statements.Count != 0) {
				return Convert.ToInt32
					((Statements.Last ().time.Subtract (Statements.First ().time).TotalDays) / 30);
			} else {
				return 0;
			}
		}
	}
}

