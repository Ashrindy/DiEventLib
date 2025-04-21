using Amicitia.IO.Binary;
using DvSceneLib.Misc;

namespace DvSceneLib;

[DvNodeCategory("Element")]
[DvNodeDescription("Camera Blur", "Makes the camera view blurry")]
public class DvElementCameraBlurParam : DvNodeElement
{
    public uint Flags = 0;
    public uint Field_04 = 0;
    public float BlurAmount = 0;
    public uint Field_0C = 0;
    public float[] CurveData;

    public DvElementCameraBlurParam() : base(DvElementID.CameraBlurParam)
    {
        CurveData = new float[32];
        for (int i = 0; i < 32; i++)
        {
            CurveData[i] = 1;
        }
    }
    public DvElementCameraBlurParam(BinaryObjectReader reader)
        => Read(reader);
    public void Read(BinaryObjectReader reader)
    {
        Flags = reader.Read<uint>();
        Field_04 = reader.Read<uint>();
        BlurAmount = reader.Read<float>();
        Field_0C = reader.Read<uint>();
        CurveData = reader.ReadArray<float>(32);
    }

    public void Write(BinaryObjectWriter writer)
    {
        writer.Write(Flags);
        writer.Write(Field_04);
        writer.Write(BlurAmount);
        writer.Write(Field_0C);
        writer.WriteArray(CurveData);
    }
}
