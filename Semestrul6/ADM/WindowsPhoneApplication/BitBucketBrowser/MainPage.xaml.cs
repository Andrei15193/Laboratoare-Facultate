using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Windows.Storage;
namespace BitBucketBrowser
{
	public partial class MainPage
		: PhoneApplicationPage
	{
		public MainPage()
		{
			InitializeComponent();
			_LoadUsernamesAsync().ContinueWith(task => App.Log("Compelted SetUsernamesAsync task"));
		}

		public IEnumerable<string> Usernames
		{
			get
			{
				return _usernames;
			}
		}

		private async void _BrowseButtonClickAsync(object sender, RoutedEventArgs e)
		{
			if (string.IsNullOrWhiteSpace(_usernameTextBox.Text))
			{
				App.Log("Displaying username error message");
				Dispatcher.BeginInvoke(() => _usernameErrorTextBlock.Visibility = Visibility.Visible);
			}
			else
				try
				{
					App.Log("Hiding username error message");
					_usernameErrorTextBlock.Visibility = Visibility.Collapsed;

					App.Log("Getting Usernames StroageFile instance");
					StorageFile usernamesFile = await ApplicationData.Current.LocalFolder.CreateFileAsync("Usernames.txt", CreationCollisionOption.OpenIfExists);
					App.Log("Got Usernames StroageFile instance");

					App.Log("Checking if username already exists in usernames list");
					if (_usernameSet.Add(_usernameTextBox.Text))
					{
						_usernames.Add(_usernameTextBox.Text);
						App.Log("Added new username to usernames list");

						App.Log("Persisting usernames");
						using (StreamWriter usernamesFileStream = new StreamWriter(await usernamesFile.OpenStreamForWriteAsync()))
							usernamesFileStream.Write(string.Join("\n", _usernames));
						App.Log("Persisted usernames");
					}

					NavigationService.Navigate(new Uri("/RepositoriesPage.xaml?username=" + _usernameTextBox.Text, UriKind.Relative));
				}
				catch (Exception exception)
				{
					App.Log(exception.Message);
				}
		}
		private async Task _LoadUsernamesAsync()
		{
			try
			{
				App.Log("Getting Usernames StroageFile instance");
				StorageFile usernamesFile = await ApplicationData.Current.LocalFolder.CreateFileAsync("Usernames.txt", CreationCollisionOption.OpenIfExists);
				App.Log("Got Usernames StroageFile instance");

				App.Log("Reading usernames from StorageFile");
				using (StreamReader usernamesFileStream = new StreamReader(await usernamesFile.OpenStreamForReadAsync()))
					foreach (string username in from username in usernamesFileStream.ReadToEnd().Split('\n')
												where !string.IsNullOrWhiteSpace(username)
												select username.Trim())
						if (_usernameSet.Add(username))
							Dispatcher.BeginInvoke(() => _usernames.Add(username));
				App.Log("Read usernames from StorageFile totaling: " + Usernames.Count());
			}
			catch (Exception exception)
			{
				App.Log(exception.Message);
			}
		}

		private void _UsernameListBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (_usernameListBox.SelectedItem != null)
				_usernameTextBox.Text = (string)_usernameListBox.SelectedItem;
		}
		private void _UsernameTextBoxTextChanged(object sender, TextChangedEventArgs e)
		{
			App.Log("_usernameTextBox Texthanged riased");
			if (_usernameListBox.SelectedItem != null && !string.Equals((string)_usernameListBox.SelectedItem, _usernameTextBox.Text, StringComparison.OrdinalIgnoreCase))
				_usernameListBox.SelectedItem = null;
		}
		private async void _ClearUsernamesButtonClickAsync(object sender, RoutedEventArgs e)
		{
			try
			{
				App.Log("Deleting Usernames file");
				await (await ApplicationData.Current.LocalFolder.GetFileAsync("Usernames.txt")).DeleteAsync(StorageDeleteOption.PermanentDelete);
				App.Log("Deleted Usernames file");

				App.Log("Clearing usernames collection");
				Dispatcher.BeginInvoke(delegate
					{
						_usernameSet.Clear();
						_usernames.Clear();
					});
				App.Log("Cleared usernames collection");
			}
			catch (Exception exception)
			{
				App.Log(exception.Message);
			}
		}

		private readonly ISet<string> _usernameSet = new SortedSet<string>(StringComparer.OrdinalIgnoreCase);
		private readonly ObservableCollection<string> _usernames = new ObservableCollection<string>();
	}
}