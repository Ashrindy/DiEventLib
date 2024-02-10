using Amicitia.IO.Binary;

namespace DiEventLib;

public class DvElementCulling : DvNodeObject
{
    public DvElementCulling() { }
    public DvElementCulling(BinaryObjectReader reader)
        => Read(reader);
    public override void Read(BinaryObjectReader reader)
    {
    }

    public override void Write(BinaryObjectWriter writer)
    {
    }
}
