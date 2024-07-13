using Amicitia.IO.Binary;

namespace DiEventLib;

public class DvElementLookAtIK : DvNodeElement
{
    public uint Field_60 { get; set; } = 0;
    public uint Field_64 { get; set; } = 0;
    public Guid GUID { get; set; } = Guid.NewGuid();
    public uint[] Field_78 { get; set; }
    public float[] Field_80 { get; set; }

    public DvElementLookAtIK() 
    {
        Field_78 = new uint[11];
        for(int i = 0; i < 11; i++)
        {
            Field_78[i] = 0;
        }
        Field_80 = new float[64];
        for (int i = 0; i < 64; i++)
        {
            Field_80[i] = 1;
        }
    }
    public DvElementLookAtIK(BinaryObjectReader reader)
        => Read(reader);
    public void Read(BinaryObjectReader reader)
    {
        Field_60 = reader.Read<uint>();
        Field_64 = reader.Read<uint>();
        GUID = reader.Read<Guid>();
        Field_78 = reader.ReadArray<uint>(11);
        Field_80 = reader.ReadArray<float>(64);
    }

    public void Write(BinaryObjectWriter writer)
    {
        writer.Write(Field_60);
        writer.Write(Field_64);
        writer.Write(GUID);
        writer.WriteArray(Field_78);
        writer.WriteArray(Field_80);
    }
}
