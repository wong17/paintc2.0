using Paintc.Controller;
using Paintc.Core;
using Paintc.Service;
using Paintc.View;
using Paintc.ViewModels.UserControls;
using Paintc.Views;
using System.Windows;
using System.Windows.Controls;

namespace Paintc.ViewModels
{
    public class MainWindowViewModel : ViewModel
    {
        public RelayCommand CloseApplicationCommand { get; }
        public RelayCommand OpenAboutDialogCommand { get; }

        public MainWindowViewModel()
        {
            CloseApplicationCommand = new RelayCommand(obj => true, obj => { Application.Current.Shutdown(); });
            OpenAboutDialogCommand = new RelayCommand(obj => true, obj => { DialogService.OpenDialog<AboutWindow>(); });
        }

        //public MainWindowViewModel()
        //{
        //    InitController();
        //}

        //private void InitController()
        //{
        //    MainWindow.Loaded += Window_Loaded;
        //    MainWindow.AboutMenuItem.Click += MenuItem_Click;
        //    MainWindow.ExitMenuItem.Click += MenuItem_Click;
        //}

        //private void MenuItem_Click(object sender, RoutedEventArgs e)
        //{
        //    if (sender is not MenuItem menuItem)
        //        return;

        //    switch(menuItem.Name)
        //    {
        //        case "ExitMenuItem":
        //            Application.Current.Shutdown();
        //            break;
        //        case "AboutMenuItem":
        //            new AboutWindowViewModel().ShowAboutWindow(MainWindow);
        //            break;
        //        default: 
        //            break;
        //    }
        //}

        //private void Window_Loaded(object sender, RoutedEventArgs e)
        //{
        //    MainWindow.ucDrawingPanel.MainScrollViewer.Focus();
        //}
    }
}
