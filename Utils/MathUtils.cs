using OpenTK.Mathematics;

namespace open_tk_renderer.Utils;

public class MathUtils
{
  /// <summary>
  /// Returns the linear blend of x and y by a
  /// if a = 0 returns x
  /// if a = 1 returns y
  /// if a = 0.5 returns half x + half y
  /// </summary>
  /// <param name="x"></param>
  /// <param name="y"></param>
  /// <param name="a"></param>
  /// <returns></returns>
  public float Mix(
    float x,
    float y,
    float a
  )
  {
    return x * (1 - a) + y * a;
  }

  public float Step(float edge, float x)
  {
    return x < edge
      ? 0
      : 1;
  }

  public static float Remap(
    float value,
    Vector2 inMinMax,
    Vector2 outMinMax
  )
  {
    return outMinMax.X +
           (value - inMinMax.X) *
           (outMinMax.Y - outMinMax.X) /
           (inMinMax.Y - inMinMax.X);
  }

  public static double Remap(
    double value,
    Vector2 inMinMax,
    Vector2 outMinMax
  )
  {
    return outMinMax.X +
           (value - inMinMax.X) *
           (outMinMax.Y - outMinMax.X) /
           (inMinMax.Y - inMinMax.X);
  }
}
