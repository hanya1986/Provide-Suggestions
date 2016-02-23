/// <summary>
/// Author: Yihao Cheng / yc7816@rit.edu
/// Date: 02/06/2016
/// </summary>

using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;

namespace MoneySaving
{
	class MainClass
	{
		/// <summary>
		/// Main program
		/// </summary>
		/// <param name="args">The command-line arguments.</param>
		public static void Main(string[] args)
		{
			bool repeat = true;
			while (repeat) {
				Console.Write ("Please input your file path(including the file extension): ");
				string path = Console.ReadLine ();
				DataReaderPresenter presenter = new DataReaderPresenter ();
				if (presenter.ReadInCSVFile (path)) {
					Tips finalTips = presenter.ProvideSolution (presenter.getCurrentData);
					finalTips.PublishFinalStatement ();
					finalTips.CreateAndPublishTips ();
				}
				repeat = presenter.Repeat ();
			}
			Environment.Exit (1);
		}
	}
}
