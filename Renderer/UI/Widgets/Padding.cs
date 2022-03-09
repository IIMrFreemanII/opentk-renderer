using OpenTK.Mathematics;

namespace open_tk_renderer.Renderer.UI.Widgets;

public class Padding : Widget
{
  public EdgeInsets padding;

  public Padding(
    EdgeInsets? padding = null,
    Widget? child = null
  )
  {
    this.padding = padding ?? new EdgeInsets(0);
    if (child != null)
    {
      children.Add(child);
    }
  }

  public override void CalcLayout(Vector2 parentSize)
  {
    size = parentSize;

    if (children.Count > 0)
    {
      Widget child = children[0];
      child.position = position + padding.size;
      child.CalcLayout(size - padding.size * 2);

      size = Vector2.Clamp(child.size + padding.size * 2, Vector2.Zero, size);
    }
  }
}