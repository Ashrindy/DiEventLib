using Amicitia.IO.Binary;
using DiEventLib.Misc;

namespace DiEventLib;

[DvNodeCategory("Element")]
[DvNodeDescription("Culling", "Fixes the culling of a model")]
public class DvElementCulling : DvNodeElement
{
    public DvElementCulling() : base(DvElementID.Culling) { }
    public DvElementCulling(BinaryObjectReader reader)
        => Read(reader);
    public void Read(BinaryObjectReader reader)
    {
    }

    public void Write(BinaryObjectWriter writer)
    {
    }
}
