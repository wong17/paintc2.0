using Microsoft.Extensions.DependencyInjection;
using Paintc.Controller;
using Paintc.Core;
using Paintc.Service;
using Paintc.ViewModels;
using Paintc.ViewModels.UserControls;
using Paintc.Views;
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
            /* Para inyectar el ViewModel. */
            services.AddSingleton(provider => new MainWindow()
            {
                DataContext = provider.GetRequiredService<MainWindowViewModel>()
            });
            services.AddSingleton<MainWindowViewModel>();
            services.AddSingleton<DrawingPanelViewModel>();
            services.AddSingleton<ToolboxPanelViewModel>();
            services.AddSingleton<AboutWindowViewModel>();
            services.AddSingleton<INavigationService, NavigationService>();
            /* Devuelve el ViewModel especificado. */
            services.AddSingleton<Func<Type, ViewModel>>(provider => (viewmodelType) => (ViewModel) provider.GetRequiredService(viewmodelType));

            _serviceProvider = services.BuildServiceProvider();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            var mainwindow = _serviceProvider.GetRequiredService<MainWindow>();
            mainwindow.Show();
            base.OnStartup(e);
        }
    }

}
