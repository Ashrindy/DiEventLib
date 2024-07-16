using Amicitia.IO.Binary;

namespace DiEventLib;

public class DvElementCameraShakeLoop : DvNodeElement
{
    public uint Field_60 { get; set; } = 0;
    public uint Field_64 { get; set; } = 0;
    public float[] Field_68 { get; set; }
    public float[] CurveData { get; set; }

    public DvElementCameraShakeLoop() : base(DvElementID.CameraShakeLoop)
    {
        Field_68 = new float[6];
        for(int i = 0; i < 6; i++)
        {
            Field_68[i] = 0;
        }
        CurveData = new float[64];
        for(int i = 0; i < 64; i++)
        {
            CurveData[i] = 1;
        }
    }
    public DvElementCameraShakeLoop(BinaryObjectReader reader)
        => Read(reader);
    public void Read(BinaryObjectReader reader)
    {
        Field_60 = reader.Read<uint>();
        Field_64 = reader.Read<uint>();
        Field_68 = reader.ReadArray<float>(6);
        CurveData = reader.ReadArray<float>(64);
    }

    public void Write(BinaryObjectWriter writer)
    {
        writer.Write(Field_60);
        writer.Write(Field_64);
        writer.WriteArray(Field_68);
        writer.WriteArray(CurveData);
    }
}
