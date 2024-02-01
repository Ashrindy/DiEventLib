using Amicitia.IO.Binary;
using System.Numerics;

namespace DiEventLib;

public class DvElementSun : DvNodeObject
{
    public uint Field_00 {  get; set; }
    public Vector3 Rotation { get; set; }
    public uint[] Field_01 { get; set; }
    public uint[] AnimData { get; set; }
    public DvElementSun() { }
    public DvElementSun(BinaryObjectReader reader)
        => Read(reader);
    public override void Read(BinaryObjectReader reader)
    {
        Field_00 = reader.Read<uint>();
        Rotation = reader.Read<Vector3>();
        Field_01 = reader.ReadArray<uint>(3);
        AnimData = reader.ReadArray<uint>(32);
    }

    public override void Write(BinaryObjectWriter writer)
    {
        writer.Write(Field_00);
        writer.Write(Rotation);
        writer.WriteArray(Field_01);
        writer.WriteArray(AnimData);
    }
}
