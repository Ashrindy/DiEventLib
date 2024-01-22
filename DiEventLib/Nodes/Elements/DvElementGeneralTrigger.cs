using DiEventLib.Nodes.NodeTypes;
using HedgeLib.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiEventLib.Nodes.Elements
{
    public struct generalTrigger
    {
        public uint field_00;
        public string triggerName;
    }

    public class DvElementGeneralTrigger : DvNodeObject
    {
        public generalTrigger generalTrigger;

        public DvElementGeneralTrigger(node Node = null, ExtendedBinaryReader reader = null, ExtendedBinaryWriter writer = null)
        {
            if (reader != null) { Read(reader); } else if (writer != null) { Write(writer, Node); }
        }

        public override void Read(ExtendedBinaryReader reader)
        {
            generalTrigger.field_00 = reader.ReadUInt32();
            generalTrigger.triggerName = Helper.ReadDVString(reader);
        }

        public override void Write(ExtendedBinaryWriter Writer, node Node)
        {
            elementProperties prop = ((DvElement)Node.info).elementInfo;
            generalTrigger elementGeneralTrigger = ((DvElementGeneralTrigger)prop.info).generalTrigger;
            Writer.Write(elementGeneralTrigger.field_00);
            Helper.WriteDvString(Writer, elementGeneralTrigger.triggerName);
        }
    }
}
