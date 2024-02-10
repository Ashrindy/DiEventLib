using Amicitia.IO.Binary;

namespace DiEventLib;

public class DvElementShadowResolution : DvNodeObject
{
    public uint ShadowRes1 { get; set; }
    public uint ShadowRes2 { get; set; }

    public DvElementShadowResolution() { }
    public DvElementShadowResolution(BinaryObjectReader reader)
        => Read(reader);
    public override void Read(BinaryObjectReader reader)
    {
        ShadowRes1 = reader.Read<uint>();
        ShadowRes2 = reader.Read<uint>();
    }

    public override void Write(BinaryObjectWriter writer)
    {
        writer.Write(ShadowRes1);
        writer.Write(ShadowRes2);
    }
}
