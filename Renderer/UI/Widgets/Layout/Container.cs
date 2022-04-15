using open_tk_renderer.Renderer.UI.Widgets.Painting;
using open_tk_renderer.Renderer.UI.Widgets.Utils;
using OpenTK.Mathematics;

namespace open_tk_renderer.Renderer.UI.Widgets.Layout;

public class Container : Widget
{
  public Color4 color;
  public EdgeInsets? padding;
  public EdgeInsets margin;
  public Alignment? alignment;
  public BoxConstraints? constraints;
  public BoxDecoration decoration;

  private Widget lastChild;

  public Container(
    BoxDecoration? decoration = null,
    Vector2? size = null,
    Widget? child = null,
    EdgeInsets? margin = null,
    EdgeInsets? padding = null,
    Alignment? alignment = null,
    BoxConstraints? constraints = null,
    Ref<Container>? @ref = null
  )
  {
    if (@ref is { }) @ref.value = this;
    
    this.size = size ?? Vector2.Zero;
    this.decoration = decoration ?? new BoxDecoration();

    this.padding = padding;
    this.margin = margin ?? EdgeInsets.All(0);
    this.alignment = alignment;
    this.constraints = constraints;

    lastChild = new Align(
      alignment,
      child
    );
    children.Add(
      new Padding(
        margin,
        new DecoratedBox(
          decoration,
          new Padding(
            padding,
            lastChild
          )
        )
      )
    );
  }

  public override void Append(Widget widget)
  {
    lastChild.children.Add(widget);
    widget.parent = lastChild;
  }

  public override void CalcSize(BoxConstraints constraints)
  {
    size = size == Vector2.Zero
      ? constraints.Biggest
      : constraints.Constrain(margin.InflateSize(size));

    foreach (var child in children)
      child.CalcSize(
        this.constraints ?? BoxConstraints.Loose(size)
      );
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
