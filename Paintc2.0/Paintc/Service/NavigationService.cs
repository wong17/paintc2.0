using Paintc.Core;

namespace Paintc.Service
{
    /// <summary>
    /// A function which returns the specified ViewModel so that it can be navigated to.
    /// </summary>
    /// <param name="viewModelFactory"></param>
    public class NavigationService(Func<Type, ViewModel> viewModelFactory) : ObservableObject, INavigationService 
    {
        /// <summary>
        /// Returns the specified viewmodel type
        /// </summary>
        private Func<Type, ViewModel> _viewModelFactory = viewModelFactory;
        private ViewModel? _CurrentView;
        public ViewModel? CurrentView
        {
            get => _CurrentView;
            private set
            {
                _CurrentView = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Navigate to the specified ViewModel
        /// </summary>
        /// <typeparam name="T">The ViewModel</typeparam>
        public void NavigateTo<T>() where T : ViewModel
        {
            ViewModel viewmodel = _viewModelFactory.Invoke(typeof(T));
            CurrentView = viewmodel;
        }
    }
}
