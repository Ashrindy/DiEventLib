using DiEventLib.Nodes.NodeTypes;
using HedgeLib.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiEventLib.Nodes.Elements
{
    public struct camBlur
    {
        public uint flag;
        public uint blurAmount;
        public float[] curveData;
    }

    public class DvElementCameraBlur : DvNodeObject
    {
        public camBlur camBlur;

        public DvElementCameraBlur(node Node = null, ExtendedBinaryReader reader = null, ExtendedBinaryWriter writer = null)
        {
            if (reader != null) { Read(reader); } else if (writer != null) { Write(writer, Node); }
        }

        public override void Read(ExtendedBinaryReader reader)
        {
            camBlur.flag = reader.ReadUInt32();
            camBlur.blurAmount = reader.ReadUInt32();
            camBlur.curveData = new float[34];
            for (int i = 0; i < 34; i++)
            {
                camBlur.curveData[i] = reader.ReadSingle();
            }
        }

        public override void Write(ExtendedBinaryWriter Writer, node Node)
        {
            elementProperties prop = ((DvElement)Node.info).elementInfo;
            camBlur elementCamBlur = ((DvElementCameraBlur)prop.info).camBlur;
            Writer.Write(elementCamBlur.flag);
            Writer.Write(elementCamBlur.blurAmount);
            foreach (var i in elementCamBlur.curveData)
            {
                Writer.Write(i);
            }
        }
    }
}
