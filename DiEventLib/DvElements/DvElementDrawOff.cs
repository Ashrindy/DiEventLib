using Amicitia.IO.Binary;

namespace DiEventLib;

public class DvElementDrawOff : DvNodeObject
{
    public uint[] Field_00 {  get; set; }

    public DvElementDrawOff() 
    { 
        Field_00 = new uint[4];
        for(int i = 0; i < 4; i++)
        {
            Field_00[i] = 0;
        }
    }
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
