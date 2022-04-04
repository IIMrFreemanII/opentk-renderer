using OpenTK.Mathematics;

namespace open_tk_renderer.Renderer.Text;

public struct Character
{
  public int TextureId { get; set; }
  public Vector2 Size { get; set; }
  public Vector2 Bearing { get; set; }
  public int Advance { get; set; }
}
