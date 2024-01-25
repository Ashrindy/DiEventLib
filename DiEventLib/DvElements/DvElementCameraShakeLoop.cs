using Amicitia.IO.Binary;
using System.Reflection.PortableExecutable;
using System.Text;

namespace DiEventLib;

public class DvElementCameraShakeLoop : DvNodeObject
{
    public uint Field_60 { get; set; }
    public uint Field_64 { get; set; }
    public float[] Field_68 { get; set; }
    public float[] CurveData { get; set; }

    public DvElementCameraShakeLoop() { }
    public DvElementCameraShakeLoop(BinaryObjectReader reader)
        => Read(reader);
    public override void Read(BinaryObjectReader reader)
    {
        Field_60 = reader.Read<uint>();
        Field_64 = reader.Read<uint>();
        Field_68 = reader.ReadArray<float>(6);
        CurveData = reader.ReadArray<float>(64);
    }

    public override void Write(BinaryObjectWriter writer)
    {
        writer.Write(Field_60);
        writer.Write(Field_64);
        writer.WriteArray(Field_68);
        writer.WriteArray(CurveData);
    }
}
