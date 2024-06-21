using Microsoft.Extensions.DependencyInjection;
using Paintc.Core;
using Paintc.Service.Implement;
using Paintc.Service.Interface;
using Paintc.ViewModels;
using Paintc.ViewModels.UserControls;
using System.Windows;

namespace Paintc
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly ServiceProvider _serviceProvider;

        public App()
        {
            IServiceCollection services = new ServiceCollection();
            // ViewModels
            services.AddSingleton<MainWindowViewModel>();
            services.AddTransient<AboutWindowViewModel>();
            services.AddTransient<SourceCodeWindowViewModel>();

            services.AddSingleton<ToolboxPanelViewModel>();
            services.AddSingleton<StatusBarPanelViewModel>();
            services.AddSingleton<SourceCodePanelViewModel>();
            services.AddSingleton<DrawingPanelViewModel>();
            services.AddSingleton<PropertiesPanelViewModel>();

            // Navigation service
            services.AddSingleton<INavigationService, NavigationService>();
            services.AddSingleton<Func<Type, ViewModel>>(serviceProvider
                => viewModelType
                => (ViewModel)serviceProvider.GetRequiredService(viewModelType));
            // Window service
            services.AddSingleton<ViewModelLocator>();
            services.AddSingleton<WindowMapper>();
            services.AddSingleton<IWindowManager, WindowManager>();

            _serviceProvider = services.BuildServiceProvider();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            var windowManager = _serviceProvider.GetRequiredService<IWindowManager>();
            windowManager.ShowWindow(_serviceProvider.GetRequiredService<MainWindowViewModel>());

            base.OnStartup(e);
        }
    }
}