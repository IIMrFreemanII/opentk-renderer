using System.Collections.Generic;

namespace open_tk_renderer.Renderer;

public static class TextureController
{
  public static readonly List<Texture> Textures = new();

  public static void Init()
  {
    FromFile("Assets/images/wall.jpeg");
    FromFile("Assets/images/awesomeface.png");
  }

  public static Texture FromFile(string path)
  {
    var texture = new Texture(path);
    Add(texture);
    return texture;
  }

  public static Texture? Get(string name)
  {
    return Textures.Find(texture => texture.name == name);
  }

  public static void Add(Texture texture)
  {
    Textures.Add(texture);
  }

  public static void Remove(Texture texture)
  {
    Textures.Remove(texture);
  }

  public static void Remove(string name)
  {
    var index = Textures.FindIndex((texture) => texture.name == name);
    Textures.RemoveAt(index);
  }
}
