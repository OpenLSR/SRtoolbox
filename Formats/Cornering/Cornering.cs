using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRtoolbox
{
    class Cornering
    {
        public CorneringFile load(string filename)
        {
            CorneringFile cor = new CorneringFile();

            FileStream file = File.Open(filename, FileMode.Open, FileAccess.Read);
            using (BinaryReader reader = new BinaryReader(file))
            {
                cor.Small = reader.ReadSingle();
                cor.Big = reader.ReadSingle();
                cor.SmallSmall_Same = reader.ReadSingle();
                cor.SmallSmall_Opp = reader.ReadSingle();
                cor.BigBig_Same = reader.ReadSingle();
                cor.BigBig_Opp = reader.ReadSingle();
                cor.BigSmall_Same = reader.ReadSingle();
                cor.SmallBig_Same = reader.ReadSingle();
                cor.BigSmall_Opp = reader.ReadSingle();
                cor.SmallBig_Opp = reader.ReadSingle();
            }

            return cor;
        }

        public void save(string filename, CorneringFile cor)
        {
            using (BinaryWriter writer = new BinaryWriter(File.Open(filename, FileMode.Create)))
            {
                writer.Write(cor.Small);
                writer.Write(cor.Big);
                writer.Write(cor.SmallSmall_Same);
                writer.Write(cor.SmallSmall_Opp);
                writer.Write(cor.BigBig_Same);
                writer.Write(cor.BigBig_Opp);
                writer.Write(cor.BigSmall_Same);
                writer.Write(cor.SmallBig_Same);
                writer.Write(cor.BigSmall_Opp);
                writer.Write(cor.SmallBig_Opp);
            }
        }
    }
}
