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

  public Container(
    Color4? color = null,
    Vector2? size = null,
    Widget? child = null,
    EdgeInsets? margin = null,
    EdgeInsets? padding = null,
    Alignment? alignment = null,
    BoxConstraints? constraints = null
  )
  {
    this.size = size ?? Vector2.Zero;
    this.color = color ?? Colors.DefaultBgColor;

    this.padding = padding;
    this.margin = margin ?? EdgeInsets.All(0);
    this.alignment = alignment;
    this.constraints = constraints;

    children.Add(
      new Padding(
        margin,
        new DecoratedBox(
          color,
          new Padding(
            padding,
            new Align(
              alignment,
              child
            )
          )
        )
      )
    );
  }

  public override void CalcSize(BoxConstraints constraints)
  {
    size = size == Vector2.Zero
      ? constraints.Biggest
      : constraints.Constrain(margin.InflateSize(size));

    foreach (var child in children)
    {
      child.CalcSize(
        this.constraints ?? BoxConstraints.Loose(size)
      );
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
