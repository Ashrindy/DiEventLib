using Amicitia.IO.Binary;
using System.Text;

namespace DiEventLib;

public class DvElementBossName : DvNodeObject
{
    public uint Field_00 { get; set; }
    public BossID BossName { get; set; }

    public DvElementBossName() { }
    public DvElementBossName(BinaryObjectReader reader)
        => Read(reader);
    public override void Read(BinaryObjectReader reader)
    {
        Field_00 = reader.Read<uint>();
        BossName = reader.Read<uint>();
    }

    public override void Write(BinaryObjectWriter writer)
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
