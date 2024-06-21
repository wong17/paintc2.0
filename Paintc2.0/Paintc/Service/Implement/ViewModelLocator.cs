using Microsoft.Extensions.DependencyInjection;
using Paintc.ViewModels;

namespace Paintc.Service.Implement
{
    public class ViewModelLocator
    {
        private readonly IServiceProvider _serviceProvider;

        public MainWindowViewModel MainWindowViewModel { get; set; }

        public ViewModelLocator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;

            MainWindowViewModel = _serviceProvider.GetRequiredService<MainWindowViewModel>();
        }

    }
}
