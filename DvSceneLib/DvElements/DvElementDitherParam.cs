using Amicitia.IO.Binary;
using DvSceneLib.Misc;

namespace DvSceneLib;

[DvNodeCategory("Element")]
[DvNodeDescription("Dither", "Adds dither to the cutscene")]
public class DvElementDitherParam : DvNodeElement
{
    public float Alpha = 0;
    public float Intensity = 0; // not quite sure on this one
    public DvElementDitherParam() : base(DvElementID.DitherParam) { }
    public DvElementDitherParam(BinaryObjectReader reader)
        => Read(reader);
    public void Read(BinaryObjectReader reader)
    {
        Alpha = reader.Read<float>();
        Intensity = reader.Read<float>();
    }

    public void Write(BinaryObjectWriter writer)
    {
        writer.Write(Alpha);
        writer.Write(Intensity);
    }
}
