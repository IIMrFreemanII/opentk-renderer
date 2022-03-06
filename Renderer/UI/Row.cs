using OpenTK.Mathematics;

namespace open_tk_renderer.Renderer.UI;

public class Row : Widget
{
    public Row(List<Widget> children)
    {
        foreach (var child in children)
        {
            child.parent = this;
        }
        this.children = children;
    }
    public override void CalcLayout(Window window)
    {
        base.CalcLayout(window);
        
        int nextX = position.X;
        foreach (Widget child in children)
        {
            int childX = GetChildX(child);
            child.position = position with { X = nextX };
            nextX += childX;
            child.CalcLayout(window);
        }
        
        // calc own size
        Vector2i size = Vector2i.Zero;
        foreach (var child in children)
        {
            // full width
            size += new Vector2i(child.size.X, 0);

            // max height
            if (child.size.Y > size.Y)
            {
                size += new Vector2i(0, child.size.Y);
            }
        }

        this.size = size;
    }

    private int GetChildX(Widget widget)
    {
        if (widget.size.X == 0)
        {
            if (widget is Column)
            {
                int maxX = 0;
            
                foreach (var widgetChild in widget.children)
                {
                    int temp = GetChildX(widgetChild);
                    if (temp > maxX)
                    {
                        maxX = temp;
                    }
                }
            
                return maxX;
            }

            int fullX = 0;
            
            foreach (var widgetChild in widget.children)
            {
                fullX += GetChildX(widgetChild);
            }
            
            return fullX;
        }
        return widget.size.X;
    }
}