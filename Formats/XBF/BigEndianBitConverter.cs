using System;

namespace SRtoolbox
{
    // Token: 0x02000003 RID: 3
    public class BigEndianBitConverter
    {
        // Token: 0x0600000F RID: 15 RVA: 0x00002270 File Offset: 0x00000470
        public static int ToInt32(byte[] data, int i)
        {
            BigEndianBitConverter.tmp = new byte[4];
            BigEndianBitConverter.tmp[3] = data[i];
            BigEndianBitConverter.tmp[2] = data[i + 1];
            BigEndianBitConverter.tmp[1] = data[i + 2];
            BigEndianBitConverter.tmp[0] = data[i + 3];
            return BitConverter.ToInt32(BigEndianBitConverter.tmp, 0);
        }

        // Token: 0x06000010 RID: 16 RVA: 0x000022C8 File Offset: 0x000004C8
        public static float ToSingle(byte[] data, int i)
        {
            BigEndianBitConverter.tmp = new byte[4];
            BigEndianBitConverter.tmp[3] = data[i];
            BigEndianBitConverter.tmp[2] = data[i + 1];
            BigEndianBitConverter.tmp[1] = data[i + 2];
            BigEndianBitConverter.tmp[0] = data[i + 3];
            return BitConverter.ToSingle(BigEndianBitConverter.tmp, 0);
        }

        // Token: 0x06000011 RID: 17 RVA: 0x00002320 File Offset: 0x00000520
        public static Half ToHalf(byte[] data, int i)
        {
            BigEndianBitConverter.tmp = new byte[2];
            BigEndianBitConverter.tmp[1] = data[i];
            BigEndianBitConverter.tmp[0] = data[i + 1];
            return Half.ToHalf(BigEndianBitConverter.tmp, 0);
        }

        // Token: 0x06000012 RID: 18 RVA: 0x00002360 File Offset: 0x00000560
        public static short ToInt16(byte[] data, int i)
        {
            BigEndianBitConverter.tmp = new byte[2];
            BigEndianBitConverter.tmp[1] = data[i];
            BigEndianBitConverter.tmp[0] = data[i + 1];
            return BitConverter.ToInt16(BigEndianBitConverter.tmp, 0);
        }

        // Token: 0x06000013 RID: 19 RVA: 0x000023A0 File Offset: 0x000005A0
        public static ushort ToUInt16(byte[] data, int i)
        {
            BigEndianBitConverter.tmp = new byte[2];
            BigEndianBitConverter.tmp[1] = data[i];
            BigEndianBitConverter.tmp[0] = data[i + 1];
            return BitConverter.ToUInt16(BigEndianBitConverter.tmp, 0);
        }

        // Token: 0x04000006 RID: 6
        private static byte[] tmp;
    }
}

