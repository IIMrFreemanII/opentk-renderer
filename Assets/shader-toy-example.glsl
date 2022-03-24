// by Ruslan Shestopalyuk

// Signed distance function for a rectangle with rounded corners.
// Rectangle is located at (0,0)
//  pos - point position
//  ext - rectangle extents, (halfWidth, halfHeight)
//  cr - corner radii, starting top left clockwise, (lt, rt, rb, lb)
float sdRoundRect(vec2 pos, vec2 ext, vec4 cr) {
  // select the radius according to the quadrant the point is in
  vec2 s = step(pos, vec2(0.0));
  float r = mix(
    mix(cr.y, cr.z, s.y),  
  	mix(cr.x, cr.w, s.y),
    s.x);
  return length(max(abs(pos) + vec2(r) - ext, 0.0)) - r;
}

void mainImage(out vec4 color, in vec2 uv) {
  float aspect = iResolution.x / iResolution.y;
  vec2 ratio = vec2(aspect, 1);
  uv = uv / iResolution.xy;
  vec2 pos = (2.0 * uv - 1.0) * ratio;
	
  vec2 boxPosition = vec2(0);
  vec2 boxExt = vec2(1, 1);
  vec4 cornerRadii = vec4(0.5);
    
  float dist = sdRoundRect(pos - boxPosition, boxExt, cornerRadii);  
    
  color = vec4(1.0)*step(dist, 0.0);
}