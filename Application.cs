using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace open_tk_renderer;

public class Application
{
    private Window _window;

    public Application()
    {
        _window = new Window(GameWindowSettings.Default, new NativeWindowSettings
        {
            Size = new Vector2i(800, 600),
            Title = "LearnOpenTK - Creating a Window",
            // This is needed to run on macos
            Flags = ContextFlags.ForwardCompatible,
        });
    }

    ~Application()
    {
        _window.Dispose();
    }

    public void Run()
    {
        _window.Run();
    }
}