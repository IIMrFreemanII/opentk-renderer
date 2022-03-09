using OpenTK.Mathematics;

namespace open_tk_renderer.Renderer.UI;

public struct Alignment
{
  public Vector2 pivot;

  public Alignment(Vector2 pivot)
  {
    this.pivot = pivot;
  }

  public static readonly Alignment BottomCenter = new(new(0.0f, 1.0f));
  public static readonly Alignment BottomLeft = new(new(-1.0f, 1.0f));
  public static readonly Alignment BottomRight = new(new(1.0f, 1.0f));

  public static readonly Alignment Center = new(new(0.0f, 0.0f));
  public static readonly Alignment CenterLeft = new(new(-1.0f, 0.0f));
  public static readonly Alignment CenterRight = new(new(1.0f, 0.0f));

  public static readonly Alignment TopCenter = new(new(0.0f, -1.0f));
  public static readonly Alignment TopLeft = new(new(-1.0f, -1.0f));
  public static readonly Alignment TopRight = new(new(1.0f, -1.0f));
}