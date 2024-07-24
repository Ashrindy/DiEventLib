using Amicitia.IO.Binary;

namespace DiEventLib;

public class DvElementChangeTimeScale : DvNodeElement
{
    public uint Field_00 { get; set; } = 0;
    public float TimeScale { get; set; } = 1;
    public uint[] Field_08 { get; set; }
    public DvElementChangeTimeScale() 
    {
        Field_08 = new uint[2];
        for(int i = 0; i < 2; i++)
        {
            Field_08[i] = 0;
        }
    }
    public DvElementChangeTimeScale(BinaryObjectReader reader)
        => Read(reader);
    public void Read(BinaryObjectReader reader)
    {
        Field_00 = reader.Read<uint>();
        TimeScale = reader.Read<float>();
        Field_08 = reader.ReadArray<uint>(2);
    }

    public void Write(BinaryObjectWriter writer)
    {
        writer.Write(Field_00);
        writer.Write(TimeScale);
        writer.WriteArray(Field_08);
    }
}
