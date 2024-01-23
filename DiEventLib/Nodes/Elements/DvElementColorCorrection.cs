using DiEventLib.Nodes.NodeTypes;
using HedgeLib.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiEventLib.Nodes.Elements
{
    public struct colorCorrection
    {
        public uint field_00;
        public float field_04;
        public float field_08;
        public float field_0c;
        public float field_1c;
        public uint field_01;
        public float field_2c;
        public uint field_02;
        public float[] curveData;
    }

    public class DvElementColorCorrection : DvNodeObject
    {
        public colorCorrection colorCorrection;

        public DvElementColorCorrection(node Node = null, ExtendedBinaryReader reader = null, ExtendedBinaryWriter writer = null)
        {
            if (reader != null) { Read(reader); } else if (writer != null) { Write(writer, Node); }
        }

        public override void Read(ExtendedBinaryReader reader)
        {
            colorCorrection.field_00 = reader.ReadUInt32();
            colorCorrection.field_04 = reader.ReadSingle();
            colorCorrection.field_08 = reader.ReadSingle();
            colorCorrection.field_0c = reader.ReadSingle();
            colorCorrection.field_1c = reader.ReadSingle();
            colorCorrection.field_01 = reader.ReadUInt32();
            colorCorrection.field_2c = reader.ReadSingle();
            colorCorrection.field_02 = reader.ReadUInt32();
            colorCorrection.curveData = new float[32];
            for (int i = 0; i < 32; i++)
            {
                colorCorrection.curveData[i] = reader.ReadSingle();
            }
        }

        public override void Write(ExtendedBinaryWriter Writer, node Node)
        {
            elementProperties prop = ((DvElement)Node.info).elementInfo;
            colorCorrection elementColorCorrection = ((DvElementColorCorrection)prop.info).colorCorrection;
            Writer.Write(elementColorCorrection.field_00);
            Writer.Write(elementColorCorrection.field_04);
            Writer.Write(elementColorCorrection.field_08);
            Writer.Write(elementColorCorrection.field_0c);
            Writer.Write(elementColorCorrection.field_1c);
            Writer.Write(elementColorCorrection.field_01);
            Writer.Write(elementColorCorrection.field_2c);
            Writer.Write(elementColorCorrection.field_02);
            foreach(var i in elementColorCorrection.curveData)
            {
                Writer.Write(i);
            }
        }
    }
}
