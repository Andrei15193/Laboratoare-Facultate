using System;
using System.ComponentModel;
using System.Windows;

namespace Partea1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    internal partial class MainWindow
        : Window, INotifyPropertyChanged
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void _OnPropertyChanged(string propertyName)
        {
            if (propertyName != null)
            {
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
            else
                throw new ArgumentNullException("propertyName");
        }
    }
}
