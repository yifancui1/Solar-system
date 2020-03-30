# Solar-system
Prepared by Yifan Cui (1002992764) in lecture section LEC2201

![](images/demo.gif)  

Better quality video in video/video.mp4 or [(click here)](video/video.mp4), please set it to **full screen**.

## Desription
This work is extended based on CSC418 2020W Assignment 6 shaderpipline, which build a small earth-moon system.

In this project, I extended it to render the whole solar system, including Sun, Mercury, Venus, Earth, Mars, Jupiter, Saturn, Uranus and Neptune. As well as thousands of small planets showing the whole universe.   
Note: The solar system is not at the correct scale and orbiting speed. The colors and textures are just approximations.

The main modifications includes:
* main.cpp: extend the model from 2 objects to more
* model.glsl: change the model of all new objects
* planet.fs: change light source and all colors and shapes (perlin noise bumps)

## To execute:
run [shaderpipeline](build-release/shaderpipline) under [build-release](build-release)
```Bash
cd build-release
./shaderpipeline
```

## Reference
### 1. Computer Graphics – Shader Pipeline
https://github.com/dilevin/computer-graphics-shader-pipeline  
Starter code was used, with my original implementation from that assignment
### 2. A simple random number generator function in glsl
https://github.com/ashima/webgl-noise/blob/master/src/noise3D.glsl  
(This is under the MIT License)
Borrowed this simple random number generator for small planets generation