using Amicitia.IO.Binary;
using DvSceneLib.Misc;

namespace DvSceneLib;

[DvNodeCategory("Element")]
[DvNodeDescription("Dither", "Changes the range of the dither on foliage")]
public class DvElementDitherParam : DvNodeElement
{
    public float GrassDitherStart = 0;
    public float GrassDitherEnd = 0;

    public DvElementDitherParam() : base(DvElementID.DitherParam) { }
    public DvElementDitherParam(BinaryObjectReader reader)
        => Read(reader);
    public void Read(BinaryObjectReader reader)
    {
        GrassDitherStart = reader.Read<float>();
        GrassDitherEnd = reader.Read<float>();
    }

    protected override void WriteElement(BinaryObjectWriter writer)
    {
        writer.Write(GrassDitherStart);
        writer.Write(GrassDitherEnd);
    }
}
