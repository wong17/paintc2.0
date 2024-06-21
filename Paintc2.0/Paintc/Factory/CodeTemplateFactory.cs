using Paintc.ViewModels;
using Paintc.Core;
using Paintc.Resources.RTT;
using Paintc.Shapes.C;

namespace Paintc.Factory
{
    public static class CodeTemplateFactory
    {
        /* Asocia una figura primitiva a un template */
        private static readonly Dictionary<Type, Func<SimpleShapeBase, BaseTemplate>> SourceCodeTemplates = new()
        {
            { typeof(CRectangle), shape => new CRectangleTemplate { rectangle = (CRectangle)shape, settings = CreateCanvasSettings() } },
            { typeof(CEllipse), shape => new CEllipseTemplate { ellipse = (CEllipse)shape, settings = CreateCanvasSettings() } },
            { typeof(CLine), shape => new CLineTemplate { line = (CLine)shape, settings = CreateCanvasSettings() } },
            { typeof(CPencil), shape => new CPencilTemplate { pencil = (CPencil)shape, settings = CreateCanvasSettings() } },
            { typeof(CPoly), shape => new CPolyTemplate { poly = (CPoly)shape, settings = CreateCanvasSettings() } }
        };

        /// <summary>
        /// Devuelve la plantilla de código asociada a la figura
        /// </summary>
        /// <param name="shape"></param>
        /// <returns></returns>
        public static BaseTemplate? GetTemplate(SimpleShapeBase shape)
        {
            if (SourceCodeTemplates.TryGetValue(shape.GetType(), out var creator))
                return creator(shape);

            return null;
        }

        private static CanvasSettings CreateCanvasSettings()
        {
            int backgroundColor = DrawingHandler.Instance.GetBackgroundColor();
            return new CanvasSettings { BackgroundColor = backgroundColor };
        }
    }
}
