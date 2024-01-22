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
            rootPath.matrix = Helper.ReadMatrix(reader);
            rootPath.flag = reader.ReadUInt32();
            rootPath.padding = new byte[12];
            for (int x = 0; (x < 12); x++)
            {
                rootPath.padding[x] = reader.ReadByte();
            }
        }

        public override void Write(ExtendedBinaryWriter Writer, node Node)
        {
            Helper.WriteMatrix(Writer, ((DvPath)Node.info).rootPath.matrix);
            Writer.Write(((DvPath)Node.info).rootPath.flag);

            foreach (var i in ((DvPath)Node.info).rootPath.padding)
            {
                Writer.Write(i);
            }
        }
    }
}
