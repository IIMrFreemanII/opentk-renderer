using open_tk_renderer.Assets;
using open_tk_renderer.Renderer;
using open_tk_renderer.Renderer.UI;
using open_tk_renderer.Renderer.UI.Widgets;
using open_tk_renderer.Renderer.UI.Widgets.Layout;
using open_tk_renderer.Renderer.UI.Widgets.Layout.Utils;
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

  public static Material DefaultMaterial;
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

    QuadMesh = new Mesh(QuadMeshData.vertexAttribs);
    DefaultMaterial =
      new Material(new Shader("default", DefaultShader.VertexSrc, DefaultShader.FragSrc));

    GL.ClearColor(Colors.DefaultBgColor);

    // ui
    // RunUi(new App());
    // RunUi(
    //   new Container(
    //     margin: new EdgeInsets(10),
    //     child: new Flex(
    //       mainAxisAlignment: MainAxisAlignment.Center,
    //       crossAxisAlignment: CrossAxisAlignment.Center,
    //       textDirection: TextDirection.Rtl,
    //       children: new List<Widget>
    //       {
    //         new Container(
    //           margin: new EdgeInsets(10),
    //           color: Colors.Red,
    //           size: new Vector2(100),
    //           child: new Container(Color4.Aqua, new Vector2(30))
    //         ),
    //         new Container(
    //           margin: new EdgeInsets(10),
    //           color: Colors.Green,
    //           size: new Vector2(100),
    //           child: new Container(Color4.Aqua, new Vector2(30))
    //         ),
    //         new Container(
    //           margin: new EdgeInsets(10),
    //           color: Colors.Blue,
    //           size: new Vector2(100),
    //           child: new Container(Color4.Aqua, new Vector2(30))
    //         )
    //       }
    //     )
    //   )
    // );
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
    widget.size = Size;
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
    GL.Clear(ClearBufferMask.ColorBufferBit);

    root = new Container(
      margin: new EdgeInsets(10),
      color: Color4.Black,
      child: new Row(
        MainAxisAlignment.Start,
        CrossAxisAlignment.Stretch,
        TextDirection.Ltr,
        new List<Widget>
        {
          new Expanded(
            child: new Container(
              margin: new EdgeInsets(10),
              color: Colors.Red,
              size: new Vector2(100),
              child: new Container(Color4.Aqua, new Vector2(30))
            )
          ),
          new Container(
            margin: new EdgeInsets(10),
            color: Colors.Green,
            size: new Vector2(100),
            child: new Container(Color4.Aqua, new Vector2(30))
          ),
          new Container(
            margin: new EdgeInsets(10),
            color: Colors.Blue,
            size: new Vector2(100),
            child: new Container(Color4.Aqua, new Vector2(30))
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

    if (!IsFocused) // Check to see if the window is focused
      return;

    if (IsKeyDown(Keys.Escape)) Close();
  }

  protected override void OnResize(ResizeEventArgs e)
  {
    base.OnResize(e);

    // Projection = Matrix4.CreateOrthographic(e.Width, e.Height, -1, 1);
    // Projection = Matrix4.CreateOrthographicOffCenter(0, ClientSize.X, ClientSize.Y, 0, -1, 1);
    Projection = Matrix4.CreateOrthographicOffCenter(0, Size.X, Size.Y, 0, -1, 1);

    // on MacOs retina display has more pixels that is why use ClientSize to match pixel size
    GL.Viewport(0, 0, ClientSize.X, ClientSize.Y);
    // GL.Viewport(0, 0, Size.X, Size.X);

    Render();
  }
}
