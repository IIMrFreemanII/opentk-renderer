using open_tk_renderer.Renderer.UI.Widgets.Utils;
using OpenTK.Mathematics;

namespace open_tk_renderer.Renderer.UI.Widgets.Layout;

public class Flex : Widget
{
  public MainAxisAlignment mainAxisAlignment;
  public CrossAxisAlignment crossAxisAlignment;
  public Axis direction;
  public TextDirection textDirection;
  public VerticalDirection verticalDirection;
  private List<Widget> _children = new();

  public Flex(
    Axis direction = Axis.Horizontal,
    MainAxisAlignment mainAxisAlignment = MainAxisAlignment.Start,
    CrossAxisAlignment crossAxisAlignment = CrossAxisAlignment.Start,
    TextDirection textDirection = TextDirection.Ltr,
    VerticalDirection verticalDirection = VerticalDirection.Down,
    List<Widget>? children = null,
    Ref<Flex>? @ref = null
  )
  {
    if (@ref is { }) @ref.value = this;
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
    _children = new List<Widget>(children);

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
            _children.Reverse();
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
            _children.Reverse();
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
    foreach (var child in _children)
      if (child is Expanded expanded)
        totalFlexFactor += expanded.flex;


    axisMultiplier = axisMultiplier.Yx;
    var expandSize = this.size * axisMultiplier;
    foreach (var child in _children)
      if (child is not Expanded)
      {
        child.CalcSize(ownConstraint);
        expandSize -= child.size * axisMultiplier;
      }

    expandSize = Vector2.Clamp(
      expandSize,
      Vector2.Zero,
      Vector2.PositiveInfinity
    );

    foreach (var child in _children)
      if (child is Expanded expanded)
      {
        var elemSize = expandSize * expanded.flex / totalFlexFactor + size;
        child.CalcSize(BoxConstraints.Tight(elemSize));
      }
  }

  public override void CalcPosition()
  {
    // reset child positions
    foreach (var child in _children) child.position = Vector2.Zero;

    var horizontal = new Vector2(x: 1, y: 0);
    var vertical = new Vector2(x: 0, y: 1);
    var axisMultiplier = direction == Axis.Horizontal
      ? horizontal
      : vertical;

    var totalChildrenSize = Vector2.Zero;
    foreach (var child in _children) totalChildrenSize += child.size;
    totalChildrenSize *= axisMultiplier;

    var size = this.size * axisMultiplier;
    var position = this.position * axisMultiplier;

    switch (mainAxisAlignment)
    {
      case MainAxisAlignment.Start:
      {
        var nextPosition = position;
        foreach (var child in _children)
        {
          child.position += nextPosition;
          nextPosition += child.size * axisMultiplier;
        }

        break;
      }
      case MainAxisAlignment.Center:
      {
        var startPosition = position + size / 2 - totalChildrenSize / 2;
        foreach (var child in _children)
        {
          child.position = startPosition;
          startPosition += child.size * axisMultiplier;
        }

        break;
      }
      case MainAxisAlignment.End:
      {
        var startPosition = size - totalChildrenSize;
        foreach (var child in _children)
        {
          child.position += position + startPosition;
          startPosition += child.size * axisMultiplier;
        }

        break;
      }
      case MainAxisAlignment.SpaceBetween:
      {
        var freeSpace = size - totalChildrenSize;
        var spaceBetween = freeSpace / (_children.Count - 1);

        var nextPosition = position;
        foreach (var child in _children)
        {
          child.position += nextPosition;
          nextPosition += child.size * axisMultiplier + spaceBetween;
        }

        break;
      }
      case MainAxisAlignment.SpaceEvenly:
      {
        var freeSpace = size - totalChildrenSize;
        var spaceBetween = freeSpace / (_children.Count + 1);

        var nextPosition = position;
        foreach (var child in _children)
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
        var spaceBetween = freeSpace / _children.Count;

        var nextPosition = position;
        for (var i = 0; i < _children.Count; i++)
        {
          var child = _children[i];
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
        foreach (var child in _children)
          child.position += nextPosition;

        break;
      }
      case CrossAxisAlignment.Center:
      {
        var nextPosition = position;
        foreach (var child in _children)
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
        foreach (var child in _children)
          child.position += position + size - child.size * axisMultiplier;
        break;
      }
      case CrossAxisAlignment.Stretch:
      {
        foreach (var child in _children) child.position += position;

        break;
      }
      case CrossAxisAlignment.Baseline:
      {
        throw new NotImplementedException();
        break;
      }
    }

    foreach (var child in _children)
      child.CalcPosition();
  }
}
