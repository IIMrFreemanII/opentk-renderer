using System.Diagnostics;
using PostSharp.Aspects;

namespace open_tk_renderer.Aspects;

[Serializable]
public class ProfilerAspect : OnMethodBoundaryAspect
{
  public override void OnEntry(MethodExecutionArgs args)
  {
    args.MethodExecutionTag = Stopwatch.StartNew();
  }
 
  public override void OnExit(MethodExecutionArgs args)
  {
    Stopwatch sw = (Stopwatch)args.MethodExecutionTag;
    sw.Stop();
 
    string output = string.Format("{0} Executed in {1} milliseconds",
                                  args.Method.Name, sw.ElapsedMilliseconds);
 
    Console.WriteLine(output);
  }
}
