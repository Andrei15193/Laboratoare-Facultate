using System;
using System.Collections.Generic;
using System.Linq;
namespace AlgoritmicaGrafelor.Laborator6.Taietura
{
	public static class Taietura
	{
		public static double CapacitateMinima(IReadOnlyList<IReadOnlyCollection<Tuple<int, double>>> graf)
		{
			if (graf == null)
				throw new ArgumentNullException("graf");
			IList<IList<Tuple<int, double>>> grafulEcarturilor = new List<IList<Tuple<int, double>>>(graf.Select(listaSuccesori => (IList<Tuple<int, double>>)listaSuccesori.ToList()));
			IReadOnlyCollection<int> varfuriFaraSuccesori = _DeterminaVarfuriFaraSuccesori(grafulEcarturilor);
			IReadOnlyCollection<int> varfuriFaraPredecesori = _DeterminaVarfuriFaraPredecesori(grafulEcarturilor);
			if (varfuriFaraSuccesori.Count != 1 || varfuriFaraPredecesori.Count != 1 || !_EConex(grafulEcarturilor))
				return double.NegativeInfinity;
			int sursa = varfuriFaraPredecesori.First();
			int destinatie = varfuriFaraSuccesori.First();
			double flux = 0;
			IReadOnlyList<Tuple<int, double>> drum = _DeterminaDrum(grafulEcarturilor, sursa, destinatie);
			do
			{
				double min = drum.Min(varf => varf.Item2);
				for (int indexVarf = 0, varfPrecedent = sursa; indexVarf < drum.Count; indexVarf++)
				{
					Tuple<int, double> varf = grafulEcarturilor[varfPrecedent].First(v => (v == drum[indexVarf]));
					double capacitateActuala = (varf.Item2 - min);
					grafulEcarturilor[varfPrecedent].Remove(varf);
					if (capacitateActuala > 0)
						grafulEcarturilor[varfPrecedent].Add(Tuple.Create(varf.Item1, capacitateActuala));
					varfPrecedent = drum[indexVarf].Item1;
				}
				flux += min;
				drum = _DeterminaDrum(grafulEcarturilor, sursa, destinatie);
			} while (drum.Count > 0);
			return flux;
		}
		private static IReadOnlyCollection<int> _DeterminaVarfuriFaraPredecesori(IList<IList<Tuple<int, double>>> graf)
		{
			ISet<int> multime = new HashSet<int>();
			for (int i = 0; i < graf.Count; i++)
				multime.Add(i);
			for (int i = 0; i < graf.Count; i++)
				multime = new HashSet<int>(multime.Except(graf[i].Select(succesor => succesor.Item1)));
			return multime.ToList();
		}
		private static IReadOnlyCollection<int> _DeterminaVarfuriFaraSuccesori(IList<IList<Tuple<int, double>>> graf)
		{
			ISet<int> multime = new HashSet<int>();
			for (int i = 0; i < graf.Count; i++)
				if (graf[i].Count == 0)
					multime.Add(i);
			return multime.ToList();
		}
		private static bool _EConex(IList<IList<Tuple<int, double>>> graf)
		{
			if (graf.Count == 0)
				return true;
			ISet<int> multime = new HashSet<int>(graf[0].Select(succesor => succesor.Item1));
			for (int i = 1; i < graf.Count; i++)
				multime = new HashSet<int>(multime.Union(graf[1].Select(succesor => succesor.Item1)));
			return (multime.Count(varf => (varf >= graf.Count)) == 0);
		}
		private static IReadOnlyList<Tuple<int, double>> _DeterminaDrum(IList<IList<Tuple<int, double>>> graf, int sursa, int destinatie)
		{
			List<Tuple<int, double>> drum = new List<Tuple<int, double>>();
			int candidat = sursa;
			while (candidat != destinatie &&
				   graf[candidat].Except(drum).FirstOrDefault() != null)
			{
				Tuple<int, double> ultimulVarf = graf[candidat].Except(drum).First();
				drum.Add(ultimulVarf);
				candidat = ultimulVarf.Item1;
			}
			if (drum.Count > 0 && drum.Last().Item1 != destinatie)
				drum.Clear();
			return drum;
		}
	}
}