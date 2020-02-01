using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRtoolbox
{
    /* Resource Files */

    public class RFxFile
    {
        public int pathLength;
        public DateTime timestamp;
        public int compressionType;
        public int compressedSize;
        public int offset;
        public string filepath;
        public byte[] data;
    }

    /* Track Files */

    public class TrackFile
    {
        public LSRUtil.TrackType type;
        public LSRUtil.TrackCoord size;
        public LSRUtil.Theme theme;
        public LSRUtil.TrackTime time;
        public int filesize;
        public Bitmap image;
        public LSRUtil.Compatibility compat;

        public List<TrackPiece> pieces;
    }

    public class TRKFile : TrackFile
    {
        public string legoHeader;
        public int crashInt;

        public byte[][][][] rawPieces;
    }

    public class XTKFile : TrackFile
    {
        // TODO
    }

    public class TrackPiece
    {
        public int nid;
        public byte id;
        public LSRUtil.Theme theme;
        public LSRUtil.TrackCoord position;
        public int height;
        public int rotation;
    }

    /* AI Files */

    public class CorneringFile
    {
        public float Small;
        public float Big;
        public float SmallSmall_Same;
        public float SmallSmall_Opp;
        public float BigBig_Same;
        public float BigBig_Opp;
        public float BigSmall_Same;
        public float SmallBig_Same;
        public float BigSmall_Opp;
        public float SmallBig_Opp;
    }

    public class AIFile
    {
        public ushort Reflex;
        public ushort RacingLine;
        public ushort Overtaking;
        public byte Blocking;
        public byte CutsCorners;
        public ushort Braking;
        public ushort Speed;
        public byte Intelligence;
        public byte Craziness;
    }
}
