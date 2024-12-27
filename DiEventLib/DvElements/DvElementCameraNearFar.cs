using Amicitia.IO.Binary;
using DiEventLib.Misc;

namespace DiEventLib;

[DvNodeCategory("Element")]
[DvNodeDescription("Near Far Setting", "Modifies the NearZ and FarZ")]
public class DvElementCameraNearFar : DvNodeElement
{
    public uint Field_00 = 0;
    public float Near = 0;
    public float Far = 1000;
    public uint[] Field_10;
    public DvElementCameraNearFar() : base(DvElementID.CameraNearFar)
    {
        Field_10 = new uint[5];
        for (int i = 0; i < 8; i++)
        {
            Field_10[i] = 0;
        }
    }
    public DvElementCameraNearFar(BinaryObjectReader reader)
        => Read(reader);
    public void Read(BinaryObjectReader reader)
    {
        Field_00 = reader.Read<uint>();
        Near = reader.Read<float>();
        Far = reader.Read<float>();
        Field_10 = reader.ReadArray<uint>(5);
    }

    public void Write(BinaryObjectWriter writer)
    {
        writer.Write(Field_00);
        writer.Write(Near);
        writer.Write(Far);
        writer.WriteArray(Field_10);
    }
}
