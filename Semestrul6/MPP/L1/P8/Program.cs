using System;
using System.Collections.Generic;
using System.Text;
namespace P8
{
	internal static class Program
	{
		static Program()
		{
			_numere.Add(0, "zero");
			_numere.Add(1, "unu");
			_numere.Add(2, "doi");
			_numere.Add(3, "trei");
			_numere.Add(4, "patru");
			_numere.Add(5, "cinci");
			_numere.Add(6, "sase");
			_numere.Add(7, "sapte");
			_numere.Add(8, "opt");
			_numere.Add(9, "noua");
			_numere.Add(10, "zece");
		}

		internal static void Main()
		{
			int numar = _CitesteIntreg("Introduceti un numar intre -1000 si 1000 inclusiv: ", -1000, 1000);
			string numarScrisInCuvinte = _ObtineNumarScrisInCuvinte(numar);
			Console.WriteLine("{0} = {1}, care are {2} litere", numar, numarScrisInCuvinte, numarScrisInCuvinte.Replace(" ", string.Empty).Length);
		}

		/// <summary>
		/// Transforma numarul dat in reprezentarea lui scrisa in cuvinte.
		/// </summary>
		/// <param name="numar">Numar de transformat.</param>
		/// <returns>Returneaza numarul in reprezentarea sa scrisa in cuvinte.</returns>
		/// <exception cref="System.ArgumentException">Cand numarul intreg este mai mic decat -1000 sau mai mare decat 1000.</exception>
		private static string _ObtineNumarScrisInCuvinte(int numar)
		{
			if (numar < -1000 || 1000 < numar)
				throw new ArgumentException("Numarul trebuie sa fie mai mare egal cu -1000 si mai mic egal cu 1000!", "numar");

			bool writeZero = true;
			StringBuilder numarInCuvinteStringBuilder = new StringBuilder();

			if (numar < 0)
				numarInCuvinteStringBuilder.Append("minus ");

			numar = Math.Abs(numar);
			if (numar == 1000)
			{
				numarInCuvinteStringBuilder.Append("o mie ");
				numar %= 1000;
				writeZero = false;
			}

			switch (numar / 100)
			{
				case 0:
					break;
				case 1:
					numarInCuvinteStringBuilder.Append("o suta ");
					writeZero = false;
					break;
				case 2:
					numarInCuvinteStringBuilder.Append("doua sute ");
					writeZero = false;
					break;
				case 3:
				case 4:
				case 5:
				case 6:
				case 7:
				case 8:
				case 9:
					numarInCuvinteStringBuilder.Append(_numere[numar / 100]).Append(" sute ");
					writeZero = false;
					break;
			}
			numar %= 100;

			if (numar <= 10)
			{
				if (numar != 0 || writeZero)
					numarInCuvinteStringBuilder.Append(_numere[numar]);
			}
			else
				if (numar == 11)
					numarInCuvinteStringBuilder.Append("unsprezece");
				else
					if (numar < 20)
						numarInCuvinteStringBuilder.Append(_numere[numar % 10]).Append("sprezece");
					else
						if (numar < 30)
						{
							numarInCuvinteStringBuilder.Append("douazeci");
							if (numar % 10 != 0)
								numarInCuvinteStringBuilder.Append(" si ").Append(_numere[numar % 10]);
						}
						else
						{
							numarInCuvinteStringBuilder.Append(_numere[numar / 10] + "zeci");
							if (numar % 10 != 0)
								numarInCuvinteStringBuilder.Append(" si ").Append(_numere[numar % 10]);
						}

			return numarInCuvinteStringBuilder.ToString().Trim();
		}
		/// <summary>
		/// Citeste un intreg de la intrarea standard aflta intre valorile specificate (inclusiv)
		/// </summary>
		/// <param name="mesaj">Mesajul care este afisat inainte de citirea numarului intreg.</param>
		/// <param name="valoareMinima">Valoarea minima inclusiva care poate fi citita.</param>
		/// <param name="valoareMaxima">Valoarea maxima inclusiva care poate fi citita.</param>
		/// <returns>Un numar intreg citit de la intrarea standard aflta intre valoareMinima si valoareMaxima inclusiv.</returns>
		private static int _CitesteIntreg(string mesaj, int valoareMinima, int valoareMaxima)
		{
			int numar;

			do
				do
					Console.Write(mesaj ?? string.Empty);
				while (!int.TryParse(Console.ReadLine(), out numar));
			while (numar < valoareMinima || valoareMaxima < numar);

			return numar;
		}
		private static readonly IDictionary<int, string> _numere = new Dictionary<int, string>();
	}
}