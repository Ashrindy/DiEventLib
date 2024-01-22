using DiEventLib.Nodes.NodeTypes;
using HedgeLib.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiEventLib.Nodes.Elements
{
    public enum animType
    {
        skeletalAnim = 1,
        UVAnim,
        matAnim = 4
    }

    public struct anim
    {
        public animType animType;
        public string filename;
    }

    public struct compAnim
    {
        public uint field_60;
        public byte[] data;
        public uint field_6c;
        public anim[] animations;
        public uint field_03;
    }

    public class DvElementComplexAnimation : DvNodeObject
    {
        public compAnim compAnim;

        public DvElementComplexAnimation(node Node = null, ExtendedBinaryReader reader = null, ExtendedBinaryWriter writer = null)
        {
            if (reader != null) { Read(reader); } else if (writer != null) { Write(writer, Node); }
        }

        public override void Read(ExtendedBinaryReader reader)
        {
            compAnim.field_60 = reader.ReadUInt32();
            compAnim.data = new byte[8];
            for (int i = 0; i < 8; i++)
            {
                compAnim.data[i] = reader.ReadByte();
            }
            compAnim.field_6c = reader.ReadUInt32();
            compAnim.animations = new anim[16];
            for (int i = 0; i < 16; i++)
            {
                compAnim.animations[i] = new anim();
                compAnim.animations[i].animType = (animType)reader.ReadUInt32();
                compAnim.animations[i].filename = Helper.ReadDVString(reader);
            }
            compAnim.field_03 = reader.ReadUInt32();
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
