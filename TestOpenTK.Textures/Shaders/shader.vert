﻿#version 460 core
//layout (location = x) sets attribute index x
layout(location = 0) in vec3 aPosition;
layout(location = 1) in vec2 aTexCoord;

out vec2 texCoord;

void main(void)
{
    texCoord = aTexCoord;
    gl_Position = vec4(aPosition, 1.0);
}