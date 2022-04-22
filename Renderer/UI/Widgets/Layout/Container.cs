using open_tk_renderer.Renderer.UI.Widgets.Painting;
using open_tk_renderer.Renderer.UI.Widgets.Utils;
using open_tk_renderer.temp;

namespace open_tk_renderer.Renderer.UI.Widgets.Layout;

public class Container : Widget
{
  public float Width
  {
    get => _sizedBox.width;
    set => _sizedBox.width = value;
  }
  public float Height
  {
    get => _sizedBox.height;
    set => _sizedBox.height = value;
  }
  private Bind<float>? bindedWidth;
  private Bind<float>? bindedHeight;
  
  public readonly EdgeInsets padding;
  public readonly EdgeInsets margin;
  // public Alignment? alignment;
  // public BoxConstraints? constraints;
  public readonly BoxDecoration decoration;
  public Widget? Child { get; set; }

  private Widget? lastElem;

  private Padding _margin;
  private DecoratedBox _decoratedBox;
  private SizedBox _sizedBox;
  private Padding _padding;
  private Bindings _bindings;

  public Container(
    Bind<float>? width = null,
    Bind<float>? height = null,
    BoxDecoration? decoration = null,
    Widget? child = null,
    EdgeInsets? margin = null,
    EdgeInsets? padding = null,
    // Alignment? alignment = null,
    // BoxConstraints? constraints = null,
    Ref<Container>? @ref = null,
    Bindings? bindings = null
  )
  {
    if (@ref is { }) @ref.value = this;
    if (bindings is { })
    {
      _bindings = bindings;
      _bindings.BindTo(this);
    }

    this.padding = padding ?? EdgeInsets.All(value: 0);
    this.margin = margin ?? EdgeInsets.All(value: 0);
    // this.alignment = alignment;
    // this.constraints = constraints;

    _margin = new Padding(this.margin);
    // if (margin is { }) marginWidget = new Padding(this.margin);

    this.decoration = decoration ?? new BoxDecoration();
    _decoratedBox = new DecoratedBox(this.decoration);
    // if (decoration is { }) decoratedBoxWidget = new DecoratedBox(this.decoration);

    _sizedBox = new SizedBox();
    if (width is { })
    {
      Width = width.Value;
      bindedWidth = width;
      bindedWidth.ValueChanged += OnWidthChange;
    }
    if (height is { })
    {
      Height = height.Value;
      bindedHeight = height;
      bindedHeight.ValueChanged += OnHeightChange;
    }

    _padding = new Padding(this.padding);
    // if (padding is { }) paddingWidget = new Padding(this.padding);

    // Align? alignWidget = null;
    // if (alignment is { }) alignWidget = new Align(this.alignment);

    Widget[] widgets =
      { _margin, _decoratedBox, _sizedBox, _padding };
    foreach (var widget in widgets)
      AppendToEnd(widget);


    if (child is { })
    {
      Child = child;
      AppendToEnd(child);
    }
  }

  ~Container()
  {
    if (bindedWidth is { })
    {
      bindedWidth.ValueChanged -= OnWidthChange;
    }
    if (bindedHeight is { })
    {
      bindedHeight.ValueChanged -= OnHeightChange;
    }
  }

  private void OnWidthChange(float value)
  {
    Width = value;
  }
  private void OnHeightChange(float value)
  {
    Height = value;
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
    var newConstraints = BoxConstraints.Loose(constraints.Biggest);

    foreach (var child in children)
    {
      child.CalcSize(newConstraints);
      size = child.size;
    }
  }
}
