using Amicitia.IO.Binary;
using System.Text;

namespace DiEventLib;

public class DvElementSun : DvNodeObject
{
    public uint Field_00 {  get; set; }
    public float UnkFloat { get; set; }
    public uint[] Field_01 { get; set; }
    public uint[] AnimData { get; set; }
    public DvElementSun() { }
    public DvElementSun(BinaryObjectReader reader)
        => Read(reader);
    public override void Read(BinaryObjectReader reader)
    {
        Field_00 = reader.Read<uint>();
        UnkFloat = reader.Read<float>();
        Field_01 = reader.ReadArray<uint>(5);
        AnimData = reader.ReadArray<uint>(32);
    }

    public override void Write(BinaryObjectWriter writer)
    {
        writer.Write(Field_00);
        writer.Write(UnkFloat);
        writer.WriteArray(Field_01);
        writer.WriteArray(AnimData);
    }
}
