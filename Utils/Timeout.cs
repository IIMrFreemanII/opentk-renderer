namespace open_tk_renderer.Utils;

public static class Timeout
{
  public static void Set(Action callback, int ms)
  {
    Task.Delay(ms).ContinueWith((task) => EventLoop.AddTask(callback));
  }
}
