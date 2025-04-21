using Amicitia.IO.Binary;
using DvSceneLib.Misc;
using System.Numerics;

namespace DvSceneLib;

[DvNodeCategory("Element")]
[DvNodeDescription("Spotlight", "")]
public class DvElementSpotlight : DvNodeElement
{
    public bool CurveEnabled = true;
    public Vector3 Position = new(0, 0, 0);
    public Vector3 FinishPosition = new(0, 0, 0);
    public Vector3 Rotation = new(0, 0, 0);
    public RGB32 LightColor = new();
    public float Range = 0;
    public float Intensity = 0;
    public float Falloff = 0;
    public float Angle = 0;
    public float[] Unk1 = new float[6];
    public float[] CurveData = new float[64];

    public DvElementSpotlight() : base(DvElementID.Spotlight) { }
    public DvElementSpotlight(BinaryObjectReader reader)
        => Read(reader);
    public void Read(BinaryObjectReader reader)
    {
        CurveEnabled = reader.Read<bool>();
        reader.Align(4);
        Position = reader.Read<Vector3>();
        FinishPosition = reader.Read<Vector3>();
        Rotation = reader.Read<Vector3>();
        LightColor = reader.Read<RGB32>();
        Range = reader.Read<float>();
        Intensity = reader.Read<float>();
        Falloff = reader.Read<float>();
        Angle = reader.Read<float>();
        Unk1 = reader.ReadArray<float>(6);
        CurveData = reader.ReadArray<float>(64);
    }

    public void Write(BinaryObjectWriter writer)
    {
        writer.Write(CurveEnabled);
        writer.Align(4);
        writer.Write(Position);
        writer.Write(FinishPosition);
        writer.Write(Rotation);
        writer.Write(LightColor);
        writer.Write(Range);
        writer.Write(Intensity);
        writer.Write(Falloff);
        writer.Write(Angle);
        writer.WriteArray(Unk1);
        writer.WriteArray(CurveData);
    }
}
