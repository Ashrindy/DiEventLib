using Amicitia.IO.Binary;
using System.Text;

namespace DiEventLib;

public class DvElementDither : DvNodeObject
{
    public float Alpha {  get; set; }
    public float Intensity {  get; set; } // not quite sure on this one
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
