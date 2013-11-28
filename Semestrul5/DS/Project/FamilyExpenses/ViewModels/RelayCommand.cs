using System;
using System.Windows.Input;

namespace FamilyExpenses.ViewModels
{
    internal sealed class RelayCommand
        : ICommand
    {
        public RelayCommand(Action<object> execute, bool canExecute = true)
        {
            if (execute != null)
            {
                _execute = execute;
                _canExecuteCommand = canExecute;
            }
            else
                throw new ArgumentNullException("execute");
        }
                                        
        #region ICommand Members

        public bool CanExecute(object parameter)
        {
            return _canExecuteCommand;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            if (_execute != null)
                _execute(parameter);
            else
                throw new InvalidOperationException("execute delegate cannot be null!");
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

        #endregion

        private bool _canExecuteCommand;
        private readonly Action<object> _execute;
    }
}
