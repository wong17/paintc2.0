using System.Runtime.InteropServices;

namespace Paintc.Interop
{
    public static class Win32Api
    {
        #region Constants

        public const int
            SW_SHOWNORMAL = 1,
            SW_SHOWMINIMIZED = 2;

        #endregion

        #region Structures

        [StructLayout(LayoutKind.Sequential)]
        public struct Point(int x, int y)
        {
            public int X = x;
            public int Y = y;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct Rect(int left, int top, int right, int bottom)
        {
            public int Left = left;
            public int Top = top;
            public int Right = right;
            public int Bottom = bottom;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct WindowPlacement
        {
            public int length;
            public int flags;
            public int showCmd;
            public Point minPosition;
            public Point maxPosition;
            public Rect normalPosition;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Establece el estado de la ventana al mostrarse, maximizada, minimizada y su ubicación especifica 
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="lpwndpl">Puntero a estructura WINDOWPLACEMENT</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern bool SetWindowPlacement(IntPtr hWnd, [In] ref WindowPlacement lpwndpl);

        /// <summary>
        /// Obtiene el estado con el que debe de mostrarse la ventana y su ubicación especifica
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="lpwndpl">Puntero a estructura WINDOWPLACEMENT</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern bool GetWindowPlacement(IntPtr hWnd, out WindowPlacement lpwndpl);

        #endregion
    }
}
