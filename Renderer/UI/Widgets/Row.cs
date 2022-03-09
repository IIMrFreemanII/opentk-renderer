using OpenTK.Mathematics;

namespace open_tk_renderer.Renderer.UI.Widgets;

public class Row : Widget
{
  public Row(List<Widget> children)
  {
    foreach (var child in children)
    {
      child.parent = this;
    }

    this.children = children;
  }

  public override void CalcLayout(Vector2 parentSize)
  {
    size = parentSize;

    Vector2 nextPosition = position;
    Vector2 availableSize = size;
    Vector2 ownSize = Vector2.Zero;
    foreach (Widget child in children)
    {
      child.position = nextPosition;
      child.CalcLayout(availableSize);
      availableSize -= new Vector2(child.size.X, 0);
      // availableSize = Vector2i.Clamp(availableSize - new Vector2i(child.size.X, 0), Vector2i.Zero, size);
      nextPosition += new Vector2(child.size.X, 0);

      // full width
      ownSize += new Vector2(child.size.X, 0);

      // max height
      if (child.size.Y > ownSize.Y)
      {
        ownSize = ownSize with { Y = child.size.Y };
      }
    }

    size = ownSize;
  }
}