using Amicitia.IO.Binary;
using DvSceneLib.Misc;

namespace DvSceneLib;

[DvNodeCategory("Element")]
[DvNodeDescription("Camera Blur", "Makes the camera view blurry")]
public class DvElementCameraBlurParam : DvNodeElement
{
    public bool Enabled = true;
    public bool SingleDirectionOpt = false;
    public bool CurveEnabled = false;
    public uint SampleAmount = 0;
    public float BlurAmount = 0;
    public float FinishBlurAmount = 0;
    public float[] CurveData = new float[32];

    public DvElementCameraBlurParam() : base(DvElementID.CameraBlurParam) { }
    public DvElementCameraBlurParam(BinaryObjectReader reader)
        => Read(reader);
    public void Read(BinaryObjectReader reader)
    {
        var Flags = reader.Read<uint>();
        Enabled = (Flags & 1) != 0;
        SingleDirectionOpt = (Flags & 2) != 0;
        CurveEnabled = (Flags & 4) != 0;
        SampleAmount = reader.Read<uint>();
        BlurAmount = reader.Read<float>();
        FinishBlurAmount = reader.Read<float>();
        CurveData = reader.ReadArray<float>(32);
    }

    protected override void WriteElement(BinaryObjectWriter writer)
    {
        var Flags = 0;
        if (Enabled) Flags |= 1;
        if (SingleDirectionOpt) Flags |= 2;
        if (CurveEnabled) Flags |= 4;
        writer.Write(Flags);
        writer.Write(SampleAmount);
        writer.Write(BlurAmount);
        writer.Write(FinishBlurAmount);
        writer.WriteArray(CurveData);
    }
}
