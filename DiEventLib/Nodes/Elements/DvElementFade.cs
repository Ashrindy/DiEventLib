using DiEventLib.Nodes.NodeTypes;
using HedgeLib.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiEventLib.Nodes.Elements
{
    public struct RGBA32
    {
        public uint A;
        public uint B;
        public uint G;
        public uint R;
    }

    public struct fade
    {
        public RGBA32 color;
        public float[] curveData;
    }

    public class DvElementFade : DvNodeObject
    {
        public fade fade;

        public DvElementFade(node Node = null, ExtendedBinaryReader reader = null, ExtendedBinaryWriter writer = null)
        {
            if (reader != null) { Read(reader); } else if (writer != null) { Write(writer, Node); }
        }

        public override void Read(ExtendedBinaryReader reader)
        {
            fade.color.A = reader.ReadUInt32();
            fade.color.B = reader.ReadUInt32();
            fade.color.G = reader.ReadUInt32();
            fade.color.R = reader.ReadUInt32();
            fade.curveData = new float[32];
            for (int i = 0; i < 32; i++)
            {
                fade.curveData[i] = reader.ReadSingle();
            }
        }

        public override void Write(ExtendedBinaryWriter Writer, node Node)
        {
            elementProperties prop = ((DvElement)Node.info).elementInfo;
            fade elementFade = ((DvElementFade)prop.info).fade;
            Writer.Write(elementFade.color.A);
            Writer.Write(elementFade.color.B);
            Writer.Write(elementFade.color.G);
            Writer.Write(elementFade.color.R);
            foreach (var i in elementFade.curveData)
            {
                Writer.Write(i);
            }
        }
    }
}
