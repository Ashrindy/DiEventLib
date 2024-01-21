using HedgeLib.IO;

namespace DiEventLib.Nodes.NodeTypes
{
    public struct cameraInfo
    {
        public uint flag;
        public uint frameProgressionCount;
        public uint captionCount;
        public byte[] padding;
        public float[] frameProgression;
        public float[] frameProgressionSpeed;
    }

    public class DvCamera : DvNodeObject
    {
        public cameraInfo cameraInfo;

        public DvCamera(node Node = null, ExtendedBinaryReader reader = null, ExtendedBinaryWriter writer = null)
        {
            if (reader != null) { Read(reader); } else if (writer != null) { Write(writer, Node); }
        }

        public override void Read(ExtendedBinaryReader reader)
        {
            cameraInfo.flag = reader.ReadUInt32();
            cameraInfo.frameProgressionCount = reader.ReadUInt32();
            cameraInfo.captionCount = reader.ReadUInt32();
            reader.JumpAhead(0x04);
            cameraInfo.frameProgression = new float[cameraInfo.frameProgressionCount];
            cameraInfo.frameProgressionSpeed = new float[cameraInfo.frameProgressionCount];
            for (int i = 0; i < cameraInfo.frameProgressionCount; i++)
            {
                cameraInfo.frameProgression[i] = reader.ReadSingle();
            }
            for (int i = 0; i < cameraInfo.frameProgressionCount; i++)
            {
                cameraInfo.frameProgressionSpeed[i] = reader.ReadSingle();
            }
        }

        public override void Write(ExtendedBinaryWriter Writer, node Node)
        {
            Writer.Write(((DvCamera)Node.info).cameraInfo.flag);
            Writer.Write(((DvCamera)Node.info).cameraInfo.frameProgressionCount);
            Writer.Write(((DvCamera)Node.info).cameraInfo.captionCount);
            Writer.AddOffset("02", 0x04);
            foreach (var i in ((DvCamera)Node.info).cameraInfo.frameProgression)
            {
                Writer.Write(i);
            }
            foreach (var i in ((DvCamera)Node.info).cameraInfo.frameProgressionSpeed)
            {
                Writer.Write(i);
            }
        }
    }
}
