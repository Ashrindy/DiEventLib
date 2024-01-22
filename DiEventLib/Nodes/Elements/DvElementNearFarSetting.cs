using DiEventLib.Nodes.NodeTypes;
using HedgeLib.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiEventLib.Nodes.Elements
{
    public struct nearFarSetting
    {
        public uint field_00;
        public float near;
        public float far;
        public uint[] field_10;
    }

    public class DvElementNearFarSetting : DvNodeObject
    {
        public nearFarSetting nearFarSetting;

        public DvElementNearFarSetting(node Node = null, ExtendedBinaryReader reader = null, ExtendedBinaryWriter writer = null)
        {
            if (reader != null) { Read(reader); } else if (writer != null) { Write(writer, Node); }
        }

        public override void Read(ExtendedBinaryReader reader)
        {
            nearFarSetting.field_00 = reader.ReadUInt32();
            nearFarSetting.near = reader.ReadSingle();
            nearFarSetting.far = reader.ReadSingle();
            nearFarSetting.field_10 = new uint[5];
            for (int i = 0; i < 5; i++)
            {
                nearFarSetting.field_10[i] = reader.ReadUInt32();
            }
        }

        public override void Write(ExtendedBinaryWriter Writer, node Node)
        {
            elementProperties prop = ((DvElement)Node.info).elementInfo;
            nearFarSetting elementNearFarSetting = ((DvElementNearFarSetting)prop.info).nearFarSetting;
            Writer.Write(elementNearFarSetting.field_00);
            Writer.Write(elementNearFarSetting.near);
            Writer.Write(elementNearFarSetting.far);
            foreach (var i in elementNearFarSetting.field_10)
            {
                Writer.Write(i);
            }
        }
    }
}
