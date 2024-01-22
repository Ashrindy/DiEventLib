using DiEventLib.Nodes.NodeTypes;
using HedgeLib.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiEventLib.Nodes.Elements
{
    public struct sun
    {
        public uint field_00;
        public float unkFloat;
        public uint[] field_01;
        public uint[] animData;
    }

    public class DvElementSun : DvNodeObject
    {
        public sun sun;

        public DvElementSun(node Node = null, ExtendedBinaryReader reader = null, ExtendedBinaryWriter writer = null)
        {
            if (reader != null) { Read(reader); } else if (writer != null) { Write(writer, Node); }
        }

        public override void Read(ExtendedBinaryReader reader)
        {
            sun.field_00 = reader.ReadUInt32();
            sun.unkFloat = reader.ReadSingle();
            sun.field_01 = new uint[5];
            for (int i = 0; i < 5; i++)
            {
                sun.field_01[i] = reader.ReadUInt32();
            }
            sun.animData = new uint[32];
            for (int i = 0; i < 32; i++)
            {
                sun.animData[i] = reader.ReadUInt32();
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
