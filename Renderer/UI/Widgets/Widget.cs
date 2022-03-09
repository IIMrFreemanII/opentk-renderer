using OpenTK.Mathematics;

namespace open_tk_renderer.Renderer.UI.Widgets;

public class Widget
{
    protected bool mounted = false;
    public Widget? parent;
    public List<Widget> children = new ();
    public Vector2 position = new(0, 0);
    public Vector2 size = new(0, 0);

    public event Action<Widget>? Rebuild;

    protected void OnRebuild(Widget widget)
    {
        Rebuild?.Invoke(widget);
    }

    public virtual Widget Build()
    {
        return this;
    }

    public virtual void Mount()
    {
        mounted = true;
    }

    public virtual void UnMount()
    {
        mounted = false;
    }

    public virtual void CalcLayout(Vector2 parentSize)
    {
        // if (parent != null)
        // {
        //     if (position == Vector2i.Zero)
        //     {
        //         position = parent.position;
        //     }
        // }
    }

    public virtual void Render()
    {
    }
}