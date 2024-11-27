#version 460
out vec4 outputColor;     //vec3 (or vec4) OpenGL automatically recognizes as a color

in vec2 texCoord;       //We transfer attribute from the vertex shader to the fragment shader using out/in

// The Uniform keyword allows you to access a shader variable at any stage of the shader chain
//Each sampler is bound to a texture unit
uniform sampler2D texture0;

void main()
{
    outputColor = texture(texture0, texCoord);
}