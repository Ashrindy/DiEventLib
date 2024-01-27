using Amicitia.IO.Binary;
using System.Text;

namespace DiEventLib;

public class DvElementTheEndCableObject : DvNodeObject
{
    public uint Flags { get; set; }
    public float[] AnimData { get; set; }
    public DvElementTheEndCableObject() { }
    public DvElementTheEndCableObject(BinaryObjectReader reader)
        => Read(reader);
    public override void Read(BinaryObjectReader reader)
    {
        Flags = reader.Read<uint>();
        reader.Skip(4);
        AnimData = reader.ReadArray<float>(1024);
    }

    public override void Write(BinaryObjectWriter writer)
    {
        writer.Write(Flags);
        writer.Skip(4);
        writer.WriteArray(AnimData);
    }
}
