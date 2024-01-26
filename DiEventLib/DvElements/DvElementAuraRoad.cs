using Amicitia.IO.Binary;
using System.Text;

namespace DiEventLib;

public class DvElementAuraRoad : DvNodeObject
{
    public uint Field_00 { get; set; }
    public float[] AnimData { get; set; }

    public DvElementAuraRoad() { }
    public DvElementAuraRoad(BinaryObjectReader reader)
        => Read(reader);
    public override void Read(BinaryObjectReader reader)
    {
        Field_00 = reader.Read<uint>();
        AnimData = reader.ReadArray<float>(64);
    }

    public override void Write(BinaryObjectWriter writer)
    {
        writer.Write(Field_00);
        writer.WriteArray(AnimData);
    }
}