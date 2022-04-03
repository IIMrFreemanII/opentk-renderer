using System;
using System.IO;
using open_tk_renderer.Utils;
using OpenTK.Graphics.OpenGL4;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace open_tk_renderer.Renderer;

public class Texture
{
  public string name;
  public Image<Rgba32> image;
  public int id;

  public Texture(string path)
  {
    id = GL.GenTexture();
    Bind();

    GL.TexParameter(
      TextureTarget.Texture2D,
      TextureParameterName.TextureWrapS,
      (int)TextureWrapMode.Repeat
    );
    GL.TexParameter(
      TextureTarget.Texture2D,
      TextureParameterName.TextureWrapT,
      (int)TextureWrapMode.Repeat
    );
    GL.TexParameter(
      TextureTarget.Texture2D,
      TextureParameterName.TextureMinFilter,
      (int)TextureMinFilter.Linear
    );
    GL.TexParameter(
      TextureTarget.Texture2D,
      TextureParameterName.TextureMagFilter,
      (int)TextureMinFilter.Linear
    );

    var absPath = PathUtils.FromLocal(path);
    name = Path.GetFileName(absPath);

    image = Image.Load<Rgba32>(absPath);
    var bytes = new Span<byte>(new byte[4 * image.Width * image.Height]);
    image.CopyPixelDataTo(bytes);

    GL.TexImage2D(
      TextureTarget.Texture2D,
      0,
      PixelInternalFormat.Rgba,
      image.Width,
      image.Height,
      0,
      PixelFormat.Rgba,
      PixelType.UnsignedByte,
      bytes.ToArray()
    );
    GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);

    Unbind();
  }

  public void Bind()
  {
    GL.BindTexture(TextureTarget.Texture2D, id);
  }

  public void Unbind()
  {
    GL.BindTexture(TextureTarget.Texture2D, 0);
  }

  public void Use(TextureUnit unit = TextureUnit.Texture0)
  {
    GL.ActiveTexture(unit);
    Bind();
  }
}
