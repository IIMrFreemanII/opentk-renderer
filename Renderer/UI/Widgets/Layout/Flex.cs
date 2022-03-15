using OpenTK.Mathematics;

namespace open_tk_renderer.Renderer.UI.Widgets.Layout;

public class Flex : Widget
{
  public MainAxisAlignment mainAxisAlignment;
  public CrossAxisAlignment crossAxisAlignment;
  public Axis direction;
  public TextDirection textDirection;
  public VerticalDirection verticalDirection;

  public Flex(
    Axis direction = Axis.Horizontal,
    MainAxisAlignment mainAxisAlignment = MainAxisAlignment.Start,
    CrossAxisAlignment crossAxisAlignment = CrossAxisAlignment.Start,
    TextDirection textDirection = TextDirection.Ltr,
    VerticalDirection verticalDirection = VerticalDirection.Down,
    List<Widget>? children = null
  )
  {
    this.direction = direction;
    this.mainAxisAlignment = mainAxisAlignment;
    this.crossAxisAlignment = crossAxisAlignment;
    this.textDirection = textDirection;
    this.verticalDirection = verticalDirection;

    this.children = children ?? new List<Widget>();
    foreach (var child in this.children) child.parent = this;
  }

  public override void Layout()
  {
    switch (direction)
    {
      case Axis.Horizontal:
      {
        switch (textDirection)
        {
          case TextDirection.Ltr:
          {
            break;
          }
          case TextDirection.Rtl:
          {
            children.Reverse();
            break;
          }
        }
        break;
      }
      case Axis.Vertical:
      {
        switch (verticalDirection)
        {
          case VerticalDirection.Down:
          {
            break;
          }
          case VerticalDirection.Up:
          {
            children.Reverse();
            break;
          }
        }
        break;
      }
    }
  }

  public override void CalcSize(Vector2 parentSize)
  {
    this.size = parentSize;

    if (crossAxisAlignment == CrossAxisAlignment.Stretch)
    {
      var horizontal = new Vector2(1, 0);
      var vertical = new Vector2(0, 1);
      var axisMultiplier = direction == Axis.Horizontal
        ? horizontal
        : vertical;
      // for cross axis swap x and y of axisMultiplier
      axisMultiplier = axisMultiplier.Yx;
      var size = this.size * axisMultiplier;

      foreach (var child in children)
      {
        child.size += size;
        child.CalcSize(this.size);
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
    var horizontal = new Vector2(1, 0);
    var vertical = new Vector2(0, 1);
    var axisMultiplier = direction == Axis.Horizontal
      ? horizontal
      : vertical;

    var totalChildrenSize = Vector2.Zero;
    foreach (var child in children) totalChildrenSize += child.size;
    totalChildrenSize *= axisMultiplier;

    var size = this.size * axisMultiplier;
    var position = this.position * axisMultiplier;

    switch (mainAxisAlignment)
    {
      case MainAxisAlignment.Start:
      {
        var nextPosition = position;
        foreach (var child in children)
        {
          child.position += nextPosition;
          nextPosition += child.size * axisMultiplier;
        }

        break;
      }
      case MainAxisAlignment.Center:
      {
        var startPosition = position + size / 2 - totalChildrenSize / 2;
        foreach (var child in children)
        {
          child.position = startPosition;
          startPosition += child.size * axisMultiplier;
        }

        break;
      }
      case MainAxisAlignment.End:
      {
        var startPosition = size - totalChildrenSize;
        foreach (var child in children)
        {
          child.position += position + startPosition;
          startPosition += child.size * axisMultiplier;
        }

        break;
      }
      case MainAxisAlignment.SpaceBetween:
      {
        var freeSpace = size - totalChildrenSize;
        var spaceBetween = freeSpace / (children.Count - 1);
      
        var nextPosition = position;
        foreach (var child in children)
        {
          child.position += nextPosition;
          nextPosition += child.size * axisMultiplier + spaceBetween;
        }
      
        break;
      }
      case MainAxisAlignment.SpaceEvenly:
      {
        var freeSpace = size - totalChildrenSize;
        var spaceBetween = freeSpace / (children.Count + 1);
      
        var nextPosition = position;
        foreach (var child in children)
        {
          nextPosition += spaceBetween;
          child.position += nextPosition;
          nextPosition += child.size * axisMultiplier;
        }
      
        break;
      }
      case MainAxisAlignment.SpaceAround:
      {
        var freeSpace = size - totalChildrenSize;
        var spaceBetween = freeSpace / children.Count;
      
        var nextPosition = position;
        for (var i = 0; i < children.Count; i++)
        {
          var child = children[i];
          if (i == 0)
          {
            nextPosition += spaceBetween / 2;
            child.position += nextPosition;
            nextPosition += child.size * axisMultiplier;
          }
          else
          {
            nextPosition += spaceBetween;
            child.position += nextPosition;
            nextPosition += child.size * axisMultiplier;
          }
        }
      
        break;
      }
    }

    // for cross axis swap x and y of axisMultiplier
    axisMultiplier = axisMultiplier.Yx;
    size = this.size * axisMultiplier;
    position = this.position * axisMultiplier;
    switch (crossAxisAlignment)
    {
      case CrossAxisAlignment.Start:
      {
        var nextPosition = position;
        foreach (var child in children)
          child.position += nextPosition;

        break;
      }
      case CrossAxisAlignment.Center:
      {
        var nextPosition = position;
        foreach (var child in children)
        {
          var halfOfParentSize = size / 2;
          var halfOfChildSize = child.size * axisMultiplier / 2;
          var center = halfOfParentSize - halfOfChildSize;
          child.position += nextPosition + center;
        }

        break;
      }
      case CrossAxisAlignment.End:
      {
        foreach (var child in children)
          child.position += position + size - child.size * axisMultiplier;
        break;
      }
      case CrossAxisAlignment.Stretch:
      {
        foreach (var child in children)
        {
          child.position += position;
        }

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