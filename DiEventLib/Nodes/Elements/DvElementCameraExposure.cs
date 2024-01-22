using DiEventLib.Nodes.NodeTypes;
using HedgeLib.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiEventLib.Nodes.Elements
{
    public struct camExposure
    {
        public int unk1;
        public float[] field_48;
        public float[] field_80;
    }

    public class DvElementCameraExposure : DvNodeObject
    {
        public camExposure camExposure;

        public DvElementCameraExposure(node Node = null, ExtendedBinaryReader reader = null, ExtendedBinaryWriter writer = null)
        {
            if (reader != null) { Read(reader); } else if (writer != null) { Write(writer, Node); }
        }

        public override void Read(ExtendedBinaryReader reader)
        {
            camExposure.unk1 = reader.ReadInt32();
            camExposure.field_48 = new float[7];
            for (int i = 0; i < 7; i++)
            {
                camExposure.field_48[i] = reader.ReadSingle();
            }
            camExposure.field_80 = new float[32];
            for (int i = 0; i < 32; i++)
            {
                camExposure.field_80[i] = reader.ReadSingle();
            }
        }

        public override void Write(ExtendedBinaryWriter Writer, node Node)
        {
            elementProperties prop = ((DvElement)Node.info).elementInfo;
            camExposure elementCamExposure = ((DvElementCameraExposure)prop.info).camExposure;
            Writer.Write(elementCamExposure.unk1);
            elementCamExposure.field_48 = new float[7];
            foreach (var i in elementCamExposure.field_48)
            {
                Writer.Write(i);
            }
            foreach (var i in elementCamExposure.field_80)
            {
                Writer.Write(i);
            }
        }
    }
}
