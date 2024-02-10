using Amicitia.IO.Binary;

namespace DiEventLib;

public class DvElementBloom : DvNodeObject
{
    public uint Field_00 { get; set; }
    public float Field_04 { get; set; }
    public float Field_08 { get; set; }
    public uint Field_10 { get; set; }
    public float Field_14 { get; set; }
    public float[] Field_18 { get; set; }
    public float[] CurveData { get; set; }
    public DvElementBloom() { }
    public DvElementBloom(BinaryObjectReader reader)
        => Read(reader);
    public override void Read(BinaryObjectReader reader)
    {
        Field_00 = reader.Read<uint>();
        Field_04 = reader.Read<float>();
        Field_08 = reader.Read<float>();
        Field_10 = reader.Read<uint>();
        Field_14 = reader.Read<float>();
        Field_18 = reader.ReadArray<float>(6);
        CurveData = reader.ReadArray<float>(32);
    }

    public override void Write(BinaryObjectWriter writer)
    {
        writer.Write(Field_00);
        writer.Write(Field_04);
        writer.Write(Field_08);
        writer.Write(Field_10);
        writer.Write(Field_14);
        writer.WriteArray(Field_18);
        writer.WriteArray(CurveData);
    }
}
