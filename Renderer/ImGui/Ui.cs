using open_tk_renderer.Renderer.UI.Widgets.Utils;
using OpenTK.Mathematics;

namespace open_tk_renderer.Renderer.ImGui;

public static partial class Ui
{
  public static bool initialized = false;

  public static Node? root;
  public static Stack<List<Node>> currentChildren = new();

  public static void Init() { }

  public static void Page(Vector2 size)
  {
    root = new SizedBox(size.X, size.Y);
    currentChildren.Push(root.children);
  }

  public static void PageEnd()
  {
    currentChildren.Pop();
    
    root.Layout();
    root.CalcSize(BoxConstraints.Tight(Window.WindowSize));
    root.CalcPosition();
    root.Render();

    if (!initialized)
    {
      initialized = true;
    }
  }
}
