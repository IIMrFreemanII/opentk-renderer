using open_tk_renderer.Renderer.UI.Widgets.Utils;
using OpenTK.Mathematics;

namespace open_tk_renderer.Renderer.UI.Widgets.Painting;

public class DecoratedBox : Widget
{
  public Color4 color;
  public Color4 borderColor = Colors.Blue;
  public float borderSize = 10;
  public Vector4 borderRadius = new(20);

  private static Material? _roundedRect;
  private static Material? _roundedRectFrame;
  private static Mesh? _mesh;
  
  private Matrix4 model = Matrix4.Identity;

  public DecoratedBox(
    Color4? color = null,
    Widget? child = null
  )
  {
    _roundedRect ??= new Material(ShadersController.Get("rounded-rect.glsl"));
    _roundedRectFrame ??= new Material(ShadersController.Get("rounded-rect-frame.glsl"));
    _mesh ??= Window.QuadMesh;

    this.color = color ?? Colors.DefaultBgColor;

    if (child != null) children.Add(child);
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

    _roundedRect.SetVector("u_color", (Vector4)color);
    _roundedRect.SetVector("u_border_radius", borderRadius);
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

    _roundedRectFrame.SetVector("u_color", (Vector4)borderColor);
    _roundedRectFrame.SetVector("u_border_radius", borderRadius);
    _roundedRectFrame.SetVector("u_size", size);
    _roundedRectFrame.SetFloat("u_border_size", borderSize);
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
    size = constraints.Biggest;

    foreach (var child in children) child.CalcSize(constraints);
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
