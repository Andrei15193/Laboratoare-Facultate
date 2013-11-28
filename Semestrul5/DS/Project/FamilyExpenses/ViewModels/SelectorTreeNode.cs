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
            if (selector != null)
                _selector = selector;
            else
                throw new ArgumentNullException("selector");
        }

        public IReadOnlyList<Purchase> GetPurchases()
        {
            if (_selector != null)
                return _selector().ToList();
            else
                throw new InvalidOperationException("selector cannot be null!");
        }

        private readonly Func<IEnumerable<Purchase>> _selector;
    }
}
