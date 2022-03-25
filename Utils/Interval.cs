namespace open_tk_renderer.Utils;

public static class Interval
{
  public static void Set(Action callback, int ms)
  {
    void InnerFunc()
    {
      Task.Delay(ms)
          .ContinueWith(
            (task) =>
            {
              callback();
              InnerFunc();
            }
          );
    }

    InnerFunc();
  }
}
