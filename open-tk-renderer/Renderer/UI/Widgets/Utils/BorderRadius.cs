using OpenTK.Mathematics;

namespace open_tk_renderer.Renderer.UI.Widgets.Utils;

public struct BorderRadius
{
  public int topLeft;
  public int topRight;
  public int bottomRight;
  public int bottomLeft;

  private BorderRadius(
    int topLeft,
    int topRight,
    int bottomRight,
    int bottomLeft
  )
  {
    this.topLeft = topLeft;
    this.topRight = topRight;
    this.bottomRight = bottomRight;
    this.bottomLeft = bottomLeft;
  }

  public static BorderRadius All(int radius)
  {
    return new BorderRadius(
      radius,
      radius,
      radius,
      radius
    );
  }

  public static BorderRadius Only(
    int topLeft = 0,
    int topRight = 0,
    int bottomRight = 0,
    int bottomLeft = 0
  )
  {
    return new BorderRadius(
      topLeft,
      topRight,
      bottomRight,
      bottomLeft
    );
  }

  public Vector4 ToVec4()
  {
    return new Vector4(
      topLeft,
      topRight,
      bottomRight,
      bottomLeft
    );
  }
}
