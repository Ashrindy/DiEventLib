using HedgeLib.IO;

namespace DiEventLib.Nodes.NodeTypes
{
    public struct characterMotionInfo
    {
        public uint flag;
        public uint frameStart;
        public uint frameEnd;
        public uint field_0c;
        public byte[] asmStateName;
        public float field_50;
        public uint field_54;
        public uint[] field_58;
    }

    public class DvCharacterMotion : DvNodeObject
    {
        public characterMotionInfo characterMotionInfo;

        public DvCharacterMotion(node Node = null, ExtendedBinaryReader reader = null, ExtendedBinaryWriter writer = null)
        {
            if (reader != null) { Read(reader); } else if (writer != null) { Write(writer, Node); }
        }

        public override void Read(ExtendedBinaryReader reader)
        {
            characterMotionInfo.flag = reader.ReadUInt32();
            characterMotionInfo.frameStart = reader.ReadUInt32();
            characterMotionInfo.frameEnd = reader.ReadUInt32();
            reader.JumpAhead(0x04);
            characterMotionInfo.asmStateName = new byte[8];
            for (int x = 0; x < 8; x++)
            {
                characterMotionInfo.asmStateName[x] = reader.ReadByte();
            }
            characterMotionInfo.field_50 = reader.ReadUInt32();
            characterMotionInfo.field_54 = reader.ReadUInt32();
            characterMotionInfo.field_58 = new uint[4];
            for (int x = 0; x < 4; x++)
            {
                characterMotionInfo.field_58[x] = reader.ReadUInt32();
            }
        }

        public override void Write(ExtendedBinaryWriter Writer, node Node)
        {
            Writer.Write(((DvCharacterMotion)Node.info).characterMotionInfo.flag);
            Writer.Write(((DvCharacterMotion)Node.info).characterMotionInfo.frameStart);
            Writer.Write(((DvCharacterMotion)Node.info).characterMotionInfo.frameEnd);
            Writer.AddOffset("04", 0x04);
            foreach (var i in ((DvCharacterMotion)Node.info).characterMotionInfo.asmStateName)
            {
                Writer.Write(i);
            }
            Writer.Write(((DvCharacterMotion)Node.info).characterMotionInfo.field_50);
            Writer.Write(((DvCharacterMotion)Node.info).characterMotionInfo.field_54);
            foreach (var i in ((DvCharacterMotion)Node.info).characterMotionInfo.field_58)
            {
                Writer.Write(i);
            }
        }
    }
}
