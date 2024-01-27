using Paintc.Model;

namespace Paintc.Controller
{
    public static class GraphicModeController
    {
        public static List<GraphicMode> GetGraphicModes()
        {
            return
            [
                new("VGA", "VGAHI", 2, 640, 480),
                new("HERC", "HERCMONOHI", 0,720, 348),
                new("HERC", "PC3270HI", 0, 720, 350),
                new("HERC", "IBM8514LO", 0, 640, 480),
                new("HERC", "IBM8514HI", 1, 1024, 768)
            ];
        }
    }
}
