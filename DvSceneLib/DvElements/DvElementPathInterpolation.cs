using Amicitia.IO.Binary;
using DvSceneLib.Misc;
using System.Numerics;

namespace DvSceneLib;

[DvNodeCategory("Element")]
[DvNodeDescription("Path Interpolation", "Interpolates path movement")]
public class DvElementPathInterpolation : DvNodeElement
{
    public struct Interpolation
    {
        public Vector3 Position;
        public Vector3 Rotation;
        public Vector3 Scale;
    }

    public bool UseAbsolutePosition = false;
    public Interpolation PointA = new();
    public Interpolation PointB = new();
    public float Unk0 = 0;
    public float[] CurveData = new float[128];

    public DvElementPathInterpolation() : base(DvElementID.PathInterpolation) { }
    public DvElementPathInterpolation(BinaryObjectReader reader)
        => Read(reader);
    public void Read(BinaryObjectReader reader)
    {
        UseAbsolutePosition = reader.Read<bool>();
        reader.Align(4);
        PointA = reader.Read<Interpolation>();
        PointB = reader.Read<Interpolation>();
        Unk0 = reader.Read<float>();
        CurveData = reader.ReadArray<float>(128);
    }

    protected override void WriteElement(BinaryObjectWriter writer)
    {
        writer.Write(UseAbsolutePosition);
        writer.Align(4);
        writer.Write(PointA);
        writer.Write(PointB);
        writer.Write(Unk0);
        writer.WriteArray(CurveData);
    }
}
