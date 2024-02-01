using Paintc.Core;

namespace Paintc.Service
{
    /* Para navegar a otras vistas */
    public interface INavigationService
    {
        ViewModel? CurrentView { get; }
        void NavigateTo<T>() where T : ViewModel;
    }
}
