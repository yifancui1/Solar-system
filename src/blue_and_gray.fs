// Set the pixel color to blue or gray depending on is_moon.
//
// Uniforms:
uniform int planet_id;
// Outputs:
out vec3 color;
void main()
{
  /////////////////////////////////////////////////////////////////////////////
  // Replace with your code:
  if(planet_id == 0)
    color = vec3(1,0,0);
  else if(planet_id == 1)
    color = vec3(.375,.375,.375);
  else if(planet_id == 2)
    color = vec3(1,1,.0);
  else if(planet_id == 3)
    color = vec3(.0,.4,.4);
  else if(planet_id == 4)
    color = vec3(.8,0,0);
  else if(planet_id == 5)
    color = vec3(.6,.6,.0);
  else if(planet_id == 6)
    color = vec3(.6,.3,.0);
  else if(planet_id == 7)
    color = vec3(.8,1,1);
  else if(planet_id == 8)
    color = vec3(.0,.4,.8);
  else
    color = vec3(0,0,1);
  /////////////////////////////////////////////////////////////////////////////
}
