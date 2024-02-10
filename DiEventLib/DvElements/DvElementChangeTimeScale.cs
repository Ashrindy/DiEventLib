using Amicitia.IO.Binary;

namespace DiEventLib;

public class DvElementChangeTimeScale : DvNodeObject
{
    public uint Field_00 { get; set; }
    public float TimeScale { get; set; }
    public uint[] Field_08 { get; set; }
    public DvElementChangeTimeScale() { }
    public DvElementChangeTimeScale(BinaryObjectReader reader)
        => Read(reader);
    public override void Read(BinaryObjectReader reader)
    {
        Field_00 = reader.Read<uint>();
        TimeScale = reader.Read<float>();
        Field_08 = reader.ReadArray<uint>(2);
    }

    public override void Write(BinaryObjectWriter writer)
    {
        writer.Write(Field_00);
        writer.Write(TimeScale);
        writer.WriteArray(Field_08);
    }
}
