using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRtoolbox
{
    // Token: 0x02000007 RID: 7
    public class Vector3D
    {
        // Token: 0x1700000A RID: 10
        // (get) Token: 0x0600001F RID: 31 RVA: 0x000034B4 File Offset: 0x000016B4
        // (set) Token: 0x06000020 RID: 32 RVA: 0x000034CB File Offset: 0x000016CB
        public double X { get; set; }

        // Token: 0x1700000B RID: 11
        // (get) Token: 0x06000021 RID: 33 RVA: 0x000034D4 File Offset: 0x000016D4
        // (set) Token: 0x06000022 RID: 34 RVA: 0x000034EB File Offset: 0x000016EB
        public double Y { get; set; }

        // Token: 0x1700000C RID: 12
        // (get) Token: 0x06000023 RID: 35 RVA: 0x000034F4 File Offset: 0x000016F4
        // (set) Token: 0x06000024 RID: 36 RVA: 0x0000350B File Offset: 0x0000170B
        public double Z { get; set; }

        // Token: 0x06000025 RID: 37 RVA: 0x00003514 File Offset: 0x00001714
        public Vector3D(double x, double y, double z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        // Token: 0x06000026 RID: 38 RVA: 0x00003538 File Offset: 0x00001738
        public override string ToString()
        {
            return string.Format("{0:0.#####} {1:0.#####} {2:0.#####} ", this.X, this.Y, this.Z).Replace(',', '.');
        }
    }
}
