using open_tk_renderer.Renderer.UI.Widgets.Layout;
using open_tk_renderer.Renderer.UI.Widgets.Utils;
using OpenTK.Mathematics;

namespace open_tk_renderer.Renderer.UI.Widgets.Painting;

public class DecoratedBox : Widget
{
  public BoxDecoration boxDecoration;

  private static Material? _roundedRect;
  private static Material? _roundedRectFrame;
  private static Mesh? _mesh;

  private Matrix4 model = Matrix4.Identity;

  public DecoratedBox(
    BoxDecoration? boxDecoration = null,
    Widget? child = null
  )
  {
    _roundedRect ??= MaterialsController.Get("roundedRect");
    _roundedRectFrame ??= MaterialsController.Get("roundedRectFrame");
    _mesh ??= Window.QuadMesh;

    this.boxDecoration = boxDecoration ?? new BoxDecoration();

    if (child != null)
      children.Add(
        new Padding(
          this.boxDecoration.padding,
          child
        )
      );
  }

  public override void Render()
  {
    UpdateModel();
    RenderBack();
    RenderRectFront();
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

  private void RenderRectFront()
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
        1
      ) *
      Matrix4.CreateTranslation(
        position.X,
        position.Y,
        0
      );
  }

  public override void CalcSize(BoxConstraints constraints)
  {
    foreach (var child in children)
    {
      child.CalcSize(constraints);
      size = constraints.Constrain(child.size);
    }

    if (children.Count == 0)
    {
      size = constraints.Biggest;
    }
  }

  public override void CalcPosition()
  {
    foreach (var child in children)
    {
      child.position = position;
      child.CalcPosition();
    }
  }
}
