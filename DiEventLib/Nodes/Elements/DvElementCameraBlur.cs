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
            Helper.WriteMatrix(Writer, ((DvPath)Node.info).rootPath.matrix);
            Writer.Write(((DvPath)Node.info).rootPath.flag);

            foreach (var i in ((DvPath)Node.info).rootPath.padding)
            {
                Writer.Write(i);
            }
        }
    }
}
