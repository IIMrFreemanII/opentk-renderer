using open_tk_renderer.Renderer.UI.ImGui.Layout;
using open_tk_renderer.Renderer.UI.ImGui.Painting;
using open_tk_renderer.Renderer.UI.Widgets.Utils;
using OpenTK.Mathematics;

namespace open_tk_renderer.Renderer.UI.ImGui;

public static class Ui
{
  public static Node? root;
  public static List<Node> nodes = new();
  public static Stack<List<Node>> currentChildren = new();

  public static void Page(Vector2 size)
  {
    root = SizedBox.Create(size.X, size.Y);
    nodes.Add(root);
    currentChildren.Push(root.children);
  }

  public static void PageEnd(bool render = true)
  {
    currentChildren.Pop();
    
    root.Layout();
    root.CalcSize(BoxConstraints.Tight(Window.WindowSize));
    root.CalcPosition();
    if (render) root.Render();
    
    for (int i = 0; i < nodes.Count; i++)
    {
      var node = nodes[i];
      node.children.Clear();
      node.Delete();
    }
    
    nodes.Clear();
  }
  
  public static void S_DecoratedBox(
    BoxDecoration? decoration = null
  )
  {
    var elem = DecoratedBox.Create(decoration);
    nodes.Add(elem);
    currentChildren.Peek().Add(elem);
    currentChildren.Push(elem.children);
  }

  public static void E_DecoratedBox()
  {
    currentChildren.Pop();
  }
  
  public static void S_SizedBox(float width = 0, float height = 0)
  {
    var elem = SizedBox.Create(width, height);
    nodes.Add(elem);
    currentChildren.Peek().Add(elem);
    currentChildren.Push(elem.children);
  }

  public static void E_SizedBox()
  {
    currentChildren.Pop();
  }
}
