using System;
using System.Collections.Generic;
using System.Linq;

namespace AlgoritmicaGrafelor.Laborator4.Multimi
{
    public static class MultimiInteriorStabile
    {
        static public IReadOnlyList<IReadOnlyList<int>> Toate(IReadOnlyList<IReadOnlyList<int>> listaSuccesorilor)
        {
            if (listaSuccesorilor != null)
                return _DeterminaSubmultimi(_Varfuri(listaSuccesorilor)).Where(submultime => _EsteInteriorStabila(submultime, listaSuccesorilor))
                                                                        .ToList();
            else
                throw new ArgumentNullException("listaSuccesorilor");
        }

        static private IReadOnlyList<IReadOnlyList<int>> _DeterminaSubmultimi(IReadOnlyCollection<int> multime)
        {
            if (multime != null)
            {
                IEqualityComparer<IEnumerable<int>> listElementsEqualityComparer = new ListElementsEqualityComparer<int>();
                HashSet<IReadOnlyList<int>> submultimi = new HashSet<IReadOnlyList<int>>(listElementsEqualityComparer);
                HashSet<IReadOnlyList<int>> multimiRamase = new HashSet<IReadOnlyList<int>>(listElementsEqualityComparer) { multime.ToList() };

                do
                {
                    IReadOnlyList<int> multimeRamasa = multimiRamase.First();
                    multimiRamase.Remove(multimeRamasa);
                    submultimi.Add(multimeRamasa);

                    foreach (int element in multimeRamasa)
                        multimiRamase.Add(multimeRamasa.Except(new[] { element }).ToList());
                } while (multimiRamase.Count > 0);

                return submultimi.ToList();
            }
            else
                throw new ArgumentNullException("multime");
        }

        static private IReadOnlyList<int> _Varfuri(IReadOnlyList<IReadOnlyList<int>> listaSuccesorilor)
        {
            List<int> varfuri = new List<int>();

            for (int varf = 0; varf < listaSuccesorilor.Count; varf++)
                varfuri.Add(varf);

            return varfuri;
        }

        static private bool _EsteInteriorStabila(IReadOnlyList<int> multimeVarfuri, IReadOnlyList<IReadOnlyList<int>> listaSuccesorilor)
        {
            int varf = 0;

            while (varf < listaSuccesorilor.Count
                   && (!multimeVarfuri.Contains(varf)
                       || listaSuccesorilor[varf].Intersect(multimeVarfuri).Count() == 0))
                varf++;

            return (varf == listaSuccesorilor.Count);
        }

        private sealed class ListElementsEqualityComparer<T>
            : IEqualityComparer<IEnumerable<T>>
        {
            public bool Equals(IEnumerable<T> x, IEnumerable<T> y)
            {
                return x.SequenceEqual(y);
            }

            public int GetHashCode(IEnumerable<T> obj)
            {
                return obj.Aggregate(0, (currentHashCode, element) => (currentHashCode ^ element.GetHashCode()));
            }
        }

    }
}
