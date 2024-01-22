using DiEventLib.Nodes.NodeTypes;
using HedgeLib.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiEventLib.Nodes.Elements
{
    public struct shadowRes
    {
        public uint shadowRes1;
        public uint shadowRes2;
    }

    public class DvElementShadowResolution : DvNodeObject
    {
        public shadowRes shadowRes;

        public DvElementShadowResolution(node Node = null, ExtendedBinaryReader reader = null, ExtendedBinaryWriter writer = null)
        {
            if (reader != null) { Read(reader); } else if (writer != null) { Write(writer, Node); }
        }

        public override void Read(ExtendedBinaryReader reader)
        {
            shadowRes.shadowRes1 = reader.ReadUInt32();
            shadowRes.shadowRes2 = reader.ReadUInt32();
        }

        public override void Write(ExtendedBinaryWriter Writer, node Node)
        {
            elementProperties prop = ((DvElement)Node.info).elementInfo;
            shadowRes elementShadowRes = ((DvElementShadowResolution)prop.info).shadowRes;
            Writer.Write(elementShadowRes.shadowRes1);
            Writer.Write(elementShadowRes.shadowRes2);
        }
    }
}
