using Paintc.Enums;
using Paintc.Model;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media;

namespace Paintc.Service
{
    public static class ToolService
    {
        private static readonly ResourceDictionary? resourceDictionary;

        private static readonly DrawingImage? SelectImage, RectangleImage, EllipseImage, 
            PolygonImage, FillerImage, EraserImage, PencilImage, LineImage;

        static ToolService()
        {
            resourceDictionary = Application.Current.Resources.MergedDictionaries.ElementAt(3);

            SelectImage = (DrawingImage)resourceDictionary["selectDrawingImage"];
            RectangleImage = (DrawingImage)resourceDictionary["rectangleDrawingImage"];
            EllipseImage = (DrawingImage)resourceDictionary["circleDrawingImage"];
            PolygonImage = (DrawingImage)resourceDictionary["polygonDrawingImage"];
            FillerImage = (DrawingImage)resourceDictionary["fillerDrawingImage"];
            EraserImage = (DrawingImage)resourceDictionary["eraserDrawingImage"];
            PencilImage = (DrawingImage)resourceDictionary["pencilDrawingImage"];
            LineImage = (DrawingImage)resourceDictionary["lineDrawingImage"];
        }

        public static List<Tool> GetTools()
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
