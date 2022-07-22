using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Game.ViewModel
{
    public class ButtonCommand : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        private Action<object> _action_param;
        private Func<bool> _func;

        public ButtonCommand(Action<object> action, Func<bool> func = null)
        {
            _action_param = action;
            _func = func;
        }

        public bool CanExecute(object parameter)
        {
            return _func == null || _func();
        }

        public void Execute(object parameter)
        {
            _action_param(parameter);
        }
    }
}
