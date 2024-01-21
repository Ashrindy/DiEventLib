using System.Numerics;
using HedgeLib.IO;

namespace DiEventLib.Nodes.NodeTypes
{
    public struct rootPathInfo
    {
        public Matrix4x4 matrix;
        public uint flag;
        public byte[] padding;
    }

    public class DvPath : DvNodeObject
    {
        public rootPathInfo rootPath;

        public DvPath(node Node = null, ExtendedBinaryReader reader = null, ExtendedBinaryWriter writer = null)
        {
            if (reader != null) { Read(reader); } else if (writer != null) { Write(writer, Node); }
        }

        public override void Read(ExtendedBinaryReader reader)
        {
            rootPath.matrix.M11 = reader.ReadSingle();
            rootPath.matrix.M12 = reader.ReadSingle();
            rootPath.matrix.M13 = reader.ReadSingle();
            rootPath.matrix.M14 = reader.ReadSingle();
            rootPath.matrix.M21 = reader.ReadSingle();
            rootPath.matrix.M22 = reader.ReadSingle();
            rootPath.matrix.M23 = reader.ReadSingle();
            rootPath.matrix.M24 = reader.ReadSingle();
            rootPath.matrix.M31 = reader.ReadSingle();
            rootPath.matrix.M32 = reader.ReadSingle();
            rootPath.matrix.M33 = reader.ReadSingle();
            rootPath.matrix.M34 = reader.ReadSingle();
            rootPath.matrix.M41 = reader.ReadSingle();
            rootPath.matrix.M42 = reader.ReadSingle();
            rootPath.matrix.M43 = reader.ReadSingle();
            rootPath.matrix.M44 = reader.ReadSingle();
            rootPath.flag = reader.ReadUInt32();
            rootPath.padding = new byte[12];
            for (int x = 0; (x < 12); x++)
            {
                rootPath.padding[x] = reader.ReadByte();
            }
        }

        public override void Write(ExtendedBinaryWriter Writer, node Node)
        {
            Writer.Write(((DvPath)Node.info).rootPath.matrix.M11);
            Writer.Write(((DvPath)Node.info).rootPath.matrix.M12);
            Writer.Write(((DvPath)Node.info).rootPath.matrix.M13);
            Writer.Write(((DvPath)Node.info).rootPath.matrix.M14);
            Writer.Write(((DvPath)Node.info).rootPath.matrix.M21);
            Writer.Write(((DvPath)Node.info).rootPath.matrix.M22);
            Writer.Write(((DvPath)Node.info).rootPath.matrix.M23);
            Writer.Write(((DvPath)Node.info).rootPath.matrix.M24);
            Writer.Write(((DvPath)Node.info).rootPath.matrix.M31);
            Writer.Write(((DvPath)Node.info).rootPath.matrix.M32);
            Writer.Write(((DvPath)Node.info).rootPath.matrix.M33);
            Writer.Write(((DvPath)Node.info).rootPath.matrix.M34);
            Writer.Write(((DvPath)Node.info).rootPath.matrix.M41);
            Writer.Write(((DvPath)Node.info).rootPath.matrix.M42);
            Writer.Write(((DvPath)Node.info).rootPath.matrix.M43);
            Writer.Write(((DvPath)Node.info).rootPath.matrix.M44);
            Writer.Write(((DvPath)Node.info).rootPath.flag);

            foreach (var i in ((DvPath)Node.info).rootPath.padding)
            {
                Writer.Write(i);
            }
        }
    }
}
