using Amicitia.IO.Binary;

namespace DiEventLib;

public class DvElementCulling : DvNodeElement
{
    public DvElementCulling() { }
    public DvElementCulling(BinaryObjectReader reader)
        => Read(reader);
    public void Read(BinaryObjectReader reader)
    {
    }

    public void Write(BinaryObjectWriter writer)
    {
    }
}
