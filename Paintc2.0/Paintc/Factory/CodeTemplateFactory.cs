using Paintc.Core;
using Paintc.Resources.RTT;
using Paintc.Shapes.C;

namespace Paintc.Factory
{
    public static class CodeTemplateFactory
    {
        public static BaseTemplate? GetTemplate(SimpleShapeBase shape)
        {
            if (shape is CRectangle rectangle)
                return new CRectangleTemplate() { rectangle = rectangle, settings = new CanvasSettings() };
            else if (shape is CEllipse ellipse)
                return new CEllipseTemplate() { ellipse = ellipse, settings = new CanvasSettings() };
            else if (shape is CLine line)
                return new CLineTemplate() { line = line, settings = new CanvasSettings() };

            return null;
        }
    }
}
