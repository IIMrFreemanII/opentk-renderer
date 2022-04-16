using open_tk_renderer.Renderer.UI.Widgets.Utils;
using OpenTK.Mathematics;

namespace open_tk_renderer.Renderer.UI.Widgets.Layout;

public class SizedBox : Widget
{
  public float? width;
  public float? height;

  public Widget? Child { get; set; }

  public SizedBox(
    float? width = null,
    float? height = null,
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
    Vector2 temp = new(width ?? 0, height ?? 0);
    temp = constraints.Constrain(temp);

    float minWidth = temp.X;
    float minHeight = temp.Y;
    float maxWidth = temp.X == 0
      ? constraints.maxWidth
      : temp.X;
    float maxHeight = temp.Y == 0
      ? constraints.maxHeight
      : temp.Y;

    BoxConstraints tempConstraints = new BoxConstraints(
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

    if (children.Count == 0)
    {
      size = tempConstraints.Smallest;
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
