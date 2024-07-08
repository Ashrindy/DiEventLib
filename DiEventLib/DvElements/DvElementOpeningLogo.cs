using Amicitia.IO.Binary;

namespace DiEventLib;

public class DvElementOpeningLogo : DvNodeElement
{
    public DvElementOpeningLogo() : base(DvElementID.OpeningLogo) { }
    public DvElementOpeningLogo(BinaryObjectReader reader)
        => Read(reader);
    public void Read(BinaryObjectReader reader)
    {
    }

    public void Write(BinaryObjectWriter writer)
    {
    }
}
