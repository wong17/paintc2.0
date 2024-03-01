using Paintc.Model;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media;

namespace Paintc.Service.Collections
{
    public static class FillPatternsService
    {
        private static readonly ResourceDictionary? _imagesResource;

        private static readonly DrawingBrush? _emptyFill, _solidFill, _lineFill, _ltslashFill, 
            _slashFill, _bkslashFill, _ltbkslashFill, _hatchFill, 
            _xhatchFill, _interleaveFill, _wideDotFill, _closeDotFill;

        static FillPatternsService()
        {
            _imagesResource = new ResourceDictionary
            {
                Source = new Uri("/Paintc;component/Resources/Dictionary/FillPatterns.xaml", UriKind.RelativeOrAbsolute)
            };

            _emptyFill = (DrawingBrush?)_imagesResource["EmptyFillPattern"];
            _solidFill = (DrawingBrush?)_imagesResource["SolidFillPattern"];
            _lineFill = (DrawingBrush?)_imagesResource["LineFillPattern"];
            _ltslashFill = (DrawingBrush?)_imagesResource["LTSlashFillPattern"];
            _slashFill = (DrawingBrush?)_imagesResource["SlashFillPattern"];
            _bkslashFill = (DrawingBrush?)_imagesResource["BKSlashFillPattern"];
            _ltbkslashFill = (DrawingBrush?)_imagesResource["LTBKSlashFillPattern"];
            _hatchFill = (DrawingBrush?)_imagesResource["HatchFillPattern"];
            _xhatchFill = (DrawingBrush?)_imagesResource["XHatchFillPattern"];
            _interleaveFill = (DrawingBrush?)_imagesResource["InterleaveFillPattern"];
            _wideDotFill = (DrawingBrush?)_imagesResource["WideDotFillPattern"];
            _closeDotFill = (DrawingBrush?)_imagesResource["CloseDotFillPattern"];
        }

        public static ObservableCollection<FillPattern>GetFillPatterns()
        {
            return [
                new FillPattern(Enums.FillPatternStyle.EMPTY_FILL, _emptyFill),
                new FillPattern(Enums.FillPatternStyle.SOLID_FILL, _solidFill),
                new FillPattern(Enums.FillPatternStyle.LINE_FILL, _lineFill),
                new FillPattern(Enums.FillPatternStyle.LTSLASH_FILL, _ltslashFill),
                new FillPattern(Enums.FillPatternStyle.SLASH_FILL, _slashFill),
                new FillPattern(Enums.FillPatternStyle.BKSLASH_FILL, _bkslashFill),
                new FillPattern(Enums.FillPatternStyle.LTBKSLASH_FILL, _ltbkslashFill),
                new FillPattern(Enums.FillPatternStyle.HATCH_FILL, _hatchFill),
                new FillPattern(Enums.FillPatternStyle.XHATCH_FILL, _xhatchFill),
                new FillPattern(Enums.FillPatternStyle.INTERLEAVE_FILL, _interleaveFill),
                new FillPattern(Enums.FillPatternStyle.WIDE_DOT_FILL, _wideDotFill),
                new FillPattern(Enums.FillPatternStyle.CLOSE_DOT_FILL, _closeDotFill)
                ];
        }
    }
}
