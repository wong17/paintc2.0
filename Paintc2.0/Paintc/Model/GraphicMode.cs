namespace Paintc.Model
{
    public class GraphicMode(string device, string mode, int code, int width, int height)
    {
        public string? DisplayName { get; set; } = $"{device} - {mode} | {width}x{height}px";
        public string? Device { get; set; } = device;
        public string? Mode { get; set; } = mode;
        public int Code { get; set; } = code;
        public int Width { get; set; } = width;
        public int Height { get; set; } = height;
    }
}
