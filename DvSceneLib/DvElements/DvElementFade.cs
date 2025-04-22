using Amicitia.IO.Binary;
using DvSceneLib.Misc;

namespace DvSceneLib;

[DvNodeCategory("Element")]
[DvNodeDescription("Fade", "Overlays the screen with a specific color that can be faded in and out")]
public class DvElementFade : DvNodeElement
{
    public bool Enabled = true;
    public RGB32 Color;
    public float[] CurveData = new float[32];

    public DvElementFade() : base(DvElementID.Fade) { }
    public DvElementFade(BinaryObjectReader reader) 
        => Read(reader);
    public void Read(BinaryObjectReader reader)
    {
        Enabled = reader.Read<bool>();
        reader.Align(4);
        Color = reader.Read<RGB32>();
        CurveData = reader.ReadArray<float>(32);
    }

    protected override void WriteElement(BinaryObjectWriter writer)
    {
        writer.Write(Enabled);
        writer.Align(4);
        writer.Write(Color);
        writer.WriteArray(CurveData);
    }
}
