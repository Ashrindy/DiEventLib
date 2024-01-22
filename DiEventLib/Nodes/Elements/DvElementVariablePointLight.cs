using DiEventLib.Nodes.NodeTypes;
using HedgeLib.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiEventLib.Nodes.Elements
{
    public struct variablePointLight
    {
        public float[] unk1;
        public int[] unk2;
        public float[] unk3;
        public int unk4;
        public int[] unk5;
        public float[] data1;
    }

    public class DvElementVariablePointLight : DvNodeObject
    {
        public variablePointLight pointLight;

        public DvElementVariablePointLight(node Node = null, ExtendedBinaryReader reader = null, ExtendedBinaryWriter writer = null)
        {
            if (reader != null) { Read(reader); } else if (writer != null) { Write(writer, Node); }
        }

        public override void Read(ExtendedBinaryReader reader)
        {
            pointLight.unk1 = new float[7];
            for (int i = 0; i < 7; i++)
            {
                pointLight.unk1[i] = reader.ReadUInt32();
            }
            pointLight.unk2 = new int[6];
            for (int i = 0; i < 6; i++)
            {
                pointLight.unk2[i] = reader.ReadInt32();
            }
            pointLight.unk3 = new float[8];
            for (int i = 0; i < 8; i++)
            {
                pointLight.unk3[i] = reader.ReadUInt32();
            }
            pointLight.unk4 = reader.ReadInt32();
            pointLight.unk5 = new int[10];
            for (int i = 0; i < 10; i++)
            {
                pointLight.unk5[i] = reader.ReadInt32();
            }
            pointLight.data1 = new float[128];
            for (int i = 0; i < 128; i++)
            {
                pointLight.data1[i] = reader.ReadSingle();
            }
        }

        public override void Write(ExtendedBinaryWriter Writer, node Node)
        {
            elementProperties prop = ((DvElement)Node.info).elementInfo;
            variablePointLight elementPointLight = ((DvElementVariablePointLight)prop.info).pointLight;
            foreach (var i in elementPointLight.unk1)
            {
                Writer.Write(i);
            }
            foreach (var i in elementPointLight.unk2)
            {
                Writer.Write(i);
            }
            foreach (var i in elementPointLight.unk3)
            {
                Writer.Write(i);
            }
            Writer.Write(elementPointLight.unk4);
            foreach (var i in elementPointLight.unk5)
            {
                Writer.Write(i);
            }
            foreach (var i in elementPointLight.data1)
            {
                Writer.Write(i);
            }
        }
    }
}
