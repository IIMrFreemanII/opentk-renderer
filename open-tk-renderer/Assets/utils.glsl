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

float roundedQuad(vec2 uv, vec2 ratio, float size, vec4 cornerRadii) {
    size = 1 - size;
    // select quadrant the point is in
    // top-left = vec2(1, 0)
    // top-right = vec2(0, 0)
    // bottom-right = vec2(0, 1)
    // bottom-left = vec2(1, 0)
    vec2 side = step(uv, vec2(0.0));
    // select the radius according to the quadrant the point is in
    float radius = mix(
        mix(cornerRadii.z, cornerRadii.y, side.y),
        mix(cornerRadii.w, cornerRadii.x, side.y),
        side.x
    );
    
    vec2 sizeFactor = vec2(1);
    sizeFactor *= ratio;
    float sizeFactorValue = (ratio.x > 1 ? sizeFactor.y : sizeFactor.x) - size;
    float radiusFactor = clamp(lerp(radius, 0, sizeFactorValue), 0, sizeFactorValue);
    
    float distance = length(max(abs(uv) - sizeFactor + vec2(radiusFactor + size), 0)) - radiusFactor;
    return distance;
}