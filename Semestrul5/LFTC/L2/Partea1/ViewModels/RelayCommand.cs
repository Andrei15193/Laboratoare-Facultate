using System;
using System.Windows.Input;

namespace Partea1.ViewModels
{
    /// <summary>
    /// Rip off Microsoft :]
    /// </summary>
    internal sealed class RelayCommand
        : ICommand
    {
        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            if (execute != null)
            {
                _execute = execute;
                _canExecute = canExecute;
            }
            else
                throw new ArgumentNullException("execute");
        }

        public bool CanExecute(object parameter)
        {
            bool canExecute = (_canExecute == null || _canExecute(parameter));

            if (!_previousCanExecute.HasValue || _previousCanExecute.Value != canExecute)
            {
                _previousCanExecute = canExecute;
                if (CanExecuteChanged != null)
                    CanExecuteChanged(this, EventArgs.Empty);
            }

            return canExecute;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            _execute(parameter);
        }

        private bool? _previousCanExecute;
        private readonly Action<object> _execute;
        private readonly Func<object, bool> _canExecute;
    }
}
