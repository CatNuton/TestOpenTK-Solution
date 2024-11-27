#version 460
//layout (location = x) sets attribute index x
in vec3 aPosition;

out vec3 aColor;    //We transfer attribute from the vertex shader to the fragment shader using out/in

void main()
{
    gl_Position = vec4(aPosition, 1.0);
    aColor = vec3(0.0f, 1.0f, 0.0f);
}