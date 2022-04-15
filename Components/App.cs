using open_tk_renderer.Renderer.UI;
using open_tk_renderer.Renderer.UI.Widgets;
using open_tk_renderer.Renderer.UI.Widgets.Layout;
using open_tk_renderer.Renderer.UI.Widgets.Utils;
using OpenTK.Mathematics;

namespace open_tk_renderer.Components;

public class App : Component<App.Props>
{
  public class Props { }

  public App(Widget target, Props props) : base(target, props)
  {
    Container container = new(
      size: new Vector2(100, 100),
      margin: EdgeInsets.All(10),
      decoration: new BoxDecoration(Colors.Red)
    );
    target.Append(container);
    // {
    //   Container container = new(
    //     size: new Vector2(500, 100),
    //     margin: EdgeInsets.Only(bottom: 10),
    //     decoration: new BoxDecoration(
    //       Colors.Red
    //     )
    //   );
    //   Row row = new(
    //     children: new List<Widget>
    //     {
    //       new Container(
    //         size: new Vector2(100),
    //         decoration: new BoxDecoration(
    //           Colors.Blue,
    //           Border.All(Colors.DefaultBgColor, 5),
    //           BorderRadius.All(10)
    //         ),
    //         margin: EdgeInsets.Only(right: 10)
    //       ),
    //       new Container(
    //         size: new Vector2(100),
    //         decoration: new BoxDecoration(
    //           Colors.Green,
    //           Border.All(Colors.DefaultBgColor, 5),
    //           BorderRadius.All(10)
    //         )
    //       )
    //     }
    //   );
    //   container.Append(row);
    //   target.Append(container);
    // }
    // {
    //   Container container = new(
    //     size: new Vector2(500, 100),
    //     decoration: new BoxDecoration(
    //       Colors.Green
    //     )
    //   );
    //   Row row = new(
    //     children: new List<Widget>
    //     {
    //       new Container(
    //         size: new Vector2(100),
    //         decoration: new BoxDecoration(
    //           Colors.Blue,
    //           Border.All(Colors.DefaultBgColor, 5),
    //           BorderRadius.All(10)
    //         ),
    //         margin: EdgeInsets.Only(right: 10)
    //       ),
    //       new Container(
    //         size: new Vector2(100),
    //         decoration: new BoxDecoration(
    //           Colors.Green,
    //           Border.All(Colors.DefaultBgColor, 5),
    //           BorderRadius.All(10)
    //         )
    //       )
    //     }
    //   );
    //   container.Append(row);
    //   target.Append(container);
    // }
  }
}
