using Amicitia.IO.Binary;

namespace DiEventLib;

public class DvElementLetterBox : DvNodeObject
{
    public float[] CurveData { get; set; }

    public DvElementLetterBox() { }
    public DvElementLetterBox(BinaryObjectReader reader)
        => Read(reader);
    public override void Read(BinaryObjectReader reader)
    {
        CurveData = reader.ReadArray<float>(32);
    }

    public override void Write(BinaryObjectWriter writer)
    {
        writer.WriteArray(CurveData);
    }
}
