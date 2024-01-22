using DiEventLib.Nodes.NodeTypes;
using HedgeLib.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiEventLib.Nodes.Elements
{
    public struct drawOff
    {
        public uint[] field_00;
    }

    public class DvElementDrawOff : DvNodeObject
    {
        public drawOff drawOff;

        public DvElementDrawOff(node Node = null, ExtendedBinaryReader reader = null, ExtendedBinaryWriter writer = null)
        {
            if (reader != null) { Read(reader); } else if (writer != null) { Write(writer, Node); }
        }

        public override void Read(ExtendedBinaryReader reader)
        {
            drawOff.field_00 = new uint[4];
            for (int i = 0; i < 4; i++)
            {
                drawOff.field_00[i] = reader.ReadUInt32();
            }
        }

        public override void Write(ExtendedBinaryWriter Writer, node Node)
        {
            elementProperties prop = ((DvElement)Node.info).elementInfo;
            foreach (var i in ((DvElementDrawOff)prop.info).drawOff.field_00)
            {
                Writer.Write(i);
            }
        }
    }
}
