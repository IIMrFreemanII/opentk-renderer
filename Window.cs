using System.Drawing;
using open_tk_renderer.Assets;
using open_tk_renderer.Renderer;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace open_tk_renderer;

public class Window : GameWindow
{
    private Shader _shader;
    private Mesh _mesh;

    public Window(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) : base(
        gameWindowSettings, nativeWindowSettings)
    {
        _mesh = new Mesh(QuadMeshData.vertexAttribs);
        _shader = new Shader(DefaultShader.VertexSrc, DefaultShader.FragSrc);
    }

    protected override void OnRenderFrame(FrameEventArgs args)
    {
        base.OnRenderFrame(args);

        GL.Clear(ClearBufferMask.ColorBufferBit);

        Graphics.DrawMesh(_mesh, _shader);

        Context.SwapBuffers();
    }

    protected override void OnUpdateFrame(FrameEventArgs args)
    {
        base.OnUpdateFrame(args);

        if (!IsFocused) // Check to see if the window is focused
        {
            return;
        }

        if (KeyboardState.IsKeyDown(Keys.Escape))
        {
            Close();
        }
    }

    protected override void OnLoad()
    {
        base.OnLoad();

        GL.ClearColor(Color.Gray);
    }

    protected override void OnResize(ResizeEventArgs e)
    {
        GL.Viewport(0, 0, Size.X, Size.Y);
        base.OnResize(e);
    }
}