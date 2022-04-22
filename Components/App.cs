using System.Collections.ObjectModel;
using open_tk_renderer.Renderer.UI;
using open_tk_renderer.Renderer.UI.Widgets.Layout;
using open_tk_renderer.Renderer.UI.Widgets.Utils;
using open_tk_renderer.temp;
using Container = open_tk_renderer.Renderer.UI.Widgets.Layout.Container;
using Timeout = open_tk_renderer.Utils.Timeout;

namespace open_tk_renderer.Components;

public class Cube
{
  public Bind<float> width = new();
  public Bind<float> height = new();

  public Cube(float width, float height)
  {
    this.width.Value = width;
    this.height.Value = height;
  }
}

public class App : Component
{
  public ObservableCollection<Cube> cubes = new()
    { new Cube(50, 50), new Cube(50, 50), new Cube(50, 50) };
  public Bind<bool> toggle = new(true);
  public App()
  {
    // Interval.Set(
    //   () =>
    //   {
    //     foreach (var cube in cubes)
    //     {
    //       cube.width.Value += 2;
    //       cube.height.Value += 2;
    //     }
    //
    //     cubes.Add(new Cube(50, 50));
    //     // toggle.Value = !toggle.Value;
    //   },
    //   1000
    // );
    Timeout.Set(
      () =>
      {
        // cubes.Remove(cubes.Last());
        // cubes.Clear();
        cubes[^2] = new Cube(100, 100);
      }, 2000);

    var itemList = new FlexList<Cube>(
      cubes,
      cube => new Container(
        cube.width,
        cube.height,
        margin: EdgeInsets.All(5),
        decoration: new BoxDecoration(color: Colors.Red)
      )
    );
    children.Add(itemList);

    // Row row = new(
    //   children: new()
    //   {
    //     new If(
    //       active: toggle,
    //       child: new Container(
    //         width: new(100),
    //         height: new(100),
    //         margin: EdgeInsets.All(10),
    //         decoration: new BoxDecoration(color: Colors.Red)
    //       )
    //     ),
    //     new Container(
    //       width: new(100),
    //       height: new(100),
    //       margin: EdgeInsets.All(10),
    //       decoration: new BoxDecoration(color: Colors.Green)
    //     )
    //   }
    // );
    // children.Add(row);
  }
}
