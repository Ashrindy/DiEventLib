using Amicitia.IO.Binary;
using DvSceneLib.Misc;
using System.Numerics;

namespace DvSceneLib;

[DvNodeCategory("Element")]
[DvNodeDescription("Vignette", "Adds a smooth black border around the screen")]
public class DvElementVignetteParam : DvNodeElement
{
    public uint Field_00 = 0;
    public uint Field_04 = 0;
    public VignetteParam1 VignetteBefore;
    public VignetteParam2 VignetteAfter;
    public float[] CurveData;

    public DvElementVignetteParam() : base(DvElementID.VignetteParam)
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
    public DvElementVignetteParam(BinaryObjectReader reader)
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
    public Vector2 Position;
    public Vector2 Scale;
    public float Size;
    public float Rotation;
    public float Field_18;
    public uint Alpha;
    public float Field_1c;
    public float Unk1;
    public float Unk2;
    public Vector2 Center;
    public Vector2 Direction;
    public float PenumbraScale;
    public float MinPenumbraScale;
    public float MaxPenumbraScale;
    public float BokehScale;
    public float MinDOFOpacityScale;
    public float MaxDOFOpacityScale;
    public float MinOpacityScale;
    public float MaxOpacityScale;
    public float MinOpacityDist;
    public float MaxOpacityDist;
}

public struct VignetteParam2
{
    public Vector2 Position;
    public Vector2 Scale;
    public float Size;
    public float Rotation;
    public float Field_18;
    public uint Alpha;
    public float Field_1c;
    public float Unk1;
    public float Unk2;
    public float Unk3;
    public float PenumbraScale;
    public float Unk4;
    public float MinPenumbraScale;
    public float MaxPenumbraScale;
    public float BokehScale;
    public float MinDOFOpacityScale;
    public float MaxDOFOpacityScale;
    public float MinOpacityScale;
    public float MaxOpacityScale;
    public float MinOpacityDist;
    public float MaxOpacityDist;
}
