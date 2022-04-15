namespace open_tk_renderer.Renderer.UI.Views;

public class View
{
    public Style Style { get; set; } = new();
    public List<View> children = new();
    public View? parent;

    public void Append(View view)
    {
        children.Add(view);
    }
    
    public void Remove()
    {
        if (parent is { })
        {
            parent.children.Remove(this);
        }
    }
    
    public void Clear()
    {
        children.Clear();
    }
}
