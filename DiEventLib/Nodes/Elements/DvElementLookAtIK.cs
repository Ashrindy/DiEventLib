using DiEventLib.Nodes.NodeTypes;
using HedgeLib.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiEventLib.Nodes.Elements
{
    public struct lookAtIK
    {
        public uint field_60;
        public uint field_64;
        public Guid guid;
        public uint[] field_78;
        public float[] field_80;
    }

    public class DvElementLookAtIK : DvNodeObject
    {
        public lookAtIK lookAtIK;

        public DvElementLookAtIK(node Node = null, ExtendedBinaryReader reader = null, ExtendedBinaryWriter writer = null)
        {
            if (reader != null) { Read(reader); } else if (writer != null) { Write(writer, Node); }
        }

        public override void Read(ExtendedBinaryReader reader)
        {
            lookAtIK.field_60 = reader.ReadUInt32();
            lookAtIK.field_64 = reader.ReadUInt32();
            lookAtIK.guid = Helper.ReadGUID(reader);
            lookAtIK.field_78 = new uint[11];
            for (int i = 0; i < 11; i++)
            {
                lookAtIK.field_78[i] = reader.ReadUInt32();
            }
            lookAtIK.field_80 = new float[64];
            for (int i = 0; i < 64; i++)
            {
                lookAtIK.field_80[i] = reader.ReadSingle();
            }
        }

        public override void Write(ExtendedBinaryWriter Writer, node Node)
        {
            elementProperties prop = ((DvElement)Node.info).elementInfo;
            lookAtIK elementLookAtIK = ((DvElementLookAtIK)prop.info).lookAtIK;
            Writer.Write(elementLookAtIK.field_60);
            Writer.Write(elementLookAtIK.field_64);
            Helper.WriteGUID(Writer, elementLookAtIK.guid);
            foreach (var i in elementLookAtIK.field_78)
            {
                Writer.Write(i);
            }
            foreach (var i in elementLookAtIK.field_80)
            {
                Writer.Write(i);
            }
        }
    }
}
