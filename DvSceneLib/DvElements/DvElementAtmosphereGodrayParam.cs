using Amicitia.IO.Binary;
using DvSceneLib.Misc;

namespace DvSceneLib;

[DvNodeCategory("Element")]
[DvNodeDescription("Atmospheric Godray", "Modifies the atmospheric godray")]
public class DvElementAtmosphereGodrayParam : DvNodeElement
{
    public bool Enabled = true;
    public float Density = 0;
    public float Decay = 1;
    public float Weight = 1;

    public DvElementAtmosphereGodrayParam() : base(DvElementID.AtmosphereGodrayParam) { }
    public DvElementAtmosphereGodrayParam(BinaryObjectReader reader)
        => Read(reader);
    public void Read(BinaryObjectReader reader)
    {
        Enabled = reader.Read<bool>();
        reader.Align(4);
        Density = reader.Read<float>();
        Decay = reader.Read<float>();
        Weight = reader.Read<float>();
    }

    protected override void WriteElement(BinaryObjectWriter writer)
    {
        writer.Write(Enabled);
        writer.Align(4);
        writer.Write(Density);
        writer.Write(Decay);
        writer.Write(Weight);
    }
}
