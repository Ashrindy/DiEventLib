using HedgeLib.IO;

namespace DiEventLib.Nodes.NodeTypes
{
    public struct characterInfo
    {
        public uint field_00;
        public string name1;
        public string name2;
        public string name3;
        public byte[] unk;
    }

    public class DvCharacter : DvNodeObject
    {
        public characterInfo characterInfo;

        public DvCharacter(node Node = null, ExtendedBinaryReader reader = null, ExtendedBinaryWriter writer = null)
        {
            if (reader != null) { Read(reader); } else if (writer != null) { Write(writer, Node); }
        }

        public override void Read(ExtendedBinaryReader reader)
        {
            characterInfo.field_00 = reader.ReadUInt32();
            characterInfo.name1 = Helper.ReadDVString(reader);
            characterInfo.name2 = Helper.ReadDVString(reader);
            characterInfo.name3 = Helper.ReadDVString(reader);
            characterInfo.unk = new byte[76];
            for (int x = 0; x < 76; x++)
            {
                characterInfo.unk[x] = reader.ReadByte();
            }
        }

        public override void Write(ExtendedBinaryWriter Writer, node Node)
        {
            Writer.Write(((DvCharacter)Node.info).characterInfo.field_00);
            Helper.WriteDvString(Writer, ((DvCharacter)Node.info).characterInfo.name1);
            Helper.WriteDvString(Writer, ((DvCharacter)Node.info).characterInfo.name2);
            Helper.WriteDvString(Writer, ((DvCharacter)Node.info).characterInfo.name3);
            foreach (var i in ((DvCharacter)Node.info).characterInfo.unk)
            {
                Writer.Write(i);
            }
        }
    }
}
