using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRtoolbox
{
    // Token: 0x02000005 RID: 5
    public class Triangle
    {
        // Token: 0x17000001 RID: 1
        // (get) Token: 0x0600000A RID: 10 RVA: 0x000032E8 File Offset: 0x000014E8
        // (set) Token: 0x0600000B RID: 11 RVA: 0x000032FF File Offset: 0x000014FF
        public int A { get; set; }

        // Token: 0x17000002 RID: 2
        // (get) Token: 0x0600000C RID: 12 RVA: 0x00003308 File Offset: 0x00001508
        // (set) Token: 0x0600000D RID: 13 RVA: 0x0000331F File Offset: 0x0000151F
        public int B { get; set; }

        // Token: 0x17000003 RID: 3
        // (get) Token: 0x0600000E RID: 14 RVA: 0x00003328 File Offset: 0x00001528
        // (set) Token: 0x0600000F RID: 15 RVA: 0x0000333F File Offset: 0x0000153F
        public int C { get; set; }

        // Token: 0x17000004 RID: 4
        // (get) Token: 0x06000010 RID: 16 RVA: 0x00003348 File Offset: 0x00001548
        // (set) Token: 0x06000011 RID: 17 RVA: 0x0000335F File Offset: 0x0000155F
        public int TextureId { get; set; }

        // Token: 0x17000005 RID: 5
        // (get) Token: 0x06000012 RID: 18 RVA: 0x00003368 File Offset: 0x00001568
        // (set) Token: 0x06000013 RID: 19 RVA: 0x0000337F File Offset: 0x0000157F
        public int UV1 { get; set; }

        // Token: 0x17000006 RID: 6
        // (get) Token: 0x06000014 RID: 20 RVA: 0x00003388 File Offset: 0x00001588
        // (set) Token: 0x06000015 RID: 21 RVA: 0x0000339F File Offset: 0x0000159F
        public int UV2 { get; set; }

        // Token: 0x17000007 RID: 7
        // (get) Token: 0x06000016 RID: 22 RVA: 0x000033A8 File Offset: 0x000015A8
        // (set) Token: 0x06000017 RID: 23 RVA: 0x000033BF File Offset: 0x000015BF
        public int UV3 { get; set; }

        // Token: 0x06000018 RID: 24 RVA: 0x000033C8 File Offset: 0x000015C8
        public Triangle(int a, int b, int c, int textureId, int e, int uv1, int uv2, int uv3)
        {
            this.A = a;
            this.B = b;
            this.C = c;
            this.TextureId = textureId;
            this.UV1 = uv1;
            this.UV2 = uv2;
            this.UV3 = uv3;
        }
    }
}
