using System;
using OpenTK.Mathematics;

namespace open_tk_renderer.Renderer.UI.Widgets.Utils;

public struct BoxConstraints
{
  public float minWidth;
  public float maxWidth;
  public float minHeight;
  public float maxHeight;

  /// <summary>
  /// The biggest size that satisfies the constraints.
  /// </summary>
  public Vector2 Biggest => new(maxWidth, maxHeight);

  /// <summary>
  /// The smallest size that satisfies the constraints.
  /// </summary>
  public Vector2 Smallest => new(minWidth, minHeight);

  public Vector2 MaxSize
  {
    get => new(maxWidth, maxHeight);
    set
    {
      maxWidth = value.X;
      maxHeight = value.Y;
    }
  }

  public Vector2 MinSize
  {
    get => new(minWidth, minHeight);
    set
    {
      minWidth = value.X;
      minHeight = value.Y;
    }
  }

  public BoxConstraints(
    float minWidth = 0,
    float maxWidth = float.PositiveInfinity,
    float minHeight = 0,
    float maxHeight = float.PositiveInfinity
  )
  {
    this.minWidth = Math.Clamp(minWidth, 0, float.PositiveInfinity);
    this.maxWidth = Math.Clamp(maxWidth, 0, float.PositiveInfinity);
    this.minHeight = Math.Clamp(minHeight, 0, float.PositiveInfinity);
    this.maxHeight = Math.Clamp(maxHeight, 0, float.PositiveInfinity);
  }

  /// <summary>
  /// Creates box constraints that is respected only by the given size.
  /// </summary>
  /// <param name="size"></param>
  /// <returns></returns>
  public static BoxConstraints Tight(Vector2 size)
  {
    return new BoxConstraints(size.X, size.X, size.Y, size.Y);
  }

  /// <summary>
  /// Creates box constraints that forbid sizes larger than the given size.
  /// </summary>
  /// <param name="size"></param>
  /// <returns></returns>
  public static BoxConstraints Loose(Vector2 size)
  {
    return new BoxConstraints(0, size.X, 0, size.Y);
  }

  /// <summary>
  /// Returns the size that both satisfies the constraints and is as close as possible to the given size.
  /// </summary>
  /// <param name="size"></param>
  /// <returns></returns>
  public Vector2 Constrain(Vector2 size)
  {
    return Vector2.Clamp(
      size,
      new Vector2(minWidth, minHeight),
      new Vector2(maxWidth, maxHeight)
    );
  }

  /// <summary>
  /// Returns new box constraints that are smaller by the given edge dimensions.
  /// </summary>
  /// <param name="edgeInsets"></param>
  /// <returns></returns>
  public BoxConstraints Deflate(EdgeInsets edgeInsets)
  {
    return new BoxConstraints(
      minWidth,
      maxWidth - edgeInsets.Horizontal,
      minHeight,
      maxHeight - edgeInsets.Vertical
    );
  }
}
