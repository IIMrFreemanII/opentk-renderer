using open_tk_renderer.Renderer.UI.Widgets.Layout.Utils;
using OpenTK.Mathematics;

namespace open_tk_renderer.Renderer.UI.Widgets.Layout;

public class Expanded : Widget
{
  public int flex;

  public Expanded(int flex = 1, Widget? child = null)
  {
    this.flex = flex;
    if (child is not null) children.Add(child);
  }

  public override void CalcSize(BoxConstraints constraints)
  {
    foreach (var child in children)
    {
      child.CalcSize(constraints);
      size = child.size;
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
