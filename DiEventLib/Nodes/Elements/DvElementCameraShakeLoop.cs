using DiEventLib.Nodes.NodeTypes;
using HedgeLib.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiEventLib.Nodes.Elements
{
    public struct cameraShakeLoop
    {
        public uint field_60;
        public uint field_64;
        public float[] field_68;
        public float[] field_curveData;
    }

    public class DvElementCameraShakeLoop : DvNodeObject
    {
        public cameraShakeLoop camShakeLoop;

        public DvElementCameraShakeLoop(node Node = null, ExtendedBinaryReader reader = null, ExtendedBinaryWriter writer = null)
        {
            if (reader != null) { Read(reader); } else if (writer != null) { Write(writer, Node); }
        }

        public override void Read(ExtendedBinaryReader reader)
        {
            camShakeLoop.field_60 = reader.ReadUInt32();
            camShakeLoop.field_64 = reader.ReadUInt32();
            camShakeLoop.field_68 = new float[6];
            for (int i = 0; i < 6; i++)
            {
                camShakeLoop.field_68[i] = reader.ReadSingle();
            }
            camShakeLoop.field_curveData = new float[64];
            for (int i = 0; i < 64; i++)
            {
                camShakeLoop.field_curveData[i] = reader.ReadSingle();
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
