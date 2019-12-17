using System;
using System.Collections.Generic;
using System.Text;

namespace SRtoolbox
{
    // Token: 0x02000004 RID: 4
    public class Mesh
    {
        // Token: 0x0400000A RID: 10
        public List<Vector3D> Vectors = new List<Vector3D>();

        // Token: 0x0400000B RID: 11
        public List<Triangle> Triangles = new List<Triangle>();

        // Token: 0x0400000C RID: 12
        public List<Vector3D> Normals = new List<Vector3D>();

        // Token: 0x0400000D RID: 13
        public List<Vector2D> UVs = new List<Vector2D>();

        // Token: 0x0400000E RID: 14
        public List<Mesh> SubMeshes = new List<Mesh>();

        // Token: 0x0400000F RID: 15
        public string Name;
    }
}
