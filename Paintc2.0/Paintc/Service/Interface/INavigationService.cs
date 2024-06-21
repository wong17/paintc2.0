using Paintc.Core;

namespace Paintc.Service.Interface
{
    public interface INavigationService
    {
        ViewModel? CurrentView { get; }
        void NavigateTo<T>() where T : ViewModel;
    }
}
