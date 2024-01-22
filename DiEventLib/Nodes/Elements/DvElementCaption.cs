using DiEventLib.Nodes.NodeTypes;
using HedgeLib.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiEventLib.Nodes.Elements
{
    public enum languageType
    {
        english = 0,
        french,
        italian,
        german,
        spanish,
        polish,
        portuguese,
        russian,
        japanese,
        chinese,
        chinese_simplified,
        korean
    }

    public struct caption
    {
        public byte[] captionName;
        public languageType languageType;
        public uint padding;
    }

    public class DvElementCaption : DvNodeObject
    {
        public caption caption;

        public DvElementCaption(node Node = null, ExtendedBinaryReader reader = null, ExtendedBinaryWriter writer = null)
        {
            if (reader != null) { Read(reader); } else if (writer != null) { Write(writer, Node); }
        }

        public override void Read(ExtendedBinaryReader reader)
        {
            caption.captionName = new byte[16];
            for (int i = 0; i < 16; i++)
            {
                caption.captionName[i] = reader.ReadByte();
            }
            caption.languageType = (languageType)reader.ReadUInt32();
            caption.padding = reader.ReadUInt32();
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
