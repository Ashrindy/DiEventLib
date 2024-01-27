using Amicitia.IO.Binary;
using System.Text;

namespace DiEventLib;

public class DvElementAura : DvNodeObject
{
    public AuraNode AuraNode1 { get; set; }
    public AuraNode AuraNode2 { get; set; }
    public uint Field_28 { get; set; }
    public uint Field_2c { get; set; }
    public float[] Unk { get; set; }

    public DvElementAura() { }
    public DvElementAura(BinaryObjectReader reader)
        => Read(reader);
    public override void Read(BinaryObjectReader reader)
    {
        AuraNode1 = reader.Read<AuraNode>();
        AuraNode2 = reader.Read<AuraNode>();
        Field_28 = reader.Read<uint>();
        Field_2c = reader.Read<uint>();
        Unk = reader.ReadArray<float>(31);
    }

    public override void Write(BinaryObjectWriter writer)
    {
        writer.Write(AuraNode1);
        writer.Write(AuraNode2);
        writer.Write(Field_28);
        writer.Write(Field_2c);
        writer.WriteArray(Unk);
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