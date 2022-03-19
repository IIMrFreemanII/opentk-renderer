#vertex
#version 330 core
layout (location = 0) in vec3 a_position;

uniform mat4 u_projection;
uniform mat4 u_model;
uniform mat4 u_view;

void main()
{
    vec4 position = u_projection * u_view * u_model * vec4(a_position, 1);
    gl_Position = position;
}

#fragment
#version 330 core
out vec4 FragColor;

// vec2 uv = vec2(0);  // centered pixel position -1 .. 1

uniform vec4 u_color;
uniform vec2 u_resolution;

void main()
{
    vec2 fragCoord = gl_FragCoord.xy;
    float aspect = u_resolution.x / u_resolution.y;   // aspect ratio x/y
    vec2 ratio = vec2(aspect, 1.0);                   // aspect ratio (x/y,1)

    vec2 uv = fragCoord / u_resolution;               // 0.0 .. 1.0
    uv = (2.0 * uv - 1.0) * ratio;                    // -1.0 .. 1.0
    

    vec3 color = vec3(1, 1, 1);
    color.rg = uv;
    color.b = 0.0;
    FragColor = vec4(color, 1);
}