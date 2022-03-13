using OpenTK.Mathematics;

namespace open_tk_renderer.Renderer.UI.Widgets.Layout;

public class Flex : Widget
{
  public const Axis Direction = Axis.Horizontal;

  public MainAxisAlignment mainAxisAlignment;
  public CrossAxisAlignment crossAxisAlignment;
  public TextDirection textDirection;
  public VerticalDirection verticalDirection;

  public Flex(
    List<Widget> children,
    MainAxisAlignment mainAxisAlignment = MainAxisAlignment.Start,
    CrossAxisAlignment crossAxisAlignment = CrossAxisAlignment.Start,
    TextDirection textDirection = TextDirection.Ltr,
    VerticalDirection verticalDirection = VerticalDirection.Down
  )
  {
    this.mainAxisAlignment = mainAxisAlignment;
    this.crossAxisAlignment = crossAxisAlignment;
    this.textDirection = textDirection;
    this.verticalDirection = verticalDirection;

    foreach (var child in children) child.parent = this;

    this.children = children;
  }

  public override void Layout()
  {
    if (textDirection == TextDirection.Rtl)
    {
      children.Reverse();
    }
  }

  public override void CalcSize(Vector2 parentSize)
  {
    size = parentSize;

    if (crossAxisAlignment == CrossAxisAlignment.Stretch)
    {
      foreach (var child in children)
      {
        Vector2 stretchedSize = child.size with { Y = size.Y };
        child.size = stretchedSize;
        child.CalcSize(size);
      }
    }
    else
    {
      foreach (var child in children)
        child.CalcSize(size);
    }
  }

  public override void CalcPosition()
  {
    float totalChildrenWidth = 0;
    foreach (var child in children) totalChildrenWidth += child.size.X;
    
    switch (mainAxisAlignment)
    {
      case MainAxisAlignment.Start:
      {
        var nextPosition = position;
        foreach (var child in children)
        {
          child.position = nextPosition;
          nextPosition += new Vector2(child.size.X, 0);
        }

        break;
      }
      case MainAxisAlignment.Center:
      {
        var startPosition = size.X / 2 - totalChildrenWidth / 2;
        foreach (var child in children)
        {
          child.position = position with { X = startPosition };
          startPosition += child.size.X;
        }

        break;
      }
      case MainAxisAlignment.End:
      {
        float startX = size.X - totalChildrenWidth;
        foreach (var child in children)
        {
          child.position = position + new Vector2(startX, 0);
          startX += child.size.X;
        }

        break;
      }
      case MainAxisAlignment.SpaceBetween:
      {
        var freeSpace = size.X - totalChildrenWidth;
        var spaceBetween = freeSpace / (children.Count - 1);

        var nextPosition = position;
        foreach (var child in children)
        {
          child.position = nextPosition;
          nextPosition += new Vector2(child.size.X + spaceBetween, 0);
        }

        break;
      }
      case MainAxisAlignment.SpaceEvenly:
      {
        var freeSpace = size.X - totalChildrenWidth;
        var spaceBetween = freeSpace / (children.Count + 1);

        var nextPosition = position;
        foreach (var child in children)
        {
          nextPosition += new Vector2(spaceBetween, 0);
          child.position = nextPosition;
          nextPosition += new Vector2(child.size.X, 0);
        }

        break;
      }
      case MainAxisAlignment.SpaceAround:
      {
        var freeSpace = size.X - totalChildrenWidth;
        var spaceBetween = freeSpace / children.Count;

        var nextPosition = position;
        for (var i = 0; i < children.Count; i++)
        {
          var child = children[i];
          if (i == 0)
          {
            nextPosition += new Vector2(spaceBetween / 2, 0);
            child.position = nextPosition;
            nextPosition += new Vector2(child.size.X, 0);
          }
          else
          {
            nextPosition += new Vector2(spaceBetween, 0);
            child.position = nextPosition;
            nextPosition += new Vector2(child.size.X, 0);
          }
        }

        break;
      }
    }

    switch (crossAxisAlignment)
    {
      case CrossAxisAlignment.Start:
      {
        foreach (var child in children)
          child.position = new Vector2(child.position.X, position.Y);

        break;
      }
      case CrossAxisAlignment.Center:
      {
        var nextPosition = position;
        foreach (var child in children)
        {
          var halfOfParentHeight = size.Y / 2;
          var halfOfChildHeight = child.size.Y / 2;
          var centerOfHeight = halfOfParentHeight - halfOfChildHeight;
          child.position += nextPosition + new Vector2(0, centerOfHeight);
        }

        break;
      }
      case CrossAxisAlignment.End:
      {
        foreach (var child in children)
          child.position = new Vector2(child.position.X, position.Y + size.Y - child.size.Y);
        break;
      }
      case CrossAxisAlignment.Baseline:
      {
        throw new NotImplementedException();
        break;
      }
    }

    foreach (var child in children)
      child.CalcPosition();
  }
}