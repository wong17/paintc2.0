using Paintc.Core;
using Paintc.ViewModels;
using Paintc.Views;
using System.Windows;

namespace Paintc.Service.Implement
{
    public class WindowMapper
    {
        // Diccionario que relaciona una ventana con su viewmodel
        // key: ViewModel, value: Window
        private readonly Dictionary<Type, Type> _mappings = [];

        public WindowMapper()
        {
            RegisterMapping<MainWindowViewModel, MainWindow>();
            RegisterMapping<AboutWindowViewModel, AboutWindow>();
            RegisterMapping<SourceCodeWindowViewModel, SourceCodeWindow>();
        }

        // Registrar una ventana y su viewmodel
        public void RegisterMapping<TViewModel, TWindow>() where TViewModel : ViewModel where TWindow : Window
        {
            _mappings.Add(typeof(TViewModel), typeof(TWindow));
        }

        // Devuelve la ventana asociada al viewmodel que recibe como parámetro
        public Type? GetWindowTypeForViewModel(Type viewModelType)
        {
            _mappings.TryGetValue(viewModelType, out var windowType);
            return windowType;
        }
    }
}
