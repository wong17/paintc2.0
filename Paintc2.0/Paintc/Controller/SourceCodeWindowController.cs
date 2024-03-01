using Paintc.Commands;
using Paintc.Core;
using Paintc.Factory;
using System.Windows;
using System.Windows.Input;

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

        /* Nombre de la figura dado desde el explorador */
        private string? name;

        public string? Name
        {
            get => name;
            set
            {
                SetField(ref name, value);
            }
        }

        public ICommand CopyButtonClick { get; private set; }

        public SourceCodeWindowController()
        {
            CopyButtonClick = new RelayCommand((obj) => true, CopyButtonClickCommand);
        }

        /// <summary>
        /// Copia el código fuente al portapapeles
        /// </summary>
        /// <param name="obj"></param>
        private void CopyButtonClickCommand(object? obj)
        {
            Clipboard.SetText(Code);
            MessageBox.Show("Source code copied to clipboard", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        /// <summary>
        /// 
        /// </summary>
        private void UpdateProperties()
        {
            if (selectedShape is null)
            {
                Name = "Error :(";
                Code = "Error: No se puede mostrar el código fuente de la figura seleccionada.";
                return;
            }

            /* Generar código C en base a propiedades de la figura */
            var primitiveShape = selectedShape.GetSimpleShape();
            var template = CodeTemplateFactory.GetTemplate(primitiveShape);
            if (template is null)
            {
                Name = "Error :(";
                Code = "Error: No se encontró una plantilla para mostrar el código fuente de la figura seleccionada.";
                return;
            }
            /* Obtener código C */
            Code = template.TransformText();
            Name = $"Code for {selectedShape.Name}";
        }

    }
}
