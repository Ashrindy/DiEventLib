using Amicitia.IO.Binary;

namespace DiEventLib;

public class DvElementDither : DvNodeObject
{
    public float Alpha { get; set; } = 0;
    public float Intensity { get; set; } = 0; // not quite sure on this one
    public DvElementDither() { }
    public DvElementDither(BinaryObjectReader reader)
        => Read(reader);
    public override void Read(BinaryObjectReader reader)
    {
        Alpha = reader.Read<float>();
        Intensity = reader.Read<float>();
    }

    public override void Write(BinaryObjectWriter writer)
    {
        writer.Write(Alpha);
        writer.Write(Intensity);
    }
}
