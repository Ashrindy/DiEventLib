using Amicitia.IO.Binary;
using DvSceneLib.Misc;

namespace DvSceneLib;

[DvNodeCategory("Element")]
[DvNodeDescription("Draw Off", "Turns off the drawing of a model")]
public class DvElementDrawOff : DvNodeElement
{
    public bool Visible = false;
    public bool IgnoreEnd = false;
    public int[] Field_00 = new int[3];

    public DvElementDrawOff() : base(DvElementID.DrawOff) { }
    public DvElementDrawOff(BinaryObjectReader reader)
        => Read(reader);
    public void Read(BinaryObjectReader reader)
    {
        var Flags = reader.Read<int>();
        Visible = (Flags & 1) != 0;
        IgnoreEnd = (Flags & 2) != 0;
        Field_00 = reader.ReadArray<int>(3);
    }

    protected override void WriteElement(BinaryObjectWriter writer)
    {
        var Flags = 0;
        if (Visible) Flags |= 1;
        if (IgnoreEnd) Flags |= 2;
        writer.Write(Flags);
        writer.WriteArray(Field_00);
    }
}
