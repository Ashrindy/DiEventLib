using HedgeLib.IO;

namespace DiEventLib.Nodes.NodeTypes
{
    public struct cameraMotionInfo
    {
        public uint flag;
        public uint frameStart;
        public uint frameEnd;
        public byte[] padding;
    }

    public class DvCameraMotion : DvNodeObject
    {
        public cameraMotionInfo cameraMotionInfo;
        public DvCameraMotion(node Node = null, ExtendedBinaryReader reader = null, ExtendedBinaryWriter writer = null)
        {
            if (reader != null) { Read(reader); } else if (writer != null) { Write(writer, Node); }
        }
        public override void Read(ExtendedBinaryReader reader)
        {
            cameraMotionInfo.flag = reader.ReadUInt32();
            cameraMotionInfo.frameStart = reader.ReadUInt32();
            cameraMotionInfo.frameEnd = reader.ReadUInt32();
            reader.JumpAhead(0x04);
        }

        public override void Write(ExtendedBinaryWriter Writer, node Node)
        {
            Writer.Write(((DvCameraMotion)Node.info).cameraMotionInfo.flag);
            Writer.Write(((DvCameraMotion)Node.info).cameraMotionInfo.frameStart);
            Writer.Write(((DvCameraMotion)Node.info).cameraMotionInfo.frameEnd);
            Writer.AddOffset("03", 0x04);
        }
    }
}
