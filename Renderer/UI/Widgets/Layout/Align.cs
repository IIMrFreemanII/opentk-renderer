using OpenTK.Mathematics;

namespace open_tk_renderer.Renderer.UI.Widgets.Layout;

public class Align : Widget
{
  public Vector2? sizeFactor;
  public Alignment alignment;

  public Align(
    Alignment? alignment = null,
    Widget? child = null,
    Vector2? sizeFactor = null
  )
  {
    this.alignment = alignment ?? Alignment.TopLeft;
    this.sizeFactor = sizeFactor;

    if (child != null) children.Add(child);
  }

  public override void CalcSize(Vector2 parentSize)
  {
    size = parentSize;

    foreach (var child in children)
    {
      child.CalcSize(size);
      if (sizeFactor.HasValue) size = child.size * sizeFactor.Value;
    }
  }

  public override void CalcPosition()
  {
    foreach (var child in children)
    {
      child.position = position + (size / 2 - child.size / 2) + alignment.pivot * (size / 2 - child.size / 2);
      child.CalcPosition();
    }
  }
}