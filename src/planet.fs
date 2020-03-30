// Generate a procedural planet and orbiting moon. Use layers of (improved)
// Perlin noise to generate planetary features such as vegetation, gaseous
// clouds, mountains, valleys, ice caps, rivers, oceans. Don't forget about the
// moon. Use `animation_seconds` in your noise input to create (periodic)
// temporal effects.
//
// Uniforms:
uniform mat4 view;
uniform mat4 proj;
uniform float animation_seconds;
uniform int planet_id;
// Inputs:
in vec3 sphere_fs_in;
in vec3 normal_fs_in;
in vec4 pos_fs_in; 
in vec4 view_pos_fs_in; 
// Outputs:
out vec3 color;
// expects: model, blinn_phong, bump_height, bump_position,
// improved_perlin_noise, tangent
bool is_cloud(vec3 sphere_fs_in){
  float theta = (animation_seconds) * M_PI/24;
  mat3 rotate = mat3(
      cos(theta),0 ,-sin(theta),
      0,1,0,
      sin(theta),0,cos(theta));
  //float random_cloud = sin(improved_perlin_noise(rotate*sphere_fs_in)*M_PI*2);
  vec3 seed = rotate * sphere_fs_in * 1.0;
  float random_cloud = 0.0;
  random_cloud += 1.0000 * improved_perlin_noise(seed); 
  seed = 2.0 * seed;
  random_cloud += 0.5000 * improved_perlin_noise(seed); 
  seed = 2.0 * seed;
  random_cloud += 0.2500 * improved_perlin_noise(seed); 
  seed = 2.0 * seed;
  random_cloud += 0.1250 * improved_perlin_noise(seed); 
  seed = 2.0 * seed;
  random_cloud += 0.0625 * improved_perlin_noise(seed);
  if(random_cloud > 0.25) return true;
  return false;
}

bool is_jpt_cloud(vec3 sphere_fs_in){
  float theta = (animation_seconds) * M_PI/24;
  mat3 rotate = mat3(
      cos(theta),0 ,-sin(theta),
      0,1,0,
      sin(theta),0,cos(theta));
  //float random_cloud = sin(improved_perlin_noise(rotate*sphere_fs_in)*M_PI*2);
  vec3 seed = rotate * vec3(0.0, sphere_fs_in[1] ,0.0) * 2.0;
  float random_cloud = improved_perlin_noise(sin(seed*M_PI*2)); 
  random_cloud += 1.0000 * improved_perlin_noise(seed); 
  seed = 2.0 * seed;
  random_cloud += 0.5000 * improved_perlin_noise(seed); 
  seed = 2.0 * seed;
  random_cloud += 0.2500 * improved_perlin_noise(seed); 
  seed = 2.0 * seed;
  random_cloud += 0.1250 * improved_perlin_noise(seed); 
  seed = 2.0 * seed;
  random_cloud += 0.0625 * improved_perlin_noise(seed);
  if(random_cloud >= 0.02) return true;
  return false;
}
bool is_sat_cloud(vec3 sphere_fs_in){
  float theta = (animation_seconds) * M_PI/24;
  mat3 rotate = mat3(
      cos(theta),0 ,-sin(theta),
      0,1,0,
      sin(theta),0,cos(theta));
  //float random_cloud = sin(improved_perlin_noise(rotate*sphere_fs_in)*M_PI*2);
  vec3 seed = rotate * vec3(0.0, sphere_fs_in[1] ,0.0) * 2.0;
  float random_cloud = improved_perlin_noise(sin(seed*M_PI)); 
  random_cloud += 1.0000 * improved_perlin_noise(seed); 
  seed = 2.0 * seed;
  random_cloud += 0.5000 * improved_perlin_noise(seed); 
  seed = 2.0 * seed;
  random_cloud += 0.2500 * improved_perlin_noise(seed); 
  seed = 2.0 * seed;
  random_cloud += 0.1250 * improved_perlin_noise(seed); 
  seed = 2.0 * seed;
  random_cloud += 0.0625 * improved_perlin_noise(seed);
  if(random_cloud >= 0.02) return true;
  return false;
}

void main()
{
  /////////////////////////////////////////////////////////////////////////////
  // Replace with your code 
  
  float coeff = 0.0001;
  float theta = (animation_seconds) * M_PI/6;
  vec3 n;
  vec3 T, B;
  tangent(normalize(sphere_fs_in), T, B);
  
  vec3 bumped_p = bump_position(planet_id , sphere_fs_in);
  vec3 bumped_pt = bump_position(planet_id , sphere_fs_in+coeff*T);
  vec3 bumped_pb =  bump_position(planet_id , sphere_fs_in+coeff*B);

  n = normalize(cross((bumped_pt-bumped_p)/coeff,(bumped_pb-bumped_p)/coeff));
  
  mat4 VM = view * model(planet_id, animation_seconds);
  n = normalize((VM*vec4(n,0.0)).xyz);
  

  mat4 rotate = mat4(
      cos(theta),0 ,sin(theta),0,
      0,1,0,0,
      -sin(theta),0,cos(theta),0,
      0,0,0,1);
  vec3 l = normalize((view * vec4(.0,.0,.0,1.0)).xyz - (VM*vec4(bumped_p,1.0)).xyz);
  float p = 2000;
  vec3 v = normalize(-(VM*vec4(bumped_p,1.0)).xyz);

  vec3 ka,kd,ks;

  vec3 seed = (rotate * vec4(sphere_fs_in,1.0)).xyz * 0.1;
  float random_cloud_colour = 0.0;
  random_cloud_colour += 1.0000 * improved_perlin_noise(seed); 
  seed = 2.0 * seed;
  random_cloud_colour += 0.5000 * improved_perlin_noise(seed); 
  seed = 2.0 * seed;
  random_cloud_colour += 0.2500 * improved_perlin_noise(seed); 
  seed = 2.0 * seed;
  random_cloud_colour += 0.1250 * improved_perlin_noise(seed); 
  seed = 2.0 * seed;
  random_cloud_colour += 0.0625 * improved_perlin_noise(seed);

  if(planet_id == 0){ //太阳
    float ratio = length(bumped_p)/length(sphere_fs_in);
    if(length(bumped_p) < length(sphere_fs_in)){//low
      kd = mix(vec3(.0, .02, .04), vec3(.04, .01, .01),sin((1-ratio)/0.1* M_PI*1.5));
      ka = mix(vec3(1.0, .6, 0.2), vec3(1, 0.4, .0),  sin((1-ratio)/0.1* M_PI*10));
      ks = vec3(.9, .9, .9);
    }
    else{//high
      kd = mix(vec3(.0, .02, .02),vec3(.06, .04, .0),sin((ratio-1) * M_PI* 1.5));
      ka = mix(vec3(1.0, .4, .0), vec3(1, 0.0, .0), sin((ratio-1) * M_PI* 15));
      ks = vec3(.9, .9, .9);
    }
    vec3 orig_color = blinn_phong(ka,kd,ks,p,n,v,l);
    vec3 seed = (rotate * vec4(sphere_fs_in,1.0)).xyz * 0.1;
    float random_shine_color = 0.0;
    random_shine_color += 1.0000 * improved_perlin_noise(seed); 
    seed = 2.0 * seed;
    random_shine_color += 0.5000 * improved_perlin_noise(seed); 
    seed = 2.0 * seed;
    random_shine_color += 0.2500 * improved_perlin_noise(seed); 
    seed = 2.0 * seed;
    random_shine_color += 0.1250 * improved_perlin_noise(seed); 
    seed = 2.0 * seed;
    random_shine_color += 0.0625 * improved_perlin_noise(seed);
    color = mix(orig_color, vec3(1.0, 1.0, 1.0), random_shine_color);
  }
  else if(planet_id == 1){ //水星
    ka = vec3(.01, .01, .01);
    kd = vec3(.3, .3, .3);
    ks = vec3(.2, .2, .2);
    color = blinn_phong(ka,kd,ks,p,n,v,l);
  }
  else if(planet_id == 2){ //金星
    float ratio = length(bumped_p)/length(sphere_fs_in);
    if(length(bumped_p) < length(sphere_fs_in)){//low
      ka = mix(vec3(.04, .01, .01),vec3(.0, .02, .04),sin((1-ratio)* M_PI*1.5));
      kd = mix(vec3(0.8, 0.4, 0.0),vec3(0.6, 0.3, 0.0),sin((1-ratio) * M_PI*10));
      ks = vec3(.9, .9, .9);
    }
    else{//high
      ka = mix(vec3(.0, .02, .02), vec3(.06, .04, .0),sin((ratio-1) * M_PI* 1.5));
      kd = mix(vec3(0.8, 0.4, 0.0), vec3(1.0, 0.7, 0.4), sin((ratio-1) * M_PI* 10));
      ks = vec3(.9, .9, .9);
    }
    color = blinn_phong(ka,kd,ks,p,n,v,l);
  }
  else if(planet_id == 3){ //地球
    float ratio = length(bumped_p)/length(sphere_fs_in);
    if(length(bumped_p) < length(sphere_fs_in)){//sea
      ka = mix(vec3(.04, .01, .01),vec3(.0, .02, .04),sin((1-ratio)/0.1* M_PI*1.3));
      kd = mix(vec3(.0, .6, 1),vec3(.0, .0, 1),   sin((1-ratio)/0.1* M_PI*1.3));
      ks = vec3(.8, .8, .8);
    }
    else{//land
      ka = mix(vec3(.0, .02, .02),vec3(.06, .04, .0),sin((ratio-1)/0.1 * M_PI*1.3));
      kd = mix(vec3(.0, .4, .4), vec3(1, 1, .0), sin((ratio-1)/0.1 * M_PI*1.3));
      ks = vec3(.1, .1, .1);
    }
    if(is_cloud(sphere_fs_in)){
      vec3 cloud_ka,cloud_kd,cloud_ks;
      cloud_ka = mix(vec3(.0, .0, .0),vec3(.0, .0, .0),sin(random_cloud_colour*M_PI));
      cloud_kd = mix(vec3(.9, .9, .9), vec3(.75, .75, .75), sin(random_cloud_colour*M_PI*1.2));
      cloud_ks = vec3(.0, .0, .0);
      vec3 cloud_n,cloud_l,cloud_v;
      cloud_n = normalize((VM*vec4(sphere_fs_in,0.0)).xyz);
      cloud_l = normalize((view * rotate * vec4(0.0,0.0,0.0,1.0)).xyz - (VM*vec4(sphere_fs_in,1.0)).xyz);
      cloud_v = normalize(-(VM*vec4(sphere_fs_in,1.0)).xyz);
      vec3 cloud_colour = blinn_phong(cloud_ka,cloud_kd,cloud_ks,p,cloud_n,cloud_v,cloud_l);
      cloud_colour = vec3(clamp(cloud_colour[0],0,1), clamp(cloud_colour[1],0,1), clamp(cloud_colour[2],0,1));
      color = cloud_colour;
    }
    else{
      color = blinn_phong(ka,kd,ks,p,n,v,l);
    }
    
  }
  else if(planet_id == 4){ //火星
    ka = vec3(.01, .01, .01);
    ks = vec3(.2, .2, .2);
    float ratio = length(bumped_p)/length(sphere_fs_in);
    if(length(bumped_p) < length(sphere_fs_in)){//low
      kd = mix(vec3(0.94, 0.41, 0.23),vec3(0.54, 0.3, 0.0),sin((1-ratio)* M_PI*10));
    }
    else{//high
      kd = mix(vec3(0.94, 0.41, 0.23), vec3(0.8, 0.4, 0.0), sin((ratio-1)* M_PI*10));
    }
    color = blinn_phong(ka,kd,ks,p,n,v,l);
  }
  else if(planet_id == 5){ //木星
    ka = vec3(.01, .01, .01);
    kd = vec3(.9, .5, .0);
    ks = vec3(.0, .0, .0);
    if(is_jpt_cloud(sphere_fs_in)){
      vec3 cloud_ka,cloud_kd,cloud_ks;
      cloud_ka = mix(vec3(.0, .0, .0),vec3(.0, .0, .0),sin(random_cloud_colour*M_PI));
      cloud_kd = mix(vec3(.9, .9, .9), vec3(.75, .75, .75), sin(random_cloud_colour*M_PI*1.2));
      cloud_ks = vec3(.0, .0, .0);
      vec3 cloud_n,cloud_l,cloud_v;
      cloud_n = normalize((VM*vec4(sphere_fs_in,0.0)).xyz);
      cloud_l = normalize((view * rotate * vec4(0.0,0.0,0.0,1.0)).xyz - (VM*vec4(sphere_fs_in,1.0)).xyz);
      cloud_v = normalize(-(VM*vec4(sphere_fs_in,1.0)).xyz);
      vec3 cloud_colour = blinn_phong(cloud_ka,cloud_kd,cloud_ks,p,cloud_n,cloud_v,cloud_l);
      color = cloud_colour;
      }
    else{
      color = blinn_phong(ka,kd,ks,p,n,v,l);
    }
  }
  else if(planet_id == 6){ //土星
    ka = vec3(.01, .01, .01);
    kd = vec3(.71, .475, .13);
    ks = vec3(.2, .2, .2);
    color = blinn_phong(ka,kd,ks,p,n,v,l);
  }
  else if(planet_id == 9){// 土星星环
    ka = vec3(.0, .0, .0);
    if(is_sat_cloud(sphere_fs_in)){
      kd = mix(vec3(.648, .578, .4), vec3(.625, .625, .625), sin(random_cloud_colour*M_PI*1.2));
    }
    else{
      kd = vec3(.0,.0,.0);
    }
    ks = vec3(.0, .0, .0);
    color = blinn_phong(ka,kd,ks,p,n,v,l);
  }
  else if(planet_id == 7){ //天王星
    ka = vec3(.01, .01, .01);
    kd = vec3(.0, .9, .9);
    ks = vec3(.2, .2, .2);
    color = blinn_phong(ka,kd,ks,p,n,v,l);
  }
  else if(planet_id == 8){ //海王星
    ka = vec3(.01, .01, .01);
    kd = vec3(.0, .3, .6);
    ks = vec3(.2, .2, .2);
    color = blinn_phong(ka,kd,ks,p,n,v,l);
  }
  else{
    ka = mix(vec3(1, 1, 1), vec3(.0, .0, .0), sin(random_cloud_colour*M_PI*0.5));
    kd = vec3(.0, .0, .0);
    ks = vec3(.2, .2, .2);
    color = blinn_phong(ka,kd,ks,p,n,v,l);
  }
  
  
    //color = 0.5+0.5*n;
    //color = vec3(0.5,0.5,0)+vec3(0.5,0.5,0)*view_pos_fs_in.xyz;
  //color = vec3(clamp(color[0],0,1), clamp(color[1],0,1), clamp(color[2],0,1));

  
  /////////////////////////////////////////////////////////////////////////////
}
