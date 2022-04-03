using System.Collections.Generic;
using open_tk_renderer.Renderer;
using open_tk_renderer.Renderer.Primitives;
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
using Image = open_tk_renderer.Renderer.UI.Widgets.Painting.Image;

namespace open_tk_renderer;

public class Window : GameWindow
{
  public static Matrix4 Projection = Matrix4.Identity;
  public static Matrix4 View = Matrix4.Identity;
  public static Vector2 Resolution = new(0);
  public static Vector2 WindowSize = new(0);
  public static double Time = 0;

  public static Mesh QuadMesh;
  private HookWidget hookWidget;

  private Widget root;

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
  }

  protected override void OnLoad()
  {
    base.OnLoad();

    // Library library = new Library();
    // var absPath = PathUtils.FromLocal("Assets/fonts/Fira_Code/static/FiraCode-Regular.ttf");
    // Face face = new Face(library, absPath);

    QuadMesh = new Mesh(Quad.vertexAttribs);

    TextureController.FromFile("Assets/images/wall.jpeg");
    TextureController.FromFile("Assets/images/awesomeface.png");

    ShadersController.ErrorShader = Shader.FromFile("Assets/error.glsl");
    var roundedRectShader = ShadersController.FromFile("Assets/rounded-rect.glsl");
    var roundedRectFrameShader = ShadersController.FromFile("Assets/rounded-rect-frame.glsl");
    var textureShader = ShadersController.FromFile("Assets/texture.glsl");

    MaterialsController.Create("roundedRect", roundedRectShader);
    MaterialsController.Create("roundedRectFrame", roundedRectFrameShader);
    MaterialsController.Create("texture", textureShader);

    Coroutine.Start(ShadersController.HandleRecompile(500));
  }

  private void RunUi(Widget widget)
  {
    // this.hookWidget = hookWidget;
    var result = BuildWidget(widget);
    // var result = widget;
    LayoutWidget(result);
    SizeAndPositionWidget(result);
    MountWidget(result);

    root = result;
  }

  private Widget BuildWidget(Widget widget)
  {
    var child = widget.Build();
    for (var i = 0; i < child.children.Count; i++)
      child.children[i] = BuildWidget(child.children[i]);

    return child;
  }

  private void MountWidget(Widget widget)
  {
    widget.Mount();
    foreach (var widgetChild in widget.children) MountWidget(widgetChild);
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

    root = new Container(
      margin: EdgeInsets.All(10),
      padding: EdgeInsets.All(10),
      decoration: new BoxDecoration(
        Colors.Red,
        Border.All(Color4.Black, 10)
        // BorderRadius.All(10)
      ),
      child: new Row(
        MainAxisAlignment.Start,
        CrossAxisAlignment.Stretch,
        TextDirection.Ltr,
        new List<Widget>
        {
          new Expanded(
            child: new Image("wall.jpeg")
          ),
          new Expanded(
            child: new Image("awesomeface.png")
          ),
          new Container(
            // margin: EdgeInsets.All(10),
            new BoxDecoration(Colors.Green),
            new Vector2(100),
            new Container(size: new Vector2(30))
          ),
          new Container(
            // margin: EdgeInsets.All(10),
            new BoxDecoration(Colors.Blue),
            new Vector2(100),
            new Container(size: new Vector2(30))
          )
        }
      )
    );
    LayoutWidget(root);
    SizeAndPositionWidget(root);
    RenderWidget(root);

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
