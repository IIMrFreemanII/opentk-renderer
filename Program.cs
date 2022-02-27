// See https://aka.ms/new-console-template for more information

using open_tk_renderer;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

using Window window = new Window(GameWindowSettings.Default, new NativeWindowSettings
{
    Size = new Vector2i(800, 600),
    Title = "LearnOpenTK - Creating a Window",
    // This is needed to run on macos
    Flags = ContextFlags.ForwardCompatible,
});
window.Run();