using DiEventLib.Nodes.NodeTypes;
using HedgeLib.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiEventLib.Nodes.Elements
{
    public struct chromaticAberration
    {
        public float[] data;
        public float[] data1;
    }

    public class DvElementChromaticAberration : DvNodeObject
    {
        public chromaticAberration chromatic;

        public DvElementChromaticAberration(node Node = null, ExtendedBinaryReader reader = null, ExtendedBinaryWriter writer = null)
        {
            if (reader != null) { Read(reader); } else if (writer != null) { Write(writer, Node); }
        }

        public override void Read(ExtendedBinaryReader reader)
        {
            chromatic.data = new float[17];
            for (int i = 0; i < 17; i++)
            {
                chromatic.data[i] = reader.ReadSingle();
            }
            chromatic.data1 = new float[32];
            for (int i = 0; i < 32; i++)
            {
                chromatic.data1[i] = reader.ReadSingle();
            }
        }

        public override void Write(ExtendedBinaryWriter Writer, node Node)
        {
            elementProperties prop = ((DvElement)Node.info).elementInfo;
            chromaticAberration elementChromatic = ((DvElementChromaticAberration)prop.info).chromatic;
            foreach (var i in elementChromatic.data)
            {
                Writer.Write(i);
            }
            foreach (var i in elementChromatic.data1)
            {
                Writer.Write(i);
            }
        }
    }
}
