using OpenTK.Graphics.OpenGL;
using System.Drawing;

namespace OpenTK.Lib
{
    public class Drawer
    {
        // Rectangle
        public static void DrawRect2(double[] vertices, Color color)
        {
            GL.Begin(PrimitiveType.Quads);
            GL.Color3(color.R, color.G, color.B);
            for (int i = 0; i < vertices.Length; i += 2)
            {
                GL.Vertex2(vertices[i], vertices[i + 1]);
            }
            GL.End();
        }
        public static void DrawRect3(double[] vertices, double[] colors)
        {
            GL.Begin(PrimitiveType.Quads);
            GL.Color3(colors[0], colors[1], colors[2]);
            for (int i = 0; i < vertices.Length; i += 3)
            {
                GL.Vertex3(vertices[i], vertices[i + 1], vertices[i + 2]);
            }
            GL.End();
        }

        // Triangle
        public static void DrawTriangle2(double[] vertices, Color color)
        {
            GL.Begin(PrimitiveType.Triangles);
            GL.Color3(color.R, color.G, color.B);
            for (int i = 0; i < vertices.Length; i += 2)
            {
                GL.Vertex2(vertices[i], vertices[i + 1]);
            }
            GL.End();
        }
        public static void DrawTriangle3(double[] vertices, Color color)
        {
            GL.Begin(PrimitiveType.Quads);
            GL.Color3(color.R, color.G, color.B);
            for (int i = 0; i < vertices.Length; i += 3)
            {
                GL.Vertex3(vertices[i], vertices[i + 1], vertices[i + 2]);
            }
            GL.End();
        }
        //Cube
        public static void DrawCube(double[] colors, double[][] vertices)
        {
            int rgb = 3;
            int sides = colors.Length / rgb;
            double[][] c = new double[sides][];

            for (int i = 0; i < sides; i++)
            {
                c[i] = new double[rgb];

                for (int j = 0; j < rgb; j++)
                {
                    c[i][j] = colors[i * rgb + j];
                }
            }
            for (int i = 0; i < vertices.Length; i++)
            {
                Drawer.DrawRect3(vertices[i], c[i]);
            }
        }
    }
}
