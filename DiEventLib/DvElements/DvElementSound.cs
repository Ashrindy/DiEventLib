using Amicitia.IO.Binary;
using System.Text;

namespace DiEventLib;

public class DvElementSound : DvNodeObject
{
    public string CueName { get; set; }
    public uint Field_a0 { get; set; }
    public uint Field_a4 { get; set; }
    public DvElementSound() { }
    public DvElementSound(BinaryObjectReader reader)
        => Read(reader);
    public override void Read(BinaryObjectReader reader)
    {
        CueName = reader.ReadString(Encoding.Default, StringBinaryFormat.FixedLength, 64);
        Field_a0 = reader.Read<uint>();
        Field_a4 = reader.Read<uint>();
    }

    public override void Write(BinaryObjectWriter writer)
    {
        writer.WriteString(Encoding.Default, StringBinaryFormat.FixedLength, CueName, 64);
        writer.Write(Field_a0);
        writer.Write(Field_a4);
    }
}
