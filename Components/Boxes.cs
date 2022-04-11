using open_tk_renderer.Renderer.UI;
using open_tk_renderer.Renderer.UI.Widgets;
using open_tk_renderer.Renderer.UI.Widgets.Layout;
using open_tk_renderer.Renderer.UI.Widgets.Painting;
using open_tk_renderer.Renderer.UI.Widgets.Utils;
using OpenTK.Mathematics;

namespace open_tk_renderer.Components;

public class Boxes : Component<Boxes.Props>
{
  public class Props { }

  public Row row = new(
    // MainAxisAlignment.Start,
    // CrossAxisAlignment.Stretch,
    // TextDirection.Ltr,
    children: new List<Widget>
    {
      // new Expanded(
      //   child: new Image("wall.jpeg")
      // ),
      // new Expanded(
      //   child: new Image("awesomeface.png")
      // )
      new Container(
        new BoxDecoration(Colors.Green),
        new Vector2(100)
      ),
      new Container(
        new BoxDecoration(Colors.Blue),
        new Vector2(100)
      )
    }
  );

  public Boxes(Widget target, Props props) : base(target, props)
  {
    target.Append(row);
  }
}
