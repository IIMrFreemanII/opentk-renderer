using OpenTK.Mathematics;

namespace open_tk_renderer.Renderer.UI;

public struct EdgeInsets
{
  public int vertical;
  public int horizontal;

  public Vector2i size => new (horizontal, vertical);

  public EdgeInsets(int size) : this(size, size)
  {
  }
  
  public EdgeInsets(int vertical, int horizontal)
  {
    this.vertical = vertical;
    this.horizontal = horizontal;
  }
}