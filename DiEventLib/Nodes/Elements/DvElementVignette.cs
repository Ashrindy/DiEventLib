using DiEventLib.Nodes.NodeTypes;
using HedgeLib.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiEventLib.Nodes.Elements
{
    public struct vignette
    {
        public float[] data1;
        public int data2;
        public float[] data3;
        public int data4;
        public float[] data5;
        public float[] data6;
    }

    public class DvElementVignette : DvNodeObject
    {
        public vignette vignette;

        public DvElementVignette(node Node = null, ExtendedBinaryReader reader = null, ExtendedBinaryWriter writer = null)
        {
            if (reader != null) { Read(reader); } else if (writer != null) { Write(writer, Node); }
        }

        public override void Read(ExtendedBinaryReader reader)
        {
            vignette.data1 = new float[9];
            for (int i = 0; i < 9; i++)
            {
                vignette.data1[i] = reader.ReadSingle();
            }
            vignette.data2 = reader.ReadInt32();
            vignette.data3 = new float[24];
            for (int i = 0; i < 24; i++)
            {
                vignette.data3[i] = reader.ReadSingle();
            }
            vignette.data4 = reader.ReadInt32();
            vignette.data5 = new float[15];
            for (int i = 0; i < 15; i++)
            {
                vignette.data5[i] = reader.ReadSingle();
            }
            vignette.data6 = new float[32];
            for (int i = 0; i < 32; i++)
            {
                vignette.data6[i] = reader.ReadSingle();
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
