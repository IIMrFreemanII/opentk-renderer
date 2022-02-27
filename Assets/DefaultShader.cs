namespace open_tk_renderer.Assets;

public static class DefaultShader
{
    public static string VertexSrc = @"#version 330 core
layout (location = 0) in vec3 aPosition;

void main()
{
    gl_Position = vec4(aPosition, 1.0);
}";

    public static string FragSrc = @"#version 330 core
out vec4 FragColor;

uniform vec4 u_color;

void main()
{
    vec4 color = u_color;

    FragColor = vec4(1.0f, 0.5f, 0.2f, 1.0f);
}";
}