using Microsoft.Win32;
using Paintc.Commands;
using Paintc.Core;
using Paintc.Resources.RTT;
using Paintc.Service;
using Paintc.Shapes.C;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace Paintc.Controller.UserControls
{
    public class SourceCodePanelController : ControllerBase
    {
        /* Código fuente para dibujar todo el contenido del canvas utilizando graphics.h */
        private string? code;

        public string? Code
        {
            get => code;
            set
            {
                SetField(ref code, value);
            }
        }

        /* Lista de todas las figuras que contiene el canvas */
        private ObservableCollection<SimpleShapeBase>? _primitiveShapes = [];

        public ObservableCollection<SimpleShapeBase>? PrimitiveShapes
        {
            get => _primitiveShapes;
            set
            {
                SetField(ref _primitiveShapes, value);
                ShowSourceCode();
            }
        }

        public ICommand CopyButtonClick { get; private set; }
        public ICommand SaveButtonClick { get; private set; }

        public SourceCodePanelController()
        {
            CopyButtonClick = new RelayCommand((obj) => true, CopyButtonClickCommand);
            SaveButtonClick = new RelayCommand((obj) => true, SaveButtonClickCommand);
            SourceCodePanelService.Instance.SetPrimitiveShapesCollectionEventHandler += SetPrimitiveShapesCollection;
        }

        /// <summary>
        /// Guarda el código fuente necesario para dibujar el contenido del canvas
        /// </summary>
        /// <param name="obj"></param>
        private void SaveButtonClickCommand(object? obj)
        {
            var dialog = new SaveFileDialog
            {
                Filter = "C file (.c)|*.c",
                Title = "Save C Source Code"
            };

            if (dialog.ShowDialog() == true)
            {
                try
                {
                    File.WriteAllText(dialog.FileName, Code);
                    MessageBox.Show("C source code saved successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error saving C source code: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void CopyButtonClickCommand(object? obj)
        {
            Clipboard.SetText(Code);
            MessageBox.Show("Source code copied to clipboard", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        /* Esto dispara el método ShowSourceCode y genera el código fuente de todo el contenido del canvas */

        private void SetPrimitiveShapesCollection(object? sender, ObservableCollection<SimpleShapeBase>? shapesCollection)
        {
            PrimitiveShapes = shapesCollection;
        }

        /* Muestra el código c generado a partir de las figuras que contiene el canvas */
        private void ShowSourceCode()
        {
            var template = new FullSourceCodeTemplate()
            {
                shapes = PrimitiveShapes,
                settings = new CanvasSettings() { BackgroundColor = DrawingHandler.Instance.GetBackgroundColor() }
            };

            Code = template.TransformText();
        }
    }
}