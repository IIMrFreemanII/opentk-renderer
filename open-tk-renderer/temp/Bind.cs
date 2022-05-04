namespace open_tk_renderer.temp;

public class Bind<T> where T : struct
{
  private T _value;
  public T Value
  {
    get => _value;
    set
    {
      _value = value;
      ValueChanged?.Invoke(value);
    }
  }
  public event Action<T>? ValueChanged;

  public Bind(T value = default)
  {
    _value = value;
  }
}
