using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System.Drawing;
using TestOpenTK.Lib;

namespace TestOpenTK.Textures
{
    public class Game : GameWindow
    {
        int vbo; // Buffer for vertices
        int vao; // Buffer for vertices attributes
        int ebo;
        PolygonMode mode = PolygonMode.Fill;
        Shader shader;
        Texture texture;
        float[] vertices =
        {
            // Position         Texture coordinates
             0.5f,  0.5f, 0.0f, 1.0f, 1.0f, // top right
             0.5f, -0.5f, 0.0f, 1.0f, 0.0f, // bottom right
            -0.5f, -0.5f, 0.0f, 0.0f, 0.0f, // bottom left
            -0.5f,  0.5f, 0.0f, 0.0f, 1.0f  // top left
        };
        uint[] indices =
        {
            0, 1, 3,   // first triangle
            1, 2, 3    // second triangle
        };

        public Game(NativeWindowSettings nws) : base(GameWindowSettings.Default, nws)
        {
        }

        protected override void OnLoad()
        {
            base.OnLoad();

            GL.ClearColor(Color.Cyan);
            GL.Enable(EnableCap.CullFace);
            GL.CullFace(CullFaceMode.Front);
            shader = new Shader("Shaders\\shader.vert", "Shaders\\shader.frag");
            Console.WriteLine($"Max number of vertex attributes supported: {shader.GetMaxVertexAttributes()}");
            Console.WriteLine($"aPosition attribute location: {shader.GetAttribLocation("aPosition")}");

            #region VBO
            vbo = GL.GenBuffer();   //Creating Buffer
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);   //Setting type of the buffer
            //BufferData arguments:
            //  Which buffer the data should be sent to.
            //  How much data is being sent (in bytes)
            //  Vertices coords as data
            /*  How the buffer will be used:
                    StaticDraw: the data will most likely not change at all or very rarely.
                    DynamicDraw: the data is likely to change a lot.
                    StreamDraw: the data will change every time it is drawn. */
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices,
                BufferUsageHint.StaticDraw);    //Buffer data
            #endregion
            #region VAO
            vao = GL.GenVertexArray();  //Creating Buffer
            GL.BindVertexArray(vao);    //Applying buffer
            //VertexAttribPointer arguments:
            //  Which vertex attribute we want to configure.
            //  The size of the vertex attribute (vec3 - 3)
            //  Type of the data
            //  Should we normalize data
            //  Space between the vertices https://opentk.net/learn/chapter1/img/2-vertex_attribute_pointer.png
            //  Start point
            GL.EnableVertexAttribArray(shader.GetAttribLocation("aPosition"));  //Activating vertex attribute.
            GL.VertexAttribPointer(shader.GetAttribLocation("aPosition"), 3, VertexAttribPointerType.Float,
                false, 5 * sizeof(float), 0);

            GL.EnableVertexAttribArray(shader.GetAttribLocation("aTexCoord"));
            GL.VertexAttribPointer(shader.GetAttribLocation("aTexCoord"), 2, VertexAttribPointerType.Float,
                false, 5 * sizeof(float), 3 * sizeof(float));
            #endregion
            #region EBO
            ebo = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ebo);
            GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(uint), indices,
                BufferUsageHint.StaticDraw);
            #endregion
            shader.Use();

            texture = Texture.SetTexture("Data//IMG.jpg", TextureWrapMode.Repeat, TextureWrapMode.Repeat,
                TextureMinFilter.Nearest, TextureMagFilter.Nearest, 0, new float[3]);
            texture.Use(TextureUnit.Texture0);
        }

        protected override void OnUnload()
        {
            //Unbind all the resources
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
            GL.BindVertexArray(0);
            GL.UseProgram(0);

            //Deleting shaders
            GL.DeleteBuffer(vbo);
            GL.DeleteVertexArray(vao);
            GL.DeleteProgram(shader.Handle);

            base.OnUnload();
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);
            GL.Viewport(0, 0, e.Width, e.Height);
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);  //What we must clear
            GL.PointSize(3.0f);
            GL.PolygonMode(MaterialFace.FrontAndBack, mode);
            shader.Use();
            texture.Use(TextureUnit.Texture0);
            //DrawArrays arguments:
            //  Pattern of drawing
            //  Start point
            //  Count of all vertices
            GL.DrawElements(BeginMode.Triangles, indices.Length, DrawElementsType.UnsignedInt, 0);
            SwapBuffers();  //Updating screen
            base.OnRenderFrame(args);
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);
            if (KeyboardState.IsKeyPressed(Keys.Escape))
            {
                Close();
            }
            if (KeyboardState.IsKeyPressed(Keys.M))
            {
                if (mode != PolygonMode.Fill)
                {
                    mode++;
                }
                else
                {
                    mode = PolygonMode.Point;
                }
            }
        }
    }
}