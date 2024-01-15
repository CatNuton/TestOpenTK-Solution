using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace OpenTKTriangle
{
    class Program
    {
        static void Main(string[] args)
        {
            var nws = new NativeWindowSettings()
            {
                ClientSize = (1080, 720),
                Location = (0, 0),
                WindowBorder = WindowBorder.Resizable,
                WindowState = WindowState.Normal,
                Flags = ContextFlags.Default,
                Profile = ContextProfile.Compatability,
            };
            nws.Title = $"LearnOpenTK. {nws.API}: {nws.APIVersion}";
            using (Game game = new Game(GameWindowSettings.Default, nws))
            {
                game.Run();
            }
        }
    }
}