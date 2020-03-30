// Set the pixel color to an interesting procedural color generated by mixing
// and filtering Perlin noise of different frequencies.
//
// Uniforms:
uniform mat4 view;
uniform mat4 proj;
uniform float animation_seconds;
uniform bool is_moon;
// Inputs:
in vec3 sphere_fs_in;
in vec3 normal_fs_in;
in vec4 pos_fs_in; 
in vec4 view_pos_fs_in; 
// Outputs:
out vec3 color;

// expects: blinn_phong, perlin_noise
void main()
{
  /////////////////////////////////////////////////////////////////////////////
  // Replace with your code 
  float theta = (animation_seconds) * M_PI/6;
  mat4 rotate = mat4(
      cos(theta),0 ,sin(theta),0,
      0,1,0,0,
      -sin(theta),0,cos(theta),0,
      0,0,0,1);
  vec3 l = normalize((view * rotate * vec4(100.0,50.0,0.0,1.0)).xyz - view_pos_fs_in.xyz);
  float p = 2000;
  
  vec3 n = normalize(normal_fs_in);
  vec3 v = normalize(-view_pos_fs_in.xyz);
  vec3 ka,kd,ks;
  if(!is_moon){
    ka = vec3(.02, .03, .08);
    kd = vec3(.2, .3, .8);
    ks = vec3(.8, .8, .8);
  }
  else{
    ka = vec3(.01, .01, .01);
    kd = vec3(.3, .3, .3);
    ks = vec3(.8, .8, .8);
  }
  float coeff = 0.0;
  vec3 seed = sphere_fs_in * 5.0;
  coeff += 1.0000 * perlin_noise(seed); 
  seed = 2.0 * seed;
  coeff += 0.5000 * perlin_noise(seed); 
  seed = 2.0 * seed;
  coeff += 0.2500 * perlin_noise(seed); 
  seed = 2.0 * seed;
  coeff += 0.1250 * perlin_noise(seed); 
  seed = 2.0 * seed;
  coeff += 0.0625 * perlin_noise(seed);

  color = (0.15*coeff+0.7)*blinn_phong(ka,kd,ks,p,n,v,l);
  //color = 0.5+0.5*n;
  //color = vec3(0.5,0.5,0)+vec3(0.5,0.5,0)*view_pos_fs_in.xyz;
  color = vec3(clamp(color[0],0,1), clamp(color[1],0,1), clamp(color[2],0,1));
  /////////////////////////////////////////////////////////////////////////////
}