using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace AlgoritmicaGrafelor.Laborator6.Taietura.Tests
{
	[TestClass]
	public class CapacitateMinima
	{
		IReadOnlyList<IReadOnlyCollection<Tuple<int, double>>> _grafCuDouaVarfuri = new[]
		{
			(IReadOnlyCollection<Tuple<int, double>>)(new []{ Tuple.Create(1, 1d) }),
			(IReadOnlyCollection<Tuple<int, double>>)new Tuple<int, double>[0]
		};
		IReadOnlyList<IReadOnlyCollection<Tuple<int, double>>> _grafCuPatruVarfuriSiDouaDrumuriDeLaSursaLadestinatie = new[]
		{
			(IReadOnlyCollection<Tuple<int, double>>)(new []{ Tuple.Create(1, 1d), Tuple.Create(2, 2d) }),
			(IReadOnlyCollection<Tuple<int, double>>)(new []{ Tuple.Create(3, 3d) }),
			(IReadOnlyCollection<Tuple<int, double>>)(new []{ Tuple.Create(3, 3d) }),
			(IReadOnlyCollection<Tuple<int, double>>)new Tuple<int, double>[0]
		};
		IReadOnlyList<IReadOnlyCollection<Tuple<int, double>>> _grafCuCinciVarfuriSiCircuit = new[]
		{
			(IReadOnlyCollection<Tuple<int, double>>)(new []{ Tuple.Create(1, 1d) }),
			(IReadOnlyCollection<Tuple<int, double>>)(new []{ Tuple.Create(2, 2d) }),
			(IReadOnlyCollection<Tuple<int, double>>)(new []{ Tuple.Create(3, 3d) }),
			(IReadOnlyCollection<Tuple<int, double>>)(new []{ Tuple.Create(1, 1d), Tuple.Create(4, 4d) }),
			(IReadOnlyCollection<Tuple<int, double>>)new Tuple<int, double>[0]
		};
		[TestMethod]
		public void TestGrafCuDouaVarfuri()
		{
			Assert.AreEqual(1d, Taietura.CapacitateMinima(_grafCuDouaVarfuri));
		}
		[TestMethod]
		public void TestGrafCuPatruVarfuriSiDouaDrumuriDeLaSursaLadestinatie()
		{
			Assert.AreEqual(3d, Taietura.CapacitateMinima(_grafCuPatruVarfuriSiDouaDrumuriDeLaSursaLadestinatie));
		}
		[TestMethod]
		public void TestGrafCuCinciVarfuriSiCircuit()
		{
			Assert.AreEqual(1d, Taietura.CapacitateMinima(_grafCuCinciVarfuriSiCircuit));
		}
	}
}
