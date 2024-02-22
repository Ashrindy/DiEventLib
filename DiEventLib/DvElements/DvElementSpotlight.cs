using Amicitia.IO.Binary;

namespace DiEventLib;

public class DvElementSpotlight : DvNodeObject
{
    public uint Flags { get; set; }
    public float[] Unk01 { get; set; }
    public RGB32 Color { get; set; }
    public float[] Unk02 { get; set; }
    public float[] AnimData { get; set; } // ev0010_mov contains a string in there? it looks like either the one i found was a newer version, or the compiler the messed the or the i'm just missing something.
    public DvElementSpotlight() { }
    public DvElementSpotlight(BinaryObjectReader reader)
        => Read(reader);
    public override void Read(BinaryObjectReader reader)
    {
        Flags = reader.Read<uint>();
        Unk01 = reader.ReadArray<float>(9);
        Color = reader.Read<RGB32>();
        Unk02 = reader.ReadArray<float>(10);
        AnimData = reader.ReadArray<float>(64);
    }

    public override void Write(BinaryObjectWriter writer)
    {
        writer.Write(Flags);
        writer.WriteArray(Unk01);
        writer.Write(Color);
        writer.WriteArray(Unk02);
        writer.WriteArray(AnimData);
    }
}
