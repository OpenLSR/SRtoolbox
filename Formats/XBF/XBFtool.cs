using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using SRtoolbox;

/*
 *
 * Disclaimer:
 * This code was decompiled using dnSpy. Understand at your own risk.
 *
 */

namespace SRtoolbox
{
    class XBFtool
    {
        // Token: 0x06000004 RID: 4 RVA: 0x00002118 File Offset: 0x00000318
        public void Setup(string args)
        {
            if (args == string.Empty)
            {
                throw new ArgumentException("No file handed over!");
            }
            if (!File.Exists(args))
            {
                throw new ArgumentException(string.Format("File {0} does not exist!", args[0]));
            }
            this.directoryname = Path.GetDirectoryName(args);
            this.extention = Path.GetExtension(args);
            this.filename = Path.GetFileName(args);
            this.filenamewithoutextension = Path.GetFileNameWithoutExtension(args);
            this.fullPath = Path.GetFullPath(args);
            if (this.extention.ToUpper() != ".XBF")
            {
                throw new ArgumentException("File extention != .XBF");
            }
        }

        // Token: 0x06000005 RID: 5 RVA: 0x000021D0 File Offset: 0x000003D0
        public void Extract(string directory)
        {
            FileInfo fileInfo = new FileInfo(this.fullPath);
            FileStream fileStream = File.Open(this.fullPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            this.fileData = new byte[(int)fileInfo.Length];
            fileStream.Read(this.fileData, 0, (int)fileInfo.Length);
            fileStream.Close();
            this.unknownInt = BitConverter.ToInt32(this.fileData, this.pos);
            ColoredConsole.WriteLineDebug("{0:x8} UnknownInt {1:x8}", new object[]
            {
                this.pos,
                this.unknownInt
            });
            this.pos += 4;
            this.unknownInt = BitConverter.ToInt32(this.fileData, this.pos);
            ColoredConsole.WriteLineDebug("{0:x8} UnknownInt {1:x8}", new object[]
            {
                this.pos,
                this.unknownInt
            });
            this.pos += 4;
            int num = BitConverter.ToInt32(this.fileData, this.pos);
            ColoredConsole.WriteLineInfo("{0:x8} SizeOfTextureNames {1:x8}", new object[]
            {
                this.pos,
                num
            });
            this.pos += 4;
            for (int i = 0; i < num; i += 2)
            {
                ColoredConsole.WriteDebug("{0:x8} ", new object[]
                {
                    this.pos
                });
                List<byte> list = new List<byte>();
                while (this.fileData[this.pos] != 0)
                {
                    list.Add(this.fileData[this.pos]);
                    this.pos++;
                    i++;
                }
                ColoredConsole.WriteLineDebug("{0}", new object[]
                {
                    Encoding.ASCII.GetString(list.ToArray())
                });
                this.textures.Add(Encoding.ASCII.GetString(list.ToArray()));
                this.pos += 2;
            }
            Mesh mesh = this.ReadMesh(true, " ");
            this.WriteObj(mesh);
        }

        // Token: 0x06000006 RID: 6 RVA: 0x0000241C File Offset: 0x0000061C
        private Mesh ReadMesh(bool isMain, string prefix)
        {
            Mesh mesh = new Mesh();
            int num = BitConverter.ToInt32(this.fileData, this.pos);
            ColoredConsole.WriteLineInfo("{0:x8} {1} NumberOfVertices {2:x8}", new object[]
            {
                this.pos,
                prefix,
                num
            });
            this.pos += 4;
            this.unknownInt = BitConverter.ToInt32(this.fileData, this.pos);
            ColoredConsole.WriteLineDebug("{0:x8} {1} UnknownInt {2:x8}", new object[]
            {
                this.pos,
                prefix,
                this.unknownInt
            });
            this.pos += 4;
            int num2 = BitConverter.ToInt32(this.fileData, this.pos);
            ColoredConsole.WriteLineInfo("{0:x8} {1} NumberOfIndices {2:x8}", new object[]
            {
                this.pos,
                prefix,
                num2
            });
            this.pos += 4;
            int num3 = BitConverter.ToInt32(this.fileData, this.pos);
            ColoredConsole.WriteLineInfo("{0:x8} {1} NumberOfSubMeshes {2:x8}", new object[]
            {
                this.pos,
                prefix,
                num3
            });
            this.pos += 4;
            this.pos += 128;
            int num4 = BitConverter.ToInt32(this.fileData, this.pos);
            ColoredConsole.WriteLineDebug("{0:x8} {1} MeshNameLength {2:x8}", new object[]
            {
                this.pos,
                prefix,
                num4
            });
            this.pos += 4;
            List<byte> list = new List<byte>();
            ColoredConsole.WriteDebug("{0:x8} {1} ", new object[]
            {
                this.pos,
                prefix
            });
            for (int i = 0; i < num4; i++)
            {
                if (this.fileData[this.pos] != 0)
                {
                    list.Add(this.fileData[this.pos]);
                }
                this.pos++;
            }
            ColoredConsole.WriteLineWarn("MeshName: {0}", new object[]
            {
                Encoding.ASCII.GetString(list.ToArray())
            });
            mesh.Name = Encoding.ASCII.GetString(list.ToArray());
            for (int i = 0; i < num3; i++)
            {
                mesh.SubMeshes.Add(this.ReadMesh(false, prefix + "  "));
            }
            ColoredConsole.WriteLineInfo("{0:x8} {1} ReadVertices", new object[]
            {
                this.pos,
                prefix
            });
            for (int i = 0; i < num; i++)
            {
                float num5 = BitConverter.ToSingle(this.fileData, this.pos);
                this.pos += 4;
                float num6 = BitConverter.ToSingle(this.fileData, this.pos);
                this.pos += 4;
                float num7 = BitConverter.ToSingle(this.fileData, this.pos);
                this.pos += 4;
                float num8 = BitConverter.ToSingle(this.fileData, this.pos);
                this.pos += 4;
                float num9 = BitConverter.ToSingle(this.fileData, this.pos);
                this.pos += 4;
                float num10 = BitConverter.ToSingle(this.fileData, this.pos);
                this.pos += 4;
                mesh.Vectors.Add(new Vector3D((double)num5, (double)num6, (double)num7));
                mesh.Normals.Add(new Vector3D((double)num8, (double)num9, (double)num10));
            }
            ColoredConsole.WriteLineInfo("{0:x8} {1} ReadIndices", new object[]
            {
                this.pos,
                prefix
            });
            for (int i = 0; i < num2; i++)
            {
                int a = BitConverter.ToInt32(this.fileData, this.pos);
                this.pos += 4;
                int b = BitConverter.ToInt32(this.fileData, this.pos);
                this.pos += 4;
                int c = BitConverter.ToInt32(this.fileData, this.pos);
                this.pos += 4;
                int textureId = BitConverter.ToInt32(this.fileData, this.pos);
                this.pos += 4;
                int e = BitConverter.ToInt32(this.fileData, this.pos);
                this.pos += 4;
                float x = BitConverter.ToSingle(this.fileData, this.pos);
                this.pos += 4;
                float y = BitConverter.ToSingle(this.fileData, this.pos);
                this.pos += 4;
                mesh.UVs.Add(new Vector2D(x, y));
                float x2 = BitConverter.ToSingle(this.fileData, this.pos);
                this.pos += 4;
                float y2 = BitConverter.ToSingle(this.fileData, this.pos);
                this.pos += 4;
                mesh.UVs.Add(new Vector2D(x2, y2));
                float x3 = BitConverter.ToSingle(this.fileData, this.pos);
                this.pos += 4;
                float y3 = BitConverter.ToSingle(this.fileData, this.pos);
                this.pos += 4;
                mesh.UVs.Add(new Vector2D(x3, y3));
                mesh.Triangles.Add(new Triangle(a, b, c, textureId, e, mesh.UVs.Count - 3, mesh.UVs.Count - 2, mesh.UVs.Count - 1));
            }
            for (int i = 0; i < num2; i++)
            {
                this.pos += 4;
            }
            if (!isMain && num > 0)
            {
                int num11 = BitConverter.ToInt32(this.fileData, this.pos);
                ColoredConsole.WriteLineError("{0:x8} {1} NumberOfFrames {2:x8}", new object[]
                {
                    this.pos,
                    prefix,
                    num11
                });
                this.pos += 4;
                if (num11 != 0)
                {
                    int num12 = BitConverter.ToInt32(this.fileData, this.pos);
                    ColoredConsole.WriteLineError("{0:x8} {1} NumberOfPoints {2:x8}", new object[]
                    {
                        this.pos,
                        prefix,
                        num12
                    });
                    this.pos += 4;
                    for (int i = 0; i < num12; i++)
                    {
                        short num13 = BitConverter.ToInt16(this.fileData, this.pos);
                        this.pos += 2;
                        short num14 = BitConverter.ToInt16(this.fileData, this.pos);
                        this.pos += 2;
                        ColoredConsole.WriteLineWarn("{0:x8} {1} Frame {2:x4} Type {3:x4}", new object[]
                        {
                            this.pos,
                            prefix,
                            num13,
                            num14
                        });
                        if ((num14 & 4096) == 4096)
                        {
                            float num15 = BitConverter.ToSingle(this.fileData, this.pos);
                            this.pos += 4;
                            float num16 = BitConverter.ToSingle(this.fileData, this.pos);
                            this.pos += 4;
                            float num17 = BitConverter.ToSingle(this.fileData, this.pos);
                            this.pos += 4;
                            float num18 = BitConverter.ToSingle(this.fileData, this.pos);
                            this.pos += 4;
                            ColoredConsole.WriteLineDebug("{0:x8} {1}   Rotation: {2} {3} {4} {5}", new object[]
                            {
                                this.pos,
                                prefix,
                                num15,
                                num16,
                                num17,
                                num18
                            });
                        }
                        if ((num14 & 8192) == 8192)
                        {
                            float num15 = BitConverter.ToSingle(this.fileData, this.pos);
                            this.pos += 4;
                            float num16 = BitConverter.ToSingle(this.fileData, this.pos);
                            this.pos += 4;
                            float num17 = BitConverter.ToSingle(this.fileData, this.pos);
                            this.pos += 4;
                            ColoredConsole.WriteLineDebug("{0:x8} {1}   Scale: {2} {3} {4}", new object[]
                            {
                                this.pos,
                                prefix,
                                num15,
                                num16,
                                num17
                            });
                        }
                        if ((num14 & 16384) == 16384)
                        {
                            float num15 = BitConverter.ToSingle(this.fileData, this.pos);
                            this.pos += 4;
                            float num16 = BitConverter.ToSingle(this.fileData, this.pos);
                            this.pos += 4;
                            float num17 = BitConverter.ToSingle(this.fileData, this.pos);
                            this.pos += 4;
                            ColoredConsole.WriteLineDebug("{0:x8} {1}   Position: {2} {3} {4}", new object[]
                            {
                                this.pos,
                                prefix,
                                num15,
                                num16,
                                num17
                            });
                        }
                        if (num14 > 28672)
                        {
                            throw new NotSupportedException("FrameType: " + num14);
                        }
                    }
                }
            }
            return mesh;
        }

        // Token: 0x06000007 RID: 7 RVA: 0x00002EAC File Offset: 0x000010AC
        public void WriteObj(Mesh mesh)
        {
            FileStream fileStream = File.Create(this.directoryname + "\\" + mesh.Name + ".obj");
            StreamWriter streamWriter = new StreamWriter(fileStream);
            foreach (Vector3D arg in mesh.Vectors)
            {
                streamWriter.WriteLine(string.Format("v {0}", arg).Replace(',', '.'));
            }
            streamWriter.WriteLine();
            foreach (Vector3D arg2 in mesh.Normals)
            {
                streamWriter.WriteLine(string.Format("vn {0}", arg2).Replace(',', '.'));
            }
            streamWriter.WriteLine();
            foreach (Vector2D arg3 in mesh.UVs)
            {
                streamWriter.WriteLine(string.Format("vt {0}", arg3).Replace(',', '.'));
            }
            string text = string.Empty;
            streamWriter.WriteLine();
            streamWriter.WriteLine("g " + mesh.Name);
            int count = mesh.Vectors.Count;
            int count2 = mesh.UVs.Count;
            foreach (Triangle triangle in mesh.Triangles)
            {
                if (triangle.TextureId < this.textures.Count && this.textures[triangle.TextureId] != text)
                {
                    text = this.textures[triangle.TextureId];
                    streamWriter.WriteLine();
                    streamWriter.WriteLine("usemtl " + text);
                }
                if (triangle.TextureId < this.textures.Count)
                {
                    streamWriter.WriteLine("f {0}/{3}/{0} {1}/{4}/{1} {2}/{5}/{2}", new object[]
                    {
                        triangle.A - count,
                        triangle.B - count,
                        triangle.C - count,
                        triangle.UV1 - count2,
                        triangle.UV2 - count2,
                        triangle.UV3 - count2
                    });
                }
                else
                {
                    streamWriter.WriteLine("f {0}//{0} {1}//{1} {2}//{2}", triangle.A - count, triangle.B - count, triangle.C - count);
                }
            }
            streamWriter.Close();
            fileStream.Close();
            foreach (Mesh mesh2 in mesh.SubMeshes)
            {
                this.WriteObj(mesh2);
            }
        }

        // Token: 0x04000001 RID: 1
        private string directoryname;

        // Token: 0x04000002 RID: 2
        private string extention;

        // Token: 0x04000003 RID: 3
        private string filename;

        // Token: 0x04000004 RID: 4
        private string filenamewithoutextension;

        // Token: 0x04000005 RID: 5
        private string fullPath;

        // Token: 0x04000006 RID: 6
        private byte[] fileData;

        // Token: 0x04000007 RID: 7
        private int pos = 0;

        // Token: 0x04000008 RID: 8
        private List<string> textures = new List<string>();

        // Token: 0x04000009 RID: 9
        private int unknownInt;
    }
}
