using OpenTK.Graphics.OpenGL4;
using StbImageSharp;
using TestOpenTK.Lib;

internal static class TextureHelpers
{

    public static Texture SetTexture(string path, TextureWrapMode textureWrapModeX, TextureWrapMode textureWrapModeY,
        TextureMinFilter textureMinFilter, TextureMagFilter textureMagFilter, int borderSize,
        float[] borderColor)
    {
        int h = GL.GenTexture();
        GL.ActiveTexture(TextureUnit.Texture0);
        GL.BindTexture(TextureTarget.Texture2D, h);

        using (var stream = File.OpenRead(path))
        {
            StbImage.stbi_set_flip_vertically_on_load(1);
            var image = ImageResult.FromStream(File.OpenRead(path), ColorComponents.RedGreenBlueAlpha);
            GL.TexImage2D(TextureTarget.Texture2D, borderSize, PixelInternalFormat.Rgba, image.Width, image.Height,
                0, PixelFormat.Rgba, PixelType.UnsignedByte, image.Data);
        }

        //S - x axis, T - y axis
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)textureWrapModeX);
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)textureWrapModeY);

        //BorderColor
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureBorderColor, borderColor);

        //Linear - smoothless draw
        //Near - pixel draw
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)textureMinFilter);
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)textureMagFilter);

        GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);

        return new Texture(h);
    }
}