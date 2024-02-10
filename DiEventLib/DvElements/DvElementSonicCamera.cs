using Amicitia.IO.Binary;

namespace DiEventLib;

public class DvElementSonicCamera : DvNodeObject
{
    public float[] Field_4c { get; set; }

    public DvElementSonicCamera() { }
    public DvElementSonicCamera(BinaryObjectReader reader)
        => Read(reader);
    public override void Read(BinaryObjectReader reader)
    {
        Field_4c = reader.ReadArray<float>(80);
    }

    public override void Write(BinaryObjectWriter writer)
    {
        writer.WriteArray(Field_4c);
    }
}
