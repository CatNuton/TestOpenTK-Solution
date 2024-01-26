using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTKVisualization.Options;
using System.Drawing;

namespace OpenTKVisualization
{
    public class Game : GameWindow
    {
        private float fov;
        private bool isLightingEnabled;
        private PolygonMode polygonMode;
        private int width, height;
        private float theta, adder = 0.01f;

        public Game(NativeWindowSettings nws, VisualizingWindowOptions vwo) :
            base(GameWindowSettings.Default, nws)
        {
            width = nws.ClientSize.X;
            height = nws.ClientSize.Y;
            fov = vwo.FOV;
            polygonMode = vwo.PolyMode;
            isLightingEnabled = vwo.IsLightingEnabled;
        }

        protected override void OnLoad()
        {
            base.OnLoad();
            GL.ClearColor(Color.Aqua);
            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.Lighting);
            GL.Enable(EnableCap.Light0);

            GL.Light(LightName.Light0, LightParameter.Position, new float[] { 0.0f, 1.0f, 1.0f, 0.0f }); // Направление света
            GL.Light(LightName.Light0, LightParameter.Ambient, new float[] { 0.2f, 0.2f, 0.2f, 1.0f }); // Цвет фонового освещения
            GL.Light(LightName.Light0, LightParameter.Diffuse, new float[] { 1.0f, 1.0f, 1.0f, 1.0f }); // Цвет рассеянного света
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);
            width = e.Width;
            height = e.Height;
            GL.Viewport(0, 0, width, height);
            SetMatrix();
        }

        private void SetMatrix()
        {
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            var aspectRatio = (float)width/height;
            var matrix = Matrix4.CreatePerspectiveFieldOfView(
                MathHelper.DegreesToRadians(fov),
                aspectRatio,
                0.1f,
                100.0f);
            GL.LoadMatrix(ref matrix);
            GL.MatrixMode(MatrixMode.Modelview);
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            GL.LoadIdentity();
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.PointSize(20.0f);
            GL.PolygonMode(MaterialFace.FrontAndBack, polygonMode);
            #region Cube
            GL.PushMatrix();

            GL.Translate(0.0f, 0.0f, -5.0f);
            GL.Rotate(90.0f, 0.0f, 0.1f, 0.1f);
            GL.Rotate(theta, 0.1f, 0.1f, 0.1f);
            GL.Scale(0.8f, 0.8f, 0.8f);

            GL.Begin(PrimitiveType.Quads);

            // Передняя грань
            GL.Normal3(0.0f, 0.0f, 1.0f);
            GL.Color3(Color.Red);
            GL.Vertex3(-1.0f, -1.0f, 1.0f);
            GL.Vertex3(1.0f, -1.0f, 1.0f);
            GL.Vertex3(1.0f, 1.0f, 1.0f);
            GL.Vertex3(-1.0f, 1.0f, 1.0f);

            // Задняя грань
            GL.Normal3(0.0f, 0.0f, -1.0f);
            GL.Color3(Color.Orange);
            GL.Vertex3(-1.0f, -1.0f, -1.0f);
            GL.Vertex3(-1.0f, 1.0f, -1.0f);
            GL.Vertex3(1.0f, 1.0f, -1.0f);
            GL.Vertex3(1.0f, -1.0f, -1.0f);

            // Верхняя грань
            GL.Normal3(0.0f, 1.0f, 0.0f);
            GL.Color3(Color.Green);
            GL.Vertex3(-1.0f, 1.0f, -1.0f);
            GL.Vertex3(-1.0f, 1.0f, 1.0f);
            GL.Vertex3(1.0f, 1.0f, 1.0f);
            GL.Vertex3(1.0f, 1.0f, -1.0f);

            // Нижняя грань
            GL.Normal3(0.0f, -1.0f, 0.0f);
            GL.Color3(Color.Yellow);
            GL.Vertex3(-1.0f, -1.0f, -1.0f);
            GL.Vertex3(1.0f, -1.0f, -1.0f);
            GL.Vertex3(1.0f, -1.0f, 1.0f);
            GL.Vertex3(-1.0f, -1.0f, 1.0f);

            // Правая грань
            GL.Normal3(1.0f, 0.0f, 0.0f);
            GL.Color3(Color.Blue);
            GL.Vertex3(1.0f, -1.0f, -1.0f);
            GL.Vertex3(1.0f, 1.0f, -1.0f);
            GL.Vertex3(1.0f, 1.0f, 1.0f);
            GL.Vertex3(1.0f, -1.0f, 1.0f);

            // Левая грань
            GL.Normal3(-1.0f, 0.0f, 0.0f);
            GL.Color3(Color.Purple);
            GL.Vertex3(-1.0f, -1.0f, -1.0f);
            GL.Vertex3(-1.0f, -1.0f, 1.0f);
            GL.Vertex3(-1.0f, 1.0f, 1.0f);
            GL.Vertex3(-1.0f, 1.0f, -1.0f);

            GL.End();
            GL.PopMatrix();
            #endregion
            #region Plain
            GL.PushMatrix();
            GL.Translate(0.0f, -1.5f, -5.0f);
            // Платформа
            GL.Begin(PrimitiveType.Quads);

            // Верхняя грань платформы
            GL.Normal3(0.0f, 1.0f, 0.0f);
            GL.Color3(Color.Brown);
            GL.Vertex3(-1.0f, 0.0f, -1.0f);
            GL.Vertex3(1.0f, 0.0f, -1.0f);
            GL.Vertex3(1.0f, 0.0f, 1.0f);
            GL.Vertex3(-1.0f, 0.0f, 1.0f);

            GL.End();
            GL.PopMatrix();
            #endregion
            SwapBuffers();
            base.OnRenderFrame(args);
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);
            if (KeyboardState.IsKeyPressed(Keys.Escape))
            {
                Close();
            }
            if (KeyboardState.IsKeyPressed(Keys.F11))
            {
                if (WindowState == WindowState.Fullscreen)
                {
                    WindowState = WindowState.Normal;
                }
                else
                {
                    WindowState = WindowState.Fullscreen;
                }
            }

            if (KeyboardState.IsKeyDown(Keys.Left))
            {
                theta += adder;
            }
            if (KeyboardState.IsKeyDown(Keys.Right))
            {
                theta -= adder;
            }
            if (KeyboardState.IsKeyPressed(Keys.Period) || KeyboardState.IsKeyPressed(Keys.KeyPadAdd))
            {
                adder += 0.005f;
            }
            else if (KeyboardState.IsKeyPressed(Keys.Minus) || KeyboardState.IsKeyPressed(Keys.KeyPadSubtract))
            {
                adder -= 0.005f;
            }

            if (KeyboardState.IsKeyPressed(Keys.P))
            {
                if (polygonMode == PolygonMode.Fill)
                {
                    polygonMode = PolygonMode.Line;
                }
                else if(polygonMode == PolygonMode.Line)
                {
                    polygonMode = PolygonMode.Point;
                }
                else
                {
                    polygonMode = PolygonMode.Fill;
                }
            }
            if (KeyboardState.IsKeyPressed(Keys.L))
            {
                if (isLightingEnabled)
                {
                    isLightingEnabled = false;
                    GL.Disable(EnableCap.Light0);
                    GL.Disable(EnableCap.Lighting);
                }
                else
                { 
                    isLightingEnabled = true;
                    GL.Enable(EnableCap.Light0);
                    GL.Enable(EnableCap.Lighting);
                }
            }
        }
    }
}