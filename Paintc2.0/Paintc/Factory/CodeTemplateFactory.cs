using Paintc.Controller;
using Paintc.Core;
using Paintc.Resources.RTT;
using Paintc.Shapes.C;

namespace Paintc.Factory
{
    public static class CodeTemplateFactory
    {
        public static BaseTemplate? GetTemplate(SimpleShapeBase shape)
        {
            int backgroundColor = DrawingHandler.Instance.GetBackgroundColor();
            var settings = new CanvasSettings() { BackgroundColor = backgroundColor };

            if (shape is CRectangle rectangle)
                return new CRectangleTemplate() { rectangle = rectangle, settings = settings };
            else if (shape is CEllipse ellipse)
                return new CEllipseTemplate() { ellipse = ellipse, settings = settings };
            else if (shape is CLine line)
                return new CLineTemplate() { line = line, settings = settings };
            else if (shape is CPencil pencil)
                return new CPencilTemplate() { pencil = pencil, settings = settings };

            return null;
        }
    }
}
