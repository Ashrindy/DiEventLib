using Amicitia.IO.Binary;

namespace DiEventLib;

public class DvElementAdditionRange : DvNodeObject
{
    public uint Field_00 { get; set; } = 0;
    public float[] Unk01 { get; set; }
    public DvElementAdditionRange() 
    { 
        Unk01 = new float[7];
        for (int i = 0; i < 7; i++)
        {
            Unk01[i] = 0;
        }
    }
    public DvElementAdditionRange(BinaryObjectReader reader)
        => Read(reader);
    public override void Read(BinaryObjectReader reader)
    {
        Field_00 = reader.Read<uint>();
        Unk01 = reader.ReadArray<float>(7);
    }

    public override void Write(BinaryObjectWriter writer)
    {
        writer.Write(Field_00);
        writer.WriteArray(Unk01);
    }
}
