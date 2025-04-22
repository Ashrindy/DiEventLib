using Amicitia.IO.Binary;
using DvSceneLib.Misc;

namespace DvSceneLib;

[DvNodeCategory("Element")]
[DvNodeDescription("Color Contrast", "Changes the colors of the cutscene")]
public class DvElementColorContrast : DvNodeElement
{
    public bool Enabled = true;
    public bool CurveEnabled = false;
    public bool UseHLSCorrection = false;
    public float Contrast = 0;
    public float DynamicRange = 0;
    public float CrushShadows = 0;
    public float CrushHighlights = 0;
    public float HLSHueOffset = 0;
    public float HLSLightnessOffset = 0;
    public float HLSSaturationOffset = 0;
    public float[] CurveData = new float[32];
    public DvElementColorContrast() : base(DvElementID.ColorContrast) { }
    public DvElementColorContrast(BinaryObjectReader reader)
        => Read(reader);
    public void Read(BinaryObjectReader reader)
    {
        var Flags = reader.Read<uint>();
        Enabled = (Flags & 1) != 0;
        CurveEnabled = (Flags & 2) != 0;
        UseHLSCorrection = (Flags & 4) != 0;
        Contrast = reader.Read<float>();
        DynamicRange = reader.Read<float>();
        CrushShadows = reader.Read<float>();
        CrushHighlights = reader.Read<float>();
        HLSHueOffset = reader.Read<float>();
        HLSLightnessOffset = reader.Read<float>();
        HLSSaturationOffset = reader.Read<float>();
        CurveData = reader.ReadArray<float>(32);
    }

    protected override void WriteElement(BinaryObjectWriter writer)
    {
        var Flags = 0;
        if (Enabled) Flags |= 1;
        if (CurveEnabled) Flags |= 2;
        if (UseHLSCorrection) Flags |= 4;
        writer.Write(Flags);
        writer.Write(Contrast);
        writer.Write(DynamicRange);
        writer.Write(CrushShadows);
        writer.Write(CrushHighlights);
        writer.Write(HLSHueOffset);
        writer.Write(HLSLightnessOffset);
        writer.Write(HLSSaturationOffset);
        writer.WriteArray(CurveData);
    }
}
