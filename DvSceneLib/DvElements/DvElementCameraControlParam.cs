using Amicitia.IO.Binary;
using DvSceneLib.Misc;

namespace DvSceneLib;

[DvNodeCategory("Element")]
[DvNodeDescription("Camera Control", "Edits the camera parameters")]
public class DvElementCameraControlParam : DvNodeElement
{
    public float ExposureValue = 0;
    public float[] CurveData = new float[32];

    public DvElementCameraControlParam() : base(DvElementID.CameraControlParam) { }
    public DvElementCameraControlParam(BinaryObjectReader reader)
        => Read(reader);
    public void Read(BinaryObjectReader reader)
    {
        reader.Skip(4);
        ExposureValue = reader.Read<float>();
        reader.Skip(24);
        CurveData = reader.ReadArray<float>(32);
    }

    protected override void WriteElement(BinaryObjectWriter writer)
    {
        writer.WriteNulls(4);
        writer.Write(ExposureValue);
        writer.WriteNulls(24);
        writer.WriteArray(CurveData);
    }
}
