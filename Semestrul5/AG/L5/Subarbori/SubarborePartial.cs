using System;
using System.Collections.Generic;
using System.Linq;
namespace AlgoritmicaGrafelor.Laborator5.Subarbori
{
	public static class SubarborePartial
	{
		static public IReadOnlyCollection<Tuple<int, int, double>> Prim(IReadOnlyCollection<Tuple<int, int, double>> multimeaMuchiilor)
		{
			if (multimeaMuchiilor == null)
				throw new ArgumentNullException("muchii");
			if (multimeaMuchiilor.Count == 0)
				return new Tuple<int, int, double>[0];
			IReadOnlyCollection<int> varfuri = multimeaMuchiilor.SelectMany(muchie => new[] { muchie.Item1, muchie.Item2 })
														  .Distinct()
														  .ToList();
			ISet<int> varfuriVizitate = new HashSet<int> { varfuri.First() };
			ISet<Tuple<int, int, double>> muchiiVizitate = new HashSet<Tuple<int, int, double>>();
			while (varfuriVizitate.Count != varfuri.Count)
			{
				IList<Tuple<int, int, double>> muchii = new List<Tuple<int, int, double>>();
				foreach (int varf in varfuriVizitate)
				{
					Tuple<int, int, double> muchieAdiacentaDeValoareMinima =
						multimeaMuchiilor.Where(m => (m.Item1 == varf || m.Item2 == varf)
													  && varfuriVizitate.Contains(m.Item1) != varfuriVizitate.Contains(m.Item2))
										 .OrderBy(m => m.Item3)
										 .FirstOrDefault();
					if (muchieAdiacentaDeValoareMinima != null)
						muchii.Add(muchieAdiacentaDeValoareMinima);
				}
				Tuple<int, int, double> muchie = muchii.OrderBy(m => m.Item3).First();
				varfuriVizitate.Add(muchie.Item1);
				varfuriVizitate.Add(muchie.Item2);
				muchiiVizitate.Add(muchie);
			}
			return muchiiVizitate.ToList();
		}
	}
}