using Amicitia.IO.Binary;
using DvSceneLib.Misc;

namespace DvSceneLib;

[DvNodeCategory("Element")]
[DvNodeDescription("Aura", "Changes the aura nodes for stuff like Super Sonic")]
public class DvElementAura : DvNodeElement
{
    public AuraNode AuraBefore = new();
    public AuraNode AuraAfter = new();
    public bool CurveEnabled = false;
    public bool Enabled = true;
    public float[] CurveData = new float[32];

    public DvElementAura() : base(DvElementID.Aura) { }
    public DvElementAura(BinaryObjectReader reader)
        => Read(reader);
    public void Read(BinaryObjectReader reader)
    {
        AuraBefore = reader.Read<AuraNode>();
        AuraAfter = reader.Read<AuraNode>();
        var Flags = reader.Read<uint>();
        CurveEnabled = (Flags & 1) != 0;
        Enabled = (Flags & 2) != 0;
        CurveData = reader.ReadArray<float>(32);
    }

    protected override void WriteElement(BinaryObjectWriter writer)
    {
        writer.Write(AuraBefore);
        writer.Write(AuraAfter);
        var Flags = 0;
        if (CurveEnabled) Flags |= 1;
        if (Enabled) Flags |= 2;
        writer.Write(Flags);
        writer.WriteArray(CurveData);
    }
}

public struct AuraNode // Variable names are going off by appgfx.rfl
{
    public RGBA32 Color;
    public float Distance;
    public float NoiseTextureScrollSpeed;
    public float BlurScale;
    public float ColorGain;
    public float NoiseGain;
}