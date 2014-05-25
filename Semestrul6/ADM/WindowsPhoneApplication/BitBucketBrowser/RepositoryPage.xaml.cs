using System.ComponentModel;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
namespace BitBucketBrowser
{
	public partial class RepositoryPage
		: PhoneApplicationPage, INotifyPropertyChanged
	{
		public RepositoryPage()
		{
			InitializeComponent();
		}

		#region INotifyPropertyChanged Members
		public event PropertyChangedEventHandler PropertyChanged;
		#endregion
		public BitBucket.Repository Repository
		{
			get
			{
				return _repository;
			}
			set
			{
				if (_repository != value)
				{
					_repository = value;
					OnRepositoryChanged();
				}
			}
		}

		protected virtual void OnRepositoryChanged()
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs("Repository"));
		}

		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			base.OnNavigatedTo(e);
			Repository = ((App)App.Current).SelectedRepository;
			((App)App.Current).SelectedRepository = null;
		}

		private BitBucket.Repository _repository;
	}
}