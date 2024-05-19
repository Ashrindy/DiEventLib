using Amicitia.IO.Binary;

namespace DiEventLib;

public class DvElementWeather : DvNodeObject
{
    public uint Field_00 { get; set; } = 0; // could be an enum of some sort, like "sunny" "cloudy" etc.
    public float[] CurveData { get; set; }

    public DvElementWeather() 
    {
        CurveData = new float[32];
        for(int i = 0; i < 32; i++)
        {
            CurveData[i] = 1;
        }
    }
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
