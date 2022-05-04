#vertex
#version 330 core
layout (location = 0) in vec3 a_position;
layout (location = 1) in vec2 a_texcoord;

uniform mat4 u_projection;
uniform mat4 u_model;
uniform mat4 u_view;

out vec2 v_texcoord;

void main()
{
    vec4 position = u_projection * u_view * u_model * vec4(a_position, 1);
    v_texcoord = a_texcoord;
    gl_Position = position;
}

#fragment
#version 330 core

// v_texcoord is x = 0, y = 0 at top left corner
in vec2 v_texcoord;

out vec4 FragColor;

uniform sampler2D u_texture0;
uniform vec4 u_color;

void main()
{
    vec4 textColor = vec4(1, 1, 1, texture(u_texture0, v_texcoord).r);
    FragColor = u_color * textColor;
}