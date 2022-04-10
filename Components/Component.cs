using open_tk_renderer.Renderer.UI.Widgets;

namespace open_tk_renderer.Components;

public class Component<T> where T : class
{
  public Widget target;
  public T? props;

  public Component(Widget target, T? props = null)
  {
    this.target = target;
    this.props = props;
  }
}
