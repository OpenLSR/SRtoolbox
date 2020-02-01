using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace SRtoolbox
{
    class RTB
    {
        DataTable table = new DataTable();
        public BindingSource bs = new BindingSource();
        /// <summary>
        /// This example method generates a DataTable.
        /// </summary>
        public void GenTable(string filename)
        {
            // Here we create a DataTable with four columns.
            table.Columns.Add("ID", typeof(string));
            table.Columns.Add("Path", typeof(string));

            FileStream fsData = new FileStream(filename, FileMode.Open);
            BinaryReader brData = new BinaryReader(fsData);
            brData.BaseStream.Seek(1056L, SeekOrigin.Begin);
            do
            {
                byte[] aid = brData.ReadBytes(2);
                Array.Reverse(aid);
                string asset = "0x" + BitConverter.ToString(aid).Replace("-", string.Empty);

                brData.BaseStream.Seek(6L, SeekOrigin.Current); // skip gay shit

                int offset = 0;
                byte nchar = brData.ReadByte();
                while (nchar != 0x00) // Seperator
                {
                    offset += 1;
                    nchar = brData.ReadByte();
                }

                brData.BaseStream.Seek(-(offset+1), SeekOrigin.Current);
                string name = Encoding.ASCII.GetString(brData.ReadBytes(offset));

                brData.BaseStream.Seek(1, SeekOrigin.Current); // Skip separator

                if (brData.ReadBytes(3) == new byte[3] { 0x37, 0x30, 0x00 })
                    break;

                table.Rows.Add(asset, name);
            }
            while (brData.BaseStream.Position < brData.BaseStream.Length);
            brData.Close();
            fsData.Close();

            this.bs.DataSource = table;
        }
    }
}
