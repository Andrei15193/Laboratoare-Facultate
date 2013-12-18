using System;
using System.Collections.Generic;
using System.Linq;
using FamilyExpenses.Model;
namespace FamilyExpenses.ViewModels
{
	internal sealed class SelectorTreeNode
		 : TreeNode
	{
		public SelectorTreeNode(string header, Func<IEnumerable<Purchase>> selector)
			: base(header)
		{
			if (selector == null)
				throw new ArgumentNullException("selector");
			_selector = selector;
		}

		public IReadOnlyList<Purchase> GetPurchases()
		{
			if (_selector == null)
				throw new InvalidOperationException("selector cannot be null!");
			return _selector().ToList();
		}

		private readonly Func<IEnumerable<Purchase>> _selector;
	}
}