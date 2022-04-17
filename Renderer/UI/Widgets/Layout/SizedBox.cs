using open_tk_renderer.Renderer.UI.Widgets.Utils;
using OpenTK.Mathematics;

namespace open_tk_renderer.Renderer.UI.Widgets.Layout;

public class SizedBox : Widget
{
  public float width;
  public float height;

  public Widget? Child { get; set; }

  public SizedBox(
    float width = 0,
    float height = 0,
    Widget? child = null,
    Ref<SizedBox>? @ref = null
  )
  {
    if (@ref is { }) @ref.value = this;

    this.width = width;
    this.height = height;

    if (child is { })
    {
      Child = child;
      child.parent = this;
      children.Add(child);
    }
  }

  public override void CalcSize(BoxConstraints constraints)
  {
    Vector2 temp = new(width, height);
    temp = constraints.Constrain(temp);

    var minWidth = temp.X;
    var minHeight = temp.Y;
    var maxWidth = temp.X == 0
      ? constraints.maxWidth
      : temp.X;
    var maxHeight = temp.Y == 0
      ? constraints.maxHeight
      : temp.Y;

    var tempConstraints = new BoxConstraints(
      minWidth,
      maxWidth,
      minHeight,
      maxHeight
    );

    foreach (var child in children)
    {
      child.CalcSize(tempConstraints);
      size = tempConstraints.Constrain(child.size);
    }

    if (children.Count == 0) size = tempConstraints.Smallest;
  }

  public override void CalcPosition()
  {
    base.CalcPosition();
    foreach (var child in children)
    {
      child.position = position;
      child.CalcPosition();
    }
  }
}
