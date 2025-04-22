using Amicitia.IO.Binary;
using DvSceneLib.Misc;

namespace DvSceneLib;

[DvNodeCategory("Element")]
[DvNodeDescription("PBA Reset", "Resets the simulation of a PBA")]
public class DvElementPbaReset : DvNodeElement
{
    public DvElementPbaReset() : base(DvElementID.PbaReset) { }
    public DvElementPbaReset(BinaryObjectReader reader) 
        => Read(reader);
    public void Read(BinaryObjectReader reader)
    {
    }

    protected override void WriteElement(BinaryObjectWriter writer)
    {
    }
}
