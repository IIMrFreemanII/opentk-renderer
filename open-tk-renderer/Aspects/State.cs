using System.Reflection;
using open_tk_renderer.Components;
using open_tk_renderer.Renderer.UI.Widgets;
using PostSharp.Aspects;
using PostSharp.Serialization;

namespace open_tk_renderer.Aspects;

[PSerializable]
public class State : LocationInterceptionAspect
{
  public string widgetName;
  public string propName;

  public State(string widgetName, string propName)
  {
    this.widgetName = widgetName;
    this.propName = propName;
  }

  private object? _widget;
  private PropertyInfo? _prop;
  
  public override void OnSetValue(LocationInterceptionArgs args)
  {
    base.OnSetValue(args);

    if (args.Instance is Component component)
    {
      if (_widget is { })
      {
        _prop?.SetValue(_widget, args.Value);
      }
      else
      {
        _widget = args.Instance?.GetType().GetField(widgetName)?.GetValue(args.Instance);
        _prop = _widget?.GetType().GetProperty(propName);
        _prop?.SetValue(_widget, args.Value);
      }

      Console.WriteLine($"set {args.Value}");
    }
  }
}
