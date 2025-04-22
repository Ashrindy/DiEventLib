using Amicitia.IO.Binary;
using DvSceneLib.Misc;
using System.Numerics;

namespace DvSceneLib;

[DvNodeCategory("Element")]
[DvNodeDescription("Camera", "Offsets the camera position")]
public class DvElementCameraOffset : DvNodeElement
{
    public bool Enabled = true;
    public Vector3 OffsetPosition = new();
    public Vector3 FinishOffsetPosition = new();
    public float[] CurveData = new float[256];

    public DvElementCameraOffset() : base(DvElementID.CameraOffset) { }
    public DvElementCameraOffset(BinaryObjectReader reader)
        => Read(reader);
    public void Read(BinaryObjectReader reader)
    {
        var Flags = reader.Read<uint>();
        Enabled = (Flags & 1) == 0;
        FinishOffsetPosition = reader.Read<Vector3>();
        OffsetPosition = reader.Read<Vector3>();
        reader.Skip(20);
        CurveData = reader.ReadArray<float>(256);
    }

    protected override void WriteElement(BinaryObjectWriter writer)
    {
        var Flags = 0;
        if (!Enabled) Flags |= 1;
        writer.Write(Flags);
        writer.Write(FinishOffsetPosition);
        writer.Write(OffsetPosition);
        writer.WriteNulls(20);
        writer.WriteArray(CurveData);
    }
}
