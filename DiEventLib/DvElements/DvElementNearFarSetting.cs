using Amicitia.IO.Binary;

namespace DiEventLib;

public class DvElementNearFarSetting : DvNodeElement
{
    public uint Field_00 { get; set; } = 0;
    public float Near { get; set; } = 0;
    public float Far { get; set; } = 1000;
    public uint[] Field_10 { get; set; }
    public DvElementNearFarSetting() 
    {
        Field_10 = new uint[5];
        for (int i = 0; i < 8; i++)
        {
            Field_10[i] = 0;
        }
    }
    public DvElementNearFarSetting(BinaryObjectReader reader)
        => Read(reader);
    public void Read(BinaryObjectReader reader)
    {
        Field_00 = reader.Read<uint>();
        Near = reader.Read<float>();
        Far = reader.Read<float>();
        Field_10 = reader.ReadArray<uint>(5);
    }

    public void Write(BinaryObjectWriter writer)
    {
        writer.Write(Field_00);
        writer.Write(Near);
        writer.Write(Far);
        writer.WriteArray(Field_10);
    }
}
