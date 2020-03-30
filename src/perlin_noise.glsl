// Given a 3d position as a seed, compute a smooth procedural noise
// value: "Perlin Noise", also known as "Gradient noise".
//
// Inputs:
//   st  3D seed
// Returns a smooth value between (-1,1)
//
// expects: random_direction, smooth_step
float perlin_noise( vec3 st) 
{
  /////////////////////////////////////////////////////////////////////////////
  // Replace with your code 
  vec3 i = floor(st);
  vec3 f = fract(st);

  vec3 u = smooth_step(f);

  return mix(
         mix( mix( dot( random_direction(i + vec3(0.0,0.0,0.0) ), f - vec3(0.0,0.0,0.0) ), dot( random_direction(i + vec3(1.0,0.0,0.0) ), f - vec3(1.0,0.0,0.0) ), u.x),
              mix( dot( random_direction(i + vec3(0.0,1.0,0.0) ), f - vec3(0.0,1.0,0.0) ), dot( random_direction(i + vec3(1.0,1.0,0.0) ), f - vec3(1.0,1.0,0.0) ), u.x), u.y),
         mix( mix( dot( random_direction(i + vec3(0.0,0.0,1.0) ), f - vec3(0.0,0.0,1.0) ), dot( random_direction(i + vec3(1.0,0.0,1.0) ), f - vec3(1.0,0.0,1.0) ), u.x),
              mix( dot( random_direction(i + vec3(0.0,1.0,1.0) ), f - vec3(0.0,1.0,1.0) ), dot( random_direction(i + vec3(1.0,1.0,1.0) ), f - vec3(1.0,1.0,1.0) ), u.x), u.y),u.z);

  /////////////////////////////////////////////////////////////////////////////
}

