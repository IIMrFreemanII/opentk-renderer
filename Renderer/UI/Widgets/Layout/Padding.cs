using OpenTK.Mathematics;

namespace open_tk_renderer.Renderer.UI.Widgets.Layout;

public class Padding : Widget
{
  public EdgeInsets padding;

  public Padding(
    EdgeInsets? padding = null,
    Widget? child = null
  )
  {
    this.padding = padding ?? new EdgeInsets(0);
    if (child != null) children.Add(child);
  }

  public override void CalcSize(Vector2 parentSize)
  {
    size = parentSize;

    foreach (var child in children)
    {
      Vector2 sizeMinusPadding = size - padding.size * 2;
      child.CalcSize(sizeMinusPadding);
      size = Vector2.Clamp(child.size + padding.size * 2, Vector2.Zero, size);
    }
  }

  public override void CalcPosition()
  {
    foreach (var child in children)
    {
      child.position = position + padding.size;
      child.CalcPosition();
    }
  }
}