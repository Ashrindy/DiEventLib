using Amicitia.IO.Binary;

namespace DiEventLib;

public class DvElementBossName : DvNodeElement
{
    public uint Field_00 { get; set; } = 0;
    public BossID BossName { get; set; } = BossID.Giganto;

    public DvElementBossName() { }
    public DvElementBossName(BinaryObjectReader reader)
        => Read(reader);
    public void Read(BinaryObjectReader reader)
    {
        Field_00 = reader.Read<uint>();
        BossName = reader.Read<BossID>();
    }

    public void Write(BinaryObjectWriter writer)
    {
        writer.Write(Field_00);
        writer.Write(BossName);
    }
}

public enum BossID : uint
{
    Giganto = 0,
    Wyvern,
    Knight,
    Supreme,
    TheEnd,
    SupremeTheEnd
}
