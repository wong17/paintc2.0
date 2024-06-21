using Paintc.Core;
using Paintc.Service.Interface;

namespace Paintc.Service.Implement
{
    public class NavigationService(Func<Type, ViewModel> viewModelFactory) : ObservableObject, INavigationService
    {
        private ViewModel? _currentView;

        public ViewModel? CurrentView
        {
            get => _currentView;
            private set
            {
                SetField(ref _currentView, value);
            }
        }

        public readonly Func<Type, ViewModel> _ViewModelFactory = viewModelFactory;

        public void NavigateTo<T>() where T : ViewModel
        {
            var viewModel = _ViewModelFactory.Invoke(typeof(T));
            CurrentView = viewModel;
        }
    }
}
