using System.Diagnostics;

namespace open_tk_renderer.Utils;

public class Throttle
{
  private Stopwatch _stopwatch;

  public Throttle()
  {
    _stopwatch = new Stopwatch();
    _stopwatch.Start();
  }
  
  public void Call(Action action, float ms)
  {
    if (_stopwatch.ElapsedMilliseconds > ms)
    {
      action();
      _stopwatch.Restart();
    }
  }
}
