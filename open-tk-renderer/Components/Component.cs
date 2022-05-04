using open_tk_renderer.Renderer.UI.Widgets;

namespace open_tk_renderer.Components;

public class Component : Widget
{
  public Component()
  {
    var widget = OnMount();
    if (widget is { })
    {
      Append(widget);
    }
  }

  ~Component()
  {
    OnUnmount();
  }
  
  public virtual Widget? OnMount()
  {
    return null;
  }

  public virtual void OnUnmount()
  {
    
  }
}
