using Amicitia.IO.Binary;
using DvSceneLib.Misc;

namespace DvSceneLib;

[DvNodeCategory("Element")]
[DvNodeDescription("Letter Box", "Makes the cutscene use a fake 21:9 aspect ratio")]
public class DvElementLetterBox : DvNodeElement
{
    public float[] CurveData = new float[32];

    public DvElementLetterBox() : base(DvElementID.LetterBox) { }
    public DvElementLetterBox(BinaryObjectReader reader)
        => Read(reader);
    public void Read(BinaryObjectReader reader)
    {
        CurveData = reader.ReadArray<float>(32);
    }

    protected override void WriteElement(BinaryObjectWriter writer)
    {
        writer.WriteArray(CurveData);
    }
}
