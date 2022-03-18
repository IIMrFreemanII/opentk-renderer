using OpenTK.Mathematics;

namespace open_tk_renderer.Renderer.UI.Widgets.Utils;

public struct EdgeInsets
{
  public readonly int left;
  public readonly int top;
  public readonly int right;
  public readonly int bottom;

  /// <summary>
  /// The total offset in the vertical direction.
  /// </summary>
  public int Vertical => top + bottom;

  /// <summary>
  /// The total offset in the horizontal direction.
  /// </summary>
  public int Horizontal => left + right;

  public Vector2 TopLeft => new (left, top);

  private EdgeInsets(
    int left,
    int top,
    int right,
    int bottom
  )
  {
    this.left = left;
    this.top = top;
    this.right = right;
    this.bottom = bottom;
  }

  public static EdgeInsets All(int value)
  {
    return new EdgeInsets(value, value, value, value);
  }

  public static EdgeInsets Only(
    int left = 0,
    int top = 0,
    int right = 0,
    int bottom = 0
  )
  {
    return new EdgeInsets(left, top, right, bottom);
  }

  public static EdgeInsets Symmetric(int vertical = 0, int horizontal = 0)
  {
    return new EdgeInsets(
      horizontal,
      vertical,
      horizontal,
      vertical
    );
  }

  public static EdgeInsets FromLTRB(
    int left,
    int top,
    int right,
    int bottom
  )
  {
    return new EdgeInsets(left, top, right, bottom);
  }

  /// <summary>
  /// Returns a new size that is bigger than the given size by the amount of inset in the horizontal and vertical directions.
  /// </summary>
  /// <param name="size"></param>
  /// <returns></returns>
  public Vector2 InflateSize(Vector2 size)
  {
    return size + new Vector2(Horizontal, Vertical);
  }

  /// <summary>
  /// Returns a new size that is smaller than the given size by the amount of inset in the horizontal and vertical directions.
  /// </summary>
  /// <param name="size"></param>
  /// <returns></returns>
  public Vector2 DeflateSize(Vector2 size)
  {
    return size - new Vector2(Horizontal, Vertical);
  }
}
