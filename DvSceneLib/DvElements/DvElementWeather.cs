using Amicitia.IO.Binary;
using DvSceneLib.Misc;

namespace DvSceneLib;

[DvNodeCategory("Element")]
[DvNodeDescription("Weather", "Changes the weather")]
public class DvElementWeather : DvNodeElement
{
    public uint Field_00 = 0; // could be an enum of some sort, like "sunny" "cloudy" etc.
    public float[] CurveData;

    public DvElementWeather() : base(DvElementID.Weather)
    {
        CurveData = new float[32];
        for(int i = 0; i < 32; i++)
        {
            CurveData[i] = 1;
        }
    }
    public DvElementWeather(BinaryObjectReader reader)
        => Read(reader);
    public void Read(BinaryObjectReader reader)
    {
        Field_00 = reader.Read<uint>();
        CurveData = reader.ReadArray<float>(32);
    }

    public void Write(BinaryObjectWriter writer)
    {
        writer.Write(Field_00);
        writer.WriteArray(CurveData);
    }
}
