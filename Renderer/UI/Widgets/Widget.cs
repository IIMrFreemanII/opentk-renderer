using OpenTK.Mathematics;

namespace open_tk_renderer.Renderer.UI.Widgets;

public class Widget
{
  protected bool mounted = false;
  public Widget? parent;
  public List<Widget> children = new();
  public Vector2 position = new(0);
  public Vector2 size = new(0);

  public event Action<Widget>? Rebuild;

  protected void OnRebuild(Widget widget)
  {
    Rebuild?.Invoke(widget);
  }

  public virtual Widget Build()
  {
    return this;
  }

  public virtual void Mount()
  {
    mounted = true;
  }

  public virtual void UnMount()
  {
    mounted = false;
  }
  
  public virtual void Layout()
  {
  }

  public virtual void CalcSize(Vector2 parentSize)
  {
  }

  // public virtual void ResetSize()
  // {
  //   size = Vector2.Zero;
  //   foreach (var child in children)
  //   {
  //     child.ResetSize();
  //   }
  // }

  public virtual void CalcPosition()
  {
  }

  public virtual void Render()
  {
  }
}