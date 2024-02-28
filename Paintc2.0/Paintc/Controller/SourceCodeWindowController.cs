using Paintc.Core;
using Paintc.Factory;

namespace Paintc.Controller
{
    /// <summary>
    /// 
    /// </summary>
    public class SourceCodeWindowController : ControllerBase
    {
        /* Figura seleccionada desde el explorador */
        private ShapeBase? selectedShape;

        public ShapeBase? SelectedShape 
        { 
            get => selectedShape; 
            set
            {
                SetField(ref selectedShape, value);
                UpdateProperties();
            } 
        }

        /* Código fuente para dibujar la figura utilizando graphics.h */
        private string? code;

        public string? Code
        {
            get => code;
            set
            {
                SetField(ref code, value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void UpdateProperties()
        {
            if (selectedShape is null)
            {
                Code = "Error: No se puede mostrar el código fuente de la figura seleccionada.";
                return;
            }

            /* Generar código C en base a propiedades de la figura */
            var primitiveShape = selectedShape.GetSimpleShape();
            var template = CodeTemplateFactory.GetTemplate(primitiveShape);
            if (template is null)
            {
                Code = "Error: No se encontró una plantilla para mostrar el código fuente de la figura seleccionada.";
                return;
            }
            /* Obtener código C */
            Code = template.TransformText();
        }

    }
}
