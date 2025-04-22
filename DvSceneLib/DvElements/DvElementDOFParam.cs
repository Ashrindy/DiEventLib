using Amicitia.IO.Binary;
using DvSceneLib.Misc;

namespace DvSceneLib;

[DvNodeCategory("Element")]
[DvNodeDescription("Depth of Field", "Adds depth of field")]
public class DvElementDOFParam : DvNodeElement
{
    public struct DOFParam
    {
        public float ForegroundMaxDepth;
        public float ForegroundStartDepth;
        public float BackgroundMaxDepth;
        public float BackgroundStartDepth;
    }

    public bool UseFocusLookAt = false;
    public bool EnableCircleDOF = false;
    public bool DrawFocalPlane = false;
    public bool CurveEnabled = false;
    public DOFParam Params = new DOFParam();
    public DOFParam FinishParams = new DOFParam();
    public float COCMaxRadius = 0;
    public float FocalTransition = 0;
    public int BokehSampleCount = 10;
    public int BokehQuality = 1;
    public float BokehIntensity = 1;
    public float RenderTargetScale = 0;
    public float[] CurveData = new float[32];

    public DvElementDOFParam() : base(DvElementID.DOFParam) { }
    public DvElementDOFParam(BinaryObjectReader reader)
        => Read(reader);
    public void Read(BinaryObjectReader reader)
    {
        var Flags = reader.Read<uint>();
        UseFocusLookAt = (Flags & 1) != 0;
        EnableCircleDOF = (Flags & 2) != 0;
        DrawFocalPlane = (Flags & 4) != 0;
        CurveEnabled = (Flags & 8) != 0;
        Params = reader.Read<DOFParam>();
        FinishParams = reader.Read<DOFParam>();
        COCMaxRadius = reader.Read<float>();
        FocalTransition = reader.Read<float>();
        BokehSampleCount = reader.Read<int>();
        BokehQuality = reader.Read<int>();
        BokehIntensity = reader.Read<float>();
        RenderTargetScale = reader.Read<float>();
        reader.Skip(20);
        CurveData = reader.ReadArray<float>(32);
    }

    protected override void WriteElement(BinaryObjectWriter writer)
    {
        var Flags = 0;
        if (UseFocusLookAt) Flags |= 1;
        if (EnableCircleDOF) Flags |= 2;
        if (DrawFocalPlane) Flags |= 4;
        if (CurveEnabled) Flags |= 8;
        writer.Write(Flags);
        writer.Write(Params);
        writer.Write(FinishParams);
        writer.Write(COCMaxRadius);
        writer.Write(FocalTransition);
        writer.Write(BokehSampleCount);
        writer.Write(BokehQuality);
        writer.Write(BokehIntensity);
        writer.Write(RenderTargetScale);
        writer.WriteNulls(20);
        writer.WriteArray(CurveData);
    }
}
