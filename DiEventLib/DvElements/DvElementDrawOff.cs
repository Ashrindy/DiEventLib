using Amicitia.IO.Binary;
using DiEventLib.Misc;

namespace DiEventLib;

[DvNodeCategory("Element")]
[DvNodeDescription("Draw Off", "Turns off the drawing of a model")]
public class DvElementDrawOff : DvNodeElement
{
    public uint[] Field_00;

    public DvElementDrawOff() : base(DvElementID.DrawOff)
    { 
        Field_00 = new uint[4];
        for(int i = 0; i < 4; i++)
        {
            Field_00[i] = 0;
        }
    }
    public DvElementDrawOff(BinaryObjectReader reader)
        => Read(reader);
    public void Read(BinaryObjectReader reader)
    {
        Field_00 = reader.ReadArray<uint>(4);
    }

    public void Write(BinaryObjectWriter writer)
    {
        writer.WriteArray(Field_00);
    }
}
