using Amicitia.IO.Binary;
using DiEventLib.Misc;

namespace DiEventLib;

[DvNodeCategory("Element")]
[DvNodeDescription("Opening Logo", "Shows the Frontiers Logo")]
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
