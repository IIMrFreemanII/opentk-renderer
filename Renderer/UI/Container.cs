using OpenTK.Mathematics;

namespace open_tk_renderer.Renderer.UI;

public class Container : Widget
{
    protected Color4 color = Color4.WhiteSmoke;
    public int rotation = 0;
    private Matrix4 model = Matrix4.Identity;

    private readonly Material _material;
    private readonly Mesh _mesh;

    public Container(Color4? color = null, Vector2i? size = null, Widget? child = null)
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
        if (child != null)
        {
            children.Add(child);
            child.parent = this;
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
}