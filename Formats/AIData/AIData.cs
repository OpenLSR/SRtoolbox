using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRtoolbox
{
    class AIData
    {
        public AIFile load(string filename)
        {
            AIFile ai = new AIFile();

            FileStream file = File.Open(filename, FileMode.Open, FileAccess.Read);
            using (BinaryReader reader = new BinaryReader(file))
            {
                reader.ReadBytes(4);
                ai.RacingLine = reader.ReadUInt16();
                ai.Braking = reader.ReadUInt16();
                ai.Overtaking = reader.ReadUInt16();
                ai.Speed = reader.ReadUInt16();
                ai.Reflex = reader.ReadUInt16();
                reader.ReadBytes(2);
                ai.Blocking = reader.ReadByte();
                ai.CutsCorners = reader.ReadByte();
                ai.Intelligence = reader.ReadByte();
                ai.Craziness = reader.ReadByte();
            }

            return ai;
        }

        public void save(string filename, AIFile ai)
        {
            using (BinaryWriter writer = new BinaryWriter(File.Open(filename, FileMode.Create)))
            {
                writer.Write(new byte[] { 0, 0, 0, 0 });
                writer.Write(ai.RacingLine);
                writer.Write(ai.Braking);
                writer.Write(ai.Overtaking);
                writer.Write(ai.Speed);
                writer.Write(ai.Reflex);
                writer.Write(new byte[] { 0, 0 });
                writer.Write(ai.Blocking);
                writer.Write(ai.CutsCorners);
                writer.Write(ai.Intelligence);
                writer.Write(ai.Craziness);
            }
        }
    }
}
