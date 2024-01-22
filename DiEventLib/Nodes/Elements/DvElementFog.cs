using DiEventLib.Nodes.NodeTypes;
using HedgeLib.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiEventLib.Nodes.Elements
{
    public struct fog
    {
        public byte[] data;
    }

    public class DvElementFog : DvNodeObject
    {
        public fog fog;

        public DvElementFog(node Node = null, ExtendedBinaryReader reader = null, ExtendedBinaryWriter writer = null)
        {
            if (reader != null) { Read(reader); } else if (writer != null) { Write(writer, Node); }
        }

        public override void Read(ExtendedBinaryReader reader)
        {
            fog.data = new byte[300];
            for (int i = 0; i < 300; i++)
            {
                fog.data[i] = reader.ReadByte();
            }
        }

        public override void Write(ExtendedBinaryWriter Writer, node Node)
        {
            elementProperties prop = ((DvElement)Node.info).elementInfo;
            fog elementFog = ((DvElementFog)prop.info).fog;
            foreach (var i in elementFog.data)
            {
                Writer.Write(i);
            }
        }
    }
}
