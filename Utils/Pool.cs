namespace open_tk_renderer.Utils;

public abstract class Pool<T> where T : new()
{
  private static Queue<T> _queue = new();
  
  public static void Prefill(int count)
  {
    _queue = new(count);
    
    for (int i = 0; i < count; i++)
    {
      _queue.Enqueue(new T());
    }
  }
  public static T Create()
  {
    if (_queue.Count > 0)
    {
      return _queue.Dequeue();
    }
    
    return new T();
  }
  public static void Delete(T obj)
  {
    _queue.Enqueue(obj);
  }
}
