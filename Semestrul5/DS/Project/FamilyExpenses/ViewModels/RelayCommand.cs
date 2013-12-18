using System;
using System.Windows.Input;
namespace FamilyExpenses.ViewModels
{
	internal sealed class RelayCommand
		: ICommand
	{
		public RelayCommand(Action<object> execute, bool canExecute = true)
		{
			if (execute == null)
				throw new ArgumentNullException("execute");
			_execute = execute;
			_canExecuteCommand = canExecute;
		}

		#region ICommand Members
		public bool CanExecute(object parameter)
		{
			return _canExecuteCommand;
		}
		public void Execute(object parameter)
		{
			_execute(parameter);
		}
		public bool CanExecuteCommand
		{
			get
			{
				return _canExecuteCommand;
			}
			set
			{
				if (value != _canExecuteCommand)
				{
					_canExecuteCommand = value;

					if (CanExecuteChanged != null)
						CanExecuteChanged(this, EventArgs.Empty);
				}
			}
		}

		public event EventHandler CanExecuteChanged;
		#endregion

		private bool _canExecuteCommand;
		private readonly Action<object> _execute;
	}
}