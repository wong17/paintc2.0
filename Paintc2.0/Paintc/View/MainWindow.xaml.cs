using Paintc.Controller;
using System.Windows;

namespace Paintc.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            _ = new MainWindowController(this);
        }
    }
}