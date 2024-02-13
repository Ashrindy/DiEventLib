using Amicitia.IO.Binary;
using System.Numerics;

namespace DiEventLib;

public class DvElementVignette : DvNodeObject
{
    public uint Field_00 { get; set; }
    public uint Field_04 { get; set; }
    public VignetteParam1 Data1 { get; set; }
    public VignetteParam2 Data2 { get; set; }
    public float[] CurveData { get; set; }

    public DvElementVignette() { }
    public DvElementVignette(BinaryObjectReader reader)
        => Read(reader);
    public override void Read(BinaryObjectReader reader)
    {
        Field_00 = reader.Read<uint>();
        Field_04 = reader.Read<uint>();
        Data1 = reader.Read<VignetteParam1>();
        Data2 = reader.Read<VignetteParam2>();
        CurveData = reader.ReadArray<float>(32);
    }

    public override void Write(BinaryObjectWriter writer)
    {
        writer.Write(Field_00);
        writer.Write(Field_04);
        writer.Write(Data1);
        writer.Write(Data2);
        writer.WriteArray(CurveData);
    }
}

public struct VignetteParam1
{
    public Vector2 Position { get; set; }
    public Vector2 Scale { get; set; }
    public float Size { get; set; }
    public float Rotation { get; set; }
    public float Field_18 { get; set; }
    public uint Alpha { get; set; }
    public float Field_1c { get; set; }
    public float Unk1 { get; set; }
    public float Unk2 { get; set; }
    public Vector2 Center { get; set; }
    public Vector2 Direction { get; set; }
    public float PenumbraScale { get; set; }
    public float MinPenumbraScale { get; set; }
    public float MaxPenumbraScale { get; set; }
    public float BokehScale { get; set; }
    public float MinDOFOpacityScale { get; set; }
    public float MaxDOFOpacityScale { get; set; }
    public float MinOpacityScale { get; set; }
    public float MaxOpacityScale { get; set; }
    public float MinOpacityDist { get; set; }
    public float MaxOpacityDist { get; set; }
}

public struct VignetteParam2
{
    public Vector2 Position { get; set; }
    public Vector2 Scale { get; set; }
    public float Size { get; set; }
    public float Rotation { get; set; }
    public float Field_18 { get; set; }
    public uint Alpha { get; set; }
    public float Field_1c { get; set; }
    public float Unk1 { get; set; }
    public float Unk2 { get; set; }
    public float Unk3 { get; set; }
    public float PenumbraScale { get; set; }
    public float Unk4 { get; set; }
    public float MinPenumbraScale { get; set; }
    public float MaxPenumbraScale { get; set; }
    public float BokehScale { get; set; }
    public float MinDOFOpacityScale { get; set; }
    public float MaxDOFOpacityScale { get; set; }
    public float MinOpacityScale { get; set; }
    public float MaxOpacityScale { get; set; }
    public float MinOpacityDist { get; set; }
    public float MaxOpacityDist { get; set; }
}
