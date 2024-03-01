using Paintc.Model;

namespace Paintc.Service.Collections
{
    public static class GraphicModesService
    {
        public static List<GraphicMode> GetGraphicModes()
        {
            return
            [
                new GraphicMode("VGA", "VGAHI", 2, 640, 480),
                new GraphicMode("HERC", "HERCMONOHI", 0, 720, 348),
                new GraphicMode("HERC", "PC3270HI", 0, 720, 350),
                new GraphicMode("HERC", "IBM8514HI", 1, 1024, 768)
            ];
        }
    }
}