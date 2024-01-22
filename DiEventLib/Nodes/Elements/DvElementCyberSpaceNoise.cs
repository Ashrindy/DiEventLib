using DiEventLib.Nodes.NodeTypes;
using HedgeLib.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiEventLib.Nodes.Elements
{
    public struct cyberSpaceNoise
    {
        public uint field_4f;
        public float[] data;
    }

    public class DvElementCyberSpaceNoise : DvNodeObject
    {
        public cyberSpaceNoise cyberSpaceNoise;

        public DvElementCyberSpaceNoise(node Node = null, ExtendedBinaryReader reader = null, ExtendedBinaryWriter writer = null)
        {
            if (reader != null) { Read(reader); } else if (writer != null) { Write(writer, Node); }
        }

        public override void Read(ExtendedBinaryReader reader)
        {
            cyberSpaceNoise.field_4f = reader.ReadUInt32();
            cyberSpaceNoise.data = new float[32];
            for (int i = 0; i < 32; i++)
            {
                cyberSpaceNoise.data[i] = reader.ReadUInt32();
            }
        }

        public override void Write(ExtendedBinaryWriter Writer, node Node)
        {
            elementProperties prop = ((DvElement)Node.info).elementInfo;
            cyberSpaceNoise elementCyberSpaceNoise = ((DvElementCyberSpaceNoise)prop.info).cyberSpaceNoise;
            Writer.Write(elementCyberSpaceNoise.field_4f);
            foreach (var i in elementCyberSpaceNoise.data)
            {
                Writer.Write(i);
            }
        }
    }
}
