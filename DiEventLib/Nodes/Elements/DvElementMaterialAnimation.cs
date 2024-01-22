using DiEventLib.Nodes.NodeTypes;
using HedgeLib.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiEventLib.Nodes.Elements
{
    public struct matAnim
    {
        public uint field_00;
        public string filename;
        public uint Field44;
        public float Field48;
        public uint Field4C;
        public uint Field50;

    }

    public class DvElementMaterialAnimation : DvNodeObject
    {
        public matAnim matAnim;

        public DvElementMaterialAnimation(node Node = null, ExtendedBinaryReader reader = null, ExtendedBinaryWriter writer = null)
        {
            if (reader != null) { Read(reader); } else if (writer != null) { Write(writer, Node); }
        }

        public override void Read(ExtendedBinaryReader reader)
        {
            matAnim.field_00 = reader.ReadUInt32();
            matAnim.filename = Helper.ReadDVString(reader);
            matAnim.Field44 = reader.ReadUInt32();
            matAnim.Field48 = reader.ReadSingle();
            matAnim.Field4C = reader.ReadUInt32();
            matAnim.Field50 = reader.ReadUInt32();
        }

        public override void Write(ExtendedBinaryWriter Writer, node Node)
        {
            elementProperties prop = ((DvElement)Node.info).elementInfo;
            matAnim elementMatAnim = ((DvElementMaterialAnimation)prop.info).matAnim;
            Writer.Write(elementMatAnim.field_00);
            Helper.WriteDvString(Writer, elementMatAnim.filename);
            Writer.Write(elementMatAnim.Field44);
            Writer.Write(elementMatAnim.Field48);
            Writer.Write(elementMatAnim.Field4C);
            Writer.Write(elementMatAnim.Field50);
        }
    }
}
