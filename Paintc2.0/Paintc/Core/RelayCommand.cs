using System.Windows.Input;

namespace Paintc.Core
{
    /// <summary>
    /// Creates a new command.
    /// </summary>
    /// <param name="canExecute">The execution status logic.</param>
    /// <param name="execute">The execution logic.</param>
    public class RelayCommand(Predicate<object?> canExecute, Action<object?> execute) : ICommand
    {
        private readonly Predicate<object?> _canExecute = canExecute;
        private readonly Action<object?> _execute = execute;

        ///<summary>
        ///Occurs when changes occur that affect whether or not the command should execute.
        ///</summary>
        public event EventHandler? CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        ///<summary>
        ///Defines the method that determines whether the command can execute in its current state.
        ///</summary>
        ///<param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to null.</param>
        ///<returns>
        ///true if this command can be executed; otherwise, false.
        ///</returns>
        public bool CanExecute(object? parameter)
        {
            return _canExecute(parameter);
        }

        ///<summary>
        ///Defines the method to be called when the command is invoked.
        ///</summary>
        ///<param name="parameter">Data used by the command. If the command does not require data to be passed, this object can be set to <see langword="null" />.</param>
        public void Execute(object? parameter)
        {
            _execute(parameter);
        }
    }
}
