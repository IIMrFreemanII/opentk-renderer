namespace open_tk_renderer.Renderer;

public static class ShadersController
{
  public static List<Shader> Shaders = new();
  public static Shader? ErrorShader;

  public static void Add(Shader shader)
  {
    Shaders.Add(shader);
  }

  public static void Remove(Shader shader)
  {
    Shaders.Remove(shader);
  }

  public static void HandleRecompile()
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
  }
}
