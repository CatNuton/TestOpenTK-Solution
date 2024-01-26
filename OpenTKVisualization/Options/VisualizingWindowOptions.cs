using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenTKVisualization.Options
{
    public class VisualizingWindowOptions
    {
        public float FOV { get; set; } = 45.0f;
        public PolygonMode PolyMode { get; set; }
        public bool IsLightingEnabled { get; set; }
    }
}
