using OpenTK.Graphics.OpenGL;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTKVisualization.Options;

namespace OpenTKVisualization
{
    class Program
    {
        private static void Main(string[] args)
        {
            var nws = new NativeWindowSettings
            {
                ClientSize = (1080, 720),
                Location = (0, 0),
                WindowBorder = WindowBorder.Resizable,
                WindowState = WindowState.Normal,
                Flags = ContextFlags.Default,
                Profile = ContextProfile.Compatability,
            };
            var vwo = new VisualizingWindowOptions
            {
                FOV = 45,
                PolyMode = PolygonMode.Fill,
            };
            using (var game = new Game(nws, vwo))
            {
                game.Run();
            }
        }
    }
}