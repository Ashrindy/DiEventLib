using Amicitia.IO.Binary;
using DvSceneLib.Misc;

namespace DvSceneLib;

[DvNodeCategory("Element")]
[DvNodeDescription("FxCol Update", "Updates the fxcol")]
public class DvElementFxColUpdate : DvNodeElement
{
    public DvElementFxColUpdate() : base(DvElementID.FxColUpdate) { }
    public DvElementFxColUpdate(BinaryObjectReader reader) 
        => Read(reader);
    public void Read(BinaryObjectReader reader)
    {
    }

    protected override void WriteElement(BinaryObjectWriter writer)
    {
    }
}
