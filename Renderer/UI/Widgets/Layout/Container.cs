using OpenTK.Mathematics;

namespace open_tk_renderer.Renderer.UI.Widgets.Layout;

public class Container : Widget
{
  public Color4 color;
  public EdgeInsets? padding;
  public EdgeInsets margin;
  public Alignment? alignment;

  public Container(
    Color4? color = null,
    Vector2? size = null,
    Widget? child = null,
    EdgeInsets? margin = null,
    EdgeInsets? padding = null,
    Alignment? alignment = null
  )
  {
    this.size = size ?? Vector2.Zero;
    this.color = color ?? Colors.DefaultBgColor;

    this.padding = padding;
    this.margin = margin ?? new EdgeInsets(0);
    this.alignment = alignment;

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

  public override void CalcSize(Vector2 parentSize)
  {
    size = size == Vector2.Zero
      ? parentSize
      : Vector2.Clamp(size + margin.size * 2, Vector2.Zero, parentSize);

    foreach (var child in children)
    {
      child.CalcSize(size);
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