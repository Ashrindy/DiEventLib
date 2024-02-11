using Amicitia.IO.Binary;

namespace DiEventLib;

public class DvElementSonicCamera : DvNodeObject
{
    public uint Field_00 { get; set; }
    public uint[] Field_01 { get; set; }
    public float[] Field_4c { get; set; }
    public byte[] Data { get; set; }

    public DvElementSonicCamera() { }
    public DvElementSonicCamera(BinaryObjectReader reader)
        => Read(reader);
    public override void Read(BinaryObjectReader reader)
    {
        Field_00 = reader.Read<uint>();
        Field_01 = reader.ReadArray<uint>(3);
        Field_4c = reader.ReadArray<float>(44);
        Data = reader.ReadArray<byte>(128);
    }

    public override void Write(BinaryObjectWriter writer)
    {
        writer.Write(Field_00);
        writer.WriteArray(Field_01);
        writer.WriteArray(Field_4c);
        writer.WriteArray(Data);
    }
}
