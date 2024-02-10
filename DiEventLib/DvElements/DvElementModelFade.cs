using Amicitia.IO.Binary;

namespace DiEventLib;

public class DvElementModelFade : DvNodeObject
{
    public uint[] Field_00 { get; set; }
    public float[] CurveData { get; set; }
    public DvElementModelFade() { }
    public DvElementModelFade(BinaryObjectReader reader)
        => Read(reader);
    public override void Read(BinaryObjectReader reader)
    {
        Field_00 = reader.ReadArray<uint>(8);
        CurveData = reader.ReadArray<float>(128);
    }

    public override void Write(BinaryObjectWriter writer)
    {
        writer.WriteArray(Field_00);
        writer.WriteArray(CurveData);
    }
}
