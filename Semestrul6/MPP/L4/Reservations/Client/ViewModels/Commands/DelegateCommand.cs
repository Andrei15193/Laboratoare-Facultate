using System;
using System.Windows.Input;
namespace Reservations.ViewModels.Commands
{
	internal sealed class DelegateCommand
		: ICommand
	{
		internal DelegateCommand(Action<object> execute, bool canExecute = true)
		{
			if (execute == null)
				throw new ArgumentNullException("execute");

			_execute = execute;
			_canExecute = canExecute;
		}

		#region ICommand Members
		public void Execute(object parameter)
		{
			_execute(parameter);
		}
		public bool CanExecute(object parameter)
		{
			return _canExecute;
		}

		public event EventHandler CanExecuteChanged;
		#endregion
		internal bool CanExecuteCommand
		{
			get
			{
				return _canExecute;
			}
			set
			{
				if (value != _canExecute)
				{
					_canExecute = value;
					if (CanExecuteChanged != null)
						CanExecuteChanged(this, EventArgs.Empty);
				}
			}
		}

		private bool _canExecute;
		private readonly Action<object> _execute;
	}
}