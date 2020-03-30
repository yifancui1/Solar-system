// Input:
//   N  3D unit normal vector
// Outputs:
//   T  3D unit tangent vector
//   B  3D unit bitangent vector
void tangent(in vec3 N, out vec3 T, out vec3 B)
{
  /////////////////////////////////////////////////////////////////////////////
  // Replace with your code 
  if(N.x == 0){
    T = vec3(1.0,0.0,0.0);
  }
  else if(N.y == 0){
    T = vec3(0.0,1.0,0.0);
  }
  else if(N.z == 0){
    T = vec3(0.0,0.0,1.0);
  }
  else{
    T = normalize(vec3(N.y,-N.x,0));
  }
  
  B = normalize(cross(N,T));
  /////////////////////////////////////////////////////////////////////////////
}
