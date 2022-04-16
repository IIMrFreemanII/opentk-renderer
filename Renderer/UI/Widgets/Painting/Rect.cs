using open_tk_renderer.Renderer.UI.Widgets.Utils;
using OpenTK.Mathematics;

namespace open_tk_renderer.Renderer.UI.Widgets.Painting;

public class Rect : Widget
{
  public Rect(Color4? color, Vector2? size)
  {
    this.size = size ?? Vector2.Zero;
    
    DecoratedBox? decoratedBoxWidget = null;
    if (color is { }) decoratedBoxWidget = new DecoratedBox(new BoxDecoration(color));

    if (decoratedBoxWidget is { })
    {
      decoratedBoxWidget.parent = this;
      children.Add(decoratedBoxWidget);
    }
  }

  public override void CalcSize(BoxConstraints constraints)
  {
    size = constraints.Constrain(size);
    var newConstraints = BoxConstraints.Loose(size);
    foreach (var child in children)
    {
      child.CalcSize(newConstraints);
    }
  }

  public override void CalcPosition()
  {
    foreach (var child in children)
    {
      child.position = position;
      child.CalcPosition();
    }
  }
}
