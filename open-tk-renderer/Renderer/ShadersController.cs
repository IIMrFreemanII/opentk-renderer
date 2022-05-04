using System.Collections;
using open_tk_renderer.Utils.Coroutines;

namespace open_tk_renderer.Renderer;

public static class ShadersController
{
  public static List<Shader> Shaders = new();
  public static Shader? ErrorShader;

  public static void Init()
  {
    ErrorShader = Shader.FromFile("Assets/error.glsl");
    FromFile("Assets/rounded-rect.glsl");
    FromFile("Assets/rounded-rect-frame.glsl");
    FromFile("Assets/texture.glsl");
    FromFile("Assets/text.glsl");
  }

  public static Shader FromFile(string filePath)
  {
    var shader = Shader.FromFile(filePath);
    Add(shader);
    return shader;
  }

  public static void Add(Shader shader)
  {
    Shaders.Add(shader);
  }

  public static Shader? Get(string name)
  {
    return Shaders.Find((shader) => shader.Name == name);
  }

  public static void Remove(Shader shader)
  {
    Shaders.Remove(shader);
  }

  public static IEnumerator HandleRecompile(int delay)
  {
    while (true)
    {
      foreach (var shader in Shaders)
      {
        var lastWriteTime = File.GetLastWriteTime(shader.FilePath);
        if (shader.lastWriteTime != lastWriteTime)
        {
          Console.WriteLine($"Recompile {shader.Name}");
          shader.lastWriteTime = lastWriteTime;
          shader.Compile();
        }
      }

      yield return Wait.FromMs(delay);
    }
  }
}
