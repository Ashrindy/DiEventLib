using Amicitia.IO.Binary;
using System.Text;

namespace DiEventLib;

public class DvElementColorCorrection : DvNodeObject
{
    public uint Field_00 {  get; set; }
    public float Field_04 {  get; set; }
    public float Field_08 {  get; set; }
    public float Field_0c {  get; set; }
    public float Field_1c {  get; set; }
    public uint Field_01 {  get; set; }
    public float Field_2c {  get; set; }
    public uint Field_02 {  get; set; }
    public float[] CurveData {  get; set; }
    public DvElementColorCorrection() { }
    public DvElementColorCorrection(BinaryObjectReader reader)
        => Read(reader);
    public override void Read(BinaryObjectReader reader)
    {
        Field_00 = reader.Read<uint>();
        Field_04 = reader.Read<float>();
        Field_08 = reader.Read<float>();
        Field_0c = reader.Read<float>();
        Field_1c = reader.Read<float>();
        Field_01 = reader.Read<uint>();
        Field_2c = reader.Read<float>();
        Field_02 = reader.Read<uint>();
        CurveData = reader.ReadArray<float>(32);
    }

    public override void Write(BinaryObjectWriter writer)
    {
        writer.Write(Field_00);
        writer.Write(Field_04);
        writer.Write(Field_08);
        writer.Write(Field_0c);
        writer.Write(Field_1c);
        writer.Write(Field_01);
        writer.Write(Field_2c);
        writer.Write(Field_02);
        writer.WriteArray(CurveData);
    }
}
