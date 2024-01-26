using System.Windows.Input;

namespace Paintc.Command
{
    /* RelayCommand recibe por medio del constructor la lógica de cada comando/evento via delegados. */
    public class RelayCommand(Action<object?> execute, Func<object?, bool>? canExecute = null) : ICommand
    {
        /* Almacena la acción que se ejecutará cuando se invoque el comando. */
        private readonly Action<object?> _execute = execute;
        /* Almacena la función que determina si el comando puede ejecutarse. */
        private readonly Func<object?, bool>? _canExecute = canExecute;

        public event EventHandler? CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public bool CanExecute(object? parameter)
        {
            return _canExecute == null || _canExecute(parameter);
        }

        public void Execute(object? parameter)
        {
            _execute(parameter);
        }
    }
}
