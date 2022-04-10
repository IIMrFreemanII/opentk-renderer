using open_tk_renderer.Components;
using open_tk_renderer.Renderer;
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

namespace open_tk_renderer;

public class Window : GameWindow
{
  public static Matrix4 Projection = Matrix4.Identity;
  public static Matrix4 View = Matrix4.Identity;
  public static Vector2 Resolution = new(0);
  public static Vector2 WindowSize = new(0);
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

    FontsController.Init();
    TextureController.Init();
    ShadersController.Init();
    MaterialsController.Init();

    Coroutine.Start(ShadersController.HandleRecompile(500));

    QuadMesh = new Mesh(Quad.vertexAttribs);
    root = new Container();
  }

  protected override void OnLoad()
  {
    base.OnLoad();

    // var app = new App(root, new App.Props());
  }

  private Widget BuildWidget(Widget widget)
  {
    var child = widget.Build();
    for (var i = 0; i < child.children.Count; i++)
      child.children[i] = BuildWidget(child.children[i]);

    return child;
  }

  private void LayoutWidget(Widget widget)
  {
    widget.Layout();
    foreach (var widgetChild in widget.children) LayoutWidget(widgetChild);
  }

  private void SizeAndPositionWidget(Widget widget)
  {
    // widget.size = Size;
    widget.CalcSize(BoxConstraints.Loose(Size));
    widget.CalcPosition();
  }

  private void RenderWidget(Widget widget)
  {
    widget.Render();
    foreach (var widgetChild in widget.children) RenderWidget(widgetChild);
  }

  protected override void OnRenderFrame(FrameEventArgs args)
  {
    base.OnRenderFrame(args);

    Render();
    // if (_shouldRenderUi)
    // {
    //   Render();
    //   _shouldRenderUi = false;
    // }
  }

  private void Render()
  {
    GL.ClearColor(Colors.DefaultBgColor);
    GL.Enable(EnableCap.Blend);
    GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
    GL.Clear(ClearBufferMask.ColorBufferBit);

    root = new Container();
    var app = new App(root, new App.Props());
    LayoutWidget(root);
    SizeAndPositionWidget(root);
    RenderWidget(root);

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
      0,
      Size.X,
      Size.Y,
      0,
      -1,
      1
    );

    // on MacOs retina display has more pixels that is why use ClientSize to match pixel size
    GL.Viewport(
      0,
      0,
      ClientSize.X,
      ClientSize.Y
    );
    // GL.Viewport(0, 0, Size.X, Size.X);

    Render();
  }
}
