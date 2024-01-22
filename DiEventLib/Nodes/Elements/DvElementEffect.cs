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
    public struct effect
    {
        public Matrix4x4 matrix;
        public uint field_9c;
        public string filename;
        public uint[] field_dc;
        public float[] animData;
    }

    public class DvElementEffect : DvNodeObject
    {
        public effect effect;

        public DvElementEffect(node Node = null, ExtendedBinaryReader reader = null, ExtendedBinaryWriter writer = null)
        {
            if (reader != null) { Read(reader); } else if (writer != null) { Write(writer, Node); }
        }

        public override void Read(ExtendedBinaryReader reader)
        {
            effect.matrix = Helper.ReadMatrix(reader);
            effect.field_9c = reader.ReadUInt32();
            effect.filename = Helper.ReadDVString(reader);
            effect.field_dc = new uint[8];
            for (int i = 0; i < 8; i++)
            {
                effect.field_dc[i] = reader.ReadUInt32();
            }
            effect.animData = new float[128];
            for (int i = 0; i < 128; i++)
            {
                effect.animData[i] = reader.ReadSingle();
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
