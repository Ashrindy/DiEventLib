using DiEventLib.Nodes.NodeTypes;
using HedgeLib.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiEventLib.Nodes.Elements
{
    public struct time
    {
        public byte[] data;
    }

    public class DvElementTime : DvNodeObject
    {
        public time time;

        public DvElementTime(node Node = null, ExtendedBinaryReader reader = null, ExtendedBinaryWriter writer = null)
        {
            if (reader != null) { Read(reader); } else if (writer != null) { Write(writer, Node); }
        }

        public override void Read(ExtendedBinaryReader reader)
        {
            time.data = new byte[164];
            for (int i = 0; i < 164; i++)
            {
                time.data[i] = reader.ReadByte();
            }
        }

        public override void Write(ExtendedBinaryWriter Writer, node Node)
        {
            elementProperties prop = ((DvElement)Node.info).elementInfo;
            time elementTime = ((DvElementTime)prop.info).time;
            foreach (var i in elementTime.data)
            {
                Writer.Write(i);
            }
        }
    }
}
