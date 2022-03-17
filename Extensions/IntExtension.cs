namespace open_tk_renderer.Extensions;

public static class IntExtension
{
  public static float Normalize(
    this int value,
    int min,
    int max
  )
  {
    return (float)(value - min) / (max - min);
  }
}
