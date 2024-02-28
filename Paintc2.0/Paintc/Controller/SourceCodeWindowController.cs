using Paintc.Core;
using Paintc.Resources.RTT;
using Paintc.Shapes;

namespace Paintc.Controller
{
    /// <summary>
    /// 
    /// </summary>
    public class SourceCodeWindowController : ControllerBase
    {
        /*  */
        private static readonly Dictionary<Type, Type> SourceCodeTemplates = new()
        {
            { typeof(RectangleShape), typeof(CRectangleTemplate) },
            { typeof(EllipseShape), typeof(CEllipseTemplate) },
            { typeof(LineShape), typeof(CLineTemplate) }
        };

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
                Code = "Error al mostrar código fuente de la figura seleccionada.";
                return;
            }

            /* Generar código C en base a propiedades de la figura */

        }

    }
}
