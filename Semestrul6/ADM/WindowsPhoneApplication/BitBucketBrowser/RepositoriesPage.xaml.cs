using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
namespace BitBucketBrowser
{
	public partial class RepositoriesPage
		: PhoneApplicationPage, INotifyPropertyChanged
	{
		public RepositoriesPage()
		{
			InitializeComponent();
		}

		#region INotifyPropertyChanged Members
		public event PropertyChangedEventHandler PropertyChanged;
		#endregion
		public string Username
		{
			get
			{
				return _username;
			}
			private set
			{
				if (!string.Equals(_username, value, StringComparison.OrdinalIgnoreCase))
				{
					_username = value;

					OnUsernameChanged();
				}
			}
		}
		public BitBucket.Repository SelectedRepository
		{
			get
			{
				return ((App)App.Current).SelectedRepository;
			}
			set
			{
				((App)App.Current).SelectedRepository = value;
				OnSelectedRepositoryChagned();
			}
		}

		public IEnumerable<BitBucket.Repository> Repositories
		{
			get
			{
				return _repositories;
			}
		}

		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			base.OnNavigatedTo(e);

			string username;
			SelectedRepository = null;

			if (NavigationContext.QueryString.TryGetValue("username", out username))
			{
				Username = username;

				_SetRepositoriesAsync().ContinueWith(task => App.Log("_SetRepositories task compelted"));
			}
		}

		protected virtual void OnUsernameChanged()
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs("Username"));
		}
		protected virtual void OnSelectedRepositoryChagned()
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs("SelectedRepository"));

			if (SelectedRepository != null)
				NavigationService.Navigate(new Uri("/RepositoryPage.xaml", UriKind.Relative));
		}

		private Task _SetRepositoriesAsync()
		{
			return Task.Factory.StartNew(delegate
				{
					HttpWebRequest httpWebRequest = HttpWebRequest.CreateHttp(string.Format("https://bitbucket.org/api/2.0/repositories/{0}/", Username));
					httpWebRequest.Method = "GET";
					try
					{
						App.Log("Requesting " + httpWebRequest.RequestUri.AbsoluteUri);
						httpWebRequest.BeginGetResponse(result =>
														{
															try
															{
																WebResponse webResponse = httpWebRequest.EndGetResponse(result);
																App.Log("Got response " + webResponse.ResponseUri.AbsoluteUri);

																BitBucket.Repositories repositories;
																using (Stream webResponseStream = webResponse.GetResponseStream())
																	repositories = (BitBucket.Repositories)new DataContractJsonSerializer(typeof(BitBucket.Repositories)).ReadObject(webResponseStream);

																Dispatcher.BeginInvoke(() => _loadingTextBlock.Visibility = Visibility.Collapsed);
																foreach (BitBucket.Repository repository in repositories.values)
																{
																	if (string.IsNullOrEmpty(repository.language))
																		repository.language = null;
																	Dispatcher.BeginInvoke(() => _repositories.Add(repository));
																}
															}
															catch (Exception exception)
															{
																App.Log(exception.Message);
															}
														},
														null);
					}
					catch (Exception exception)
					{
						App.Log(exception.Message);
					}
				});
		}

		private string _username;
		private readonly ObservableCollection<BitBucket.Repository> _repositories = new ObservableCollection<BitBucket.Repository>();
	}
}