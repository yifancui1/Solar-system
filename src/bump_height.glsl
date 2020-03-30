// Create a bumpy surface by using procedural noise to generate a height (
// displacement in normal direction).
//
// Inputs:
//   is_moon  whether we're looking at the moon or centre planet
//   s  3D position of seed for noise generation
// Returns elevation adjust along normal (values between -0.1 and 0.1 are
//   reasonable.
float bump_height( int planet_id, vec3 s)
{
  /////////////////////////////////////////////////////////////////////////////
  // Replace with your code 
  float coeff = 0.0;
  vec3 seed = s * 2;
  if(planet_id == 0){
    seed *= 5;
  }
  
  coeff += 1.0000 * improved_perlin_noise(seed); 
  seed = 2.0 * seed;
  coeff += 0.5000 * improved_perlin_noise(seed); 
  seed = 2.0 * seed;
  coeff += 0.2500 * improved_perlin_noise(seed); 
  seed = 2.0 * seed;
  coeff += 0.1250 * improved_perlin_noise(seed); 
  seed = 2.0 * seed;
  coeff += 0.0625 * improved_perlin_noise(seed);

  
  
  return coeff/20.0 ;
  /////////////////////////////////////////////////////////////////////////////
}
