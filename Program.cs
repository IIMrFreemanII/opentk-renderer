// See https://aka.ms/new-console-template for more information

using BenchmarkDotNet.Running;
using open_tk_renderer;
using open_tk_renderer.benchmarks;


bool benchmark = false;

if (benchmark)
{
  // BenchmarkRunner.Run<BindBenchmark>();
  // BenchmarkRunner.Run<ClassAllocBenchmark>();
}
else
{
  Application application = new Application();
  application.Run();
}
