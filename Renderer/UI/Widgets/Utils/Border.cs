using OpenTK.Mathematics;

namespace open_tk_renderer.Renderer.UI.Widgets.Utils;

public struct Border
{
  public Color4 color;

  public int left;
  public int top;
  public int right;
  public int bottom;

  private Border(
    Color4 color,
    int left,
    int top,
    int right,
    int bottom
  )
  {
    this.color = color;

    this.left = left;
    this.top = top;
    this.right = right;
    this.bottom = bottom;
  }

  public static Border All(Color4 color, int width)
  {
    return new Border(
      color,
      width,
      width,
      width,
      width
    );
  }
}
