using DiEventLib.Nodes.NodeTypes;
using HedgeLib.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiEventLib.Nodes.Elements
{
    public struct modelClipping
    {
        public byte[] data;
    }

    public class DvElementModelClipping : DvNodeObject
    {
        public modelClipping modelClipping;

        public DvElementModelClipping(node Node = null, ExtendedBinaryReader reader = null, ExtendedBinaryWriter writer = null)
        {
            if (reader != null) { Read(reader); } else if (writer != null) { Write(writer, Node); }
        }

        public override void Read(ExtendedBinaryReader reader)
        {
            modelClipping.data = new byte[20];
            for (int i = 0; i < 20; i++)
            {
                modelClipping.data[i] = reader.ReadByte();
            }
        }

        public override void Write(ExtendedBinaryWriter Writer, node Node)
        {
            elementProperties prop = ((DvElement)Node.info).elementInfo;
            modelClipping elementModelClipping = ((DvElementModelClipping)prop.info).modelClipping;
            foreach (var i in elementModelClipping.data)
            {
                Writer.Write(i);
            }
        }
    }
}
