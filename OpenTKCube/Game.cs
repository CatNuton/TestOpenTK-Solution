using OpenTK.Graphics.OpenGL;
using OpenTK.Lib;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System.Drawing;

namespace OpenTKCube
{
    public class Game : GameWindow
    {
        double[] platformVertices =
        {
            10.0, -10.0, 10.0,
            10.0, -10.0, -10.0,
            -10.0, -10.0, -10.0,
            -10.0, -10.0, 10.0,
        };
        double[] platformColors =
        {
            8, 0, 41
        };

        double[] colors =
        {
            //Left
            1.0, 1.0, 0.0,
            //Right
            1.0, 0.0, 1.0,
            //Bottom
            0.0, 1.0, 1.0,
            //Top
            1.0, 0.0, 0.0,
            //Back
            0.0, 1.0, 0.0,
            //Forward
            0.0, 0.0, 1.0,
        };

        double[][] vertices =
        {
            // Left
            new double[]
            {
                -10.0, 10.0, 10.0,
                -10.0, 10.0, -10.0,
                -10.0, -10.0, -10.0,
                -10.0, -10.0, 10.0,
            },
            // Right
            new double[]
            {
                10.0, 10.0, 10.0,
                10.0, 10.0, -10.0,
                10.0, -10.0, -10.0,
                10.0, -10.0, 10.0,
            },
            // Bottom
            new double[]
            {
                10.0, -10.0, 10.0,
                10.0, -10.0, -10.0,
                -10.0, -10.0, -10.0,
                -10.0, -10.0, 10.0,
            },
            // Top
            new double[]
            {
                10.0, 10.0, 10.0,
                10.0, 10.0, -10.0,
                -10.0, 10.0, -10.0,
                -10.0, 10.0, 10.0,
            },

            // Back
            new double[]
            {
                10.0, 10.0, -10.0,
                10.0, -10.0, -10.0,
                -10.0, -10.0, -10.0,
                -10.0, 10.0, -10.0,
            },

            // Forward
            new double[] 
            {
                10.0, 10.0, 10.0,
                10.0, -10.0, 10.0,
                -10.0, -10.0, 10.0,
                -10.0, 10.0, 10.0,
            },
        };

        double adder = 0.01f;
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

            float aspectRatio = (float)e.Width / (float)e.Height;
            var perspectiveMatrix = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(45.0f), aspectRatio, 0.1f, 100.0f);
            GL.LoadMatrix(ref perspectiveMatrix);

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

            GL.Translate(0.0, 0.0, -55.0);
            GL.Rotate(90.0f, 0.0f, 0.1f, 0.1f);
            GL.Rotate(theta, 0.1f, 0.1f, 0.1f);
            GL.Scale(1.0f, 1.0f, 1.0f);

            Drawer.DrawCube(colors, vertices);
            //Setting matrix to default
            GL.PopMatrix();
            #endregion

            #region Cube2
            GL.PushMatrix();

            GL.Color3(1.0f, 1.0f, 1.0f);
            GL.Translate(0.0f, -2.0, -55.0);
            GL.Scale(1.5f, 1.5f, 1.5f);

            Drawer.DrawRect3(platformVertices, platformColors);

            GL.PopMatrix();
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
                else if (WindowState == WindowState.Fullscreen)
                {
                    WindowState = WindowState.Normal;
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
                adder += 0.01f;
            }
            if (KeyboardState.IsKeyPressed(Keys.Minus) || KeyboardState.IsKeyPressed(Keys.KeyPadSubtract))
            {
                adder -= 0.01f;
            }
        }
    }
}