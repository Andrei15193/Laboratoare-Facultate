using System;
using System.Collections.Generic;
using System.Text;
namespace P8
{
	internal static class Program
	{
		static Program()
		{
			_numberCorespondence.Add(0, "zero");
			_numberCorespondence.Add(1, "unu");
			_numberCorespondence.Add(2, "doi");
			_numberCorespondence.Add(3, "trei");
			_numberCorespondence.Add(4, "patru");
			_numberCorespondence.Add(5, "cinci");
			_numberCorespondence.Add(6, "sase");
			_numberCorespondence.Add(7, "sapte");
			_numberCorespondence.Add(8, "opt");
			_numberCorespondence.Add(9, "noua");
			_numberCorespondence.Add(10, "zece");
		}

		internal static void Main()
		{
			bool writeZero = true;
			int number = _ReadInteger("Introduceti un numar intre -1000 si 1000 inclusiv: ", -1000, 1000), numberCopy = number;
			StringBuilder numberStringBuilder = new StringBuilder();

			if (number < 0)
				numberStringBuilder.Append("minus ");

			number = Math.Abs(number);
			if (number == 1000)
			{
				numberStringBuilder.Append("o mie ");
				number %= 1000;
				writeZero = false;
			}

			switch (number / 100)
			{
				case 0:
					break;
				case 1:
					numberStringBuilder.Append("o suta ");
					writeZero = false;
					break;
				case 2:
					numberStringBuilder.Append("doua sute ");
					writeZero = false;
					break;
				case 3:
				case 4:
				case 5:
				case 6:
				case 7:
				case 8:
				case 9:
					numberStringBuilder.Append(_numberCorespondence[number / 100]).Append(" sute ");
					writeZero = false;
					break;
			}
			number %= 100;

			if (number <= 10)
			{
				if (number != 0 || writeZero)
					numberStringBuilder.Append(_numberCorespondence[number]);
			}
			else
				if (number == 11)
					numberStringBuilder.Append("unsprezece");
				else
					if (number < 20)
						numberStringBuilder.Append(_numberCorespondence[number % 10]).Append("sprezece");
					else
						if (number < 30)
						{
							numberStringBuilder.Append("douazeci");
							if (number % 10 != 0)
								numberStringBuilder.Append(" si ").Append(_numberCorespondence[number % 10]);
						}
						else
						{
							numberStringBuilder.Append(_numberCorespondence[number / 10] + "zeci");
							if (number % 10 != 0)
								numberStringBuilder.Append(" si ").Append(_numberCorespondence[number % 10]);
						}

			string numberAsNameString = numberStringBuilder.ToString().Trim();

			Console.WriteLine("{0} = {1}, care are {2} litere", numberCopy, numberAsNameString, numberAsNameString.Replace(" ", string.Empty).Length);
		}

		private static int _ReadInteger(string message, int minValue, int maxValue)
		{
			int integer;

			do
				do
					Console.Write(message);
				while (!int.TryParse(Console.ReadLine(), out integer));
			while (integer < minValue || maxValue < integer);

			return integer;
		}
		private static readonly IDictionary<int, string> _numberCorespondence = new Dictionary<int, string>();
	}
}