using Amicitia.IO.Binary;
using DiEventLib.Misc;
using System.Text;

namespace DiEventLib;

[DvNodeCategory("Element")]
[DvNodeDescription("Sound", "Plays a sound")]
public class DvElementSound : DvNodeElement
{
    public string CueName = "";
    public uint Field_a0 = 0;
    public uint Field_a4 = 0;
    public DvElementSound() : base(DvElementID.Sound) { }
    public DvElementSound(BinaryObjectReader reader)
        => Read(reader);
    public void Read(BinaryObjectReader reader)
    {
        CueName = reader.ReadString(Encoding.Default, StringBinaryFormat.FixedLength, 64);
        Field_a0 = reader.Read<uint>();
        Field_a4 = reader.Read<uint>();
    }

    public void Write(BinaryObjectWriter writer)
    {
        writer.WriteString(Encoding.Default, StringBinaryFormat.FixedLength, CueName, 64);
        writer.Write(Field_a0);
        writer.Write(Field_a4);
    }
}
