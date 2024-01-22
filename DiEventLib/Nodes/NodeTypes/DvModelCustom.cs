using HedgeLib.IO;

namespace DiEventLib.Nodes.NodeTypes
{
    public struct modelCustomInfo
    {
        public uint field_00;
        public string name1;
        public string name2;
        public string name3;
        public byte[] unk;
    }

    public class DvModelCustom : DvNodeObject
    {
        public modelCustomInfo modelCustomInfo;

        public DvModelCustom(node Node = null, ExtendedBinaryReader reader = null, ExtendedBinaryWriter writer = null)
        {
            if (reader != null) { Read(reader); } else if (writer != null) { Write(writer, Node); }
        }

        public override void Read(ExtendedBinaryReader reader)
        {
            modelCustomInfo.field_00 = reader.ReadUInt32();
            modelCustomInfo.name1 = Helper.ReadDVString(reader);
            modelCustomInfo.name2 = Helper.ReadDVString(reader);
            modelCustomInfo.name3 = Helper.ReadDVString(reader);
            modelCustomInfo.unk = new byte[76];
            for (int x = 0; x < 76; x++)
            {
                modelCustomInfo.unk[x] = reader.ReadByte();
            }
        }

        public override void Write(ExtendedBinaryWriter Writer, node Node)
        {
            Writer.Write(((DvModelCustom)Node.info).modelCustomInfo.field_00);
            Helper.WriteDvString(Writer, ((DvModelCustom)Node.info).modelCustomInfo.name1);
            Helper.WriteDvString(Writer, ((DvModelCustom)Node.info).modelCustomInfo.name2);
            Helper.WriteDvString(Writer, ((DvModelCustom)Node.info).modelCustomInfo.name3);
            foreach (var i in ((DvModelCustom)Node.info).modelCustomInfo.unk)
            {
                Writer.Write(i);
            }
        }
    }
}
