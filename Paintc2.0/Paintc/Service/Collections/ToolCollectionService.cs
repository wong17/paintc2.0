using Paintc.Enums;
using Paintc.Model;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media;

namespace Paintc.Service.Collections
{
    public static class ToolCollectionService
    {
        private static readonly ResourceDictionary? imagesResource;

        private static readonly DrawingImage? SelectImage, RectangleImage, EllipseImage,
            PolygonImage, FillerImage, EraserImage, PencilImage, LineImage;

        static ToolCollectionService()
        {
            /* Resource in App.xaml (se obtienen en tiempo de ejecución, en modo de diseño xaml da un error de
               Index was out of range. Must be non-negative and less than the size of the collection. (Parameter 'index')
               aunque funciona al ejecutar.
             */
            //imagesResource = Application.Current.Resources.MergedDictionaries.ElementAt(3);

            imagesResource = new ResourceDictionary
            {
                Source = new Uri("/Paintc;component/Resources/Images/Images.xaml", UriKind.RelativeOrAbsolute)
            };

            SelectImage = (DrawingImage)imagesResource["selectDrawingImage"];
            RectangleImage = (DrawingImage)imagesResource["rectangleDrawingImage"];
            EllipseImage = (DrawingImage)imagesResource["circleDrawingImage"];
            PolygonImage = (DrawingImage)imagesResource["polygonDrawingImage"];
            FillerImage = (DrawingImage)imagesResource["fillerDrawingImage"];
            EraserImage = (DrawingImage)imagesResource["eraserDrawingImage"];
            PencilImage = (DrawingImage)imagesResource["pencilDrawingImage"];
            LineImage = (DrawingImage)imagesResource["lineDrawingImage"];
        }

        public static ObservableCollection<Tool> GetTools()
        {
            return [
                new Tool(ToolType.SelectTool, "Select", SelectImage),
                new Tool(ToolType.RectangleTool, "Rectangle", RectangleImage),
                new Tool(ToolType.EllipseTool, "Ellipse", EllipseImage),
                new Tool(ToolType.PolygonTool, "Polygon", PolygonImage),
                new Tool(ToolType.FillerTool, "Filler", FillerImage),
                new Tool(ToolType.EraserTool, "Eraser", EraserImage),
                new Tool(ToolType.PencilTool, "Pencil", PencilImage),
                new Tool(ToolType.LineTool, "Line", LineImage)];
        }
    }
}