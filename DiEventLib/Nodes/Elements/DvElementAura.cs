using DiEventLib.Nodes.NodeTypes;
using HedgeLib.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiEventLib.Nodes.Elements
{
    public struct aura
    {
        public byte[] data;
    }

    public class DvElementAura : DvNodeObject
    {
        public aura aura;

        public DvElementAura(node Node = null, ExtendedBinaryReader reader = null, ExtendedBinaryWriter writer = null)
        {
            if (reader != null) { Read(reader); } else if (writer != null) { Write(writer, Node); }
        }

        public override void Read(ExtendedBinaryReader reader)
        {
            aura.data = new byte[204];
            for (int i = 0; i < 204; i++)
            {
                aura.data[i] = reader.ReadByte();
            }
        }

        public override void Write(ExtendedBinaryWriter Writer, node Node)
        {
            elementProperties prop = ((DvElement)Node.info).elementInfo;
            aura elementAura = ((DvElementAura)prop.info).aura;
            foreach (var i in elementAura.data)
            {
                Writer.Write(i);
            }
        }
    }
}
