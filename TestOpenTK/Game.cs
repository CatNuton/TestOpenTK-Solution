using OpenTK.Graphics.OpenGL;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System.Drawing;

namespace OpenTKTriangle
{
    public class Game : GameWindow
    {
        double[] vertices = {
            -0.5f, -0.5f, //Bottom-left vertex
            0.5f, -0.5f, //Bottom-right vertex
            0.0f,  0.5f  //Top vertex
        };
        public Game(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) :
            base(gameWindowSettings, nativeWindowSettings)
        {
        }

        protected override void OnLoad()
        {
            base.OnLoad();
            GL.ClearColor(Color.Cyan);
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);
            GL.Viewport(0, 0, e.Width, e.Height);
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.Color3(0.0f, 1.0f, 0.2f);
            GL.Begin(PrimitiveType.Triangles);

            GL.Vertex2(vertices[0], vertices[1]);
            GL.Vertex2(vertices[2], vertices[3]);
            GL.Vertex2(vertices[4], vertices[5]);

            GL.End();

            SwapBuffers();
            base.OnRenderFrame(args);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            if (KeyboardState.IsKeyDown(Keys.Escape))
            {
                Close();
            }
        }
    }
}