using OpenTK.Mathematics;

namespace open_tk_renderer.Renderer.UI;

public class Column : Widget
{
  public Column(List<Widget> children)
  {
    foreach (var child in children)
    {
      child.parent = this;
    }

    this.children = children;
  }

  public override void CalcLayout(Vector2i parentSize)
  {
    size = parentSize;

    Vector2i nextPosition = position;
    Vector2i availableSize = size;
    Vector2i ownSize = Vector2i.Zero;
    foreach (Widget child in children)
    {
      child.position = nextPosition;
      child.CalcLayout(availableSize);
      availableSize -= new Vector2i(0, child.size.Y);
      // availableSize = Vector2i.Clamp(availableSize - new Vector2i(0, child.size.Y), Vector2i.Zero, size);
      nextPosition += new Vector2i(0, child.size.Y);

      // full height
      ownSize += new Vector2i(0, child.size.Y);
      
      // max width
      if (child.size.X > ownSize.X)
      {
        ownSize = ownSize with { X = child.size.X };
      }
    }

    size = ownSize;
  }
}