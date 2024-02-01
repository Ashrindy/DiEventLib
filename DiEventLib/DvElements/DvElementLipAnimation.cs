using Amicitia.IO.Binary;
using System.Text;

namespace DiEventLib;

public class DvElementLipAnimation : DvNodeObject
{
    public uint Field_00 { get; set; }
    public string FileName { get; set; }
    public uint[] Field_04 { get; set; }
    public float[] CurveData { get; set; }
    public DvElementLipAnimation() { }
    public DvElementLipAnimation(BinaryObjectReader reader)
        => Read(reader);
    public override void Read(BinaryObjectReader reader)
    {
        Field_00 = reader.Read<uint>();
        FileName = reader.ReadString(Encoding.Default, StringBinaryFormat.FixedLength, 48);
        Field_04 = reader.ReadArray<uint>(3);
        CurveData = reader.ReadArray<float>(32);
    }

    public override void Write(BinaryObjectWriter writer)
    {
        writer.Write(Field_00);
        writer.WriteString(Encoding.Default, StringBinaryFormat.FixedLength, FileName, 48);
        writer.WriteArray(Field_04);
        writer.WriteArray(CurveData);
    }
}
