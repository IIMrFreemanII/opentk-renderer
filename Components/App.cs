using open_tk_renderer.Renderer.UI;
using open_tk_renderer.Renderer.UI.Widgets;
using open_tk_renderer.Renderer.UI.Widgets.Layout;
using open_tk_renderer.Renderer.UI.Widgets.Utils;
using OpenTK.Mathematics;

namespace open_tk_renderer.Components;

public class App : Component<App.Props>
{
  public class Props { }

  public Container container = new(
    size: new Vector2(100),
    decoration: new BoxDecoration(
      Colors.Blue,
      Border.All(Color4.Black, 10),
      BorderRadius.All(10)
    )
  );

  public Row row = new(
    // mainAxisAlignment: MainAxisAlignment.Start,
    // CrossAxisAlignment.Stretch,
    // textDirection: TextDirection.Ltr,
    children: new List<Widget>
    {
      new Container(
        size: new Vector2(100),
        decoration: new BoxDecoration(
          Colors.Blue,
          Border.All(Color4.Black, 10),
          BorderRadius.All(10)
        )
      ),
      new Container(
        size: new Vector2(100),
        decoration: new BoxDecoration(
          Colors.Green,
          Border.All(Color4.Black, 10),
          BorderRadius.All(10)
        )
      )
    }
  );

  public App(Widget target, Props props) : base(target, props)
  {
    target.Append(row);
    // var boxes = new Boxes(elem, new Boxes.Props());
  }
}
