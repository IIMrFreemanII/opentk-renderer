using System;
using System.Collections.Generic;

namespace open_tk_renderer.Utils;

public static class EventLoop
{
  private static Queue<Action> _macroTasks = new();

  public static void HandleTasks()
  {
    while (_macroTasks.Count > 0) _macroTasks.Dequeue()();
  }

  public static void AddTask(Action callback)
  {
    _macroTasks.Enqueue(callback);
  }
}
