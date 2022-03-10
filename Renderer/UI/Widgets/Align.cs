using OpenTK.Mathematics;

namespace open_tk_renderer.Renderer.UI.Widgets;

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
      child.CalcLayout(size);

      if (sizeFactor.HasValue)
      {
        size = child.size * sizeFactor.Value;
      }

      child.position = position + (size / 2 - child.size / 2) + alignment.pivot * (size / 2 - child.size / 2);
      
      // recalculate again to take into account new position
      child.CalcLayout(size);
    }
  }
}