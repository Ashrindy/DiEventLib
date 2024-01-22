using DiEventLib.Nodes.NodeTypes;
using HedgeLib.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiEventLib.Nodes.Elements
{
    public struct dof
    {
        public uint field_60;
        public float field_64;
        public float field_68;
        public float field_6c;
        public float far1;
        public float field_74;
        public float field_78;
        public float field_7c;
        public float far2;
        public float field_84;
        public float field_88;
        public uint field_8c;
        public uint field_90;
        public float field_94;
        public float field_98;
        public float field_9c;
        public float field_a0;
        public float field_a4;
        public float field_a8;
        public float field_ac;
        public float[] animData;
    }

    public class DvElementDOF : DvNodeObject
    {
        public dof DOF;

        public DvElementDOF(node Node = null, ExtendedBinaryReader reader = null, ExtendedBinaryWriter writer = null)
        {
            if (reader != null) { Read(reader); } else if (writer != null) { Write(writer, Node); }
        }

        public override void Read(ExtendedBinaryReader reader)
        {
            DOF.field_60 = reader.ReadUInt32();
            DOF.field_64 = reader.ReadSingle();
            DOF.field_68 = reader.ReadSingle();
            DOF.field_6c = reader.ReadSingle();
            DOF.far1 = reader.ReadSingle();
            DOF.field_74 = reader.ReadSingle();
            DOF.field_78 = reader.ReadSingle();
            DOF.field_7c = reader.ReadSingle();
            DOF.far2 = reader.ReadSingle();
            DOF.field_84 = reader.ReadSingle();
            DOF.field_88 = reader.ReadSingle();
            DOF.field_8c = reader.ReadUInt32();
            DOF.field_90 = reader.ReadUInt32();
            DOF.field_94 = reader.ReadSingle();
            DOF.field_98 = reader.ReadSingle();
            DOF.field_9c = reader.ReadSingle();
            DOF.field_a0 = reader.ReadSingle();
            DOF.field_a4 = reader.ReadSingle();
            DOF.field_a8 = reader.ReadSingle();
            DOF.field_ac = reader.ReadSingle();
            DOF.animData = new float[32];
            for (int i = 0; i < 32; i++)
            {
                DOF.animData[i] = reader.ReadSingle();
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
