using open_tk_renderer.Renderer.UI.Widgets.Utils;
using OpenTK.Mathematics;

namespace open_tk_renderer.Renderer.ImGui;

public class Node
{
  public Vector2 position;
  public Vector2 size;
  public List<Node> children = new();

  public virtual void CalcSize(BoxConstraints constraints)
  {
    size = constraints.Biggest;

    for (int i = 0; i < children.Count; i++)
    {
      var child = children[i];
      child.CalcSize(constraints);
      size = constraints.Constrain(child.size);
    }
  }
  
  public virtual void CalcPosition()
  {
    for (int i = 0; i < children.Count; i++)
    {
      var child = children[i];
      child.position = position;
      child.CalcPosition();
    }
  }
  
  public virtual void Layout()
  {
    for (int i = 0; i < children.Count; i++)
    {
      children[i].Layout();
    }
  }
  
  public virtual void Render()
  {
    for (int i = 0; i < children.Count; i++)
    {
      children[i].Render();
    }
  }
}
