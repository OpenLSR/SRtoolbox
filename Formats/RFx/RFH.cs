using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Ionic.Zlib;

namespace SRtoolbox
{
    internal class RFile
    {
        
        public int uncompressedSize;
        public int pathLength;
        public int compressionType;
        public int compressedSize;
        public int offset;
        public string filepath;
        public byte[] data;
    }

    internal class RFH
    {
        private List<RFile> RFH_fileList = new List<RFile>();

        public void load(string fileRFH, string fileRFD)
        {
            FileStream fsHeader = new FileStream(fileRFH, FileMode.Open);
            BinaryReader brHeader = new BinaryReader(fsHeader);
            FileStream fsData = new FileStream(fileRFD, FileMode.Open);
            BinaryReader brData = new BinaryReader(fsData);
            do
            {
                int pathLen = brHeader.ReadInt16() - 1;
                int ucmpSize = brHeader.ReadInt32();

                /*
                byte[] temp = new byte[4];
                byte[] tmp2 = brHeader.ReadBytes(2);
                temp[2] = tmp2[0]; temp[3] = tmp2[1];
                int timestamp = BitConverter.ToInt32(temp, 0);
                */

                // The timestamp is here, but it's stupid, so I'm ignoring it.
                brHeader.BaseStream.Seek(2L, SeekOrigin.Current); 
                
                int cmpType = brHeader.ReadInt32();
                int cmpSize = brHeader.ReadInt32();
                int offset = brHeader.ReadInt32();
                byte[] relPath = brHeader.ReadBytes(pathLen);
                brHeader.BaseStream.Seek(1L, SeekOrigin.Current);
                brData.BaseStream.Seek((long)offset, SeekOrigin.Begin);
                byte[] data = brData.ReadBytes(cmpSize);
                this.RFH_fileList.Add(new RFile
                {
                    filepath = Encoding.UTF8.GetString(relPath),
                    pathLength = pathLen,
                    compressionType = cmpType,
                    compressedSize = cmpSize,
                    uncompressedSize = ucmpSize,
                    offset = offset,
                    data = data
                });
            }
            while (brHeader.BaseStream.Position < brHeader.BaseStream.Length);
            brData.Close();
            fsData.Close();
            brHeader.Close();
            fsHeader.Close();
        }

        public bool extractAllFiles(string directory)
        {
            bool success = false;
            if (RFH_fileList.Count() > 0)
            {
                foreach (RFile file in this.RFH_fileList)
                {
                    extractFile(file, directory, true);
                }
                success = true;
            }
            return success;
        }

        public bool extractFile(RFile file, string directory, bool preserveStructure)
        {
            bool success = false;
            if (file != null)
            {
                Directory.CreateDirectory(directory + (preserveStructure ? ("\\" + Path.GetDirectoryName(file.filepath) + "\\") : ""));
                FileStream fileStream = new FileStream(directory + "\\" + (preserveStructure ? ("\\" + Path.GetDirectoryName(file.filepath) + "\\") : "") + Path.GetFileName(file.filepath), FileMode.Create);
                BinaryWriter binaryWriter = new BinaryWriter(fileStream);
                byte[] buffer = file.data;
                if (file.data.Count() > 4)
                {
                    if(file.compressionType != 0)
                    {
                        byte[] array = new byte[file.data.Length - 4];
                        Buffer.BlockCopy(file.data, 4, array, 0, file.data.Length - 4);
                        buffer = ZlibStream.UncompressBuffer(array);
                    }
                }
                binaryWriter.Write(buffer);
                binaryWriter.Close();
                fileStream.Close();
                //File.SetLastWriteTimeUtc(directory + "\\" + file.filepath, DateTimeOffset.FromUnixTimeSeconds(file.timestamp).UtcDateTime);
                success = true;
            }
            return success;
        }

        public byte[] packFile(string filepath, string directory, int offset, bool compress) // finish this later...
        {
            byte[] pkFile = new byte[16 ^ 4];
            if (filepath != null && File.Exists(filepath)) {
                FileStream fsFile = new FileStream(filepath, FileMode.Open);
                BinaryReader brFile = new BinaryReader(fsFile);

                byte[] leFile = brFile.ReadBytes((int)fsFile.Length);
                byte[] cmpFile = new byte[16 ^ 3];

                Int16 pathLen = (Int16)(filepath.Length + 1);
                pkFile.Concat(BitConverter.GetBytes(pathLen));

                Int32 ucmpSize = (Int32)(fsFile.Length);
                pkFile.Concat(BitConverter.GetBytes(ucmpSize));

                pkFile.Concat(new byte[2] { 155, 57 }); // Modified time, don't caare

                Int32 cmpType = compress ? 2 : 0;
                pkFile.Concat(BitConverter.GetBytes(cmpType));

                Int32 cmpSize = 0;
                if (compress)
                {
                    cmpFile = ZlibStream.CompressBuffer(leFile);
                    cmpSize = (Int32)(cmpFile.Length);
                }
                else
                {
                    cmpSize = (Int32)(fsFile.Length);
                }
                pkFile.Concat(BitConverter.GetBytes(cmpSize));

                pkFile.Concat(BitConverter.GetBytes((Int32)offset));

                pkFile.Concat(Encoding.ASCII.GetBytes(filepath));

                pkFile.Concat(new byte[1] { 0 });

                pkFile.Concat(compress ? cmpFile : leFile);
            }
            return pkFile;
        }
    }
}
