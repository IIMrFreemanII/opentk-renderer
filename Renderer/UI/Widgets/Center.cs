using OpenTK.Mathematics;

namespace open_tk_renderer.Renderer.UI.Widgets;

public class Center : Widget
{
  public Vector2? sizeFactor;
  
  public Center(Widget? child = null, Vector2? sizeFactor = null)
  {
    if (child != null)
    {
      children.Add(child);
    }

    this.sizeFactor = sizeFactor;
  }

  public override void CalcLayout(Vector2 parentSize)
  {
    size = parentSize;
    if (children.Count > 0)
    {
      Widget child = children[0];
      child.CalcLayout(size);

      if (sizeFactor != null)
      {
        size = child.size * (Vector2)sizeFactor;
      }
      child.position = size / 2 - child.size / 2;
    }
  }
}