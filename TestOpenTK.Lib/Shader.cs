using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestOpenTK.Lib
{
    public class Shader : IDisposable
    {
        public int Handle { get; set; } //Shader GPU program
        bool disposedValue = false;

        public Shader(string vertexPath, string fragmentPath)
        {
            var vertexShader = SetupShader(vertexPath, ShaderType.VertexShader);
            var fragmentShader = SetupShader(fragmentPath, ShaderType.FragmentShader);

            Handle = GL.CreateProgram();
            //Attaching program to Handle
            GL.AttachShader(Handle, vertexShader);
            GL.AttachShader(Handle, fragmentShader);
            //Running our program
            GL.LinkProgram(Handle);

            //Deleting shader from RAM
            GL.DetachShader(Handle, vertexShader);
            GL.DetachShader(Handle, fragmentShader);
            GL.DeleteShader(fragmentShader);
            GL.DeleteShader(vertexShader);
        }

        private static int SetupShader(string ShaderPath, ShaderType shaderType)
        {
            //Shader setup
            var shaderPath = File.ReadAllText(ShaderPath);
            var shader = GL.CreateShader(shaderType);
            GL.ShaderSource(shader, shaderPath); //Apply source code to shader
            CompileShader(shader);
            return shader;
        }

        private static void CompileShader(int vertexShader)
        {
            GL.CompileShader(vertexShader);

            GL.GetShader(vertexShader, ShaderParameter.CompileStatus, out var status);
            if (status == 0)
            {
                var infoLog = GL.GetShaderInfoLog(vertexShader);
                Console.WriteLine(infoLog);
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                GL.DeleteProgram(Handle);

                disposedValue = false;
            }
        }

        ~Shader()
        {
            if (!disposedValue)
            {
                Console.WriteLine("GPU Resource leak! Did you forget to call Dispose()?");
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Use()
        {
            GL.UseProgram(Handle);
        }

        public int GetAttribLocation(string attribName)
        {
            return GL.GetAttribLocation(Handle, attribName);
        }

        public int GetMaxVertexAttributes()
        {
            int nrAttributes;
            GL.GetInteger(GetPName.MaxVertexAttribs, out nrAttributes);
            return nrAttributes;
        }
    }
}
