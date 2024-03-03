using System.ComponentModel;
using System.Windows;
using System.Windows.Interop;
using System.Runtime.InteropServices;
using static Paintc.Interop.Win32Api;
using Paintc.Properties;
using Newtonsoft.Json;
using Paintc.Controller;

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
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);

            // Load window placement details for previous application session from application settings
            // Note - if window was closed on a monitor that is now disconnected from the computer,
            //        SetWindowPlacement will place the window onto a visible monitor.

            // Obtiene el valor del archivo user.config en C:\Users\...\AppData\Local\Paintc\...\...\user.config
            var saveWindowStateFlag = Convert.ToBoolean(Settings.Default.SaveWindowState);
            // Actualiza propiedades "SaveWindowState" de la ventana y "IsChecked" del checkbox por medio de un Binding
            MainWindowController.SetSaveWindowState(this, saveWindowStateFlag);
            if (saveWindowStateFlag)
            {
                WindowPlacement wp = JsonConvert.DeserializeObject<WindowPlacement>(Settings.Default.WindowPlacement);
                wp.length = Marshal.SizeOf(typeof(WindowPlacement));
                wp.flags = 0;
                wp.showCmd = (wp.showCmd == SW_SHOWMINIMIZED ? SW_SHOWNORMAL : wp.showCmd);
                SetWindowPlacement(new WindowInteropHelper(this).Handle, ref wp);
                return;
            }
            // Establece el tamaño y posición de la ventana por defecto
            Width = 1920;
            Height = 1080;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        // WARNING - Not fired when Application.SessionEnding is fired
        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            // Obtener valor de la propiedad SaveWindowState
            var saveWindowStateFlag = MainWindowController.GetSaveWindowState(this);
            if (saveWindowStateFlag)
            {
                // Persist window placement details to application settings
                GetWindowPlacement(new WindowInteropHelper(this).Handle, out WindowPlacement wp);
                Settings.Default.WindowPlacement = JsonConvert.SerializeObject(wp);
            }
            Settings.Default.SaveWindowState = saveWindowStateFlag;
            Settings.Default.Save();
        }
    }
}