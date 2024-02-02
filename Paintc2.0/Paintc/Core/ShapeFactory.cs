using Paintc.Enums;
using Paintc.Model;

namespace Paintc.Core
{
    public static class ShapeFactory
    {
        public static ShapeBase? Create(ToolType toolType)
        {
            return toolType switch
            {
                ToolType.PolygonTool => new PolygonShape(),
                ToolType.PencilTool => new FreeShape(),
                ToolType.LineTool => new LineShape(),
                ToolType.RectangleTool => new RectangleShape(),
                ToolType.EllipseTool => new EllipseShape(),
                _ => null,
            };
        }
    }
}
