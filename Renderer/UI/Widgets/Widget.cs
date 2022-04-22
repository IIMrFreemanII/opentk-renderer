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

  public virtual void RemoveSelf()
  {
    if (parent is { })
    {
      parent.children.Remove(this);
      parent = null;
    }
  }

  public virtual void RemoveAt(int index)
  {
    children[index].parent = null;
    children.RemoveAt(index);
  }
  
  public virtual void Remove(Widget child)
  {
    child.parent = null;
    children.Remove(child);
  }

  public virtual void Clear()
  {
    foreach (var child in children)
    {
      child.parent = null;
    }
    children.Clear();
  }

  public virtual void Replace(int index, Widget child)
  {
    var oldChild = children[index];
    oldChild.parent = null;
    child.parent = this;
    children[index] = child;
  }

  public virtual void Layout()
  {
    foreach (var child in children)
    {
      child.Layout();
    }
  }

  public virtual void CalcSize(BoxConstraints constraints)
  {
    size = constraints.Biggest;
    
    foreach (var child in children)
    {
      child.CalcSize(constraints);
      size = constraints.Constrain(child.size);
    }
  }

  public virtual void CalcPosition()
  {
    foreach (var child in children)
    {
      child.position = position;
      child.CalcPosition();
    }
  }

  public virtual void Render()
  {
    foreach (var child in children)
    {
      child.Render();
    }
  }
}
