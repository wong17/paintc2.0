using Paintc.Enums;
using Paintc.Model;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media;

namespace Paintc.Service
{
    public static class ToolService
    {
        private static readonly ResourceDictionary? imagesResource;

        private static readonly DrawingImage? SelectImage, RectangleImage, EllipseImage, 
            PolygonImage, FillerImage, EraserImage, PencilImage, LineImage;

        static ToolService()
        {
            // Resource in App.xaml
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
