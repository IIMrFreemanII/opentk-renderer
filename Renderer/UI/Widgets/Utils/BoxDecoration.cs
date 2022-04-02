using OpenTK.Mathematics;

namespace open_tk_renderer.Renderer.UI.Widgets.Utils;

public class BoxDecoration
{
  /// <summary>
  /// The color to fill in the background of the box.
  /// </summary>
  public Color4 color;

  /// <summary>
  /// A border to draw above the background
  /// </summary>
  public Border border;

  /// <summary>
  /// if non-null, the corners of this box are rounded by this BorderRadius
  /// </summary>
  public BorderRadius borderRadius;

  /// <summary>
  /// Returns the insets to apply when using this decoration on a box
  /// that has contents, so that the contents do not overlap the edges
  /// of the decoration. For example, if the decoration draws a frame
  /// around its edge, the padding would return the distance by which
  /// to inset the children so as to not overlap the frame.
  /// </summary>
  public EdgeInsets padding;

  public BoxDecoration(
    Color4? color = null,
    Border? border = null,
    BorderRadius? borderRadius = null
  )
  {
    this.color = color ?? Colors.DefaultBgColor;
    this.border = border ?? Border.All(Color4.Black, 0);
    this.borderRadius = borderRadius ?? BorderRadius.All(0);
    padding = EdgeInsets.FromLTRB(
      this.border.left,
      this.border.top,
      this.border.right,
      this.border.bottom
    );
  }
}
