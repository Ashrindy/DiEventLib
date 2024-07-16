using Amicitia.IO.Binary;
using System.Numerics;

namespace DiEventLib;

public class DvElementSun : DvNodeElement
{
    public uint Field_00 { get; set; } = 0;
    public Vector3 Rotation { get; set; } = new(0, 0, 0);
    public uint[] Field_01 { get; set; }
    public uint[] AnimData { get; set; }
    public DvElementSun() : base(DvElementID.Sun)
    {
        Field_01 = new uint[3];
        for (int i = 0; i < 3; i++)
        {
            Field_01[i] = 0;
        }
        AnimData = new uint[32];
        for (int i = 0; i < 32; i++)
        {
            AnimData[i] = 1;
        }
    }
    public DvElementSun(BinaryObjectReader reader)
        => Read(reader);
    public void Read(BinaryObjectReader reader)
    {
        Field_00 = reader.Read<uint>();
        Rotation = reader.Read<Vector3>();
        Field_01 = reader.ReadArray<uint>(3);
        AnimData = reader.ReadArray<uint>(32);
    }

    public void Write(BinaryObjectWriter writer)
    {
        writer.Write(Field_00);
        writer.Write(Rotation);
        writer.WriteArray(Field_01);
        writer.WriteArray(AnimData);
    }
}
