using open_tk_renderer.Renderer.UI.Widgets.Utils;
using OpenTK.Mathematics;

namespace open_tk_renderer.Renderer.UI.Widgets.Layout;

public class Center : Widget
{
  public Vector2? sizeFactor;

  public Center(
    Widget? child = null,
    Vector2? sizeFactor = null,
    Ref<Center>? @ref = null
  )
  {
    if (@ref is { }) @ref.value = this;

    if (child != null) children.Add(child);

    this.sizeFactor = sizeFactor;
  }

  public override void CalcSize(BoxConstraints constraints)
  {
    size = constraints.Biggest;
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
      if (sizeFactor != null) size = child.size * (Vector2)sizeFactor;
      child.position = size / 2 - child.size / 2;
      child.CalcPosition();
    }
  }
}
