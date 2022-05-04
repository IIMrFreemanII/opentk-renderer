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

  private static void S_Node(Node node)
  {
    nodes.Add(node);
    currentChildren.Peek().Add(node);
    currentChildren.Push(node.children);
  }

  private static void E_Node()
  {
    currentChildren.Pop();
  }

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
    S_Node(DecoratedBox.Create(decoration));
  }

  public static void E_DecoratedBox()
  {
    E_Node();
  }

  public static void S_SizedBox(float width = 0, float height = 0)
  {
    S_Node(SizedBox.Create(width, height));
  }

  public static void E_SizedBox()
  {
    E_Node();
  }

  public static void S_Padding(EdgeInsets? insets = null)
  {
    S_Node(Padding.Create(insets));
  }

  public static void E_Padding()
  {
    E_Node();
  }

  public static void S_Flex(
    Axis direction = Axis.Horizontal,
    MainAxisAlignment mainAxisAlignment = MainAxisAlignment.Start,
    CrossAxisAlignment crossAxisAlignment = CrossAxisAlignment.Start,
    TextDirection textDirection = TextDirection.Ltr,
    VerticalDirection verticalDirection = VerticalDirection.Down
  )
  {
    S_Node(
      Flex.Create(
        direction,
        mainAxisAlignment,
        crossAxisAlignment,
        textDirection,
        verticalDirection
      )
    );
  }

  public static void E_Flex()
  {
    E_Node();
  }

  public static void S_Expanded(int flex = 0)
  {
    S_Node(Expanded.Create(flex));
  }

  public static void E_Expanded()
  {
    E_Node();
  }

  public static void S_Align(Alignment? alignment = null, Vector2? sizeFactor = null)
  {
    S_Node(Align.Create(alignment, sizeFactor));
  }

  public static void E_Align()
  {
    E_Node();
  }

  public static void S_Center(Vector2? sizeFactor = null)
  {
    S_Node(Center.Create(sizeFactor));
  }

  public static void E_Center()
  {
    E_Node();
  }
}
