using Amicitia.IO.Binary;
using DvSceneLib.Misc;

namespace DvSceneLib;

[DvNodeCategory("Element")]
[DvNodeDescription("Debug Motion", "")]
public class DvElementDebugMotion : DvElementMultipleAnim
{
    public DvElementDebugMotion()
    {
        Category = DvNodeCategory.Element;
        ElementID = DvElementID.DebugMotion;
        NodeName = DvElementID.DebugMotion.ToString();
        Priority = 0;
        Guid = Guid.NewGuid();
    }
    public DvElementDebugMotion(BinaryObjectReader reader)
        => Read(reader);
}


