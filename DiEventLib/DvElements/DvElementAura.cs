using Amicitia.IO.Binary;

namespace DiEventLib;

public class DvElementAura : DvNodeElement
{
    public AuraNode AuraBefore { get; set; } = new();
    public AuraNode AuraAfter { get; set; } = new();
    public uint Field_00 { get; set; } = 0;
    public float[] AnimData { get; set; }

    public DvElementAura() 
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
    public RGBA32 Color { get; set; }
    public float Distance { get; set; }
    public float NoiseTextureScrollSpeed { get; set; }
    public float BlurScale { get; set; }
    public float ColorGain { get; set; }
    public float NoiseGain { get; set; }
}