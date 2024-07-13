using Amicitia.IO.Binary;
using System.Numerics;

namespace DiEventLib;

public class DvElementVignette : DvNodeElement
{
    public uint Field_00 { get; set; } = 0;
    public uint Field_04 { get; set; } = 0;
    public VignetteParam1 VignetteBefore { get; set; }
    public VignetteParam2 VignetteAfter { get; set; }
    public float[] CurveData { get; set; }

    public DvElementVignette() 
    {
        VignetteBefore = new VignetteParam1 
        { 
            Position = new(0,0),
            Scale = new(0,0),
            Size = 0,
            Rotation = 0,
            Field_18 = 0,
            Alpha = 0,
            Field_1c = 0,
            Unk1 = 0,
            Unk2 = 0,
            Center = new(0,0),
            Direction = new(0,0),
            PenumbraScale = 0,
            MinPenumbraScale = 0,
            MaxPenumbraScale = 0,
            BokehScale = 0,
            MinDOFOpacityScale = 0,
            MaxDOFOpacityScale = 0,
            MinOpacityScale = 0,
            MaxOpacityScale = 0,
            MinOpacityDist = 0,
            MaxOpacityDist = 0,
        };
        VignetteAfter = new VignetteParam2
        {
            Position = new(0, 0),
            Scale = new(0, 0),
            Size = 0,
            Rotation = 0,
            Field_18 = 0,
            Alpha = 0,
            Field_1c = 0,
            Unk1 = 0,
            Unk2 = 0,
            Unk3 = 0,
            PenumbraScale = 0,
            Unk4 = 0,
            MinPenumbraScale = 0,
            MaxPenumbraScale = 0,
            BokehScale = 0,
            MinDOFOpacityScale = 0,
            MaxDOFOpacityScale = 0,
            MinOpacityScale = 0,
            MaxOpacityScale = 0,
            MinOpacityDist = 0,
            MaxOpacityDist = 0,
        };
        CurveData = new float[32];
        for (int i = 0; i < 32; i++)
        {
            CurveData[i] = 1;
        }
    }
    public DvElementVignette(BinaryObjectReader reader)
        => Read(reader);
    public void Read(BinaryObjectReader reader)
    {
        Field_00 = reader.Read<uint>();
        Field_04 = reader.Read<uint>();
        VignetteBefore = reader.Read<VignetteParam1>();
        VignetteAfter = reader.Read<VignetteParam2>();
        CurveData = reader.ReadArray<float>(32);
    }

    public void Write(BinaryObjectWriter writer)
    {
        writer.Write(Field_00);
        writer.Write(Field_04);
        writer.Write(VignetteBefore);
        writer.Write(VignetteAfter);
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
