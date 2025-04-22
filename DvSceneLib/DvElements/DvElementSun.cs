using Amicitia.IO.Binary;
using DvSceneLib.Misc;
using System.Numerics;

namespace DvSceneLib;

[DvNodeCategory("Element")]
[DvNodeDescription("Sun", "Modifies the sun data like rotation")]
public class DvElementSun : DvNodeElement
{
    public bool CurveEnabled = false;
    public Vector3 Rotation = new(0, 0, 0);
    public Vector3 FinishRotation = new(0, 0, 0);
    public float[] CurveData = new float[32];

    public DvElementSun() : base(DvElementID.Sun) { }
    public DvElementSun(BinaryObjectReader reader)
        => Read(reader);
    public void Read(BinaryObjectReader reader)
    {
        CurveEnabled = reader.Read<bool>();
        reader.Align(4);
        Rotation = reader.Read<Vector3>();
        FinishRotation = reader.Read<Vector3>();
        CurveData = reader.ReadArray<float>(32);
    }

    protected override void WriteElement(BinaryObjectWriter writer)
    {
        writer.Write(CurveEnabled);
        writer.Align(4);
        writer.Write(Rotation);
        writer.Write(FinishRotation);
        writer.WriteArray(CurveData);
    }
}
