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
            Helper.WriteMatrix(Writer, ((DvPath)Node.info).rootPath.matrix);
            Writer.Write(((DvPath)Node.info).rootPath.flag);

            foreach (var i in ((DvPath)Node.info).rootPath.padding)
            {
                Writer.Write(i);
            }
        }
    }
}
