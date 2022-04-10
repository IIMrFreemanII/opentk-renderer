using open_tk_renderer.Renderer.UI.Widgets;
using open_tk_renderer.Renderer.UI.Widgets.Layout;
using open_tk_renderer.Renderer.UI.Widgets.Painting;
using open_tk_renderer.Renderer.UI.Widgets.Utils;

namespace open_tk_renderer.Components;

public class Boxes : Component<Boxes.Props>
{
  public class Props { }

  public Row row = new(
    MainAxisAlignment.Start,
    CrossAxisAlignment.Stretch,
    TextDirection.Ltr,
    new List<Widget>
    {
      new Expanded(
        child: new Image("wall.jpeg")
      ),
      new Expanded(
        child: new Image("awesomeface.png")
      )
      // new Container(
      //   new BoxDecoration(Colors.Green),
      //   new Vector2(100),
      //   new Container(size: new Vector2(30))
      // ),
      // new Container(
      //   new BoxDecoration(Colors.Blue),
      //   new Vector2(100),
      //   new Container(size: new Vector2(30))
      // )
    }
  );

  public Boxes(Widget target, Props props) : base(target, props)
  {
    target.Append(row);
  }
}
