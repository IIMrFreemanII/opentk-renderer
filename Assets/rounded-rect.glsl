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
// v_textcoord is x = 0, y = 0 at top left corner
in vec2 v_textcoord;

out vec4 FragColor;

uniform vec4 u_color;
uniform vec4 u_border_radius;
uniform vec2 u_size;

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
  
float normalize(float value, float min, float max) {
    return (value - min) / (max - min);
}

vec4 normalize(vec4 value, float min, float max) {
    return (value - min) / (max - min);
}

float lerp(float norm, float min, float max) {
    return (max - min) * norm + min;
}

float roundedQuad(vec2 uv, vec2 ratio, vec2 size, vec4 cornerRadii) {
    size = 1 - size;
    
    vec2 side = step(uv, vec2(0.0));
    // select the radius according to the quadrant the point is in
    float radius = 0;
    // top-left
    if (side == vec2(1, 1)) radius = cornerRadii.x;
    // top-right
    if (side == vec2(0, 1)) radius = cornerRadii.y;
    // bottom-right
    if (side == vec2(0, 0)) radius = cornerRadii.z;
    // bottom-left
    if (side == vec2(1, 0)) radius = cornerRadii.w;
    
    vec2 sizeFactor = vec2(1);
    sizeFactor *= ratio;
    float sizeFactorValue = (ratio.x > 1 ? sizeFactor.y - size.y : sizeFactor.x - size.x);
    float radiusFactor = clamp(lerp(radius, 0, sizeFactorValue), 0, sizeFactorValue);
    
    return length(max(abs(uv) - sizeFactor + vec2(radiusFactor + size), 0)) - radiusFactor;
}

const vec2 minusOneToOne = vec2(-1, 1);
const vec2 zeroToOne = vec2(0, 1);

void main()
{
    // vec2 fragCoord = gl_FragCoord.xy;
    // aspect ratio x/y 
    float aspect = u_size.x / u_size.y;
    // aspect ratio (x/y,1)
    vec2 ratio = vec2(aspect, 1.0);
    // 0.0 .. 1.0
    vec2 uv = v_textcoord;               
    // -1.0 .. 1.0
    uv = (2.0 * uv - 1.0) * ratio;
    
    // 0.0 .. 1.0
    float time = remap(sin(u_time), minusOneToOne, zeroToOne);
    
    // corner radii, starting top left clockwise, (lt, rt, rb, lb)
    vec4 cornerRadii = normalize(u_border_radius, 0, 50);
    float smoothness = 0.003;
    vec2 quadSize = vec2(1);
    
    float distance = roundedQuad(uv, ratio, quadSize, cornerRadii);
    vec4 color = vec4(step(distance, 0));
    color *= u_color;
    
    FragColor = color;
}