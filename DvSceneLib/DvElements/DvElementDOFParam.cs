using Amicitia.IO.Binary;
using DvSceneLib.Misc;

namespace DvSceneLib;

[DvNodeCategory("Element")]
[DvNodeDescription("Depth of Field", "Adds depth of field")]
public class DvElementDOFParam : DvNodeElement
{
    public uint Field_60 = 0;
    public DOFParam[] DOFParams;
    public float Field_84 = 0;
    public float Field_88 = 0;
    public uint Field_8c = 0;
    public uint Field_90 = 0;
    public float Field_94 = 0;
    public float Field_98 = 0;
    public float Field_9c = 0;
    public float Field_a0 = 0;
    public float Field_a4 = 0;
    public float Field_a8 = 0;
    public float Field_ac = 0;
    public float[] AnimData;

    public DvElementDOFParam() : base(DvElementID.DOFParam)
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
    public DvElementDOFParam(BinaryObjectReader reader)
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