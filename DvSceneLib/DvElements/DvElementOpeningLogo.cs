using Amicitia.IO.Binary;
using DvSceneLib.Misc;

namespace DvSceneLib;

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

    protected override void WriteElement(BinaryObjectWriter writer)
    {
    }
}
