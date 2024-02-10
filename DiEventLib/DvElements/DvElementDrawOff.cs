using Amicitia.IO.Binary;

namespace DiEventLib;

public class DvElementDrawOff : DvNodeObject
{
    public uint[] Field_00 {  get; set; }

    public DvElementDrawOff() { }
    public DvElementDrawOff(BinaryObjectReader reader)
        => Read(reader);
    public override void Read(BinaryObjectReader reader)
    {
        Field_00 = reader.ReadArray<uint>(4);
    }

    public override void Write(BinaryObjectWriter writer)
    {
        writer.WriteArray(Field_00);
    }
}
