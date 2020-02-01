using Pfim;
using SRtoolbox.VariableTypes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SRtoolbox
{
    class TRK
    {
        private static GCHandle handle;

        private List<TrackPiece> pieces;

        public TRKFile load(string filename)
        {
            TRKFile trk = new TRKFile();
            pieces = new List<TrackPiece>();

            int realsize, iters;
            bool sizeMismatch = false;

            FileStream file = File.Open(filename, FileMode.Open, FileAccess.Read);
            using (BinaryReader reader = new BinaryReader(file))
            {
                realsize = (int)file.Length;
                trk.legoHeader = Encoding.UTF8.GetString(reader.ReadBytes(12));
                trk.crashInt = reader.ReadInt32();
                trk.filesize = reader.ReadInt32();
                if (trk.filesize != 65576) sizeMismatch = true;
                else if (trk.filesize != realsize) sizeMismatch = true;
                if (sizeMismatch) MessageBox.Show("Filesize mismatch detected, may cause problems!","TRK/XTK Manager");
                trk.type = (LSRUtil.TrackType)reader.ReadInt32();
                trk.theme = (LSRUtil.Theme)reader.ReadInt32();
                trk.time = (LSRUtil.TrackTime)reader.ReadInt32();
                int sz = 8 * ((int)trk.type + 1);
                trk.size = new LSRUtil.TrackCoord(sz-1, sz-1);
                trk.compat = LSRUtil.Compatibility.Full;

                try
                {
                    var image = Pfim.Pfim.FromFile(Path.Combine(Path.GetDirectoryName(filename), "Images", Path.GetFileNameWithoutExtension(filename) + ".tga"));
                    handle = GCHandle.Alloc(image.Data, GCHandleType.Pinned);
                    var ptr = Marshal.UnsafeAddrOfPinnedArrayElement(image.Data, 0);
                    trk.image = new Bitmap(image.Width, image.Height, image.Stride, PixelFormat.Format24bppRgb, ptr);
                } catch (FileNotFoundException) {
                    Console.WriteLine("DEBUG: No image found for "+Path.GetFileName(filename));
                }

                iters = (int)trk.size.X+1;
                int skip = (56 - ((int)trk.type * 8)) * 16;
                byte[][][][] pieces_bytes = new byte[iters][][][];

                for (int y = 0; y < iters; y++)
                {
                    byte[][][] tmp = new byte[iters][][];
                    for (int x = 0; x < iters; x++)
                    {
                        byte[][] piece = new byte[3][];
                        reader.ReadBytes(4);
                        for (int z = 0; z < 3; z++)
                        {
                            byte[] bytes = reader.ReadBytes(4);
                            piece[z] = bytes;
                        }
                        // right here
                        TrackPiece newpc = LSRUtil.ParsePiece(piece);
                        newpc.position = new LSRUtil.TrackCoord(x, y);
                        pieces.Add(newpc);
                        tmp[x] = piece;
                    }
                    reader.ReadBytes(skip);
                    pieces_bytes[y] = tmp;
                }

                trk.pieces = pieces;

                trk.compat = LSRUtil.CheckCompat(trk);
            }

            return trk;
        }
    }
}
