using Amicitia.IO.Binary;
using DvSceneLib.Misc;

namespace DvSceneLib;

[DvNodeCategory("Element")]
[DvNodeDescription("Boss Name", "Displays the bosses name")]
public class DvElementBossName : DvNodeElement
{
    public uint Field_00;
    public BossID BossName;

    public DvElementBossName() : base(DvElementID.BossName)
    {
        Field_00 = 0;
        BossName = BossID.Giganto;
    }

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
