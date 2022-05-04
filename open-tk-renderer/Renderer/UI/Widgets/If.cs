using open_tk_renderer.Renderer.UI.Widgets.Utils;
using open_tk_renderer.temp;
using OpenTK.Mathematics;

namespace open_tk_renderer.Renderer.UI.Widgets;

public class If : Widget
{
  private bool _active = true;
  public bool Active
  {
    get => _active;
    set
    {
      _active = value;
      if (value)
      {
        if (children.Count == 0)
        {
          if (child is { })
          {
            children.Add(child);
          }
        }
      }
      else
      {
        if (children.Count > 0)
        {
          children.Clear();
        }
      }
    }
  }
  private Bind<bool>? _bindedActive;
  public Widget? child;
  
  public If(Bind<bool>? active = null, Widget? child = null)
  {
    if (active is { })
    {
      _bindedActive = active;
      Active = active.Value;
      active.ValueChanged += OnActiveChange;
    }
    if (child is { })
    {
      this.child = child;

      if (Active)
      {
        children.Add(child);
      }
    }
  }

  ~If()
  {
    if (_bindedActive is { })
    {
      _bindedActive.ValueChanged -= OnActiveChange;
    }
  }

  private void OnActiveChange(bool value)
  {
    Active = value;
  }

  public override void CalcSize(BoxConstraints constraints)
  {
    size = Vector2.Zero;
    
    foreach (var child in children)
    {
      child.CalcSize(constraints);
      size = constraints.Constrain(child.size);
    }
  }
}
