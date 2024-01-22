using DiEventLib.Nodes.NodeTypes;
using HedgeLib.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace DiEventLib.Nodes.Elements
{
    public struct pathAdjustment
    {
        public Matrix4x4 matrix;
        public uint[] field_40;
    }

    public class DvElementPathAdjustment : DvNodeObject
    {
        public pathAdjustment pathAdjustment;

        public DvElementPathAdjustment(node Node = null, ExtendedBinaryReader reader = null, ExtendedBinaryWriter writer = null)
        {
            if (reader != null) { Read(reader); } else if (writer != null) { Write(writer, Node); }
        }

        public override void Read(ExtendedBinaryReader reader)
        {
            pathAdjustment.matrix = Helper.ReadMatrix(reader);
            pathAdjustment.field_40 = new uint[4];
            for (int i = 0; i < 4; i++)
            {
                pathAdjustment.field_40[i] = reader.ReadUInt32();
            }
        }

        public override void Write(ExtendedBinaryWriter Writer, node Node)
        {
            elementProperties prop = ((DvElement)Node.info).elementInfo;
            Helper.WriteMatrix(Writer, ((DvElementPathAdjustment)prop.info).pathAdjustment.matrix);
            foreach (var i in ((DvElementPathAdjustment)prop.info).pathAdjustment.field_40)
            {
                Writer.Write(i);
            }
        }
    }
}
