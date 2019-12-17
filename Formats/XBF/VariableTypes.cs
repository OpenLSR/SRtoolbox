using System;

namespace SRtoolbox.VariableTypes
{
    // Token: 0x02000006 RID: 6
    public class Color4
    {
        // Token: 0x04000013 RID: 19
        public int R;

        // Token: 0x04000014 RID: 20
        public int B;

        // Token: 0x04000015 RID: 21
        public int G;

        // Token: 0x04000016 RID: 22
        public int A;
    }

    // Token: 0x02000007 RID: 7
    public class UVSet
    {
        // Token: 0x04000017 RID: 23
        public float U;

        // Token: 0x04000018 RID: 24
        public float V;
    }

    // Token: 0x02000008 RID: 8
    public class Vector2
    {
        // Token: 0x04000019 RID: 25
        public float X;

        // Token: 0x0400001A RID: 26
        public float Y;
    }

    // Token: 0x02000009 RID: 9
    public class Vector3 : Vector2
    {
        // Token: 0x0400001B RID: 27
        public float Z;
    }

    // Token: 0x0200000A RID: 10
    public class Vector4 : Vector3
    {
        // Token: 0x0400001C RID: 28
        public float W;
    }
}