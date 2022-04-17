using open_tk_renderer.Renderer.UI.Widgets.Utils;
using OpenTK.Mathematics;

namespace open_tk_renderer.Renderer.UI.Widgets;

public class Widget
{
  public Widget? parent;
  public List<Widget> children = new();
  public Vector2 position = new(value: 0);
  public Vector2 size = new(value: 0);
  public bool dirty = true;

  public virtual void Append(Widget widget)
  {
    widget.parent = this;
    children.Add(widget);
  }

  public virtual void Remove()
  {
    if (parent is { }) parent.children.Remove(this);
  }

  public virtual void Clear()
  {
    children.Clear();
  }

  public virtual void Layout() { }

  public virtual void CalcSize(BoxConstraints constraints) { }

  public virtual void CalcPosition()
  {
    dirty = false;
  }

  public virtual void Render() { }
}
