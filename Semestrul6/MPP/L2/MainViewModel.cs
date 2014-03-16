using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
namespace L2
{
	internal class MainViewModel
		: INotifyPropertyChanged
	{
		public MainViewModel()
		{
			_comandaDeSortare = new ComandaDeSortare(this);
			_comandaDeAdaugare = new ComandaDeAdaugare(this);
			_comandaDeEliminare = new ComandaDeEliminare(this);
		}

		#region INotifyPropertyChanged Members
		public event PropertyChangedEventHandler PropertyChanged;
		#endregion
		public int IndexSelectat
		{
			get
			{
				return _indexSelectat;
			}
			set
			{
				_indexSelectat = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("IndexSelectat"));
				PoateElimina = (0 <= _indexSelectat && _indexSelectat < _tablou.Count);
			}
		}
		public bool PoateElimina
		{
			get
			{
				return _poateElimina;
			}
			set
			{
				_poateElimina = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("PoateElimina"));
			}
		}
		public Tablou Sursa
		{
			get
			{
				return _tablou;
			}
			private set
			{
				_tablou = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("Sursa"));
			}
		}
		public Tablou Sortat
		{
			get
			{
				return _sortat;
			}
			private set
			{
				_sortat = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("Sortat"));
			}
		}
		public MetodaDeSortare MetodaDeSortare
		{
			get;
			set;
		}
		public TipSortare TipSortare
		{
			get;
			set;
		}
		public ICommand ComandaPentruSortare
		{
			get
			{
				return _comandaDeSortare;
			}
		}
		public ICommand ComandaPentruAdaugare
		{
			get
			{
				return _comandaDeAdaugare;
			}
		}
		public ICommand ComandaPentruEliminare
		{
			get
			{
				return _comandaDeEliminare;
			}
		}

		private bool _poateElimina;
		private int _indexSelectat;
		private Tablou _tablou = new Tablou(new[] { 5, 4, 3, 2, 1 });
		private Tablou _sortat = new Tablou();
		private readonly ICommand _comandaDeSortare;
		private readonly ICommand _comandaDeAdaugare;
		private readonly ICommand _comandaDeEliminare;

		private sealed class ComandaDeSortare
			: ICommand
		{
			public ComandaDeSortare(MainViewModel mainViewModel)
			{
				if (mainViewModel == null)
					throw new ArgumentNullException("mainViewModel");
				_mainViewModel = mainViewModel;
			}

			#region ICommand Members
			public void Execute(object parameter)
			{
				switch (_mainViewModel.MetodaDeSortare)
				{
					case MetodaDeSortare.Interclasare:
						if (_mainViewModel.TipSortare == TipSortare.Ascendenta)
							_mainViewModel.Sortat = _mainViewModel.Sursa.SortarePrinInterclasare(_TrebuieInversateAscendent);
						else
							_mainViewModel.Sortat = _mainViewModel.Sursa.SortarePrinInterclasare(_TrebuieInversateDescendent);
						break;
					case MetodaDeSortare.Numarare:
						if (_mainViewModel.TipSortare == TipSortare.Ascendenta)
							_mainViewModel.Sortat = _mainViewModel.Sursa.SortarePrinNumarare(_TrebuieInversateAscendent);
						else
							_mainViewModel.Sortat = _mainViewModel.Sursa.SortarePrinNumarare(_TrebuieInversateDescendent);
						break;
					case MetodaDeSortare.Selectie:
						if (_mainViewModel.TipSortare == TipSortare.Ascendenta)
							_mainViewModel.Sortat = _mainViewModel.Sursa.SortarePrinSelectie(_TrebuieInversateAscendent);
						else
							_mainViewModel.Sortat = _mainViewModel.Sursa.SortarePrinSelectie(_TrebuieInversateDescendent);
						break;
					default:
					case MetodaDeSortare.Insertie:
						if (_mainViewModel.TipSortare == TipSortare.Ascendenta)
							_mainViewModel.Sortat = _mainViewModel.Sursa.SortarePrinInsertie(_TrebuieInversateAscendent);
						else
							_mainViewModel.Sortat = _mainViewModel.Sursa.SortarePrinInsertie(_TrebuieInversateDescendent);
						break;
				}
			}
			public bool CanExecute(object parameter)
			{
				return true;
			}
			public event EventHandler CanExecuteChanged;
			#endregion

			private static bool _TrebuieInversateAscendent(int primul, int alDoilea)
			{
				return !(primul <= alDoilea);
			}
			private static bool _TrebuieInversateDescendent(int primul, int alDoilea)
			{
				return !(primul >= alDoilea);
			}
			private readonly MainViewModel _mainViewModel;
		}
		private sealed class ComandaDeAdaugare
			: ICommand
		{
			public ComandaDeAdaugare(MainViewModel mainViewModel)
			{
				if (mainViewModel == null)
					throw new ArgumentNullException("mainViewModel");
				_mainViewModel = mainViewModel;
			}

			#region ICommand Members
			public void Execute(object parameter)
			{
				int numar;

				if (parameter != null && int.TryParse(parameter.ToString(), out numar))
					_mainViewModel.Sursa = new Tablou(_mainViewModel.Sursa.Concat(new[] { numar }));
			}
			public bool CanExecute(object parameter)
			{
				return true;
			}
			public event EventHandler CanExecuteChanged;
			#endregion

			private readonly MainViewModel _mainViewModel;
		}
		private sealed class ComandaDeEliminare
			: ICommand
		{
			public ComandaDeEliminare(MainViewModel mainViewModel)
			{
				if (mainViewModel == null)
					throw new ArgumentNullException("mainViewModel");
				_mainViewModel = mainViewModel;
			}

			#region ICommand Members
			public void Execute(object parameter)
			{
				if (0 <= _mainViewModel.IndexSelectat && _mainViewModel.IndexSelectat < _mainViewModel.Sursa.Count)
				{
					Tablou tablouNow = new Tablou(_mainViewModel.Sursa.Count - 1);

					for (int index = 0; index < _mainViewModel.IndexSelectat; index++)
						tablouNow[index] = _mainViewModel.Sursa[index];
					for (int index = _mainViewModel.IndexSelectat + 1; index < _mainViewModel.Sursa.Count; index++)
						tablouNow[index - 1] = _mainViewModel.Sursa[index];

					_mainViewModel.Sursa = tablouNow;
				}
			}
			public bool CanExecute(object parameter)
			{
				return true;
			}
			public event EventHandler CanExecuteChanged;
			#endregion

			private readonly MainViewModel _mainViewModel;
		}
	}
}