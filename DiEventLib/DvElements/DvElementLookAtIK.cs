using Amicitia.IO.Binary;
using System.Text;

namespace DiEventLib;

public class DvElementLookAtIK : DvNodeObject
{
    public uint Field_60 { get; set; }
    public uint Field_64 { get; set; }
    public Guid GUID { get; set; }
    public uint[] Field_78 { get; set; }
    public float[] Field_80 { get; set; }

    public DvElementLookAtIK() { }
    public DvElementLookAtIK(BinaryObjectReader reader)
        => Read(reader);
    public override void Read(BinaryObjectReader reader)
    {
        Field_60 = reader.Read<uint>();
        Field_64 = reader.Read<uint>();
        GUID = reader.Read<Guid>();
        Field_78 = reader.ReadArray<uint>(11);
        Field_80 = reader.ReadArray<float>(64);
    }

    public override void Write(BinaryObjectWriter writer)
    {
        writer.Write(Field_60);
        writer.Write(Field_64);
        writer.Write(GUID);
        writer.WriteArray(Field_78);
        writer.WriteArray(Field_80);
    }
}
