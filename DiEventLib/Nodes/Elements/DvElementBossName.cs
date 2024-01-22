using DiEventLib.Nodes.NodeTypes;
using HedgeLib.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiEventLib.Nodes.Elements
{
    public enum bossID
    {
        giganto = 0,
        wyvern,
        knight,
        supreme,
        theEnd,
        supremeTheEnd
    }

    public struct bossName
    {
        public uint field_00;
        public bossID bossID;
    }

    public class DvElementBossName : DvNodeObject
    {
        public bossName bossName;

        public DvElementBossName(node Node = null, ExtendedBinaryReader reader = null, ExtendedBinaryWriter writer = null)
        {
            if (reader != null) { Read(reader); } else if (writer != null) { Write(writer, Node); }
        }

        public override void Read(ExtendedBinaryReader reader)
        {
            bossName.field_00 = reader.ReadUInt32();
            bossName.bossID = (bossID)reader.ReadUInt32();
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
