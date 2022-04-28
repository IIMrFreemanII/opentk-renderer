using open_tk_renderer.Renderer.UI.Widgets.Utils;
using open_tk_renderer.Utils;
using OpenTK.Mathematics;

namespace open_tk_renderer.Renderer.UI.ImGui.Layout;

public class FlexPool : Pool<Flex> { }

public class Flex : Node
{
  public MainAxisAlignment mainAxisAlignment;
  public CrossAxisAlignment crossAxisAlignment;
  public Axis direction;
  public TextDirection textDirection;
  public VerticalDirection verticalDirection;

  public static Flex Create(
    Axis direction = Axis.Horizontal,
    MainAxisAlignment mainAxisAlignment = MainAxisAlignment.Start,
    CrossAxisAlignment crossAxisAlignment = CrossAxisAlignment.Start,
    TextDirection textDirection = TextDirection.Ltr,
    VerticalDirection verticalDirection = VerticalDirection.Down
  )
  {
    var obj = FlexPool.Create();
    obj.mainAxisAlignment = mainAxisAlignment;
    obj.crossAxisAlignment = crossAxisAlignment;
    obj.direction = direction;
    obj.textDirection = textDirection;
    obj.verticalDirection = verticalDirection;

    return obj;
  }
  
  public override void Delete()
  {
    base.Delete();
    FlexPool.Delete(this);
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

    base.Layout();
  }

  public override void CalcSize(BoxConstraints constraints)
  {
    this.size = constraints.Biggest;

    var horizontal = new Vector2(x: 1, y: 0);
    var vertical = new Vector2(x: 0, y: 1);
    var axisMultiplier = direction == Axis.Horizontal
      ? horizontal
      : vertical;

    var size = this.size;
    var ownConstraint = BoxConstraints.Loose(constraints.Biggest);
    if (crossAxisAlignment == CrossAxisAlignment.Stretch)
    {
      // for cross axis swap x and y of axisMultiplier
      axisMultiplier = axisMultiplier.Yx;
      size = this.size * axisMultiplier;
      ownConstraint = constraints with { minWidth = size.X, minHeight = size.Y };
    }

    var totalFlexFactor = 0;
    for (int i = 0; i < children.Count; i++)
    {
      var child = children[i];
      if (child is Expanded expanded)
        totalFlexFactor += expanded.flex;
    }

    if (crossAxisAlignment == CrossAxisAlignment.Stretch)
    {
      axisMultiplier = axisMultiplier.Yx;
    }
    var expandSize = this.size * axisMultiplier;
    for (int i = 0; i < children.Count; i++)
    {
      var child = children[i];
      if (child is not Expanded)
      {
        child.CalcSize(ownConstraint);
        expandSize -= child.size * axisMultiplier;
      }
    }

    expandSize = Vector2.Clamp(
      expandSize,
      Vector2.Zero,
      Vector2.PositiveInfinity
    );
    
    for (int i = 0; i < children.Count; i++)
    {
      var child = children[i];
      if (child is Expanded expanded)
      {
        var elemSize = expandSize * expanded.flex / totalFlexFactor;
        var minWidth = elemSize.X;
        var maxWidth = elemSize.X == 0 ? size.X : elemSize.X;
        var minHeight = elemSize.Y;
        var maxHeight = elemSize.Y == 0 ? size.Y : elemSize.Y;
        child.CalcSize(new BoxConstraints(minWidth, maxWidth, minHeight, maxHeight));
      }
    }
  }

  public override void CalcPosition()
  {
    var horizontal = new Vector2(x: 1, y: 0);
    var vertical = new Vector2(x: 0, y: 1);
    var axisMultiplier = direction == Axis.Horizontal
      ? horizontal
      : vertical;

    var totalChildrenSize = Vector2.Zero;
    for (int i = 0; i < children.Count; i++)
    {
      var child = children[i];
      totalChildrenSize += child.size;
    }

    totalChildrenSize *= axisMultiplier;

    var size = this.size * axisMultiplier;
    var position = this.position * axisMultiplier;

    switch (mainAxisAlignment)
    {
      case MainAxisAlignment.Start:
      {
        var nextPosition = position;
        for (int i = 0; i < children.Count; i++)
        {
          var child = children[i];
          child.position += nextPosition;
          nextPosition += child.size * axisMultiplier;
        }

        break;
      }
      case MainAxisAlignment.Center:
      {
        var startPosition = position + size / 2 - totalChildrenSize / 2;

        for (int i = 0; i < children.Count; i++)
        {
          var child = children[i];
          child.position = startPosition;
          startPosition += child.size * axisMultiplier;
        }

        break;
      }
      case MainAxisAlignment.End:
      {
        var startPosition = size - totalChildrenSize;
        for (int i = 0; i < children.Count; i++)
        {
          var child = children[i];
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
        for (int i = 0; i < children.Count; i++)
        {
          var child = children[i];
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
        for (int i = 0; i < children.Count; i++)
        {
          var child = children[i];
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
        for (int i = 0; i < children.Count; i++)
        {
          var child = children[i];
          child.position += nextPosition;
        }

        break;
      }
      case CrossAxisAlignment.Center:
      {
        var nextPosition = position;
        for (int i = 0; i < children.Count; i++)
        {
          var child = children[i];
          var halfOfParentSize = size / 2;
          var halfOfChildSize = child.size * axisMultiplier / 2;
          var center = halfOfParentSize - halfOfChildSize;
          child.position += nextPosition + center;
        }

        break;
      }
      case CrossAxisAlignment.End:
      {
        for (int i = 0; i < children.Count; i++)
        {
          var child = children[i];
          child.position += position + size - child.size * axisMultiplier;
        }

        break;
      }
      case CrossAxisAlignment.Stretch:
      {
        for (int i = 0; i < children.Count; i++)
        {
          var child = children[i];
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

    for (int i = 0; i < children.Count; i++)
    {
      var child = children[i];
      child.CalcPosition();
    }
  }
}
