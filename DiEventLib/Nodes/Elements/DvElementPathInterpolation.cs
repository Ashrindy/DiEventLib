using DiEventLib.Nodes.NodeTypes;
using HedgeLib.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiEventLib.Nodes.Elements
{
    public struct pathInterpolation
    {
        public byte[] data;
    }

    public class DvElementPathInterpolation : DvNodeObject
    {
        public pathInterpolation pathInterpolation;

        public DvElementPathInterpolation(node Node = null, ExtendedBinaryReader reader = null, ExtendedBinaryWriter writer = null)
        {
            if (reader != null) { Read(reader); } else if (writer != null) { Write(writer, Node); }
        }

        public override void Read(ExtendedBinaryReader reader)
        {
            pathInterpolation.data = new byte[592];
            for (int i = 0; i < 592; i++)
            {
                pathInterpolation.data[i] = reader.ReadByte();
            }
        }

        public override void Write(ExtendedBinaryWriter Writer, node Node)
        {
            elementProperties prop = ((DvElement)Node.info).elementInfo;
            pathInterpolation elementPathInterpolation = ((DvElementPathInterpolation)prop.info).pathInterpolation;
            foreach (var i in elementPathInterpolation.data)
            {
                Writer.Write(i);
            }
        }
    }
}
