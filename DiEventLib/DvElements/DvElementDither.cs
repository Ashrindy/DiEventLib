using Amicitia.IO.Binary;
using DiEventLib.Misc;

namespace DiEventLib;

[DvNodeCategory("Element")]
[DvNodeDescription("Dither", "Adds dither to the cutscene")]
public class DvElementDither : DvNodeElement
{
    public float Alpha = 0;
    public float Intensity = 0; // not quite sure on this one
    public DvElementDither() : base(DvElementID.Dither) { }
    public DvElementDither(BinaryObjectReader reader)
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
