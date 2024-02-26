using Paintc.Enums;
using Paintc.Model;
using System.Collections.ObjectModel;
using System.Windows.Media;

namespace Paintc.Service.Collections
{
    public static class CGAColorPaletteService
    {
        public static ObservableCollection<CGAColor> GetColorPalette()
        {
            return [
                new CGAColor(CGAColorPalette.Black, Colors.Black),
                new CGAColor(CGAColorPalette.Blue, Color.FromRgb(0x00, 0x00, 0xAA)),
                new CGAColor(CGAColorPalette.Green, Color.FromRgb(0x00, 0xAA, 0x00)),
                new CGAColor(CGAColorPalette.Cyan, Color.FromRgb(0x00, 0xAA, 0xAA)),
                new CGAColor(CGAColorPalette.Red, Color.FromRgb(0xAA, 0x00, 0x00)),
                new CGAColor(CGAColorPalette.Magenta, Color.FromRgb(0xAA, 0x00, 0xAA)),
                new CGAColor(CGAColorPalette.Brown, Color.FromRgb(0xAA, 0x55, 0x00)),
                new CGAColor(CGAColorPalette.LightGray, Color.FromRgb(0xAA, 0xAA, 0xAA)),
                new CGAColor(CGAColorPalette.DarkGray, Color.FromRgb(0x55, 0x55, 0x55)),
                new CGAColor(CGAColorPalette.LightBlue, Color.FromRgb(0x55, 0x55, 0xFF)),
                new CGAColor(CGAColorPalette.LightGreen, Color.FromRgb(0x55, 0xFF, 0x55)),
                new CGAColor(CGAColorPalette.LightCyan, Color.FromRgb(0x55, 0xFF, 0xFF)),
                new CGAColor(CGAColorPalette.LightRed, Color.FromRgb(0xFF, 0x55, 0x55)),
                new CGAColor(CGAColorPalette.LightMagenta, Color.FromRgb(0xFF, 0x55, 0xFF)),
                new CGAColor(CGAColorPalette.Yellow, Color.FromRgb(0xFF, 0xFF, 0x55)),
                new CGAColor(CGAColorPalette.White, Colors.White)];
        }

        public static Color GetColor(CGAColorPalette? color)
        {
            if (color == null)
                return GetColorPalette().Last().Color;

            return GetColorPalette().Where(c => c.Cpalette == color).First().Color;
        }
    }
}