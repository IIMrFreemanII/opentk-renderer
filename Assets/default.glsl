#vertex
#version 330 core
layout (location = 0) in vec3 a_position;
layout (location = 1) in vec2 a_texcoord;

uniform mat4 u_projection;
uniform mat4 u_model;
uniform mat4 u_view;

out vec2 v_textcoord;

void main()
{
    vec4 position = u_projection * u_view * u_model * vec4(a_position, 1);
    v_textcoord = a_texcoord;
    gl_Position = position;
}

#fragment
#version 330 core
in vec2 v_textcoord;

out vec4 FragColor;

uniform vec4 u_color;
uniform vec2 u_resolution;
uniform float u_time;

float remap(
    float value,
    vec2 inMinMax,
    vec2 outMinMax
  )
  {
    return outMinMax.x +
           (value - inMinMax.x) *
           (outMinMax.y - outMinMax.x) /
           (inMinMax.y - inMinMax.x);
  }

const vec2 minusOneToOne = vec2(-1, 1);
const vec2 zeroToOne = vec2(0, 1);

void main()
{
    // vec2 fragCoord = gl_FragCoord.xy;
    // aspect ratio x/y 
    float aspect = u_resolution.x / u_resolution.y;   
    // aspect ratio (x/y,1)
    vec2 ratio = vec2(aspect, 1.0);
    // 0.0 .. 1.0
    // vec2 uv = fragCoord / u_resolution;               
    vec2 uv = v_textcoord;               
    // -1.0 .. 1.0
    uv = (2.0 * uv - 1.0) * ratio;
    
    float temp = remap(sin(u_time), minusOneToOne, zeroToOne);
                     
    float size = 0;
    float radius = 0 + temp;
    // float thickness = 0.9;
    float distance = length(max(abs(uv), radius) - radius);
    vec3 color = vec3(step(1 - radius + 0.0001, distance));
    // color += 1 - step(1 - thickness, distance);
    
    FragColor = vec4(color, 1);
}