using Amicitia.IO.Binary;

namespace DiEventLib;

public class DvElementQTE : DvNodeObject
{
    public QTEType QTEType { get; set; } = QTEType.PressPrompt;
    public QTEButton QTEButton { get; set; } = QTEButton.A;
    public float RedCircleSize { get; set; } = 0;
    public float RedCircleThickness { get; set; } = 0;
    public float WhiteLineThickness { get; set; } = 0;
    public float WhiteLineSpeed { get; set; } = 0;
    public float Multiplier { get; set; } = 0;
    public float RedCircleOutlineThickness { get; set; } = 0;
    public float WhiteLineOutlineThickness { get; set; } = 0;
    public uint FailCount { get; set; } = 0;
    public uint Field_88 { get; set; } = 0;
    public byte[] Field_8c { get; set; }
    public float Field_cc { get; set; } = 0;
    public float Field_d0 { get; set; } = 0;
    public float Field_d4 { get; set; } = 0;
    public float Field_d8 { get; set; } = 0;
    public float Field_e0 { get; set; } = 0;
    public string unkStr { get; set; } = "";
    public DvElementQTE() 
    {
        Field_8c = new byte[64];
        for(int i = 0; i < 64; i++)
        {
            Field_8c[i] = 0;
        }
    }
    public DvElementQTE(BinaryObjectReader reader)
        => Read(reader);
    public override void Read(BinaryObjectReader reader)
    {
        QTEType = reader.Read<QTEType>();
        QTEButton = reader.Read<QTEButton>();
        RedCircleSize = reader.Read<float>();
        RedCircleThickness = reader.Read<float>();
        WhiteLineThickness = reader.Read<float>();
        WhiteLineSpeed = reader.Read<float>();
        Multiplier = reader.Read<float>();
        RedCircleOutlineThickness = reader.Read<float>();
        WhiteLineOutlineThickness = reader.Read<float>();
        FailCount = reader.Read<uint>();
        Field_88 = reader.Read<uint>();
        Field_8c = reader.ReadArray<byte>(64);
        Field_cc = reader.Read<float>();
        Field_d0 = reader.Read<float>();
        Field_d4 = reader.Read<float>();
        Field_d8 = reader.Read<float>();
        Field_e0 = reader.Read<float>();
        unkStr = reader.ReadString(StringBinaryFormat.FixedLength, 256);
    }

    public override void Write(BinaryObjectWriter writer)
    {
        writer.Write(QTEType);
        writer.Write(QTEButton);
        writer.Write(RedCircleSize);
        writer.Write(RedCircleThickness);
        writer.Write(WhiteLineThickness);
        writer.Write(WhiteLineSpeed);
        writer.Write(Multiplier);
        writer.Write(RedCircleOutlineThickness);
        writer.Write(WhiteLineOutlineThickness);
        writer.Write(FailCount);
        writer.Write(Field_88);
        writer.WriteArray(Field_8c);
        writer.Write(Field_cc);
        writer.Write(Field_d0);
        writer.Write(Field_d4);
        writer.Write(Field_d8);
        writer.Write(Field_e0);
        writer.WriteString(StringBinaryFormat.FixedLength, unkStr, 256);
    }
}

public enum QTEType : uint
{
    PressPrompt = 0,
    Mash,
    RedCircle,
    TheEndVariant,
    Unknown
}

public enum QTEButton : uint
{
    A = 0,
    B,
    X,
    Y,
    LB_RB,
    LB,
    RB,
    MashA,
    MashB,
    MashX,
    MashY,
    MashLB,
    MashRB,
    Unknown1,
    Unknown2,
    Unknown3
}
