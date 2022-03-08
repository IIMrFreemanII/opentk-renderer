using OpenTK.Mathematics;

namespace open_tk_renderer.Renderer.UI;

public class Container : Widget
{
  protected Color4 color = Color4.WhiteSmoke;
  public int rotation = 0;
  public EdgeInsets padding;
  public EdgeInsets margin;
  private Matrix4 model = Matrix4.Identity;

  private readonly Material _material;
  private readonly Mesh _mesh;

  public Container(
    Color4? color = null,
    Vector2i? size = null,
    Widget? child = null,
    EdgeInsets? padding = null,
    EdgeInsets? margin = null
  )
  {
    _material = Window.DefaultMaterial;
    _mesh = Window.QuadMesh;

    if (size != null)
    {
      this.size = (Vector2i)size;
    }

    if (color != null)
    {
      this.color = (Color4)color;
    }

    if (padding != null)
    {
      this.padding = (EdgeInsets)padding;
    }
    
    if (margin != null)
    {
      this.margin = (EdgeInsets)margin;
    }

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
      Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(rotation)) *
      Matrix4.CreateTranslation(position.X, position.Y, 0);
  }

  public override void CalcLayout(Vector2i parentSize)
  {
    size = size == Vector2i.Zero ? parentSize : size;
    // size = size == Vector2i.Zero ? parentSize : Vector2i.Clamp(size, Vector2i.Zero, parentSize);

    position += margin.size;
    if (children.Count > 0)
    {
      Widget child = children[0];
      child.position = position + padding.size;
      child.CalcLayout(size - padding.size * 2);

      size = Vector2i.Clamp(child.size + padding.size * 2, Vector2i.Zero, size);
    }
  }
}