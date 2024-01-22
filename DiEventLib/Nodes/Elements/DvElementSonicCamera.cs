using DiEventLib.Nodes.NodeTypes;
using HedgeLib.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiEventLib.Nodes.Elements
{
    public struct sonicCam
    {
        public float[] field_4c;
    }

    public class DvElementSonicCamera : DvNodeObject
    {
        public sonicCam sonicCam;

        public DvElementSonicCamera(node Node = null, ExtendedBinaryReader reader = null, ExtendedBinaryWriter writer = null)
        {
            if (reader != null) { Read(reader); } else if (writer != null) { Write(writer, Node); }
        }

        public override void Read(ExtendedBinaryReader reader)
        {
            sonicCam.field_4c = new float[80];
            for (int i = 0; i < 80; i++)
            {
                sonicCam.field_4c[i] = reader.ReadSingle();
            }
        }

        public override void Write(ExtendedBinaryWriter Writer, node Node)
        {
            elementProperties prop = ((DvElement)Node.info).elementInfo;
            sonicCam elementSonicCam = ((DvElementSonicCamera)prop.info).sonicCam;
            foreach (var i in elementSonicCam.field_4c)
            {
                Writer.Write(i);
            }
        }
    }
}
