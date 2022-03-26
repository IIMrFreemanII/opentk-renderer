using System.Collections;

namespace open_tk_renderer.Utils;

public class Coroutine : IEnumerator
{
  private static List<Coroutine> _coroutines = new();
  private static List<int> _coroutinesToRemove = new();

  private int _index;
  private IEnumerator _enumerator;

  private Coroutine(int index, IEnumerator enumerator)
  {
    _index = index;
    _enumerator = enumerator;
  }

  public bool MoveNext()
  {
    return _enumerator.MoveNext();
  }

  public void Reset()
  {
    _enumerator.Reset();
  }

  public object? Current => _enumerator.Current;

  public static Coroutine Start(IEnumerator coroutine)
  {
    var temp = new Coroutine(_coroutines.Count, coroutine);
    _coroutines.Add(temp);
    return temp;
  }

  public static void Stop(Coroutine coroutine)
  {
    _coroutines.RemoveAt(coroutine._index);
  }

  public static void Handle()
  {
    foreach (var coroutine in _coroutines)
    {
      if (coroutine.Current is IEnumerator current && current.MoveNext())
        continue;

      if (coroutine.Current is Task { IsCompleted: false })
        continue;

      if (!coroutine.MoveNext()) _coroutinesToRemove.Add(coroutine._index);
    }

    foreach (var coroutineIndex in _coroutinesToRemove) _coroutines.RemoveAt(coroutineIndex);

    _coroutinesToRemove.Clear();
  }
}
