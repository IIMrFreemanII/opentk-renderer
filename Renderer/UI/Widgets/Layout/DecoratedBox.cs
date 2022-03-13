using OpenTK.Mathematics;

namespace open_tk_renderer.Renderer.UI.Widgets.Layout;

public class DecoratedBox : Widget
{
  public Color4 color;

  private readonly Material _material;
  private readonly Mesh _mesh;
  private Matrix4 model = Matrix4.Identity;

  public DecoratedBox(
    Color4? color = null,
    Widget? child = null
  )
  {
    _material = Window.DefaultMaterial;
    _mesh = Window.QuadMesh;

    this.color = color ?? Colors.DefaultBgColor;

    if (child != null)
    {
      children.Add(child);
    }
  }

  public override void Render()
  {
    UpdateModel();
    _material.uniforms["u_model"].value = model;
    _material.uniforms["u_view"].value = Window.View;
    _material.uniforms["u_projection"].value = Window.Projection;
    _material.uniforms["u_color"].value = (Vector4)color;
    Graphics.DrawMesh(_mesh, _material);
  }

  private void UpdateModel()
  {
    model =
      Matrix4.CreateScale(size.X, size.Y, 1) *
      Matrix4.CreateTranslation(position.X, position.Y, 0);
  }

  public override void CalcSize(Vector2 parentSize)
  {
    size = parentSize;
    
    foreach (var child in children)
    {
      child.CalcSize(size);
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