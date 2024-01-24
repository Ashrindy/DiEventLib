using Amicitia.IO.Binary;
using System.Text;

namespace DiEventLib;

public class DvElementGameCamera : DvNodeObject
{
    public float[] field_4c { get; set; }

    public DvElementGameCamera() { }
    public DvElementGameCamera(BinaryObjectReader reader)
        => Read(reader);
    public override void Read(BinaryObjectReader reader)
    {
        field_4c = reader.ReadArray<float>(26);
    }

    public override void Write(BinaryObjectWriter writer)
    {
        writer.WriteArray(field_4c);
    }
}
