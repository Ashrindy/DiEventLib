using DiEventLib.Nodes.NodeTypes;
using HedgeLib.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiEventLib.Nodes.Elements
{
    public struct sound
    {
        public string cueName;
        public uint field_a0;
        public uint field_a4;
    }

    public class DvElementSound : DvNodeObject
    {
        public sound sound;

        public DvElementSound(node Node = null, ExtendedBinaryReader reader = null, ExtendedBinaryWriter writer = null)
        {
            if (reader != null) { Read(reader); } else if (writer != null) { Write(writer, Node); }
        }

        public override void Read(ExtendedBinaryReader reader)
        {
            sound.cueName = Helper.ReadDVString(reader);
            sound.field_a0 = reader.ReadUInt32();
            sound.field_a4 = reader.ReadUInt32();
        }

        public override void Write(ExtendedBinaryWriter Writer, node Node)
        {
            elementProperties prop = ((DvElement)Node.info).elementInfo;
            sound elementSound = ((DvElementSound)prop.info).sound;
            Helper.WriteDvString(Writer, elementSound.cueName);
            Writer.Write(elementSound.field_a0);
            Writer.Write(elementSound.field_a4);
        }
    }
}
