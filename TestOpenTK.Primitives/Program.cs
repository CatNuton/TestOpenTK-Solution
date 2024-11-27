using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace TestOpenTK.Primitives
{
    class Program
    {
        static void Main(string[] args)
        {
            var nws = new NativeWindowSettings
            {
                //Window
                ClientSize = (1080, 720),
                Location = (0, 0),
                WindowBorder = WindowBorder.Resizable,
                WindowState = WindowState.Normal,
                //API
                API = ContextAPI.OpenGL,
                APIVersion = new Version(4, 6),
                Flags = ContextFlags.Default,
                Profile = ContextProfile.Compatability,
            };
            using (var game = new Game(nws))
            {
                game.Run();
            }
        }
    }
}