using open_tk_renderer.Renderer.UI.Widgets.Layout.Utils;
using OpenTK.Mathematics;

namespace open_tk_renderer.Renderer.UI.Widgets.Layout;

public class Padding : Widget
{
  public EdgeInsets padding;

  public Padding(
    EdgeInsets? padding = null,
    Widget? child = null
  )
  {
    this.padding = padding ?? new EdgeInsets(0);
    if (child is not null) children.Add(child);
  }

  public override void CalcSize(BoxConstraints constraints)
  {
    size = constraints.Biggest;

    foreach (var child in children)
    {
      child.CalcSize(constraints.Deflate(padding));
      size = constraints.Constrain(child.size + padding.size * 2);
    }
  }

  public override void CalcPosition()
  {
    foreach (var child in children)
    {
      child.position = position + padding.size;
      child.CalcPosition();
    }
  }
}
