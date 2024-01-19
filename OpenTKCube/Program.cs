using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace OpenTKCube
{
    class Program
    {
        static void Main(string[] args)
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
            using (var game = new Game(GameWindowSettings.Default, nws))
            {
                game.Run();
            }
        }
    }
}