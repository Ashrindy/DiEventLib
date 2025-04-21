using Amicitia.IO.Binary;
using DvSceneLib.Misc;

namespace DvSceneLib;

[DvNodeCategory("Element")]
[DvNodeDescription("Aura", "Changes the aura nodes for stuff like Super Sonic")]
public class DvElementAura : DvNodeElement
{
    public AuraNode AuraBefore = new();
    public AuraNode AuraAfter = new();
    public uint Field_00 = 0;
    public float[] AnimData;

    public DvElementAura() : base(DvElementID.Aura)
    { 
        AnimData = new float[32];
        for(int i = 0; i < 32; i++)
        {
            AnimData[i] = 1;
        }
    }
    public DvElementAura(BinaryObjectReader reader)
        => Read(reader);
    public void Read(BinaryObjectReader reader)
    {
        AuraBefore = reader.Read<AuraNode>();
        AuraAfter = reader.Read<AuraNode>();
        Field_00 = reader.Read<uint>();
        AnimData = reader.ReadArray<float>(32);
    }

    public void Write(BinaryObjectWriter writer)
    {
        writer.Write(AuraBefore);
        writer.Write(AuraAfter);
        writer.Write(Field_00);
        writer.WriteArray(AnimData);
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