using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CrudApplication.Commands
{
    public class RelayCommand : ICommand
    {

        private Action<object> _executeMethod;
        private Func<object, bool> _canExecuteMethod;

        public RelayCommand(Action<object> executeMethod , Func<object, bool> canExecuteMethod)
        {
            _executeMethod = executeMethod;
            _canExecuteMethod = canExecuteMethod;
        }

        public bool CanExecute(object parameter)
        {
            if(_canExecuteMethod != null)
            {
               return _canExecuteMethod(parameter);
            }

            else
            {
                return false;
            }
        }

        public void Execute(object parameter)
        {
            _executeMethod(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}
