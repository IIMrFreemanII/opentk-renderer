using System.Collections.ObjectModel;
using System.Collections.Specialized;
using open_tk_renderer.Renderer.UI;
using open_tk_renderer.Renderer.UI.Widgets;
using open_tk_renderer.Renderer.UI.Widgets.Layout;
using open_tk_renderer.Renderer.UI.Widgets.Utils;
using open_tk_renderer.temp;
using open_tk_renderer.Utils;
using Container = open_tk_renderer.Renderer.UI.Widgets.Layout.Container;

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
  // public ObservableCollection<Cube> cubes = new();
  public Bind<bool> toggle = new(true);
  public App()
  {
    Interval.Set(
      () =>
      {
        // foreach (var cube in cubes)
        // {
        //   cube.width.Value += 1;
        //   cube.height.Value += 1;
        // }
        //
        // cubes.Add(new Cube(10, 10));
        toggle.Value = !toggle.Value;
      },
      1000
    );

    // Init(target);
    Row row = new(
      children: new()
      {
        new If(
          active: toggle,
          child: new Container(
            width: new(100),
            height: new(100),
            margin: EdgeInsets.All(10),
            decoration: new BoxDecoration(color: Colors.Red)
          )
        ),
        new Container(
          width: new(100),
          height: new(100),
          margin: EdgeInsets.All(10),
          decoration: new BoxDecoration(color: Colors.Green)
        )
      }
    );
    // cubes.CollectionChanged += (obj, e) =>
    // {
    //   switch (e.Action)
    //   {
    //     case NotifyCollectionChangedAction.Add:
    //     {
    //       if (e.NewItems?[0] is Cube cube)
    //       {
    //         var container = new Container(
    //           width: cube.width,
    //           height: cube.height,
    //           margin: EdgeInsets.All(2),
    //           // bindings: new Bindings(cube, new()
    //           // {
    //           //   {nameof(Cube.Width), nameof(Container.Width)},
    //           //   {nameof(Cube.Height), nameof(Container.Height)}
    //           // }),
    //           decoration: new BoxDecoration(Colors.Green)
    //         );
    //         row.Append(container);
    //       }
    //       break;
    //     }
    //   }
    // };
    children.Add(row);
  }
}
