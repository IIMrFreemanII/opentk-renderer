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
uniform vec2 u_resolution;
uniform vec2 u_size;
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
    float aspect = u_size.x / u_size.y;
    // aspect ratio (x/y,1)
    vec2 ratio = vec2(aspect, 1.0);
    // 0.0 .. 1.0
    vec2 uv = v_textcoord;               
    // -1.0 .. 1.0
    uv = (2.0 * uv - 1.0) * ratio;
    
    // 0.0 .. 1.0
    float time = remap(sin(u_time), minusOneToOne, zeroToOne);
    
    // select quadrant the point is in
    // top-left = vec2(1, 0)
    // top-right = vec2(0, 0)
    // bottom-right = vec2(0, 1)
    // bottom-left = vec2(1, 0)
    vec2 side = step(uv, vec2(0.0));
    // corner radii, starting top left clockwise, (lt, rt, rb, lb)
    vec4 cornerRadii = vec4(0.2, 0.4, 0.6, 0.8);
    // select the radius according to the quadrant the point is in
    float radius = mix(
        mix(cornerRadii.z, cornerRadii.y, side.y),
        mix(cornerRadii.w, cornerRadii.x, side.y),
        side.x
    );
    // to match aspect ratio
    radius *= (ratio.x > 1 ? ratio.y : ratio.x);
    
    vec4 bgColor = vec4(0, 0, 0, 1);
    vec4 borderColor = vec4(1, 1, 1, 1);
    vec2 size = vec2(1) * ratio;
    float borderSize = 0.1;
    float smoothness = 0.001;
    // to match aspect ratio
    borderSize *= (ratio.x > 1 ? ratio.y : ratio.x);
    float distance = length(max(abs(uv) - size + vec2(radius), 0)) - radius;
    vec4 color = vec4(smoothstep(distance, distance + smoothness, 0)) * borderColor;
    float innerDistance = distance + borderSize;
    color *= vec4(1 - smoothstep(innerDistance, innerDistance + smoothness, 0));
    color += vec4(step(distance + borderSize, 0)) * bgColor;
    
    FragColor = color;
}