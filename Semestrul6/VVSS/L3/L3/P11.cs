using System;
using System.Collections.Generic;
using System.Linq;
namespace L3
{
	public static class P11
	{
		public static IEnumerable<int> MaxDistincte(IEnumerable<int> sir)
		{
			if (sir == null)
				throw new ArgumentNullException("sir");

			if (sir.Count() > 100)
				throw new ArgumentException("Lungimea nu poate fi mai mare decat 100!", "sir");
			if (sir.Any(element => (element < -30000 || 30000 < element)))
				throw new ArgumentException("Elementele trebuie sa fie intregi si in intervalul [-30 000, 30 000]!");

			List<int> rezultat = new List<int>();
			ICollection<int> temp = new List<int>();

			foreach (int element in sir)
				if (temp.Contains(element))
				{
					if (temp.Count > rezultat.Count)
					{
						rezultat.Clear();
						rezultat.AddRange(temp);
					}
					temp.Clear();
				}
				else
					temp.Add(element);

			if (temp.Count > rezultat.Count)
				return temp;
			else
				return rezultat;
		}
	}
}