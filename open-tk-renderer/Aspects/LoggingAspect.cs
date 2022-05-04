using PostSharp.Aspects;
using PostSharp.Serialization;

namespace open_tk_renderer.Aspects;

[PSerializable]
public class LoggingAspect : OnMethodBoundaryAspect
{
  public override void OnEntry(MethodExecutionArgs args)
  {
    Console.WriteLine("Method {0}({1}) started.", args.Method.Name, string.Join( ", ", args.Arguments ) );
  }
}
