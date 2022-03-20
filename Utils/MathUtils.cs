using OpenTK.Mathematics;

namespace open_tk_renderer.Utils;

public class MathUtils
{
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
