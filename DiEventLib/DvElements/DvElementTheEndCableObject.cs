using Amicitia.IO.Binary;

namespace DiEventLib;

public class DvElementTheEndCableObject : DvNodeElement
{
    public uint Flags { get; set; } = 0;
    public uint Field_04 { get; set; } = 0;
    public float[] AnimData { get; set; }
    public DvElementTheEndCableObject() 
    {
        AnimData = new float[1024];
        for (int i = 0; i < 1024; i++)
        {
            AnimData[i] = 1;
        }
    }
    public DvElementTheEndCableObject(BinaryObjectReader reader)
        => Read(reader);
    public void Read(BinaryObjectReader reader)
    {
        Flags = reader.Read<uint>();
        Field_04 = reader.Read<uint>();
        AnimData = reader.ReadArray<float>(1024);
    }

    public void Write(BinaryObjectWriter writer)
    {
        writer.Write(Flags);
        writer.Write(Field_04);
        writer.WriteArray(AnimData);
    }
}
