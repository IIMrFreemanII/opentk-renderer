using open_tk_renderer.Renderer.UI.Widgets.Utils;
using OpenTK.Mathematics;

namespace open_tk_renderer.Renderer.ImGui;

public class DecoratedBox : Node
{
  public BoxDecoration boxDecoration;
  
  private static Material? _roundedRect;
  private static Material? _roundedRectFrame;
  private static Mesh? _mesh;
  
  private Matrix4 model = Matrix4.Identity;

  public DecoratedBox(BoxDecoration? boxDecoration = null)
  {
    // todo: optimize
    _roundedRect ??= MaterialsController.Get("roundedRect");
    _roundedRectFrame ??= MaterialsController.Get("roundedRectFrame");
    _mesh ??= Window.QuadMesh;
    
    this.boxDecoration = boxDecoration ?? new BoxDecoration();
  }
  
  public override void Render()
  {
    UpdateModel();
    RenderBack();
    RenderFront();
    
    base.Render();
  }
  
  private void RenderBack()
  {
    _roundedRect.SetMatrix("u_model", model);
    _roundedRect.SetMatrix("u_view", Window.View);
    _roundedRect.SetMatrix("u_projection", Window.Projection);

    _roundedRect.SetVector("u_color", (Vector4)boxDecoration.color);
    _roundedRect.SetVector("u_border_radius", boxDecoration.borderRadius.ToVec4());
    _roundedRect.SetVector("u_size", size);
    _roundedRect.SetVector("u_resolution", Window.Resolution);
    _roundedRect.SetFloat("u_time", (float)Window.Time);

    Graphics.DrawMesh(_mesh, _roundedRect);
  }
  
  private void RenderFront()
  {
    _roundedRectFrame.SetMatrix("u_model", model);
    _roundedRectFrame.SetMatrix("u_view", Window.View);
    _roundedRectFrame.SetMatrix("u_projection", Window.Projection);

    _roundedRectFrame.SetVector("u_color", (Vector4)boxDecoration.border.color);
    _roundedRectFrame.SetVector("u_border_radius", boxDecoration.borderRadius.ToVec4());
    _roundedRectFrame.SetVector("u_size", size);
    _roundedRectFrame.SetFloat("u_border_size", boxDecoration.border.bottom);
    _roundedRectFrame.SetVector("u_resolution", Window.Resolution);
    _roundedRectFrame.SetFloat("u_time", (float)Window.Time);

    Graphics.DrawMesh(_mesh, _roundedRectFrame);
  }
  
  private void UpdateModel()
  {
    model =
      Matrix4.CreateScale(
        size.X,
        size.Y,
        z: 1
      ) *
      Matrix4.CreateTranslation(
        position.X,
        position.Y,
        z: 0
      );
  }
  
  public override void CalcSize(BoxConstraints constraints)
  {
    var newConstraints = BoxConstraints.Loose(constraints.Biggest);
    for (int i = 0; i < children.Count; i++)
    {
      var child = children[i];
      child.CalcSize(newConstraints);
      size = newConstraints.Constrain(child.size);
    }
    
    if (children.Count == 0) size = newConstraints.Biggest;
  }
}

public static partial class Ui
{
  public static void DecoratedBox(
    BoxDecoration? decoration = null
  )
  {
    S_DecoratedBox(decoration);
    E_DecoratedBox();
  }

  public static void S_DecoratedBox(
    BoxDecoration? decoration = null
  )
  {
    var elem = new DecoratedBox(decoration);
    currentChildren.Peek().Add(elem);
    currentChildren.Push(elem.children);
  }

  public static void E_DecoratedBox()
  {
    currentChildren.Pop();
  }
}
