using DateSemistructurate.Laborator1.Model;
using System.Collections.Generic;
using System.ComponentModel;

namespace DateSemistructurate.Laborator1.ViewModels
{
    internal class MainViewModel : INotifyPropertyChanged
    {
        public MainViewModel()
        {
            _rootCategory = new Category("Producatori");
            _allCategories.Add(_rootCategory);
            var nokia = new Category("Nokia", _rootCategory);
            var sony = new Category("Sony", _rootCategory);
            var lenovo = new Category("Lenovo", _rootCategory);
            var laptopuriLenovo = new Category("Laptopuri", lenovo);
            var telefoaneNokia = new Category("Telefoane", nokia);
            var telefoaneNokia3xyz = new Category("3xyz", telefoaneNokia);
            _allCategories.Add(nokia);
            _allCategories.Add(sony);
            _allCategories.Add(lenovo);
            _allCategories.Add(laptopuriLenovo);
            _allCategories.Add(new Category("Netbook", laptopuriLenovo));
            _allCategories.Add(new Category("Notebook", laptopuriLenovo));
            _allCategories.Add(new Category("Ultrabook", laptopuriLenovo));
            _allCategories.Add(new Category("Laptopuri", sony));
            _allCategories.Add(new Category("Audio", sony));
            _allCategories.Add(telefoaneNokia);
            _allCategories.Add(new Category("Lumia", telefoaneNokia));
            _allCategories.Add(telefoaneNokia3xyz);
            _allCategories.Add(new Category("3310", telefoaneNokia3xyz));
            _allCategories.Add(new Category("3410", telefoaneNokia3xyz));
            _selectedCategory = _rootCategory;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public Category RootCategory
        {
            get
            {
                return _rootCategory;
            }
        }

        public Category SelectedCategory
        {
            get
            {
                return _selectedCategory;
            }
            set
            {
                _selectedCategory = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("SelectedCategory"));
            }
        }

        private Category _selectedCategory;
        private readonly Category _rootCategory;
        private readonly IList<Category> _allCategories = new List<Category>();
    }
}
