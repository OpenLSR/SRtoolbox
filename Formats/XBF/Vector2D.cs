using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRtoolbox
{
    // Token: 0x02000006 RID: 6
    public class Vector2D
    {
        // Token: 0x17000008 RID: 8
        // (get) Token: 0x06000019 RID: 25 RVA: 0x0000341C File Offset: 0x0000161C
        // (set) Token: 0x0600001A RID: 26 RVA: 0x00003433 File Offset: 0x00001633
        public float X { get; set; }

        // Token: 0x17000009 RID: 9
        // (get) Token: 0x0600001B RID: 27 RVA: 0x0000343C File Offset: 0x0000163C
        // (set) Token: 0x0600001C RID: 28 RVA: 0x00003453 File Offset: 0x00001653
        public float Y { get; set; }

        // Token: 0x0600001D RID: 29 RVA: 0x0000345C File Offset: 0x0000165C
        public Vector2D(float x, float y)
        {
            this.X = x;
            this.Y = y;
        }

        // Token: 0x0600001E RID: 30 RVA: 0x00003478 File Offset: 0x00001678
        public override string ToString()
        {
            return string.Format("{0:0.#####} {1:0.#####} ", this.X, this.Y).Replace(',', '.');
        }
    }
}
