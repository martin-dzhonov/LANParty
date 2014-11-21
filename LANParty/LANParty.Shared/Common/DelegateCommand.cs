using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace LANParty.Common
{
    public class DelegateCommand : ICommand
    {
        private Action _execute;
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            this._execute();
        }

        public DelegateCommand(Action execute)
        {
            this._execute = execute;
        }
    }
}
