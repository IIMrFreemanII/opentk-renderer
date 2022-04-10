using System;
using System.Collections.Generic;
using open_tk_renderer.Renderer.UI.Widgets.Utils;
using OpenTK.Mathematics;

namespace open_tk_renderer.Renderer.UI.Widgets;

public class Widget
{
  protected bool mounted = false;
  public Widget? parent;
  public List<Widget> children = new();
  public Vector2 position = new(0);
  public Vector2 size = new(0);

  public event Action<Widget>? Rebuild;

  protected void OnRebuild(Widget widget)
  {
    Rebuild?.Invoke(widget);
  }

  public virtual Widget Build()
  {
    return this;
  }

  public virtual void Append(Widget widget)
  {
    widget.parent = this;
    children.Add(widget);
  }

  public virtual void Layout() { }

  public virtual void CalcSize(BoxConstraints constraints) { }

  public virtual void CalcPosition() { }

  public virtual void Render() { }
}
