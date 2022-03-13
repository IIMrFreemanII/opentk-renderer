using OpenTK.Mathematics;

namespace open_tk_renderer.Renderer.UI.Widgets.Layout;

public class Center : Widget
{
  public Vector2? sizeFactor;

  public Center(Widget? child = null, Vector2? sizeFactor = null)
  {
    if (child != null) children.Add(child);

    this.sizeFactor = sizeFactor;
  }

  public override void CalcSize(Vector2 parentSize)
  {
    size = parentSize;

    foreach (var child in children) child.CalcSize(size);
  }

  public override void CalcPosition()
  {
    foreach (var child in children)
    {
      if (sizeFactor != null) size = child.size * (Vector2)sizeFactor;
      child.position = size / 2 - child.size / 2;
    }
  }
}