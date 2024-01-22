using DiEventLib.Nodes.NodeTypes;
using HedgeLib.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiEventLib.Nodes.Elements
{
    public struct visAnim
    {
        public uint field_40;
        public string filename;
        public byte[] data1;
    }

    public class DvElementVisibilityAnimation : DvNodeObject
    {
        public visAnim visAnim;

        public DvElementVisibilityAnimation(node Node = null, ExtendedBinaryReader reader = null, ExtendedBinaryWriter writer = null)
        {
            if (reader != null) { Read(reader); } else if (writer != null) { Write(writer, Node); }
        }

        public override void Read(ExtendedBinaryReader reader)
        {
            visAnim.field_40 = reader.ReadUInt32();
            visAnim.filename = Helper.ReadDVString(reader);
            visAnim.data1 = new byte[16];
            for (int i = 0; i < 16; i++)
            {
                visAnim.data1[i] = reader.ReadByte();
            }
        }

        public override void Write(ExtendedBinaryWriter Writer, node Node)
        {
            elementProperties prop = ((DvElement)Node.info).elementInfo;
            visAnim elementVisAnim = ((DvElementVisibilityAnimation)prop.info).visAnim;
            Writer.Write(elementVisAnim.field_40);
            Helper.WriteDvString(Writer, elementVisAnim.filename);
            foreach (var i in elementVisAnim.data1)
            {
                Writer.Write(i);
            }
        }
    }
}
