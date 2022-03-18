using open_tk_renderer.Renderer.UI.Widgets.Utils;

namespace open_tk_renderer.Renderer.UI.Widgets.Layout;

public class Row : Flex
{
  public Row(
    MainAxisAlignment mainAxisAlignment = MainAxisAlignment.Start,
    CrossAxisAlignment crossAxisAlignment = CrossAxisAlignment.Start,
    TextDirection textDirection = TextDirection.Ltr,
    List<Widget>? children = null
  ) : base(
    Axis.Horizontal,
    mainAxisAlignment,
    crossAxisAlignment,
    textDirection,
    VerticalDirection.Down,
    children
  ) { }
}
