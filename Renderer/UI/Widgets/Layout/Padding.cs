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

    this.padding = padding ?? EdgeInsets.All(value: 0);
    if (child is not null) children.Add(child);
  }

  public override void CalcSize(BoxConstraints constraints)
  {
    size = constraints.Biggest;
    var newConstraints = BoxConstraints.Loose(size);

    foreach (var child in children)
    {
      child.CalcSize(newConstraints.Deflate(padding));
      size = newConstraints.Constrain(padding.InflateSize(child.size));
    }
  }

  public override void CalcPosition()
  {
    base.CalcPosition();
    foreach (var child in children)
    {
      child.position = position + padding.TopLeft;
      child.CalcPosition();
    }
  }
}
