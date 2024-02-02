using Paintc.Core;
using Paintc.Enums;
using Paintc.Shapes;

namespace Paintc.Factory
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
