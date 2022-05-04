using System.Diagnostics;

namespace open_tk_renderer.Utils;


public class Debounce
{
  private Stopwatch _stopwatch;
  
  public Debounce()
  {
    _stopwatch = new Stopwatch();
    _stopwatch.Start();
  }
  
  // todo: implement debounce
  // public void Call(Action action, float ms)
  // {
  //   if (_stopwatch.ElapsedMilliseconds > ms)
  //   {
  //     action();
  //     _stopwatch.Restart();
  //   }
  // }
}
