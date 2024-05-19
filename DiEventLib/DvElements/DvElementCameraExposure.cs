using Amicitia.IO.Binary;

namespace DiEventLib;

public class DvElementCameraExposure : DvNodeObject
{
    public int unk1 { get; set; } = 0;
    public float[] Field_48 { get; set; }
    public float[] Field_80 { get; set; }

    public DvElementCameraExposure() 
    {
        Field_48 = new float[7];
        for (int i = 0; i < 7; i++)
        {
            Field_48[i] = 0;
        }
        Field_80 = new float[32];
        for (int i = 0; i < 7; i++)
        {
            Field_80[i] = 0;
        }
    }
    public DvElementCameraExposure(BinaryObjectReader reader)
        => Read(reader);
    public override void Read(BinaryObjectReader reader)
    {
        unk1 = reader.Read<int>();
        Field_48 = reader.ReadArray<float>(7);
        Field_80 = reader.ReadArray<float>(32);
    }

    public override void Write(BinaryObjectWriter writer)
    {
        writer.Write(unk1);
        writer.WriteArray(Field_48);
        writer.WriteArray(Field_80);
    }
}
