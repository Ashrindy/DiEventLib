using Amicitia.IO.Binary;
using DiEventLib.Misc;

namespace DiEventLib;

[DvNodeCategory("Element")]
[DvNodeDescription("Camera Hedgehog", "")]
public class DvElementCameraHedgehog : DvNodeElement
{
    public uint Field_00 = 0;
    public uint[] Field_01;
    public float[] Field_4c;
    public byte[] Data;

    public DvElementCameraHedgehog() : base(DvElementID.CameraHedgehog)
    {
        Field_01 = new uint[3];
        for(int i = 0; i < 3; i++)
        {
            Field_01[i] = 0;
        }
        Field_4c = new float[44];
        for (int i = 0; i < 44; i++)
        {
            Field_4c[i] = 0;
        }
        Data = new byte[128];
        for (int i = 0; i < 128; i++)
        {
            Data[i] = 0;
        }
    }
    public DvElementCameraHedgehog(BinaryObjectReader reader)
        => Read(reader);
    public void Read(BinaryObjectReader reader)
    {
        Field_00 = reader.Read<uint>();
        Field_01 = reader.ReadArray<uint>(3);
        Field_4c = reader.ReadArray<float>(44);
        Data = reader.ReadArray<byte>(128);
    }

    public void Write(BinaryObjectWriter writer)
    {
        writer.Write(Field_00);
        writer.WriteArray(Field_01);
        writer.WriteArray(Field_4c);
        writer.WriteArray(Data);
    }
}
