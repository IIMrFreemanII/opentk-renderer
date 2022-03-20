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

void main()
{
    FragColor = vec4(1, 0, 1, 1);
}