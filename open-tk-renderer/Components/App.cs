using System.Collections.ObjectModel;
using open_tk_renderer.Renderer.UI;
using open_tk_renderer.Renderer.UI.Widgets;
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

  public override Widget? OnMount()
  {
    Timeout.Set(
      () =>
      {
        // cubes.Remove(cubes.Last());
        // cubes.Clear();
        cubes[^2] = new Cube(100, 100);
      },
      2000
    );
    
    return new FlexList<Cube>(
      cubes,
      cube => new Container(
        cube.width,
        cube.height,
        margin: EdgeInsets.All(5),
        decoration: new BoxDecoration(color: Colors.Red)
      )
    );
  }
}
