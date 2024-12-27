using Amicitia.IO.Binary;
using DiEventLib.Misc;

namespace DiEventLib;

[DvNodeCategory("Element")]
[DvNodeDescription("Culling Disable", "Disables the culling of a model")]
public class DvElementCullingDisable : DvNodeElement
{
    public DvElementCullingDisable() : base(DvElementID.CullingDisable) { }
    public DvElementCullingDisable(BinaryObjectReader reader)
        => Read(reader);
    public void Read(BinaryObjectReader reader)
    {
    }

    public void Write(BinaryObjectWriter writer)
    {
    }
}
