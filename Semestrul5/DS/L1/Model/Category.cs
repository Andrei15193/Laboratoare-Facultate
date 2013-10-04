using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DateSemistructurate.Laborator1.Model
{
    internal class Category
    {
        public Category(string name, Category parent = null)
        {
            if (name != null)
                if (!string.IsNullOrWhiteSpace(name))
                {
                    _name = name;
                    _readonlySubcategories = new ReadOnlyCollection<Category>(_subcategories);
                    if (parent != null)
                        parent._subcategories.Add(this);
                }
                else
                    throw new ArgumentException("No name provided!");
            else
                throw new ArgumentNullException("name");
        }

        public string Name
        {
            get
            {
                return _name;
            }
        }

        public IReadOnlyCollection<Category> Subcategories
        {
            get
            {
                return _readonlySubcategories;
            }
        }

        private readonly string _name;
        private readonly IReadOnlyCollection<Category> _readonlySubcategories;
        private readonly IList<Category> _subcategories = new List<Category>();
    }
}
