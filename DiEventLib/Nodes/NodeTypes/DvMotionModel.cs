using HedgeLib.IO;

namespace DiEventLib.Nodes.NodeTypes
{
    public struct motionModelInfo
    {
        public uint field_00;
        public uint frameStart;
        public uint frameEnd;
        public uint field_0c;
        public byte[] asmStateName;
        public float field_50;
        public uint field_54;
        public uint[] field_58;
    }

    public class DvMotionModel : DvNodeObject
    {
        public motionModelInfo motionModelInfo;

        public DvMotionModel(node Node = null, ExtendedBinaryReader reader = null, ExtendedBinaryWriter writer = null)
        {
            if (reader != null) { Read(reader); } else if (writer != null) { Write(writer, Node); }
        }

        public override void Read(ExtendedBinaryReader reader)
        {
            reader.JumpAhead(0x04);
            motionModelInfo.frameStart = reader.ReadUInt32();
            motionModelInfo.frameEnd = reader.ReadUInt32();
            reader.JumpAhead(0x04);
            motionModelInfo.asmStateName = new byte[8];
            for (int x = 0; x < 8; x++)
            {
                motionModelInfo.asmStateName[x] = reader.ReadByte();
            }
            motionModelInfo.field_50 = reader.ReadUInt32();
            motionModelInfo.field_54 = reader.ReadUInt32();
            motionModelInfo.field_58 = new uint[4];
            for (int x = 0; x < 4; x++)
            {
                motionModelInfo.field_58[x] = reader.ReadUInt32();
            }
        }

        public override void Write(ExtendedBinaryWriter Writer, node Node)
        {
            Writer.AddOffset("05", 0x04);
            Writer.Write(((DvMotionModel)Node.info).motionModelInfo.frameStart);
            Writer.Write(((DvMotionModel)Node.info).motionModelInfo.frameEnd);
            Writer.AddOffset("06", 0x04);
            foreach (var i in ((DvMotionModel)Node.info).motionModelInfo.asmStateName)
            {
                Writer.Write(i);
            }
            Writer.Write(((DvMotionModel)Node.info).motionModelInfo.field_50);
            Writer.Write(((DvMotionModel)Node.info).motionModelInfo.field_54);
            foreach (var i in ((DvMotionModel)Node.info).motionModelInfo.field_58)
            {
                Writer.Write(i);
            }
        }
    }
}
