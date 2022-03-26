using System.Collections;
using System.Diagnostics;

namespace open_tk_renderer.Utils.Coroutines;

public class Wait : IEnumerator
{
  private Stopwatch _stopwatch;
  private float _ms;

  private Wait(float ms)
  {
    _ms = ms;
    _stopwatch = new Stopwatch();
    _stopwatch.Start();
  }

  public static Wait FromMs(float ms)
  {
    return new Wait(ms);
  }

  public bool MoveNext()
  {
    return _ms > _stopwatch.ElapsedMilliseconds;
  }

  public void Reset()
  {
    _stopwatch.Restart();
  }

  public object? Current => null;
}
