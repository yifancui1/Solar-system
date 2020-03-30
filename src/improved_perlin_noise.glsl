// Given a 3d position as a seed, compute an even smoother procedural noise
// value. "Improving Noise" [Perlin 2002].
//
// Inputs:
//   st  3D seed
// Values between  -½ and ½ ?
//
// expects: random_direction, improved_smooth_step
float improved_perlin_noise( vec3 st) 
{
  /////////////////////////////////////////////////////////////////////////////
  // Replace with your code 
  vec3 i = floor(st);
  vec3 f = fract(st);

  vec3 u = improved_smooth_step(f);
  vec3 g[12];
  g[0] = normalize(vec3(1.0,1.0,0.0));
  g[1] = normalize(vec3(-1.0,1.0,0.0));
  g[2] = normalize(vec3(1.0,-1.0,0.0));
  g[3] = normalize(vec3(-1.0,-1.0,0.0));
  g[4] = normalize(vec3(1.0,0.0,1.0));
  g[5] = normalize(vec3(-1.0,0.0,1.0));
  g[6] = normalize(vec3(1.0,0.0,-1.0));
  g[7] = normalize(vec3(-1.0,0.0,-1.0));
  g[8] = normalize(vec3(0.0,1.0,1.0));
  g[9] = normalize(vec3(0.0,-1.0,1.0));
  g[10] = normalize(vec3(0.0,1.0,-1.0));
  g[11] = normalize(vec3(0.0,-1.0,-1.0));


  return mix(
         mix( mix( dot( g[int(((random_direction(i + vec3(0.0,0.0,0.0)))*100).x)%12], f - vec3(0.0,0.0,0.0) ), dot( g[int(((random_direction(i + vec3(1.0,0.0,0.0)))*100).x)%12], f - vec3(1.0,0.0,0.0) ), u.x),
              mix( dot( g[int(((random_direction(i + vec3(0.0,1.0,0.0)))*100).x)%12], f - vec3(0.0,1.0,0.0) ), dot( g[int(((random_direction(i + vec3(1.0,1.0,0.0)))*100).x)%12], f - vec3(1.0,1.0,0.0) ), u.x), u.y),
         mix( mix( dot( g[int(((random_direction(i + vec3(0.0,0.0,1.0)))*100).x)%12], f - vec3(0.0,0.0,1.0) ), dot( g[int(((random_direction(i + vec3(1.0,0.0,1.0)))*100).x)%12], f - vec3(1.0,0.0,1.0) ), u.x),
              mix( dot( g[int(((random_direction(i + vec3(0.0,1.0,1.0)))*100).x)%12], f - vec3(0.0,1.0,1.0) ), dot( g[int(((random_direction(i + vec3(1.0,1.0,1.0)))*100).x)%12], f - vec3(1.0,1.0,1.0) ), u.x), u.y),u.z);
  /////////////////////////////////////////////////////////////////////////////
}

