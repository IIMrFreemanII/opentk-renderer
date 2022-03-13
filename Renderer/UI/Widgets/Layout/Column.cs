using OpenTK.Mathematics;

namespace open_tk_renderer.Renderer.UI.Widgets.Layout;

public class Column : Widget
{
  public MainAxisAlignment mainAxisAlignment;
  public CrossAxisAlignment crossAxisAlignment;
  public TextDirection textDirection;
  public VerticalDirection verticalDirection;

  public Column(
    List<Widget> children,
    MainAxisAlignment mainAxisAlignment = MainAxisAlignment.Start,
    CrossAxisAlignment crossAxisAlignment = CrossAxisAlignment.Start,
    TextDirection textDirection = TextDirection.Ltr,
    VerticalDirection verticalDirection = VerticalDirection.Down
  )
  {
    foreach (var child in children) child.parent = this;

    this.children = children;
  }

  // public override void CalcLayout(Vector2 parentSize)
  // {
  //   size = parentSize;
  //
  //   var nextPosition = position;
  //   var availableSize = size;
  //   // Vector2 ownSize = Vector2.Zero;
  //   foreach (var child in children)
  //   {
  //     child.position = nextPosition;
  //     child.CalcLayout(availableSize);
  //     availableSize -= new Vector2(0, child.size.Y);
  //     // availableSize = Vector2i.Clamp(availableSize - new Vector2i(0, child.size.Y), Vector2i.Zero, size);
  //     nextPosition += new Vector2(0, child.size.Y);
  //
  //     // // full height
  //     // ownSize += new Vector2(0, child.size.Y);
  //     //
  //     // // max width
  //     // if (child.size.X > ownSize.X)
  //     // {
  //     //   ownSize = ownSize with { X = child.size.X };
  //     // }
  //   }
  //
  //   // size = size with { X = ownSize.X };
  // }
}