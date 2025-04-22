using Amicitia.IO.Binary;
using DvSceneLib.Misc;
using System.Numerics;

namespace DvSceneLib;

[DvNodeCategory("Element")]
[DvNodeDescription("QTE", "Adds a simple QTE element")]
public class DvElementQTE : DvNodeElement
{
    public QTEType QTEType = QTEType.PressPrompt;
    public QTEButton QTEButton = QTEButton.A;
    public float RedCircleSize = 0;
    public float RedCircleThickness = 0;
    public float WhiteLineThickness = 0;
    public float WhiteLineSpeed = 0;
    public float Multiplier = 0;
    public float RedCircleOutlineThickness = 0;
    public float WhiteLineOutlineThickness = 0;
    public uint FailCount = 0;
    public uint MashCount = 0;
    public string ASMVariableName = "";
    public float QTEStart = 0;
    public float QTEEnd = 0;
    public float SpeedMultiplier = 0;
    public Vector2 Offset = new(0,0);
    public float Unk0 = 0;
    public byte[] Field_E0 = new byte[0xC0];
    public string SoundCueName = "";

    public DvElementQTE() : base(DvElementID.QTE) { }
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
        MashCount = reader.Read<uint>();
        ASMVariableName = reader.ReadDvString(Utils.StringEncoding.Default);
        QTEStart = reader.Read<float>();
        QTEEnd = reader.Read<float>();
        SpeedMultiplier = reader.Read<float>();
        Offset = reader.Read<Vector2>();
        Unk0 = reader.Read<float>();
        Field_E0 = reader.ReadArray<byte>(0xC0);
        SoundCueName = reader.ReadDvString(Utils.StringEncoding.Default);
    }

    protected override void WriteElement(BinaryObjectWriter writer)
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
        writer.Write(MashCount);
        writer.WriteDvString(ASMVariableName, Utils.StringEncoding.Default);
        writer.Write(QTEStart);
        writer.Write(QTEEnd);
        writer.Write(SpeedMultiplier);
        writer.Write(Offset);
        writer.Write(Unk0);
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
