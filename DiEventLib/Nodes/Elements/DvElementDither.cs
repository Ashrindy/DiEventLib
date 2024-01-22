using DiEventLib.Nodes.NodeTypes;
using HedgeLib.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiEventLib.Nodes.Elements
{
    public struct ditherParam
    {
        public float param1;
        public float param2;
    }

    public class DvElementDither : DvNodeObject
    {
        public ditherParam dither;

        public DvElementDither(node Node = null, ExtendedBinaryReader reader = null, ExtendedBinaryWriter writer = null)
        {
            if (reader != null) { Read(reader); } else if (writer != null) { Write(writer, Node); }
        }

        public override void Read(ExtendedBinaryReader reader)
        {
            dither.param1 = reader.ReadSingle();
            dither.param2 = reader.ReadSingle();
        }

        public override void Write(ExtendedBinaryWriter Writer, node Node)
        {
            elementProperties prop = ((DvElement)Node.info).elementInfo;
            ditherParam elementDither = ((DvElementDither)prop.info).dither;
            Writer.Write(elementDither.param1);
            Writer.Write(elementDither.param2);
        }
    }
}
