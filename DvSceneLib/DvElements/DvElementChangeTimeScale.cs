using Amicitia.IO.Binary;
using DvSceneLib.Misc;

namespace DvSceneLib;

[DvNodeCategory("Element")]
[DvNodeDescription("Change Time Scale", "Speeds up the game time")]
public class DvElementChangeTimeScale : DvNodeElement
{
    public uint Field_00 = 0;
    public float TimeScale = 1;
    public int Field_08 = 0;
    public float Multiplier = 1;

    public DvElementChangeTimeScale() : base(DvElementID.ChangeTimeScale) { }
    public DvElementChangeTimeScale(BinaryObjectReader reader)
        => Read(reader);
    public void Read(BinaryObjectReader reader)
    {
        Field_00 = reader.Read<uint>();
        TimeScale = reader.Read<float>();
        Field_08 = reader.Read<int>();
        Multiplier = reader.Read<float>();
    }

    protected override void WriteElement(BinaryObjectWriter writer)
    {
        writer.Write(Field_00);
        writer.Write(TimeScale);
        writer.Write(Field_08);
        writer.Write(Multiplier);
    }
}
