using Amicitia.IO.Binary;
using DvSceneLib.Misc;

namespace DvSceneLib;

[DvNodeCategory("Element")]
[DvNodeDescription("Depth of Field", "Adds depth of field")]
public class DvElementQTE : DvNodeElement
{
    public QTEType QTEType { get; set; }
    public QTEButton QTEButton { get; set; }
    public float RedCircleSize { get; set; }
    public float RedCircleThickness { get; set; }
    public float WhiteLineThickness { get; set; }
    public float WhiteLineSpeed { get; set; }
    public float Multiplier { get; set; }
    public float RedCircleOutlineThickness { get; set; }
    public float WhiteLineOutlineThickness { get; set; }
    public uint FailCount { get; set; }
    public uint Field_88 { get; set; }
    public byte[] Field_8c { get; set; }
    public float Field_cc { get; set; }
    public float Field_d0 { get; set; }
    public float Field_d4 { get; set; }
    public uint Field_d8 { get; set; }
    public float Field_dc { get; set; }
    public byte[] Field_E0 { get; set; }
    public string SoundCueName { get; set; }
    public DvElementQTE() : base(DvElementID.QTE)
    {
        Field_8c = new byte[64];
        for(int i = 0; i < 64; i++)
        {
            Field_8c[i] = 0;
        }
        Field_E0 = new byte[0xC0];
        for (int i = 0; i < 0xC0; i++)
        {
            Field_E0[i] = 0;
        }
    }
    public DvElementQTE(BinaryObjectReader reader)
        => Read(reader);
    public void Read(BinaryObjectReader reader)
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
        Field_d8 = reader.Read<uint>();
        Field_dc = reader.Read<float>();
        Field_E0 = reader.ReadArray<byte>(0xC0);
        SoundCueName = reader.ReadDvString(Utils.StringEncoding.Default);
    }

    public void Write(BinaryObjectWriter writer)
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
        writer.Write(Field_dc);
        writer.WriteArray(Field_E0);
        writer.WriteDvString(SoundCueName, Utils.StringEncoding.Default);
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
