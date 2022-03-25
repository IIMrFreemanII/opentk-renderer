using Timeout = open_tk_renderer.Utils.Timeout;

namespace open_tk_renderer.Renderer.UI.Widgets;

public class UseStateArgs<T>
{
  public T value;
  public readonly Action<T> setter;

  public UseStateArgs(T value, Action<T> setter)
  {
    this.value = value;

    this.setter = (arg) =>
    {
      Timeout.Set(
        () =>
        {
          Console.WriteLine(arg);
          SetValue(arg);
          setter(arg);
        },
        0
      );
    };
  }

  private void SetValue(T value)
  {
    this.value = value;
  }
}

public class HookWidget : Widget
{
  private readonly List<object> _useStateHooks = new();
  private int _counter;

  protected (T value, Action<T> setter) UseState<T>(T initialValue)
  {
    if (mounted)
    {
      UseStateArgs<T> hookArgs =
        _useStateHooks[_counter++ % _useStateHooks.Count] as UseStateArgs<T>;
      return (hookArgs.value, hookArgs.setter);
    }

    var result = new UseStateArgs<T>(
      initialValue,
      (value) =>
      {
        var child = Build();
        OnRebuild(child);
      }
    );
    _useStateHooks.Add(result);

    return (result.value, result.setter);
  }
}
