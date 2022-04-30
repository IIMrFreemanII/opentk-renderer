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

  public static void S_Page(Vector2 size)
  {
    root = SizedBox.Create(size.X, size.Y);
    nodes.Add(root);
    currentChildren.Push(root.children);
  }

  public static void E_Page(bool render = true)
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

  public static void S_Padding(EdgeInsets? insets = null)
  {
    var elem = Padding.Create(insets);
    nodes.Add(elem);
    currentChildren.Peek().Add(elem);
    currentChildren.Push(elem.children);
  }

  public static void E_Padding()
  {
    currentChildren.Pop();
  }

  public static void S_Flex(
    Axis direction = Axis.Horizontal,
    MainAxisAlignment mainAxisAlignment = MainAxisAlignment.Start,
    CrossAxisAlignment crossAxisAlignment = CrossAxisAlignment.Start,
    TextDirection textDirection = TextDirection.Ltr,
    VerticalDirection verticalDirection = VerticalDirection.Down
  )
  {
    var elem = Flex.Create(
      direction,
      mainAxisAlignment,
      crossAxisAlignment,
      textDirection,
      verticalDirection
    );
    nodes.Add(elem);
    currentChildren.Peek().Add(elem);
    currentChildren.Push(elem.children);
  }

  public static void E_Flex()
  {
    currentChildren.Pop();
  }

  public static void S_Expanded(int flex = 0)
  {
    var elem = Expanded.Create(flex);
    nodes.Add(elem);
    currentChildren.Peek().Add(elem);
    currentChildren.Push(elem.children);
  }

  public static void E_Expanded()
  {
    currentChildren.Pop();
  }

  public static void S_Align(Alignment? alignment = null, Vector2? sizeFactor = null)
  {
    var elem = Align.Create(alignment, sizeFactor);
    nodes.Add(elem);
    currentChildren.Peek().Add(elem);
    currentChildren.Push(elem.children);
  }

  public static void E_Align()
  {
    currentChildren.Pop();
  }
  
  public static void S_Center(Vector2? sizeFactor = null)
  {
    var elem = Center.Create(sizeFactor);
    nodes.Add(elem);
    currentChildren.Peek().Add(elem);
    currentChildren.Push(elem.children);
  }

  public static void E_Center()
  {
    currentChildren.Pop();
  }
}
