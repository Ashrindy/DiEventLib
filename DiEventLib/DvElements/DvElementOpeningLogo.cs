using Amicitia.IO.Binary;

namespace DiEventLib;

public class DvElementOpeningLogo : DvNodeObject
{
    public DvElementOpeningLogo() { }
    public DvElementOpeningLogo(BinaryObjectReader reader)
        => Read(reader);
    public override void Read(BinaryObjectReader reader)
    {
    }

    public override void Write(BinaryObjectWriter writer)
    {
    }
}
