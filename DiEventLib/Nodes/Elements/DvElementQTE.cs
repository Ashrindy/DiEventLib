using DiEventLib.Nodes.NodeTypes;
using HedgeLib.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiEventLib.Nodes.Elements
{
    public enum QTEType
    {
        pressPrompt = 0,
        mash,
        redCircle,
        theEndVariant,
        unknown
    }

    public enum QTEButton
    {
        a = 0,
        b,
        x,
        y,
        lb_rb,
        lb,
        rb,
        mashA,
        mashB,
        mashX,
        mashY,
        mashLB,
        mashRB,
        unknown1,
        unknown2,
        unknown3
    }

    public struct QTE
    {
        public QTEType qteType;
        public QTEButton qteButton;
        public float redCircleSize;
        public float redCircleThickness;
        public float whiteLineThickness;
        public float whiteLineSpeed;
        public float multiplier;
        public float redCircleOutlineThickness;
        public float whiteLineOutlineThickness;
        public uint failCount;
        public uint field_88;
        public byte[] field_8c;
        public float field_cc;
        public float field_d0;
        public float field_d4;
        public byte[] field_d8;
    }

    public class DvElementQTE : DvNodeObject
    {
        public QTE QTE;

        public DvElementQTE(node Node = null, ExtendedBinaryReader reader = null, ExtendedBinaryWriter writer = null)
        {
            if (reader != null) { Read(reader); } else if (writer != null) { Write(writer, Node); }
        }

        public override void Read(ExtendedBinaryReader reader)
        {
            QTE.qteType = (QTEType)reader.ReadUInt32();
            QTE.qteButton = (QTEButton)reader.ReadUInt32();
            QTE.redCircleSize = reader.ReadSingle();
            QTE.redCircleThickness = reader.ReadSingle();
            QTE.whiteLineThickness = reader.ReadSingle();
            QTE.whiteLineSpeed = reader.ReadSingle();
            QTE.multiplier = reader.ReadSingle();
            QTE.redCircleOutlineThickness = reader.ReadSingle();
            QTE.whiteLineOutlineThickness = reader.ReadSingle();
            QTE.failCount = reader.ReadUInt32();
            QTE.field_88 = reader.ReadUInt32();
            QTE.field_8c = new byte[64];
            for (int i = 0; i < 64; i++)
            {
                QTE.field_8c[i] = reader.ReadByte();
            }
            QTE.field_cc = reader.ReadSingle();
            QTE.field_d0 = reader.ReadSingle();
            QTE.field_d4 = reader.ReadSingle();
            QTE.field_d8 = new byte[264];
            for (int i = 0; i < 264; i++)
            {
                QTE.field_d8[i] = reader.ReadByte();
            }
        }

        public override void Write(ExtendedBinaryWriter Writer, node Node)
        {
            elementProperties prop = ((DvElement)Node.info).elementInfo;
            QTE elementQTE = ((DvElementQTE)prop.info).QTE;
            Writer.Write((uint)elementQTE.qteType);
            Writer.Write((uint)elementQTE.qteButton);
            Writer.Write(elementQTE.redCircleSize);
            Writer.Write(elementQTE.redCircleThickness);
            Writer.Write(elementQTE.whiteLineThickness);
            Writer.Write(elementQTE.whiteLineSpeed);
            Writer.Write(elementQTE.multiplier);
            Writer.Write(elementQTE.redCircleOutlineThickness);
            Writer.Write(elementQTE.whiteLineOutlineThickness);
            Writer.Write(elementQTE.failCount);
            Writer.Write(elementQTE.field_88);
            foreach (var i in elementQTE.field_8c)
            {
                Writer.Write(i);
            }
            Writer.Write(elementQTE.field_cc);
            Writer.Write(elementQTE.field_d0);
            Writer.Write(elementQTE.field_d4);
            foreach (var i in elementQTE.field_d8)
            {
                Writer.Write(i);
            }
        }
    }
}
