using Amicitia.IO.Binary;
using DvSceneLib.Misc;

namespace DvSceneLib;

[DvNodeCategory("Element")]
[DvNodeDescription("Boss Name", "Displays the bosses name")]
public class DvElementBossName : DvNodeElement
{
    public uint Field_00 = 0;
    public BossID BossName = BossID.Giganto;

    public DvElementBossName() : base(DvElementID.BossName) { }
    public DvElementBossName(BinaryObjectReader reader)
        => Read(reader);
    public void Read(BinaryObjectReader reader)
    {
        Field_00 = reader.Read<uint>();
        BossName = reader.Read<BossID>();
    }

    protected override void WriteElement(BinaryObjectWriter writer)
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
