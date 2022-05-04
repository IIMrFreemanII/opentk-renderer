using open_tk_renderer.Renderer.UI.Widgets.Utils;

namespace open_tk_renderer.Renderer.UI.Widgets.Layout;

public class Column : Flex
{
  public Column(
    MainAxisAlignment mainAxisAlignment = MainAxisAlignment.Start,
    CrossAxisAlignment crossAxisAlignment = CrossAxisAlignment.Start,
    VerticalDirection verticalDirection = VerticalDirection.Down,
    List<Widget>? children = null,
    Ref<Column>? @ref = null
  ) : base(
    Axis.Vertical,
    mainAxisAlignment,
    crossAxisAlignment,
    TextDirection.Ltr,
    verticalDirection,
    children
  )
  {
    if (@ref is { }) @ref.value = this;
  }
}
