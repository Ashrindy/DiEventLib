using Amicitia.IO.Binary;
using System.Reflection.PortableExecutable;
using System.Text;

namespace DiEventLib;

public class DvElementVisibilityAnimation : DvNodeObject
{
    public uint Field_00 {  get; set; }
    public string FileName { get; set; }
    public uint Field_44 { get; set; }
    public float Field_48 { get; set; }
    public uint Field_4c { get; set; }
    public uint Field_50 { get; set; }
    public DvElementVisibilityAnimation() { }
    public DvElementVisibilityAnimation(BinaryObjectReader reader)
        => Read(reader);
    public override void Read(BinaryObjectReader reader)
    {
        Field_00 = reader.Read<uint>();
        FileName = reader.ReadString(Encoding.Default, StringBinaryFormat.FixedLength, 64);
        Field_44 = reader.Read<uint>();
        Field_48 = reader.Read<float>();
        Field_4c = reader.Read<uint>();
        Field_50 = reader.Read<uint>();
    }

    public override void Write(BinaryObjectWriter writer)
    {
        writer.Write(Field_00);
        writer.WriteString(Encoding.Default, StringBinaryFormat.FixedLength, FileName, 64);
        writer.Write(Field_44);
        writer.Write(Field_48);
        writer.Write(Field_4c);
        writer.Write(Field_50);
    }
}
