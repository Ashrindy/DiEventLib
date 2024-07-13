using Amicitia.IO.Binary;

namespace DiEventLib;

public class DvElementAuraRoad : DvNodeElement
{
    public uint Field_00 { get; set; } = 0;
    public float[] AnimData { get; set; }

    public DvElementAuraRoad() 
    { 
        AnimData = new float[64];
        for(int i = 0; i < 64; i++)
        {
            AnimData[i] = 1;
        }
    }
    public DvElementAuraRoad(BinaryObjectReader reader)
        => Read(reader);
    public void Read(BinaryObjectReader reader)
    {
        Field_00 = reader.Read<uint>();
        AnimData = reader.ReadArray<float>(64);
    }

    public void Write(BinaryObjectWriter writer)
    {
        writer.Write(Field_00);
        writer.WriteArray(AnimData);
    }
}