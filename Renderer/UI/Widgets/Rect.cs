using OpenTK.Mathematics;

namespace open_tk_renderer.Renderer.UI.Widgets;

public class Rect : Widget
{
    protected Color4 color = Color4.WhiteSmoke;
    public int rotation = 0;
    private Matrix4 model = Matrix4.Identity;

    private readonly Material _material;
    private readonly Mesh _mesh;

    public Rect(Color4? color = null, Vector2? size = null)
    {
        _material = Window.DefaultMaterial;
        _mesh = Window.QuadMesh;

        if (size != null)
        {
            this.size = (Vector2)size;
        }
        if (color != null)
        {
            this.color = (Color4)color;
        }
    }

    // public override void CalcLayout(Window window)
    // {
    //     base.CalcLayout(window);
    //     
    //     // foreach (Widget child in children)
    //     // {
    //     //     child.CalcLayout(window);
    //     // }
    //     
    //     // // calc own size
    //     // Vector2i size = Vector2i.Zero;
    //     // foreach (var child in children)
    //     // {
    //     //     // full size
    //     //     size += child.size;
    //     // }
    //     //
    //     // this.size = size;
    // }

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