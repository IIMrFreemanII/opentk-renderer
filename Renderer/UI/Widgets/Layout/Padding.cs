using open_tk_renderer.Renderer.UI.Widgets.Utils;

namespace open_tk_renderer.Renderer.UI.Widgets.Layout;

public class Padding : Widget
{
  public EdgeInsets padding;

  public Padding(
    EdgeInsets? padding = null,
    Widget? child = null,
    Ref<Padding>? @ref = null
  )
  {
    if (@ref is { }) @ref.value = this;
    
    this.padding = padding ?? EdgeInsets.All(0);
    if (child is not null) children.Add(child);
  }

  public override void CalcSize(BoxConstraints constraints)
  {
    size = constraints.Biggest;

    foreach (var child in children)
    {
      child.CalcSize(constraints.Deflate(padding));
      size = constraints.Constrain(padding.InflateSize(child.size));
    }
  }

  public override void CalcPosition()
  {
    foreach (var child in children)
    {
      child.position = position + padding.TopLeft;
      child.CalcPosition();
    }
  }
}
