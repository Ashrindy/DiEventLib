using Amicitia.IO.Binary;
using DvSceneLib.Misc;

namespace DvSceneLib;

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
