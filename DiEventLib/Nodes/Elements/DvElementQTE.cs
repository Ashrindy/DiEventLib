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
}
