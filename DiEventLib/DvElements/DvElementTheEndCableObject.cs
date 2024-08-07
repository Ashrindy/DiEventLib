using Amicitia.IO.Binary;
using DiEventLib.Misc;

namespace DiEventLib;

[DvNodeCategory("Element")]
[DvNodeDescription("The End Cable Object", "Adds the Supreme/The End cable")]
public class DvElementTheEndCableObject : DvNodeElement
{
    public uint Flags = 0;
    public uint Field_04 = 0;
    public float[] AnimData;
    public DvElementTheEndCableObject() : base(DvElementID.TheEndCableObject)
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
