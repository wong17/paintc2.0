﻿using Paintc.Core;
using Paintc.Enums;
using Paintc.Shapes.CSClasses;
using System.Windows.Media;

namespace Paintc.Factory
{
    public static class ShapeFactory
    {
        public static ShapeBase? Create(ToolType toolType, string? autoGeneratedName, Color color)
        {
            return toolType switch
            {
                ToolType.PolygonTool => new PolygonShape(autoGeneratedName, color),
                ToolType.PencilTool => new FreeShape(autoGeneratedName, color),
                ToolType.LineTool => new LineShape(autoGeneratedName, color),
                ToolType.RectangleTool => new RectangleShape(autoGeneratedName, color),
                ToolType.EllipseTool => new EllipseShape(autoGeneratedName, color),
                _ => null,
            };
        }
    }
}