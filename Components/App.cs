using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using open_tk_renderer.Aspects;
using open_tk_renderer.Renderer.UI;
using open_tk_renderer.Renderer.UI.Widgets;
using open_tk_renderer.Renderer.UI.Widgets.Layout;
using open_tk_renderer.Renderer.UI.Widgets.Painting;
using open_tk_renderer.Renderer.UI.Widgets.Utils;
using open_tk_renderer.Utils;
using OpenTK.Mathematics;
using PostSharp;
using PostSharp.Patterns.Model;
using Container = open_tk_renderer.Renderer.UI.Widgets.Layout.Container;

namespace open_tk_renderer.Components;

[NotifyPropertyChanged]
public class Cube
{
  public float Width { get; set; }
  public float Height { get; set; }
}

public class App : Component
{
  public class Props { }

  // [State(nameof(container1), nameof(Container.Width))]
  // [State(nameof(container2), nameof(Container.Width))]
  // public float Width { get; set; } = 100;
  // public Bind<float> height = new(100);
  public ObservableCollection<Cube> cubes = new();

  public App(Widget target)
  {
    Interval.Set(
      () =>
      {
        foreach (var cube in cubes)
        {
          cube.Width += 1;
          cube.Height += 1;
        }

        cubes.Add(new Cube { Width = 10, Height = 10 });
      },
      1000
    );

    Init(target);
  }

  public override void OnUpdate()
  {
    
  }

  public override void OnMount(Widget target)
  {
    MountRow(target);
  }

  private void MountRow(Widget target)
  {
    Row row = new();
    // todo: think how to write less boilerplate
    // todo: how to bind row to observable collection
    cubes.CollectionChanged += (obj, e) =>
    {
      switch (e.Action)
      {
        case NotifyCollectionChangedAction.Add:
        {
          if (e.NewItems?[0] is Cube cube)
          {
            var container = new Container(
              width: cube.Width,
              height: cube.Height,
              margin: EdgeInsets.All(2),
              bindings: new Bindings(cube, new()
              {
                {nameof(Cube.Width), nameof(Container.Width)},
                {nameof(Cube.Height), nameof(Container.Height)}
              }),
              decoration: new BoxDecoration(Colors.Green)
            );
            row.Append(container);
          }
          break;
        }
      }
    };
    target.Append(row);
  }
}
