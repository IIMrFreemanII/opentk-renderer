using open_tk_renderer.Renderer.UI.Widgets.Painting;
using open_tk_renderer.Renderer.UI.Widgets.Utils;
using OpenTK.Mathematics;

namespace open_tk_renderer.Renderer.UI.Widgets.Layout;

public class Container : Widget
{
  public float? width;
  public float? height;
  public EdgeInsets? padding;
  public EdgeInsets margin;
  public Alignment? alignment;
  public BoxConstraints? constraints;
  public BoxDecoration? decoration;
  public Widget? Child { get; set; }

  private Widget? lastElem;

  public Container(
    float? width = null,
    float? height = null,
    BoxDecoration? decoration = null,
    Widget? child = null,
    EdgeInsets? margin = null,
    EdgeInsets? padding = null,
    Alignment? alignment = null,
    BoxConstraints? constraints = null,
    Ref<Container>? @ref = null
  )
  {
    if (@ref is { }) @ref.value = this;

    this.width = width;
    this.height = height;
    // this.size = size ?? Vector2.Zero;
    this.decoration = decoration;

    this.padding = padding;
    this.margin = margin ?? EdgeInsets.All(0);
    this.alignment = alignment;
    this.constraints = constraints;

    Padding? marginWidget = null;
    if (margin is { }) marginWidget = new Padding(this.margin);

    SizedBox? sizedBoxWidget = null;
    if (width is { } || height is { }) sizedBoxWidget = new SizedBox(this.width, this.height);

    DecoratedBox? decoratedBoxWidget = null;
    if (decoration is { }) decoratedBoxWidget = new DecoratedBox(this.decoration);

    Padding? paddingWidget = null;
    if (padding is { }) paddingWidget = new Padding(this.padding);

    Align? alignWidget = null;
    if (alignment is { }) alignWidget = new Align(this.alignment);

    Widget?[] widgets =
      { marginWidget, decoratedBoxWidget, sizedBoxWidget, paddingWidget, alignWidget };
    foreach (var widget in widgets)
      if (widget is { })
        AppendToEnd(widget);

    if (child is { })
    {
      Child = child;
      AppendToEnd(child);
    }
  }

  public override void Append(Widget widget)
  {
    if (Child is { }) throw new Exception("Container can only have one child");

    Child = widget;
    base.Append(widget);
  }

  private void AppendToEnd(Widget widget)
  {
    if (lastElem is { })
    {
      lastElem.Append(widget);
      lastElem = widget;
    }
    else
    {
      widget.parent = this;
      children.Add(widget);
      lastElem = widget;
    }
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
