using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
namespace L2
{
	internal class Tablou
		: IReadOnlyList<int>, ICloneable
	{
		/// <summary>
		/// Creaza un un tablou avand capacitatea egala cu cea specificata.
		/// </summary>
		/// <param name="capacitate">
		/// Specifica capacitatea tabloului.
		/// </param>
		public Tablou(int capacitate)
		{
			_valori = new List<int>(new int[capacitate]);
		}
		/// <summary>
		/// Creaza un un tablou avand capacitatea egala cu cea specificata.
		/// </summary>
		/// <param name="valori">
		/// Valorile tabloului.
		/// </param>
		public Tablou(params int[] valori)
		{
			_valori = valori.ToList();
		}
		/// <summary>
		/// Creaza un un tablou avand capacitatea egala cu cea specificata.
		/// </summary>
		/// <param name="valori">
		/// Valorile tabloului.
		/// </param>
		public Tablou(IEnumerable<int> valori)
		{
			_valori = valori.ToList();
		}

		#region IReadOnlyList<int> Members
		public int this[int index]
		{
			get
			{
				return _valori[index];
			}
			set
			{
				_valori[index] = value;
			}
		}
		#endregion
		#region IReadOnlyCollection<int> Members
		/// <summary>
		/// Returneaza capacitatea tabloului.
		/// </summary>
		public int Count
		{
			get
			{
				return _valori.Count;
			}
		}
		#endregion
		#region IEnumerable<int> Members
		/// <summary>
		/// Returneaza un enumerator pe tablou.
		/// </summary>
		/// <returns>
		/// Un enumerator care parcurge (de la indexul cel mai mic la indexul
		/// cel mai mare) tabloul.
		/// </returns>
		public IEnumerator<int> GetEnumerator()
		{
			return _valori.GetEnumerator();
		}
		#endregion
		#region IEnumerable Members
		/// <summary>
		/// Returneaza un enumerator pe tablou.
		/// </summary>
		/// <returns>
		/// Un enumerator care parcurge (de la indexul cel mai mic la indexul
		/// cel mai mare) tabloul.
		/// </returns>
		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
		#endregion
		#region ICloneable Members
		/// <summary>
		/// Returneaza o copie a tabloului.
		/// </summary>
		/// <returns>
		/// O copie a tabloului.
		/// </returns>
		public object Clone()
		{
			return new Tablou(this);
		}
		#endregion
		/// <summary>
		/// Returneaza valorile tabloului intr-un nou tablou sortate prin insertie.
		/// </summary>
		/// <param name="trebuieInversate">
		/// O functie de sortare. Returneaza true daca elementele trebuie inversate,
		/// false altfel.
		/// </param>
		/// <returns>
		/// Un nou tablou care contine aceleasi valori dar sortate prin insertie.
		/// </returns>
		public Tablou SortarePrinInsertie(Comparator trebuieInversate)
		{
			Tablou tablouSortat = (Tablou)Clone();

			for (int index = 0; index < tablouSortat.Count - 1; index++)
				if (trebuieInversate(tablouSortat[index], tablouSortat[index + 1]))
				{
					int noulIndex = index;
					while (noulIndex > 0 && trebuieInversate(tablouSortat[noulIndex], tablouSortat[index + 1]))
						noulIndex--;

					int valoareInserata = tablouSortat[index + 1];
					for (int shiftIndex = index; shiftIndex >= noulIndex; shiftIndex--)
						tablouSortat[shiftIndex + 1] = tablouSortat[shiftIndex];
					tablouSortat[noulIndex] = valoareInserata;
				}

			return tablouSortat;
		}
		/// <summary>
		/// Returneaza valorile tabloului intr-un nou tablou sortate prin insertie.
		/// </summary>
		/// <param name="trebuieInversate">
		/// O functie de sortare. Returneaza true daca elementele trebuie inversate,
		/// false altfel.
		/// </param>
		/// <returns>
		/// Un nou tablou care contine aceleasi valori dar sortate prin interclasare.
		/// </returns>
		public Tablou SortarePrinInterclasare(Comparator trebuieInversate)
		{
			return new Tablou(_MergeSort(_valori.Take(_valori.Count / 2), _valori.Skip(_valori.Count / 2), trebuieInversate));
		}
		/// <summary>
		/// Returneaza valorile tabloului intr-un nou tablou sortate prin insertie.
		/// </summary>
		/// <param name="trebuieInversate">
		/// O functie de sortare. Returneaza true daca elementele trebuie inversate,
		/// false altfel.
		/// </param>
		/// <returns>
		/// Un nou tablou care contine aceleasi valori dar sortate prin numarare.
		/// </returns>
		public Tablou SortarePrinNumarare(Comparator trebuieInversate)
		{
			IDictionary<int, int> valoriDupaNumarDeAparenete = new Dictionary<int, int>();

			foreach (int valoare in _valori)
			{
				int numarDeValoriEgale;

				if (!valoriDupaNumarDeAparenete.TryGetValue(valoare, out numarDeValoriEgale))
					valoriDupaNumarDeAparenete.Add(valoare, 1);
				else
					valoriDupaNumarDeAparenete[valoare] = numarDeValoriEgale + 1;
			}

			int indexTablou = 0;
			Tablou tablouSortat = new Tablou(_valori.Count);

			while (valoriDupaNumarDeAparenete.Count > 0)
			{
				int primaValoare = valoriDupaNumarDeAparenete.Keys.First();

				foreach (int valoare in valoriDupaNumarDeAparenete.Keys.Skip(1))
					if (trebuieInversate(primaValoare, valoare))
						primaValoare = valoare;

				for (int numarAparente = valoriDupaNumarDeAparenete[primaValoare]; numarAparente > 0; numarAparente--)
					tablouSortat[indexTablou++] = primaValoare;
				valoriDupaNumarDeAparenete.Remove(primaValoare);
			}

			return tablouSortat;
		}
		/// <summary>
		/// Returneaza valorile tabloului intr-un nou tablou sortate prin insertie.
		/// </summary>
		/// <param name="trebuieInversate">
		/// O functie de sortare. Returneaza true daca elementele trebuie inversate,
		/// false altfel.
		/// </param>
		/// <returns>
		/// Un nou tablou care contine aceleasi valori dar sortate prin selectie.
		/// </returns>
		public Tablou SortarePrinSelectie(Comparator trebuieInversate)
		{
			Tablou tablouSortat = (Tablou)Clone();

			for (int index = 0; index < tablouSortat.Count; index++)
			{
				int indexValoareInterschimbat = index;

				for (int indexCautat = index + 1; indexCautat < tablouSortat.Count; indexCautat++)
					if (trebuieInversate(tablouSortat[index], tablouSortat[indexCautat]))
						indexValoareInterschimbat = indexCautat;

				if (indexValoareInterschimbat != index)
				{
					int aux = tablouSortat[index];
					tablouSortat[index] = tablouSortat[indexValoareInterschimbat];
					tablouSortat[indexValoareInterschimbat] = aux;
				}
			}

			return tablouSortat;
		}

		/// <summary>
		/// Realizeaza o sortare prin interclasare.
		/// </summary>
		private IEnumerable<int> _MergeSort(IEnumerable<int> enumerable1, IEnumerable<int> enumerable2, Comparator trebuieInversate)
		{
			if (enumerable1.Skip(1).Any())
				enumerable1 = _MergeSort(enumerable1.Take(enumerable1.Count() / 2), enumerable1.Skip(enumerable1.Count() / 2), trebuieInversate);
			if (enumerable2.Skip(1).Any())
				enumerable2 = _MergeSort(enumerable2.Take(enumerable2.Count() / 2), enumerable2.Skip(enumerable2.Count() / 2), trebuieInversate);

			using (IEnumerator<int> enumerator1 = enumerable1.GetEnumerator(), enumerator2 = enumerable2.GetEnumerator())
			{
				bool poateAvansa1 = enumerator1.MoveNext(), poateAvansa2 = enumerator2.MoveNext();

				while (poateAvansa1 && poateAvansa2)
					if (trebuieInversate(enumerator1.Current, enumerator2.Current))
					{
						yield return enumerator2.Current;
						poateAvansa2 = enumerator2.MoveNext();
					}
					else
					{
						yield return enumerator1.Current;
						poateAvansa1 = enumerator1.MoveNext();
					}
				while (poateAvansa1)
				{
					yield return enumerator1.Current;
					poateAvansa1 = enumerator1.MoveNext();
				}
				while (poateAvansa2)
				{
					yield return enumerator2.Current;
					poateAvansa2 = enumerator2.MoveNext();
				}
			}
		}

		private readonly List<int> _valori;
	}
}