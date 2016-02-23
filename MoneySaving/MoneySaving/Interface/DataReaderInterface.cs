using System;

namespace MoneySaving
{
	public interface DataReaderInterface
	{
		TransactionData getCurrentData{ get;}

		bool ReadInCSVFile(string path);

		Tips ProvideSolution(TransactionData data);

		string FilterWithAPI(string description);

		string FilterWithoutAPI(string description);

		bool Repeat();
	}
}

