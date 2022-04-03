using System;
using System.IO;

namespace open_tk_renderer.Utils;

public static class PathUtils
{
  public static string FromLocal(string path)
  {
    var baseDir = Directory.GetCurrentDirectory();
    var absolutePath = Path.Combine(baseDir, $"../../../{path}");
    var localPath = new Uri(absolutePath).LocalPath;
    return localPath;
  }
}
