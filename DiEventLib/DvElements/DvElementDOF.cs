using Amicitia.IO.Binary;

namespace DiEventLib;

public class DvElementDOF : DvNodeElement
{
    public uint Field_60 { get; set; } = 0;
    public DOFParam[] DOFParams { get; set; }
    public float Field_84 { get; set; } = 0;
    public float Field_88 { get; set; } = 0;
    public uint Field_8c { get; set; } = 0;
    public uint Field_90 { get; set; } = 0;
    public float Field_94 { get; set; } = 0;
    public float Field_98 { get; set; } = 0;
    public float Field_9c { get; set; } = 0;
    public float Field_a0 { get; set; } = 0;
    public float Field_a4 { get; set; } = 0;
    public float Field_a8 { get; set; } = 0;
    public float Field_ac { get; set; } = 0;
    public float[] AnimData { get; set; }

    public DvElementDOF()
    {
        DOFParams = new DOFParam[2];
        for (int i = 0; i < 2; i++)
        {
            DOFParams[i] = new DOFParam { Focus = 0, FocusRange = 0, Near = 0, Far = 0 };
        }
        AnimData = new float[32];
        for (int i = 0; i < 32; i++)
        {
            AnimData[i] = 1;
        }
    }
    public DvElementDOF(BinaryObjectReader reader)
        => Read(reader);
    public void Read(BinaryObjectReader reader)
    {
        Field_60 = reader.Read<uint>();
        DOFParams = reader.ReadArray<DOFParam>(2);
        Field_84 = reader.Read<float>();
        Field_88 = reader.Read<float>();
        Field_8c = reader.Read<uint>();
        Field_90 = reader.Read<uint>();
        Field_94 = reader.Read<float>();
        Field_98 = reader.Read<float>();
        Field_9c = reader.Read<float>();
        Field_a0 = reader.Read<float>();
        Field_a4 = reader.Read<float>();
        Field_a8 = reader.Read<float>();
        Field_ac = reader.Read<float>();
        AnimData = reader.ReadArray<float>(32);
    }

    public void Write(BinaryObjectWriter writer)
    {
        writer.Write(Field_60);
        writer.WriteArray(DOFParams);
        writer.Write(Field_84);
        writer.Write(Field_88);
        writer.Write(Field_8c);
        writer.Write(Field_90);
        writer.Write(Field_94);
        writer.Write(Field_98);
        writer.Write(Field_9c);
        writer.Write(Field_a0);
        writer.Write(Field_a4);
        writer.Write(Field_a8);
        writer.Write(Field_ac);
        writer.WriteArray(AnimData);
    }
}

public struct DOFParam
{
    public float Focus { get; set; }
    public float FocusRange { get; set; }
    public float Near { get; set; }
    public float Far { get; set; }
}