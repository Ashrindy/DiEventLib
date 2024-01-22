using DiEventLib.Nodes.NodeTypes;
using HedgeLib.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiEventLib.Nodes.Elements
{
    public struct auraRoad
    {
        public uint field_00;
        public float[] animData;
    }

    public class DvElementAuraRoad : DvNodeObject
    {
        public auraRoad auraRoad;

        public DvElementAuraRoad(node Node = null, ExtendedBinaryReader reader = null, ExtendedBinaryWriter writer = null)
        {
            if (reader != null) { Read(reader); } else if (writer != null) { Write(writer, Node); }
        }

        public override void Read(ExtendedBinaryReader reader)
        {
            auraRoad.field_00 = reader.ReadUInt32();
            auraRoad.animData = new float[64];
            for (int i = 0; i < 64; i++)
            {
                auraRoad.animData[i] = reader.ReadUInt32();
            }
        }

        public override void Write(ExtendedBinaryWriter Writer, node Node)
        {
            elementProperties prop = ((DvElement)Node.info).elementInfo;
            auraRoad elementAuraRoad = ((DvElementAuraRoad)prop.info).auraRoad;
            Writer.Write(elementAuraRoad.field_00);
            foreach (var i in elementAuraRoad.animData)
            {
                Writer.Write(i);
            }
        }
    }
}
