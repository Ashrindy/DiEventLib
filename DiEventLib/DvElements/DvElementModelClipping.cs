using Amicitia.IO.Binary;

namespace DiEventLib;

public class DvElementModelClipping : DvNodeObject
{
    public int unk0 { get; set; } = 0;
    public int unk1 { get; set; } = 0;
    public int unk2 { get; set; } = 0;
    public float unk3 { get; set; } = 0;
    public int unk4 { get; set; } = 0;
    public DvElementModelClipping() 
    {
    }
    public DvElementModelClipping(BinaryObjectReader reader)
        => Read(reader);
    public override void Read(BinaryObjectReader reader)
    {
        unk0 = reader.Read<int>();
        unk1 = reader.Read<int>();
        unk2 = reader.Read<int>();
        unk3 = reader.Read<float>();
        unk4 = reader.Read<int>();
    }

    public override void Write(BinaryObjectWriter writer)
    {
        writer.Write(unk0);
        writer.Write(unk1);
        writer.Write(unk2);
        writer.Write(unk3);
        writer.Write(unk4);
    }
}
