using Amicitia.IO.Binary;
using System.Numerics;
using System.Text;

namespace DiEventLib;

public class DvElementWeather : DvNodeObject
{
    public uint Field_00 { get; set; }
    public float[] CurveData { get; set; }

    public DvElementWeather() { }
    public DvElementWeather(BinaryObjectReader reader)
        => Read(reader);
    public override void Read(BinaryObjectReader reader)
    {
        Field_00 = reader.Read<uint>();
        CurveData = reader.ReadArray<float>(32);
    }

    public override void Write(BinaryObjectWriter writer)
    {
        writer.Write(Field_00);
        writer.WriteArray(CurveData);
    }
}
