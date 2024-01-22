using DiEventLib.Nodes.NodeTypes;
using HedgeLib.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiEventLib.Nodes.Elements
{
    public struct weather
    {
        public uint[] field_40;
    }

    public class DvElementWeather : DvNodeObject
    {
        public weather weather;

        public DvElementWeather(node Node = null, ExtendedBinaryReader reader = null, ExtendedBinaryWriter writer = null)
        {
            if (reader != null) { Read(reader); } else if (writer != null) { Write(writer, Node); }
        }

        public override void Read(ExtendedBinaryReader reader)
        {
            weather.field_40 = new uint[33];
            for (int i = 0; i < 33; i++)
            {
                weather.field_40[i] = reader.ReadUInt32();
            }
        }

        public override void Write(ExtendedBinaryWriter Writer, node Node)
        {
            elementProperties prop = ((DvElement)Node.info).elementInfo;
            weather elementWeather = ((DvElementWeather)prop.info).weather;
            foreach (var i in elementWeather.field_40)
            {
                Writer.Write(i);
            }
        }
    }
}
