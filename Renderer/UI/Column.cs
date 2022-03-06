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

  public override void CalcLayout(Window window)
  {
    base.CalcLayout(window);

    int nextY = position.Y;
    foreach (Widget child in children)
    {
      int childY = GetChildY(child);
      child.position = position with { Y = nextY };
      nextY += childY;
      child.CalcLayout(window);
    }

    // calc own size
    Vector2i size = Vector2i.Zero;
    foreach (var child in children)
    {
      // full height
      size += new Vector2i(0, child.size.Y);

      // max width
      if (child.size.X > size.X)
      {
        size += new Vector2i(child.size.X, 0);
      }
    }

    this.size = size;
  }

  private int GetChildY(Widget widget)
  {
    if (widget.size.Y == 0)
    {
      if (widget is Row)
      {
        int maxY = 0;

        foreach (var widgetChild in widget.children)
        {
          int temp = GetChildY(widgetChild);
          if (temp > maxY)
          {
            maxY = temp;
          }
        }

        return maxY;
      }

      int fullY = 0;

      foreach (var widgetChild in widget.children)
      {
        fullY += GetChildY(widgetChild);
      }

      return fullY;
    }

    return widget.size.Y;
  }
}