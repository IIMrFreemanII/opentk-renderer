using open_tk_renderer.Renderer.UI.Widgets;

namespace open_tk_renderer.Components;

public class Component : Widget
{
  // public Widget target;
  // public T? props;

  public void Init(Widget target)
  {
    // this.target = target;
    // OnMount(target);
  }

  public virtual void OnUpdate() { }

  public virtual void OnMount(Widget target) { }

  public virtual void OnUnMount() { }
}
