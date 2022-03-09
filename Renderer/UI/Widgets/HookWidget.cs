namespace open_tk_renderer.Renderer.UI.Widgets;

public class UseStateArgs
{
    public object value;
    public readonly Action<object> setter;

    public UseStateArgs(object value, Action<object> setter)
    {
        this.value = value;

        this.setter = (arg) =>
        {
            Task.Delay(0)
                .ContinueWith(
                    task =>
                    {
                        SetValue(arg);
                        setter(arg);
                    }
                );
        };
    }

    private void SetValue(object value)
    {
        this.value = value;
    }
}

public class HookWidget : Widget
{
    private readonly List<UseStateArgs> _useStateHooks = new();
    private int _counter;

    protected (T value, Action<T> setter) UseState<T>(T initialValue)
    {
        if (mounted)
        {
            var hookArgs = _useStateHooks[_counter++ % _useStateHooks.Count];
            var value = (T)hookArgs.value;
            Action<T> setter = (arg) => hookArgs.setter(arg);
            return (value, setter);
        }

        var result = new UseStateArgs(
            initialValue,
            (value) =>
            {
                var child = Build();
                OnRebuild(child);
            }
        );
        _useStateHooks.Add(result);

        var value1 = (T)result.value;
        Action<T> setter1 = (arg) => result.setter(arg);

        return (value1, setter1);
    }
}