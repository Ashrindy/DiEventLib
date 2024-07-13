using Amicitia.IO.Binary;

namespace DiEventLib;

public class DvElementShadowResolution : DvNodeElement
{
    public uint ShadowRes1 { get; set; } = 0;
    public uint ShadowRes2 { get; set; } = 0;    

    public DvElementShadowResolution() { }
    public DvElementShadowResolution(BinaryObjectReader reader)
        => Read(reader);
    public void Read(BinaryObjectReader reader)
    {
        ShadowRes1 = reader.Read<uint>();
        ShadowRes2 = reader.Read<uint>();
    }

    public void Write(BinaryObjectWriter writer)
    {
        writer.Write(ShadowRes1);
        writer.Write(ShadowRes2);
    }
}
