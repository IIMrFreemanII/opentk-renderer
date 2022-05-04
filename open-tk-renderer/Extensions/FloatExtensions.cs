namespace open_tk_renderer.Extensions;

public static class FloatExtensions
{
  public static float Normalize(
    this float value,
    float min,
    float max
  )
  {
    return (value - min) / (max - min);
  }
}
