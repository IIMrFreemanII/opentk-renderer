using System.Collections;

namespace open_tk_renderer.Utils.Coroutines;

public class WaitForTask : IEnumerator
{
  private readonly Task _task;

  public WaitForTask(Task task)
  {
    _task = task;
  }

  public bool MoveNext()
  {
    return !_task.IsCompleted;
  }

  public void Reset()
  {
    throw new NotImplementedException();
  }

  public object? Current => null;
}
