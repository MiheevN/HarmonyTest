using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfHarmony.ViewModel
{
    public class ButtonCommand : ICommand
    {
        private Action Execute { get; set; }
        private Func<bool> Can { get; set; }

        event EventHandler ICommand.CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }

            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }

        public ButtonCommand(Action execute, Func<bool> can = null)
        {
            Execute = execute;
            if (can != null)
            {
                Can = can;
            }
        }

        bool ICommand.CanExecute(object parameter)
        {
            if (Can == null)
            {
                return true;
            }
            return Can();
        }

        void ICommand.Execute(object parameter)
        {
            Execute?.Invoke();
        }
    }
}
