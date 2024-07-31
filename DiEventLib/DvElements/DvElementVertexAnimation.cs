using Amicitia.IO.Binary;
using System.Text;

namespace DiEventLib;

public class DvElementVertexAnimation : DvNodeElement
{
    public uint Field_00 { get; set; } = 0;
    public string FileName { get; set; } = "";
    public uint Field_44 { get; set; } = 0;
    public float Field_48 { get; set; } = 0;
    public uint Field_4c { get; set; } = 0;
    public uint Field_50 { get; set; } = 0;
    public DvElementVertexAnimation() { }
    public DvElementVertexAnimation(BinaryObjectReader reader)
        => Read(reader);
    public void Read(BinaryObjectReader reader)
    {
        Field_00 = reader.Read<uint>();
        FileName = reader.ReadString(Encoding.Default, StringBinaryFormat.FixedLength, 16);
        Field_44 = reader.Read<uint>();
        Field_48 = reader.Read<float>();
        Field_4c = reader.Read<uint>();
        Field_50 = reader.Read<uint>();
    }

    public void Write(BinaryObjectWriter writer)
    {
        writer.Write(Field_00);
        writer.WriteString(Encoding.Default, StringBinaryFormat.FixedLength, FileName, 16);
        writer.Write(Field_44);
        writer.Write(Field_48);
        writer.Write(Field_4c);
        writer.Write(Field_50);
    }
}
