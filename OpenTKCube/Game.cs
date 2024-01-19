using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System.Drawing;

namespace OpenTKCube
{
    public class Game : GameWindow
    {
        double theta = 0;
        public Game(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) :
            base(gameWindowSettings, nativeWindowSettings)
        {
        }

        protected override void OnLoad()
        {
            base.OnLoad();
            GL.ClearColor(Color.Gray);
            GL.Enable(EnableCap.DepthTest);
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);
            GL.Viewport(0, 0, e.Width, e.Height);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            var matrix = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(45),
                e.Width / e.Height, 1.0f, 100.0f);
            GL.LoadMatrix(ref matrix);
            GL.MatrixMode(MatrixMode.Modelview);
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            GL.LoadIdentity();
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.PointSize(50.0f);
            GL.LineWidth(50.0f);

            #region Cube1
            //Changing matrix
            GL.PushMatrix();

            GL.Translate(0.0, 0.0, -45.0);
            GL.Rotate(90.0f, 0.0f, 0.1f, 0.1f);
            GL.Rotate(theta, 0.1f, 0.1f, 0.1f);
            GL.Scale(0.8f, 0.8f, 0.8f);

            GL.Begin(PrimitiveType.Quads);
                GL.Color3(1.0, 1.0, 0.0);
                GL.Vertex3(-10.0, 10.0, 10.0);
                GL.Vertex3(-10.0, 10.0, -10.0);
                GL.Vertex3(-10.0, -10.0, -10.0);
                GL.Vertex3(-10.0, -10.0, 10.0);

                GL.Color3(1.0, 0.0, 1.0);
                GL.Vertex3(10.0, 10.0, 10.0);
                GL.Vertex3(10.0, 10.0, -10.0);
                GL.Vertex3(10.0, -10.0, -10.0);
                GL.Vertex3(10.0, -10.0, 10.0);

                GL.Color3(0.0, 1.0, 1.0);
                GL.Vertex3(10.0, -10.0, 10.0);
                GL.Vertex3(10.0, -10.0, -10.0);
                GL.Vertex3(-10.0, -10.0, -10.0);
                GL.Vertex3(-10.0, -10.0, 10.0);

                GL.Color3(1.0, 0.0, 0.0);
                GL.Vertex3(10.0, 10.0, 10.0);
                GL.Vertex3(10.0, 10.0, -10.0);
                GL.Vertex3(-10.0, 10.0, -10.0);
                GL.Vertex3(-10.0, 10.0, 10.0);

                GL.Color3(0.0, 1.0, 0.0);
                GL.Vertex3(10.0, 10.0, -10.0);
                GL.Vertex3(10.0, -10.0, -10.0);
                GL.Vertex3(-10.0, -10.0, -10.0);
                GL.Vertex3(-10.0, 10.0, -10.0);

                GL.Color3(0.0, 0.0, 1.0);
                GL.Vertex3(10.0, 10.0, 10.0);
                GL.Vertex3(10.0, -10.0, 10.0);
                GL.Vertex3(-10.0, -10.0, 10.0);
                GL.Vertex3(-10.0, 10.0, 10.0);
            GL.End();
            //Setting matrix to default
            GL.PopMatrix();
            #endregion

            #region Cube2
            GL.Color3(1.0f, 1.0f, 1.0f);
            GL.Translate(0.0f, -15.0, -45.0);
            GL.Scale(1.0f, 0.1f, 1.0f);

            GL.Begin(PrimitiveType.Quads);
            GL.Vertex3(-10.0, 10.0, 10.0);
            GL.Vertex3(-10.0, 10.0, -10.0);
            GL.Vertex3(-10.0, -10.0, -10.0);
            GL.Vertex3(-10.0, -10.0, 10.0);

            GL.Vertex3(10.0, 10.0, 10.0);
            GL.Vertex3(10.0, 10.0, -10.0);
            GL.Vertex3(10.0, -10.0, -10.0);
            GL.Vertex3(10.0, -10.0, 10.0);

            GL.Vertex3(10.0, -10.0, 10.0);
            GL.Vertex3(10.0, -10.0, -10.0);
            GL.Vertex3(-10.0, -10.0, -10.0);
            GL.Vertex3(-10.0, -10.0, 10.0);

            GL.Vertex3(10.0, 10.0, 10.0);
            GL.Vertex3(10.0, 10.0, -10.0);
            GL.Vertex3(-10.0, 10.0, -10.0);
            GL.Vertex3(-10.0, 10.0, 10.0);

            GL.Vertex3(10.0, 10.0, -10.0);
            GL.Vertex3(10.0, -10.0, -10.0);
            GL.Vertex3(-10.0, -10.0, -10.0);
            GL.Vertex3(-10.0, 10.0, -10.0);

            GL.Vertex3(10.0, 10.0, 10.0);
            GL.Vertex3(10.0, -10.0, 10.0);
            GL.Vertex3(-10.0, -10.0, 10.0);
            GL.Vertex3(-10.0, 10.0, 10.0);
            GL.End();
            #endregion

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
            if (KeyboardState.IsKeyPressed(Keys.F11))
            {
                if (WindowState == WindowState.Normal)
                {
                    WindowState = WindowState.Fullscreen;
                }
                else
                {
                    WindowState = WindowState.Normal;
                }
            }
            if (KeyboardState.IsKeyDown(Keys.Left))
            {
                theta += 0.1f;
            }
            if (KeyboardState.IsKeyDown(Keys.Right))
            {
                theta -= 0.1f;
            }
        }
    }
}