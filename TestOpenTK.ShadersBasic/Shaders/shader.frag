#version 460
out vec3 FragColor;     //vec3 (or vec4) OpenGL automatically recognizes as a color

in vec3 aColor;         //We transfer attribute from the vertex shader to the fragment shader using out/in

void main()
{
    FragColor = vec3(aColor);
}