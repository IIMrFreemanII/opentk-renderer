using open_tk_renderer.Renderer.UI;
using open_tk_renderer.Renderer.UI.Widgets;
using open_tk_renderer.Renderer.UI.Widgets.Layout;
using open_tk_renderer.Renderer.UI.Widgets.Painting;
using open_tk_renderer.Renderer.UI.Widgets.Utils;
using open_tk_renderer.Utils;
using OpenTK.Mathematics;

namespace open_tk_renderer.Components;

public class App : Component<App.Props>
{
  public class Props { }

  public Ref<Container> container = new();

  public App(Widget target, Props props) : base(target, props)
  {
    // Widget root = new Center(
    //   new Container(
    //     // width: 100,
    //     // height: 100,
    //     // margin: EdgeInsets.All(10),
    //     // padding: EdgeInsets.All(10),
    //     // alignment: Alignment.Center,
    //     decoration: new BoxDecoration(Colors.Red),
    //     child: new Rect(Colors.Green, new Vector2(value: 100)),
    //     @ref: container
    //   )
    // );
    // target.Append(root);
    //
    // Interval.Set(
    //   () =>
    //   {
    //     container.value.padding.top += 10;
    //     Console.WriteLine(container.value.Width);
    //   }, 1000);
    //------------------------------------------------------------
    {
      Row row = new(
        children: new List<Widget>
        {
          new Container(
            width: 100,
            height: 100,
            // margin: EdgeInsets.All(10),
            padding: EdgeInsets.All(value: 10),
            // alignment: Alignment.Center,
            decoration: new BoxDecoration(Colors.Green),
            child: new Rect(Colors.Red, new Vector2(value: 100))
          ),
          new Container(
            width: 100,
            height: 100,
            // margin: EdgeInsets.All(10),
            padding: EdgeInsets.All(value: 10),
            // alignment: Alignment.Center,
            decoration: new BoxDecoration(Colors.Red),
            child: new Rect(Colors.Green, new Vector2(value: 100))
          )
        }
      );
      target.Append(row);
    }
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
