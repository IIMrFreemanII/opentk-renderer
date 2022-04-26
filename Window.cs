using open_tk_renderer.Components;
using open_tk_renderer.Renderer;
using open_tk_renderer.Renderer.ImGui;
using open_tk_renderer.Renderer.Primitives;
using open_tk_renderer.Renderer.Text;
using open_tk_renderer.Renderer.UI;
using open_tk_renderer.Renderer.UI.Widgets;
using open_tk_renderer.Renderer.UI.Widgets.Layout;
using open_tk_renderer.Renderer.UI.Widgets.Utils;
using open_tk_renderer.Utils;
using open_tk_renderer.Utils.Coroutines;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using Timeout = open_tk_renderer.Utils.Timeout;

namespace open_tk_renderer;

public class Window : GameWindow
{
  public static Matrix4 Projection = Matrix4.Identity;
  public static Matrix4 View = Matrix4.Identity;
  public static Vector2 Resolution = new(value: 0);
  public static Vector2 WindowSize = new(value: 0);
  public static double Time = 0;

  public static Mesh QuadMesh;

  public Widget root;
  private bool _shouldRenderUi = false;

  public Window(
    GameWindowSettings gameWindowSettings,
    NativeWindowSettings nativeWindowSettings
  ) : base(
    gameWindowSettings,
    nativeWindowSettings
  )
  {
    VSync = VSyncMode.On;

    QuadMesh = new Mesh(Quad.vertexAttribs);

    FontsController.Init();
    TextureController.Init();
    ShadersController.Init();
    MaterialsController.Init();
    Ui.Init();

    Coroutine.Start(ShadersController.HandleRecompile(delay: 500));
  }

  protected override void OnLoad()
  {
    base.OnLoad();
    InitWatcher();

    // root = new SizedBox();
    // root.Append(new App());
  }

  private Throttle _throttle = new();
  private FileSystemWatcher _watcher;
  private void InitWatcher()
  {
    var path = PathUtils.FromLocal("");
    _watcher = new FileSystemWatcher(path);
    _watcher.NotifyFilter = NotifyFilters.LastWrite;
    _watcher.IncludeSubdirectories = true;
    _watcher.EnableRaisingEvents = true;
    _watcher.Changed += (obj, e) =>
    {
      if (e.ChangeType != WatcherChangeTypes.Changed) return;

      _throttle.Call(
        () =>
        {
          Timeout.Set(
            () =>
            {
              // root = new SizedBox();
              // root.Append(new App());
              Console.WriteLine("Hot reload!");
            },
            ms: 100
          );
        },
        ms: 500
      );
    };
  }

  private void LayoutWidget(Widget widget)
  {
    widget.Layout();
  }

  private void SizeAndPositionWidget(Widget widget)
  {
    widget.CalcSize(BoxConstraints.Tight(Size));
    widget.CalcPosition();
  }

  private void RenderWidget(Widget widget)
  {
    widget.Render();
  }

  private void HandleHierarchyRender(Widget widget)
  {
    if (widget.dirty)
    {
      // do re-render stuff
      LayoutWidget(widget);
      SizeAndPositionWidget(widget);
      RenderWidget(widget);
    }
    else
    {
      foreach (var child in widget.children) HandleHierarchyRender(child);
    }
  }

  protected override void OnRenderFrame(FrameEventArgs args)
  {
    base.OnRenderFrame(args);

    Render();
    // if (_shouldRenderUi)
    // {
    //   Render();
    //   // Render();
    //   _shouldRenderUi = false;
    // }

    // SwapBuffers();
  }

  private void Render()
  {
    GL.ClearColor(Colors.DefaultBgColor);
    GL.Enable(EnableCap.Blend);
    GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
    GL.Clear(ClearBufferMask.ColorBufferBit);

    // root = new SizedBox();
    // var app = new App(root, new App.Props());
    // HandleHierarchyRender(root);
    // LayoutWidget(root);
    // SizeAndPositionWidget(root);
    // RenderWidget(root);

    Ui.Page(Size);
    {
      Ui.DecoratedBox(new BoxDecoration(Colors.Red));
    }
    Ui.PageEnd();

    // Graphics.DrawText(
    //   "FiraCode-Regular",
    //   "(C) LearnOpenGL.com",
    //   new Vector2(0, 0),
    //   16,
    //   Color4.White
    // );
    // Graphics.DrawText(
    //   "FiraCode-Regular",
    //   "Привет, как дела?",
    //   new Vector2(0, 16),
    //   16,
    //   Color4.White
    // );

    SwapBuffers();
  }

  protected override void OnUpdateFrame(FrameEventArgs args)
  {
    base.OnUpdateFrame(args);
    Time += args.Time;

    EventLoop.HandleTasks();
    Coroutine.Handle();

    if (!IsFocused) // Check to see if the window is focused
      return;

    if (IsKeyPressed(Keys.Escape)) Close();
  }

  protected override void OnResize(ResizeEventArgs e)
  {
    base.OnResize(e);
    Resolution = ClientSize;
    WindowSize = Size;

    // Projection = Matrix4.CreateOrthographic(e.Width, e.Height, -1, 1);
    // Projection = Matrix4.CreateOrthographicOffCenter(0, ClientSize.X, ClientSize.Y, 0, -1, 1);
    Projection = Matrix4.CreateOrthographicOffCenter(
      left: 0,
      Size.X,
      Size.Y,
      top: 0,
      depthNear: -1,
      depthFar: 1
    );

    // on MacOs retina display has more pixels that is why use ClientSize to match pixel size
    GL.Viewport(
      x: 0,
      y: 0,
      ClientSize.X,
      ClientSize.Y
    );
    // GL.Viewport(0, 0, Size.X, Size.X);

    Render();
  }
}
