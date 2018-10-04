namespace Vitae
{
    using System;
    using System.Windows.Input;

    public class RelayCommand : ICommand
    {
        ////////////////////
        //                //
        //     FIELDS     //
        //                //
        ////////////////////

        readonly Action<object> _execute;
        readonly Predicate<object> _canExecute;

        ////////////////////////
        //                    //
        //     PROPERTIES     //
        //                    //
        ////////////////////////

        public event EventHandler CanExecuteChanged 
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        ///////////////////////
        //                   //
        //     INTERFACE     //
        //                   //
        ///////////////////////

        public RelayCommand(Action<object> execute) : this(execute, null) 
        {
        }

        public RelayCommand(Action<object> execute, Predicate<object> canExecute) 
        {
            if (execute == null) throw new ArgumentNullException("execute");

            _execute = execute;
            _canExecute = canExecute;           
        }

        public bool CanExecute(object parameter) 
        {
            return _canExecute == null ? true : _canExecute(parameter);
        }

        public void Execute(object parameter) 
        {
            _execute(parameter);
        }

    }
}