using Microsoft.Win32;
using Paintc.Controller;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Paintc.Service
{
    public static class CanvasImageSaverService
    {
        /// <summary>
        ///
        /// </summary>
        public static void SaveCanvasContent()
        {
            var drawingArea = DrawingHandler.Instance.DrawingPanel?.CustomCanvas;
            if (drawingArea is null)
            {
                MessageBox.Show("An error ocurred while trying to saving the content.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Guardar la matriz de transformación actual
            Transform transform = drawingArea.LayoutTransform;
            // Resetear el estado actual en caso de que este escalada
            drawingArea.LayoutTransform = null;

            Size size = new(drawingArea.ActualWidth, drawingArea.ActualHeight);
            // Measure and arrange the surface
            // VERY IMPORTANT
            drawingArea.Measure(size);
            drawingArea.Arrange(new Rect(size));

            // Crear un mapa de bits con el canvas
            RenderTargetBitmap renderBitmap = new((int)size.Width, (int)size.Height, 96d, 96d, PixelFormats.Pbgra32);
            renderBitmap.Render(drawingArea);

            // Restaurar el layout anterior
            drawingArea.LayoutTransform = transform;

            // Convierte la imagen a formato compatible con 16 colores
            FormatConvertedBitmap formattedBitmap = new(renderBitmap, PixelFormats.Indexed4, BitmapPalettes.Halftone8, 0);

            // Guarda la imagen en formato BMP
            BitmapEncoder encoder = new BmpBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(formattedBitmap));

            var dialog = new SaveFileDialog
            {
                Filter = "Bitmap Image|*.bmp"
            };
            if (dialog.ShowDialog() == true)
            {
                using var fileStream = new FileStream(dialog.FileName, FileMode.Create);
                encoder.Save(fileStream);
            }
        }
    }
}