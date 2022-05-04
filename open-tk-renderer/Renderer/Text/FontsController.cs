using SharpFont;

namespace open_tk_renderer.Renderer.Text;

public static class FontsController
{
  public static readonly Library Library = new();
  private static readonly List<Font> Fonts = new();
  
  public static void Init()
  {
    FromFile("Assets/fonts/Fira_Code/static/FiraCode-Regular.ttf");
  }
  
  public static Font FromFile(string path)
  {
    var font = Font.FromFile(path);
    Add(font);
    return font;
  }
  
  public static Font? Get(string name)
  {
    return Fonts.Find(font => font.name == name);
  }

  public static void Add(Font font)
  {
    Fonts.Add(font);
  }
  
  public static void Remove(Font font)
  {
    Fonts.Remove(font);
  }
  
  public static void Remove(string name)
  {
    int index = Fonts.FindIndex(font => font.name == name);
    Fonts.RemoveAt(index);
  }
}
