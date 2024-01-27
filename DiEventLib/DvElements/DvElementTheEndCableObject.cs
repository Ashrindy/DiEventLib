using Amicitia.IO.Binary;
using System.Text;

namespace DiEventLib;

public class DvElementTheEndCableObject : DvNodeObject
{
    public uint Flags { get; set; }
    public uint Field_04 { get; set; }
    public float[] AnimData { get; set; }
    public DvElementTheEndCableObject() { }
    public DvElementTheEndCableObject(BinaryObjectReader reader)
        => Read(reader);
    public override void Read(BinaryObjectReader reader)
    {
        Flags = reader.Read<uint>();
        Field_04 = reader.Read<uint>();
        AnimData = reader.ReadArray<float>(1024);
    }

    public override void Write(BinaryObjectWriter writer)
    {
        writer.Write(Flags);
        writer.Write(Field_04);
        writer.WriteArray(AnimData);
    }
}
