namespace Paintc.Model
{
    public class GraphicMode
    {
        public string? DisplayName { get; set; }
        public string? Device { get; set; }
        public string? Mode { get; set; }
        public int Code { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public GraphicMode() { }

        public GraphicMode(string? device, string? mode, int code, int width, int height)
        {
            Device = device;
            Mode = mode;
            Code = code;
            Width = width;
            Height = height;
            DisplayName = $"{Device}-{Mode} | {Width}x{Height}px";
        }
    }
}
