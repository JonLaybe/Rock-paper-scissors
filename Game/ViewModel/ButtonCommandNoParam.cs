using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Game.ViewModel
{
    public class ButtonCommandNoParam : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        private Action _action;
        private Func<bool> _func;

        public ButtonCommandNoParam(Action action, Func<bool> func = null)
        {
            _action = action;
            _func = func;
        }

        public bool CanExecute(object parameter)
        {
            return _func == null || _func();
        }

        public void Execute(object parameter)
        {
            _action();
        }
    }
}
