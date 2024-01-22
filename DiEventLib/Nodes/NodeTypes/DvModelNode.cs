using HedgeLib.IO;

namespace DiEventLib.Nodes.NodeTypes
{
    public struct modelNodeInfo
    {
        public uint field_00;
        public string name1;
        public byte[] padding;
    }

    public class DvModelNode : DvNodeObject
    {
        public modelNodeInfo modelNodeInfo;

        public DvModelNode(node Node = null, ExtendedBinaryReader reader = null, ExtendedBinaryWriter writer = null)
        {
            if (reader != null) { Read(reader); } else if (writer != null) { Write(writer, Node); }
        }

        public override void Read(ExtendedBinaryReader reader)
        {
            modelNodeInfo.field_00 = reader.ReadUInt32();
            modelNodeInfo.name1 = Helper.ReadDVString(reader);
            modelNodeInfo.padding = new byte[12];
            for (int x = 0; (x < 12); x++)
            {
                modelNodeInfo.padding[x] = reader.ReadByte();
            }
        }

        public override void Write(ExtendedBinaryWriter Writer, node Node)
        {
            Writer.Write(((DvModelNode)Node.info).modelNodeInfo.field_00);
            Helper.WriteDvString(Writer, ((DvModelNode)Node.info).modelNodeInfo.name1);
            foreach (var i in ((DvModelNode)Node.info).modelNodeInfo.padding)
            {
                Writer.Write(i);
            }
        }
    }
}
